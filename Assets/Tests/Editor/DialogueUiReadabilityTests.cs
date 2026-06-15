#if UNITY_EDITOR
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace DormGame.Tests
{
    public class DialogueUiReadabilityTests
    {
        private const string TestScenePath = "Assets/_Project/Scenes/TestMovement.unity";
        private const string ChoiceButtonPrefabPath = "Assets/_Project/Prefabs/UI/ChoiceButton.prefab";
        private const string DialogueFontPath = "Assets/_Project/Fonts/simhei SDF HQ.asset";

        [Test]
        public void DialogueCanvas_UsesReadablePreviewScale()
        {
            EditorSceneManager.OpenScene(TestScenePath);

            var scaler = GameObject.Find("DialogueCanvas").GetComponent<CanvasScaler>();
            var canvas = GameObject.Find("DialogueCanvas").GetComponent<Canvas>();
            var dialogueText = FindDialogueText();
            var nextButtonText = GameObject.Find("NextButton").GetComponentInChildren<TextMeshProUGUI>(true);

            Assert.That(scaler.uiScaleMode, Is.EqualTo(CanvasScaler.ScaleMode.ConstantPixelSize));
            Assert.That(scaler.scaleFactor, Is.EqualTo(1f));
            Assert.That(canvas.pixelPerfect, Is.True);
            Assert.That(dialogueText.fontSize, Is.GreaterThanOrEqualTo(40f));
            Assert.That(nextButtonText.fontSize, Is.GreaterThanOrEqualTo(32f));
        }

        [Test]
        public void ChoiceButtonPrefab_UsesReadableFontSize()
        {
            var choiceButton = AssetDatabase.LoadAssetAtPath<GameObject>(ChoiceButtonPrefabPath);
            var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>(true);

            Assert.That(choiceText.fontSize, Is.GreaterThanOrEqualTo(32f));
        }

        [Test]
        public void DialogueFonts_UseHighResolutionAtlasesAndCrispSdfMaterials()
        {
            AssertCrispDialogueFont("Assets/_Project/Fonts/simhei SDF HQ.asset");
            AssertCrispDialogueFont("Assets/_Project/Fonts/simhei SDF Dynamic.asset");
        }

        [Test]
        public void DialogueUI_DoesNotReplaceConfiguredFontOnShow()
        {
            EditorSceneManager.OpenScene(TestScenePath);

            var expectedFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(DialogueFontPath);
            var dialogueText = FindDialogueText();
            var dialogueUI = UnityEngine.Object.FindObjectOfType<DormGame.Dialogue.DialogueUI>(true);

            Assert.That(expectedFont, Is.Not.Null);
            Assert.That(dialogueText, Is.Not.Null);
            Assert.That(dialogueUI, Is.Not.Null);

            var character = ScriptableObject.CreateInstance<DormGame.Data.CharacterDefinition>();
            character.displayName = "测试角色";

            try
            {
                dialogueText.font = expectedFont;

                dialogueUI.Show(
                    new DormGame.Dialogue.DialogueNode
                    {
                        nodeId = "font-test",
                        textKey = "你好！我是测试角色，欢迎来到宿舍。",
                        choices = new DormGame.Dialogue.DialogueChoice[0]
                    },
                    character);

                Assert.That(dialogueText.font, Is.SameAs(expectedFont));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(character);
            }
        }

        [Test]
        public void DialogueUI_HasExplicitHqFontAssigned()
        {
            EditorSceneManager.OpenScene(TestScenePath);

            var expectedFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(DialogueFontPath);
            Assert.That(expectedFont, Is.Not.Null);

            var dialogueUI = UnityEngine.Object.FindObjectOfType<DormGame.Dialogue.DialogueUI>(true);
            Assert.That(dialogueUI, Is.Not.Null);

            var serializedObject = new SerializedObject(dialogueUI);
            var dialogueFont = serializedObject.FindProperty("dialogueFont");
            Assert.That(dialogueFont, Is.Not.Null);

            Assert.That(dialogueFont.objectReferenceValue, Is.SameAs(expectedFont));
        }

        [Test]
        public void DialogueUI_AppliesExplicitFontOnShow()
        {
            EditorSceneManager.OpenScene(TestScenePath);

            var expectedFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(DialogueFontPath);
            Assert.That(expectedFont, Is.Not.Null);

            var dialogueText = FindDialogueText();
            var dialogueUI = UnityEngine.Object.FindObjectOfType<DormGame.Dialogue.DialogueUI>(true);

            Assert.That(dialogueText, Is.Not.Null);
            Assert.That(dialogueUI, Is.Not.Null);

            dialogueText.font = null;

            var character = ScriptableObject.CreateInstance<DormGame.Data.CharacterDefinition>();
            character.displayName = "测试角色";

            try
            {
                dialogueUI.Show(
                    new DormGame.Dialogue.DialogueNode
                    {
                        nodeId = "font-apply-test",
                        textKey = "字体应用测试",
                        choices = new DormGame.Dialogue.DialogueChoice[0]
                    },
                    character);

                Assert.That(dialogueText.font, Is.SameAs(expectedFont));
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(character);
            }
        }

        [Test]
        public void DialogueUI_AppliesExplicitFontToChoiceButtons()
        {
            EditorSceneManager.OpenScene(TestScenePath);

            var expectedFont = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(DialogueFontPath);
            Assert.That(expectedFont, Is.Not.Null);

            var dialogueUI = UnityEngine.Object.FindObjectOfType<DormGame.Dialogue.DialogueUI>(true);
            Assert.That(dialogueUI, Is.Not.Null);

            var serializedObject = new SerializedObject(dialogueUI);
            var choicesContainer = serializedObject.FindProperty("choicesContainer").objectReferenceValue as Transform;
            Assert.That(choicesContainer, Is.Not.Null);

            try
            {
                dialogueUI.ShowChoices(new[]
                {
                    new DormGame.Dialogue.DialogueChoice { choiceTextKey = "选择一" }
                });

                var choiceButtonText = choicesContainer.GetComponentInChildren<TextMeshProUGUI>(true);
                Assert.That(choiceButtonText, Is.Not.Null);
                Assert.That(choiceButtonText.font, Is.SameAs(expectedFont));
            }
            finally
            {
                // 清理生成的按钮，避免污染场景
                foreach (Transform child in choicesContainer)
                {
                    UnityEngine.Object.DestroyImmediate(child.gameObject);
                }
            }
        }

        private static void AssertCrispDialogueFont(string assetPath)
        {
            var fontAsset = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(assetPath);

            Assert.That(fontAsset, Is.Not.Null, assetPath);
            Assert.That(fontAsset.atlasWidth, Is.GreaterThanOrEqualTo(2048), assetPath);
            Assert.That(fontAsset.atlasHeight, Is.GreaterThanOrEqualTo(2048), assetPath);
            Assert.That(fontAsset.material.GetFloat("_Sharpness"), Is.GreaterThanOrEqualTo(0.35f), assetPath);
            Assert.That(fontAsset.material.GetFloat("_WeightNormal"), Is.GreaterThanOrEqualTo(0.1f), assetPath);
            Assert.That(fontAsset.material.GetFloat("_FaceDilate"), Is.GreaterThanOrEqualTo(0.03f), assetPath);
        }

        private static TextMeshProUGUI FindDialogueText()
        {
            var dialoguePanel = GameObject.Find("DialoguePanel").transform;
            foreach (var text in dialoguePanel.GetComponentsInChildren<TextMeshProUGUI>(true))
            {
                if (text.transform.parent == dialoguePanel)
                {
                    return text;
                }
            }

            Assert.Fail("DialoguePanel is missing its direct dialogue text child.");
            return null;
        }
    }
}
#endif
