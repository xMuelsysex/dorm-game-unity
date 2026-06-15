using UnityEngine;

namespace DormGame.Data
{
    [CreateAssetMenu(fileName = "Character", menuName = "DormGame/Character")]
    public class CharacterDefinition : ScriptableObject
    {
        public string characterId;         // 稳定唯一ID
        public string displayName;         // 显示名称（暂时直接中文）
        public Sprite portrait;            // 对话框立绘
        public GameObject walkPrefab;      // 场景中可走动的预制体
        public string llmPersona;          // 喂给LLM的人设（阶段3用）
        public DialogueScript[] mainScripts;  // 手写主线剧本集
        public float walkSpeed = 1.5f;
    }
}
