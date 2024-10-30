using Cinemachine;
using UnityEngine;

namespace cdvproject.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera; // Віртуальна камера
        [SerializeField] private float minZoom = 3f; // Мінімальний зум
        [SerializeField] private float maxZoom = 10f; // Максимальний зум
        [SerializeField] private float zoomSpeed = 2f; // Швидкість зуму при зміні значення
        [SerializeField] private float smoothSpeed = 5f; // Швидкість згладження зуму

        private float targetZoom; // Цільове значення зуму
        private float currentZoom; // Поточний зум камери

        private void Awake()
        {
            if (virtualCamera == null)
            {
                virtualCamera = GetComponent<CinemachineVirtualCamera>();
            }

            // Зчитуємо збережене значення зуму при запуску гри
            targetZoom = PlayerPrefs.GetFloat("CameraZoom", virtualCamera.m_Lens.OrthographicSize);
            currentZoom = targetZoom;
            virtualCamera.m_Lens.OrthographicSize = currentZoom;
        }

        private void Update()
        {
            HandleZoom();
            SmoothZoom();
        }

        private void HandleZoom()
        {
            // Зчитуємо вхідні дані від колесика миші та обчислюємо нове цільове значення зуму
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                targetZoom -= scrollInput * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom); // Обмеження зуму
            }
        }

        private void SmoothZoom()
        {
            // Плавно змінюємо поточний зум до цільового
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, smoothSpeed * Time.deltaTime);
            virtualCamera.m_Lens.OrthographicSize = currentZoom;
        }

        private void OnDisable()
        {
            // Зберігаємо поточне значення зуму, коли скрипт вимикається або гра закривається
            PlayerPrefs.SetFloat("CameraZoom", currentZoom);
            PlayerPrefs.Save();
        }
    }
}