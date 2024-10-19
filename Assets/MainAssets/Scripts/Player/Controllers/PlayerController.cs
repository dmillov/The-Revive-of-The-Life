using UnityEngine;

namespace cdvproject.Player.Movement
{
    /// <summary>
    /// The PlayerController class is responsible for managing the player's movement by linking
    /// input handling and movement logic. It captures the player's input from the PlayerInputHandler 
    /// and passes this input to the PlayerMover, which handles the actual movement of the player.
    /// 
    /// This class ties together the input and movement components, making it a key part of the player's 
    /// overall control system.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// The PlayerInputHandler component responsible for capturing input.
        /// This is where player input, such as movement directions, is gathered.
        /// </summary>
        [SerializeField] private PlayerInputHandler inputHandler;

        /// <summary>
        /// The PlayerMover component responsible for moving the player based on input.
        /// This is where the actual movement logic is executed.
        /// </summary>
        [SerializeField] private PlayerMover playerMover;

        /// <summary>
        /// Called once per frame to handle player movement.
        /// This method captures movement input from the PlayerInputHandler and passes it to
        /// the PlayerMover to move the player accordingly.
        /// </summary>
        private void Update()
        {
            // Capture movement input from the input handler
            Vector2 movementInput = inputHandler.CaptureMovementInput();

            // Move the player based on the captured input
            playerMover.Move(movementInput);
        }
    }
}