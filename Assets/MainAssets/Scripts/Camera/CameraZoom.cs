using Cinemachine;
using UnityEngine;

namespace cdvproject.Camera
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualCamera; // ³�������� ������
        [SerializeField] private float minZoom = 3f; // ̳�������� ���
        [SerializeField] private float maxZoom = 10f; // ������������ ���
        [SerializeField] private float zoomSpeed = 2f; // �������� ���� ��� ��� ��������
        [SerializeField] private float smoothSpeed = 5f; // �������� ���������� ����

        private float targetZoom; // ֳ����� �������� ����
        private float currentZoom; // �������� ��� ������

        private void Awake()
        {
            if (virtualCamera == null)
            {
                virtualCamera = GetComponent<CinemachineVirtualCamera>();
            }

            // ������� ��������� �������� ���� ��� ������� ���
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
            // ������� ����� ��� �� �������� ���� �� ���������� ���� ������� �������� ����
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (scrollInput != 0f)
            {
                targetZoom -= scrollInput * zoomSpeed;
                targetZoom = Mathf.Clamp(targetZoom, minZoom, maxZoom); // ��������� ����
            }
        }

        private void SmoothZoom()
        {
            // ������ ������� �������� ��� �� ���������
            currentZoom = Mathf.Lerp(currentZoom, targetZoom, smoothSpeed * Time.deltaTime);
            virtualCamera.m_Lens.OrthographicSize = currentZoom;
        }

        private void OnDisable()
        {
            // �������� ������� �������� ����, ���� ������ ���������� ��� ��� �����������
            PlayerPrefs.SetFloat("CameraZoom", currentZoom);
            PlayerPrefs.Save();
        }
    }
}