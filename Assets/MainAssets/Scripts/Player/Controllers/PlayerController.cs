using cdvproject.Camera;
using UnityEngine;
using Sirenix.OdinInspector;

namespace cdvproject.Player
{
    /// <summary>
    /// Controls the player's state, movement, and camera interactions based on input.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private PlayerInputHandler inputHandler;            // Handles player input.
        [SerializeField] private PlayerMover playerMover;                    // Manages player movement.
        [SerializeField] private PlayerStateController playerStateController; // Manages player state.
        [SerializeField] private CameraController cameraController;          // Manages camera behavior.

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current state of the player.
        /// </summary>
        public PlayerState CurrentState => playerStateController.GetCurrentState();

        /// <summary>
        /// Gets the player's current state as a descriptive text.
        /// </summary>
        public string CurrentStateText
        {
            get
            {
                return CurrentState switch
                {
                    PlayerState.Running => "running",
                    PlayerState.Walking => "walking",
                    PlayerState.Idle => "idle",
                    PlayerState.Freeze => "freeze",
                    _ => "Unknown"
                };
            }
        }

        #endregion

        #region Unity Lifecycle

        private void FixedUpdate()
        {
            // Skip updates if the player state is frozen.
            if (CurrentState == PlayerState.Freeze) return;

            Vector2 movementInput = inputHandler.CaptureMovementInput();
            Vector2 normalizedInput = movementInput.magnitude > 1f ? movementInput.normalized : movementInput;
            bool isRunning = movementInput.magnitude > 5f;

            // Update player state and camera based on movement input.
            UpdatePlayerState(isRunning, normalizedInput);
            playerMover.Move(normalizedInput); // Move player with normalized input.
        }

        #endregion

        #region Player State Management

        /// <summary>
        /// Updates the player's state and camera zoom based on movement.
        /// </summary>
        /// <param name="isRunning">Indicates if the player is running.</param>
        /// <param name="normalizedInput">Normalized movement input vector.</param>
        private void UpdatePlayerState(bool isRunning, Vector2 normalizedInput)
        {
            if (isRunning)
            {
                playerStateController.ChangeState(PlayerState.Running);
                playerMover.SetRunning(true);
                cameraController.SetCameraZoom(true); // Zoom in camera while running.
            }
            else if (normalizedInput.magnitude > 0f)
            {
                playerStateController.ChangeState(PlayerState.Walking);
                playerMover.SetRunning(false);
                cameraController.SetCameraZoom(false); // Reset camera zoom while walking.
            }
            else
            {
                playerStateController.ChangeState(PlayerState.Idle);
                playerMover.SetRunning(false);
                cameraController.SetCameraZoom(false); // Reset camera zoom when idle.
            }
        }

        #endregion
    }
}