using UnityEngine;

namespace cdvproject
{
    /// <summary>
    /// Class for handling keyboard input interactions for PC.
    /// Implements the <see cref="IInteractionInput"/> interface to provide input functionality.
    /// </summary>
    public class KeyboardInput : IInteractionInput
    {
        private readonly KeyCode _key;              // The key that triggers the interaction.
        private readonly float holdDuration;          // Duration required to hold the key.
        private float holdTime;                       // Time the key has been held down.

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardInput"/> class.
        /// </summary>
        /// <param name="key">The KeyCode to be used for interaction.</param>
        /// <param name="holdDuration">The duration for which the key must be held down (default is 1.0 second).</param>
        public KeyboardInput(KeyCode key, float holdDuration = 1.0f)
        {
            _key = key;
            this.holdDuration = holdDuration;
            holdTime = 0f; // Initialize hold time.
        }

        /// <summary>
        /// Checks if the input has been received by holding the key for the required duration.
        /// </summary>
        /// <returns>True if the key is held for the required duration; otherwise, false.</returns>
        public bool IsInputReceived()
        {
            if (Input.GetKey(_key)) // If the key is pressed
            {
                holdTime += Time.deltaTime; // Increment hold time
                if (holdTime >= holdDuration) // If hold time meets or exceeds the required duration
                {
                    return true; // Return true when hold time is met
                }
            }
            else
            {
                holdTime = 0f; // Reset the timer if the key is released
            }

            return false; // Return false if hold time is insufficient
        }

        /// <summary>
        /// Gets the progress of holding the key as a value between 0 and 1.
        /// </summary>
        /// <returns>A float representing the hold progress (0 to 1).</returns>
        public float GetHoldProgress()
        {
            if (Input.GetKey(_key))
            {
                holdTime += Time.deltaTime; // Update hold time
            }
            else
            {
                holdTime = 0f; // Reset if the key is released
            }

            // Return progress as a ratio of hold time to required duration
            return Mathf.Clamp(holdTime / holdDuration, 0f, 1f);
        }

        /// <summary>
        /// Checks if the key is currently being held down.
        /// </summary>
        /// <returns>True if the key is held; otherwise, false.</returns>
        public bool IsHolding()
        {
            return Input.GetKey(_key); // Return true if the key is still pressed
        }

        /// <summary>
        /// Gets the key input as a string representation.
        /// </summary>
        /// <returns>A string representation of the key.</returns>
        public string GetInputAsString() => _key.ToString();
    }
}