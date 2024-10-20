using Sirenix.OdinInspector;
using UnityEngine;

namespace cdvproject.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        [Title("Movement Settings")]
        [SerializeField, Range(10f, 40f)] private float walkSpeed = 15f;
        [SerializeField, Range(10f, 40f)] private float runSpeed = 25f;

        [SerializeField, Range(0f, 1f)] private float movementSmoothing = 0.05f;

        private Rigidbody2D rb;
        private Vector2 currentVelocity = Vector2.zero;
        private float currentSpeed; // Додаємо поле для поточної швидкості

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentSpeed = walkSpeed; // Встановлюємо початкову швидкість
        }

        public void Move(Vector2 input)
        {
            Vector2 targetPosition = rb.position + input.normalized * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, movementSmoothing));
        }

        public void SetRunning(bool isRunning)
        {
            currentSpeed = isRunning ? runSpeed : walkSpeed; // Встановлюємо швидкість залежно від стану
        }
    }
}