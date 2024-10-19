using UnityEngine;
using UnityEngine.UI;

namespace cdvproject
{
    /// <summary>
    /// Class for handling mobile input interactions through a button.
    /// Implements the <see cref="IInteractionInput"/> interface to provide input functionality.
    /// </summary>
    public class MobileMenuInput : IInteractionInput
    {
        private bool _isInputReceived;   // Indicates whether the input has been received.
        private Button _mobileButton;     // The button used for mobile input.

        /// <summary>
        /// Initializes the mobile input class with the specified button.
        /// </summary>
        /// <param name="mobileButton">The button that will trigger the input.</param>
        public void Initialize(Button mobileButton)
        {
            _mobileButton = mobileButton;  // Store the reference to the button
            _mobileButton.onClick.AddListener(OnMobileButtonClick); // Add click listener
        }

        /// <summary>
        /// Called when the mobile button is clicked.
        /// Sets the input received flag to true.
        /// </summary>
        private void OnMobileButtonClick()
        {
            _isInputReceived = true; // Mark input as received when button is clicked
        }

        /// <summary>
        /// Checks if the button is currently being held down.
        /// </summary>
        /// <returns>True if the button was pressed; otherwise, false.</returns>
        public bool IsHolding()
        {
            return _isInputReceived; // Returns true if the button was clicked
        }

        /// <summary>
        /// Gets the hold progress of the button as a value between 0 and 1.
        /// </summary>
        /// <returns>Returns 1 if the button was pressed; otherwise, 0.</returns>
        public float GetHoldProgress()
        {
            // You can implement hold progress logic if needed.
            // Currently returns 1, meaning the button was pressed.
            return _isInputReceived ? 1f : 0f;
        }

        /// <summary>
        /// Gets the input as a string representation.
        /// </summary>
        /// <returns>A string representing the input action.</returns>
        public string GetInputAsString() => "Tap"; // Represents the mobile input as "Tap"

        /// <summary>
        /// Checks if the input has been received.
        /// </summary>
        /// <returns>True if the input was received; otherwise, false.</returns>
        public bool IsInputReceived()
        {
            if (_isInputReceived)
            {
                _isInputReceived = false; // Reset the flag after input is received
                return true; // Return true for the received input
            }
            return false; // Return false if no input was received
        }
    }
}