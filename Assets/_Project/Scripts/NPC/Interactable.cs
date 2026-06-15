using UnityEngine;
using DormGame.Data;
using DormGame.Dialogue;

namespace DormGame.NPC
{
    /// <summary>
    /// 可交互物体组件 - 触发对话
    /// </summary>
    public class Interactable : MonoBehaviour
    {
        [SerializeField] private string interactableId;
        [SerializeField] private float interactionRadius = 2f;
        [SerializeField] private CharacterDefinition character;

        public string InteractableId => interactableId;
        public float InteractionRadius => interactionRadius;

        void OnValidate()
        {
            // 自动生成 ID
            if (string.IsNullOrEmpty(interactableId))
            {
                interactableId = $"{gameObject.name}_{GetInstanceID()}";
            }
        }

        public void OnInteract()
        {
            Debug.Log($"[Interactable] OnInteract called for: {interactableId}");

            if (character != null)
            {
                Debug.Log($"[Interactable] Character assigned: {character.characterId}");

                // 触发对话系统（传递自己的 Transform）
                if (DialogueSystem.Instance != null)
                {
                    Debug.Log("[Interactable] DialogueSystem.Instance found, calling StartDialogue...");
                    DialogueSystem.Instance.StartDialogue(character, transform);
                }
                else
                {
                    Debug.LogError("[Interactable] DialogueSystem.Instance is NULL!");
                }
            }
            else
            {
                Debug.LogWarning($"Interactable {interactableId} has no character assigned!");
            }
        }

        void OnDrawGizmosSelected()
        {
            // 可视化交互范围
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRadius);
        }
    }
}
