using UnityEngine;
using DG.Tweening; // Підключення бібліотеки DOTween

public class CircularMovement2D : MonoBehaviour
{
    public float radius = 5f; // Радіус кола
    public float duration = 20f; // Час одного повного оберту
    public Vector2 center = Vector2.zero; // Центр кола
    private Tween circularTween; // Збереження твіну для подальшої зміни параметрів

    private void Start()
    {
        // Обчислення позицій на колі (8 точок)
        Vector3[] waypoints = new Vector3[8];
        for (int i = 0; i < waypoints.Length; i++)
        {
            float angle = i * Mathf.PI * 2f / waypoints.Length;
            waypoints[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius + (Vector3)center;
        }

        // Створення твіну з рухом по колу
        circularTween = transform.DOPath(waypoints, duration, PathType.CatmullRom)
            .SetOptions(true)  // Рух по замкнутому шляху
            .SetEase(Ease.Linear)  // Рівномірний рух
            .SetLoops(-1, LoopType.Restart); // Циклічний рух (безкінечно)
    }

    // Метод для зміни швидкості шляхом зміни timeScale
    public void ChangeSpeed(float speedMultiplier)
    {
        if (circularTween != null)
        {
            circularTween.timeScale = speedMultiplier; // Зміна швидкості
        }
    }
}
