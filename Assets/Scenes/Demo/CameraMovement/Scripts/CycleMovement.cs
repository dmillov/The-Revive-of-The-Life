using UnityEngine;
using DG.Tweening; // ϳ��������� �������� DOTween

public class CircularMovement2D : MonoBehaviour
{
    public float radius = 5f; // ����� ����
    public float duration = 20f; // ��� ������ ������� ������
    public Vector2 center = Vector2.zero; // ����� ����
    private Tween circularTween; // ���������� ���� ��� �������� ���� ���������

    private void Start()
    {
        // ���������� ������� �� ��� (8 �����)
        Vector3[] waypoints = new Vector3[8];
        for (int i = 0; i < waypoints.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / waypoints.Length;
            waypoints[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius + (Vector3)center;
        }

        // ��������� ���� � ����� �� ����
        circularTween = transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetOptions(true)  // ��� �� ���������� �����
            .SetEase(Ease.Linear)  // г�������� ���
            .SetLoops(-1, LoopType.Restart); // �������� ��� (���������)
    }

    // ����� ��� ���� �������� ������ ���� timeScale
    public void ChangeSpeed(float speedMultiplier)
    {
        if (circularTween != null)
        {
            circularTween.timeScale = speedMultiplier; // ���� ��������
        }
    }
}
