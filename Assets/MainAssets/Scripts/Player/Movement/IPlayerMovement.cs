using UnityEngine;

namespace cdvproject.Player.Movement
{
    /// <summary>
    /// Interface for handling player movement in 2D games.
    /// Defines the basic contract for movement behavior, allowing flexible implementation for various movement systems.
    /// </summary>
    public interface IPlayerMovement
    {
        /// <summary>
        /// Moves the player based on the provided input vector.
        /// This method will be implemented by classes that define specific movement behaviors, such as physics-based or AI-driven movement.
        /// </summary>
        /// <param name="input">The directional input used to control player movement, typically coming from a player input system or AI logic.</param>
        void Move(Vector2 input);
    }
}