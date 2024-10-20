using Sirenix.OdinInspector;
using UnityEngine;

namespace cdvproject.Player.Movement
{
    /// <summary>
    /// Controls player movement in a 2D game using Unity's physics system and a smooth damping technique.
    /// This class implements the IPlayerMovement interface, allowing for interchangeable movement logic.
    /// It handles input-independent movement logic for better modularity and flexibility.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour, IPlayerMovement
    {
        #region Inspector Fields

        [Title("Movement Settings")]
        [SerializeField, Range(1f, 20f), Tooltip("Base movement speed of the player.")]
        private float moveSpeed = 5f;

        [SerializeField, Range(0f, 1f), Tooltip("How smooth the player movement is.")]
        private float movementSmoothing = 0.05f;

        #endregion

        #region Private Fields

        // Rigidbody2D component responsible for moving the player with Unity's physics.
        private Rigidbody2D rb;

        // Velocity reference for smoothing the movement transitions.
        private Vector2 currentVelocity = Vector2.zero;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes necessary components such as the Rigidbody2D.
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        #endregion

        #region Movement Methods

        /// <summary>
        /// Moves the player based on input provided by external sources (e.g., player input).
        /// This method calculates the target position and smoothly moves the player to that position using physics-based movement.
        /// </summary>
        /// <param name="input">The directional input, typically coming from a player input system or AI.</param>
        public void Move(Vector2 input)
        {
            // Calculate the target position based on player input, movement speed, and fixed delta time.
            Vector2 targetPosition = rb.position + input.normalized * moveSpeed * Time.fixedDeltaTime;

            // Move the player smoothly toward the target position, applying smooth dampening to the movement.
            rb.MovePosition(Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, movementSmoothing));
        }

        #endregion
    }
}