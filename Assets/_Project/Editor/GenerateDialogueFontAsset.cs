using UnityEditor;
using UnityEngine;
using TMPro;

public class GenerateDialogueFontAsset
{
    [MenuItem("Tools/Generate Dialogue Font Asset")]
    public static void Generate()
    {
        // 加载黑体字体
        var font = AssetDatabase.LoadAssetAtPath<Font>("Assets/_Project/Fonts/simhei.ttf");
        if (font == null)
        {
            Debug.LogError("simhei.ttf not found!");
            return;
        }

        // 对话中需要的所有汉字（从 TestDialogue.asset 提取）
        string characters = "你好！我是测试角色，欢迎来到宿舍。想做什么呢？聊天再见很高兴和下次～" +
                           "继续▶"; // 加上 UI 按钮文字

        // 添加 ASCII 字符（保险起见）
        for (char c = ' '; c <= '~'; c++)
        {
            characters += c;
        }

        Debug.Log($"Generating font with {characters.Length} characters...");

        // 使用 TextMeshPro 的 FontEngine 生成字体
        var fontAsset = TMP_FontAsset.CreateFontAsset(font);
        fontAsset.name = "SimHei Dialogue Static";

        // 设置为 Static 模式
        fontAsset.atlasPopulationMode = AtlasPopulationMode.Static;

        // 保存
        AssetDatabase.CreateAsset(fontAsset, "Assets/_Project/Fonts/SimHei Dialogue Static.asset");
        AssetDatabase.SaveAssets();

        Debug.Log($"Font asset created: {fontAsset.name}");

        // 尝试添加字符到 atlas（这一步可能需要手动用 Font Asset Creator）
        EditorUtility.DisplayDialog("Font Created",
            "Font asset created, but characters need to be added manually.\n\n" +
            "Please:\n" +
            "1. Open Window > TextMeshPro > Font Asset Creator\n" +
            "2. Select Source Font: simhei.ttf\n" +
            "3. Set Character Set: Custom Characters\n" +
            "4. Paste: " + characters + "\n" +
            "5. Click Generate Font Atlas\n" +
            "6. Save to: SimHei Dialogue Static.asset",
            "OK");
    }
}
