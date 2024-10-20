using UnityEngine;

namespace cdvproject.Player.Movement
{
    /// <summary>
    /// Class responsible for handling player input for movement.
    /// This class captures the player's movement input from the input system and returns it as a Vector2.
    /// Currently, this implementation uses the old Unity Input system.
    /// 
    /// To upgrade this system to the new Unity Input System, you'll need to:
    /// 1. Install the new Input System package via the Unity Package Manager.
    /// 2. Replace `Input.GetAxisRaw` calls with the InputAction system.
    /// 3. Map the movement controls in the Input Action Asset for proper handling.
    /// 4. Update this class to handle the input via the InputAction system for improved flexibility and scalability.
    /// </summary>
    public class PlayerInputHandler : MonoBehaviour
    {
        /// <summary>
        /// Captures player movement input.
        /// 
        /// This method currently uses the old Unity Input System and retrieves horizontal
        /// and vertical input values from the player via the "Horizontal" and "Vertical" axes.
        /// The returned Vector2 represents the direction of movement.
        /// 
        /// To use the new Input System, the input capturing logic will need to be refactored to use InputAction objects.
        /// </summary>
        /// <returns>
        /// A Vector2 representing the player's movement input where:
        /// - X is the horizontal axis input.
        /// - Y is the vertical axis input.
        /// </returns>
        public Vector2 CaptureMovementInput()
        {
            // Using the old Input System to capture movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            return new Vector2(horizontal, vertical);
        }
    }
}