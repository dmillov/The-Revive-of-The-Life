using Cinemachine;
using UnityEngine;

namespace cdvproject.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera; // Віртуальна камера
        [SerializeField] private float zoomedInOrthoSize = 4f; // Розмір камери при бігу
        [SerializeField] private float zoomedOutOrthoSize = 6f; // Розмір камери при ходьбі
        [SerializeField] private float transitionSpeed = 2f; // Швидкість переходу

        private void Awake()
        {
            if (virtualCamera == null)
            {
                virtualCamera = GetComponent<CinemachineVirtualCamera>(); // Отримуємо віртуальну камеру, якщо не призначена
            }
        }

        public void SetCameraZoom(bool isRunning)
        {
            if (virtualCamera != null)
            {
                float targetOrthoSize = isRunning ? zoomedInOrthoSize : zoomedOutOrthoSize;
                virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, targetOrthoSize, transitionSpeed * Time.deltaTime);
            }
        }
    }
}