using UnityEngine;
using Sirenix.OdinInspector;

namespace cdvproject.Player.Movement
{
    /// <summary>
    /// This class is responsible for controlling the player's movement in a 2D game.
    /// It uses Unity's physics system via the Rigidbody2D component for smooth and responsive player motion.
    /// The class handles input capturing, applying movement, and provides adjustable parameters for movement speed and smoothing.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Inspector Fields

        [Title("Movement Settings")]
        [InfoBox("Adjust the movement speed and smoothness for the player.")]
        [SerializeField, Range(1f, 20f), Tooltip("Base movement speed of the player.")]
        private float moveSpeed = 5f;

        [SerializeField, Range(0f, 1f), Tooltip("How smooth the player movement is.")]
        private float movementSmoothing = 0.05f;

        #endregion

        #region Private Fields

        // Reference to the Rigidbody2D component, used for applying physics-based movement.
        private Rigidbody2D rb;

        // Used to smooth the player's velocity for more natural movement transitions.
        private Vector2 currentVelocity = Vector2.zero;

        // Stores the player's current movement input.
        private Vector2 movementInput;

        #endregion

        #region Unity Methods

        /// <summary>
        /// Initializes components and sets up any necessary references.
        /// Called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            InitializeComponents();
        }

        /// <summary>
        /// Captures player input during the Update phase. This phase is called once per frame.
        /// It is the appropriate place for handling user input, which can vary based on framerate.
        /// </summary>
        private void Update()
        {
            CaptureInput();
        }

        /// <summary>
        /// Handles the movement logic for the player. This is called in the FixedUpdate phase,
        /// which runs at a consistent frame rate in sync with Unity's physics system.
        /// </summary>
        private void FixedUpdate()
        {
            MovePlayer();
        }

        #endregion

        #region Odin Inspector Button

        /// <summary>
        /// Resets the movement settings to default values. 
        /// This button is available in the Unity Inspector for easy configuration during development.
        /// </summary>
        [Button("Reset Movement Settings", ButtonSizes.Medium)]
        private void ResetSettings()
        {
            moveSpeed = 5f;
            movementSmoothing = 0.05f;
        }

        #endregion

        #region Movement Methods

        /// <summary>
        /// Initializes necessary components, such as the Rigidbody2D used for player movement.
        /// This method is called once at the start to prepare the object for further actions.
        /// </summary>
        private void InitializeComponents()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        /// <summary>
        /// Captures the player's input for movement based on the horizontal and vertical axes ("Horizontal" and "Vertical").
        /// The input is raw, meaning it does not apply any smoothing or interpolation, providing immediate responsiveness.
        /// </summary>
        private void CaptureInput()
        {
            movementInput.x = Input.GetAxisRaw("Horizontal");
            movementInput.y = Input.GetAxisRaw("Vertical");
        }

        /// <summary>
        /// Moves the player based on the captured input. 
        /// The movement is smoothed using Unity's SmoothDamp function to provide a gradual transition in velocity.
        /// This method uses Rigidbody2D's MovePosition for smooth and physics-friendly movement.
        /// </summary>
        private void MovePlayer()
        {
            // Calculate the target position based on player input, movement speed, and fixed delta time.
            Vector2 targetPosition = rb.position + movementInput.normalized * moveSpeed * Time.fixedDeltaTime;

            // Smooth the player's movement toward the target position.
            rb.MovePosition(Vector2.SmoothDamp(rb.position, targetPosition, ref currentVelocity, movementSmoothing));
        }

        #endregion
    }
}