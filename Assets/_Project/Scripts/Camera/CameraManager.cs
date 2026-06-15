using UnityEngine;

namespace DormGame.Camera
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }

        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private Vector3 dialogueCameraOffset = new Vector3(0, 2, -5);

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void FocusOnCharacter(Transform npcTransform)
        {
            if (npcTransform == null)
            {
                Debug.LogWarning("NPC Transform is null for camera focus");
                return;
            }

            if (cameraFollow == null)
            {
                Debug.LogError("CameraFollow not assigned!");
                return;
            }

            // 计算对话相机位置和看向点
            Vector3 npcPos = npcTransform.position;
            Vector3 cameraPos = npcPos + dialogueCameraOffset;
            Vector3 lookTarget = npcPos + Vector3.up * 1.5f;

            // 平滑移动到 NPC 前方
            cameraFollow.FocusOn(cameraPos, lookTarget);
        }

        public void ReturnToPlayerView()
        {
            if (cameraFollow == null)
            {
                Debug.LogError("CameraFollow not assigned!");
                return;
            }

            // 恢复跟随 Player
            cameraFollow.ReturnToFollow();
        }
    }
}
