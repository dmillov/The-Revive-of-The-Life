namespace cdvproject
{
    /// <summary>
    /// Interface for handling interaction input.
    /// Provides methods to check for input actions and their status.
    /// </summary>
    public interface IInteractionInput
    {
        /// <summary>
        /// Checks if the input has been received.
        /// </summary>
        /// <returns>True if the input was received; otherwise, false.</returns>
        bool IsInputReceived();

        /// <summary>
        /// Gets the input as a string representation.
        /// </summary>
        /// <returns>A string representing the input action.</returns>
        string GetInputAsString();

        /// <summary>
        /// Checks if the input is currently being held down.
        /// </summary>
        /// <returns>True if the input is being held; otherwise, false.</returns>
        bool IsHolding();

        /// <summary>
        /// Gets the progress of holding the input as a value between 0 and 1.
        /// </summary>
        /// <returns>A float representing the hold progress (0 to 1).</returns>
        float GetHoldProgress();
    }
}
