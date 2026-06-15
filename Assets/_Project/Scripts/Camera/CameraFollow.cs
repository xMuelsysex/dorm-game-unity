using UnityEngine;

namespace DormGame.Camera
{
    /// <summary>
    /// 简单的相机跟随脚本 - 替代 Cinemachine
    /// </summary>
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0, 5, -8);
        [SerializeField] private float smoothSpeed = 5f;
        [SerializeField] private bool lookAtTarget = true;
        [SerializeField] private Vector3 lookAtOffset = new Vector3(0, 1.5f, 0);

        private bool isFollowing = true;

        void LateUpdate()
        {
            if (target == null || !isFollowing) return;

            // 平滑跟随
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;

            // 看向目标
            if (lookAtTarget)
            {
                transform.LookAt(target.position + lookAtOffset);
            }
        }

        /// <summary>
        /// 启用/禁用跟随（对话时禁用）
        /// </summary>
        public void SetFollowing(bool follow)
        {
            isFollowing = follow;
        }

        /// <summary>
        /// 平滑移动到指定位置并看向目标点
        /// </summary>
        public void FocusOn(Vector3 position, Vector3 lookTarget)
        {
            isFollowing = false;
            StartCoroutine(SmoothFocusCoroutine(position, lookTarget));
        }

        /// <summary>
        /// 恢复跟随 Player
        /// </summary>
        public void ReturnToFollow()
        {
            isFollowing = true;
        }

        private System.Collections.IEnumerator SmoothFocusCoroutine(Vector3 targetPos, Vector3 lookTarget)
        {
            float elapsed = 0f;
            float duration = 0.5f;
            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            Quaternion targetRot = Quaternion.LookRotation(lookTarget - targetPos);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                transform.position = Vector3.Lerp(startPos, targetPos, t);
                transform.rotation = Quaternion.Slerp(startRot, targetRot, t);

                yield return null;
            }

            transform.position = targetPos;
            transform.rotation = targetRot;
        }
    }
}
