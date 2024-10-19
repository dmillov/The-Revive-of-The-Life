using Sirenix.OdinInspector;
using UnityEngine;

namespace cvdproject.Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        [Title("Movement Settings")]
        [SerializeField, Range(1f, 20f)] private float moveSpeed = 5f;

        [SerializeField, Range(0f, 1f)] private float movementSmoothing = 0.05f;

        private Rigidbody2D rb;
        private Vector2 currentVelocity = Vector2.zero;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void Move(Vector2 input)
        {
            Vector2 targetPosition = rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, movementSmoothing));
        }
    }
}
