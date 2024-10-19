using UnityEngine;
using Sirenix.OdinInspector;

namespace cvdproject.Player.Movement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [Title("Movement Settings")]
        [InfoBox("Adjust the movement speed and smoothness for the player.")]
        [SerializeField, Range(1f, 20f), Tooltip("Base movement speed of the player.")]
        private float moveSpeed = 5f;

        [SerializeField, Range(0f, 1f), Tooltip("How smooth the player movement is.")]
        private float movementSmoothing = 0.05f;

        private Rigidbody2D rb;
        private Vector2 currentVelocity = Vector2.zero;
        private Vector2 movementInput;

        private void Awake()
        {
            InitializeComponents();
        }

        private void Update()
        {
            CaptureInput();
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        [Button("Reset Movement Settings", ButtonSizes.Medium)]
        private void ResetSettings()
        {
            moveSpeed = 5f;
            movementSmoothing = 0.05f;
        }

        #region Movement Methods

        /// <summary>
        /// ≤н≥ц≥ал≥зуЇ компоненти на об'Їкт≥ гравц€.
        /// </summary>
        private void InitializeComponents()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// ќтримуЇ користувацький вх≥д дл€ руху гравц€.
        /// </summary>
        private void CaptureInput()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }

        /// <summary>
        /// ѕерем≥щуЇ гравц€ з плавною ≥нтерпол€ц≥Їю.
        /// </summary>
        private void MovePlayer()
        {
            Vector2 targetPosition = rb.position + movementInput.normalized * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, movementSmoothing));
        }

        #endregion
    }
}