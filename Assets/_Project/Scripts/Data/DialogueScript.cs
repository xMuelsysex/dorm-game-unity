using UnityEngine;
using DormGame.Dialogue;

namespace DormGame.Data
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "DormGame/Dialogue Script")]
    public class DialogueScript : ScriptableObject
    {
        public string scriptId;
        public DialogueTrigger trigger;
        public DialogueNode[] nodes;
    }
}
