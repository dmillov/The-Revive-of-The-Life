using SGS29.Utilities;
using UnityEngine;

namespace cdvproject.PromptInteraction
{
    /// <summary>
    /// The <c>ProgressInteraction</c> class manages the smooth updating of a UI progress bar 
    /// or similar element based on user interaction inputs. It specifically handles the 
    /// progress for keyboard-based inputs, providing visual feedback through a RectTransform.
    /// This class inherits from <see cref="MonoSingleton{T}"/>, making it a singleton component
    /// in the Unity scene.
    /// </summary>
    public class ProgressInteraction : MonoSingleton<ProgressInteraction>
    {
        /// <summary>
        /// The RectTransform representing the progress UI element.
        /// This UI element's right offset is adjusted to reflect interaction progress.
        /// </summary>
        [SerializeField] private RectTransform progressRectTransform;

        /// <summary>
        /// The starting value for the RectTransform's right offset.
        /// This defines the initial position of the progress bar or UI element.
        /// </summary>
        [SerializeField] private float startRightValue = 122f;

        /// <summary>
        /// The target value for the RectTransform's right offset when progress is complete (i.e., 100%).
        /// This defines the final position of the progress bar or UI element.
        /// </summary>
        [SerializeField] private float endRightValue = 0f;

        /// <summary>
        /// The smoothing duration in seconds for the transition of the progress bar.
        /// This value controls how quickly the progress bar's movement interpolates between values.
        /// </summary>
        [SerializeField] private float smoothTime = 0.1f;

        /// <summary>
        /// The current right offset value for the RectTransform.
        /// This value is dynamically updated during the interaction.
        /// </summary>
        private float currentRightValue;

        /// <summary>
        /// The velocity used by the <see cref="Mathf.SmoothDamp"/> function to smooth the transitions.
        /// </summary>
        private float velocity = 0f;

        /// <summary>
        /// Unity's Start method initializes the right offset value at the beginning.
        /// It sets the <c>currentRightValue</c> to <c>startRightValue</c> when the scene loads.
        /// </summary>
        void Start()
        {
            currentRightValue = startRightValue;
        }

        /// <summary>
        /// Updates the right offset of the <see cref="RectTransform"/> based on user input.
        /// This method is specifically designed for keyboard interactions where the user holds a key.
        /// The method smoothly animates the progress UI element from the starting to the ending position
        /// based on the hold progress of the interaction input.
        /// </summary>
        /// <param name="interactionInput">The input used for the interaction. Must be of type <see cref="KeyboardInput"/>.</param>
        /// <remarks>
        /// - This method only updates the progress UI element if the input is keyboard-based 
        ///   (i.e., an instance of <see cref="KeyboardInput"/>).
        /// - The progress is expected to range from 0 to 1, where 0 means no progress and 1 means full progress.
        /// - The <see cref="RectTransform"/>'s right boundary is adjusted using its <c>offsetMax</c> property.
        /// - If <c>progressRectTransform</c> is not assigned, no updates will be applied.
        /// </remarks>
        public void UpdateProgressRectTransform(IInteractionInput interactionInput)
        {
            // Checks if progressRectTransform is set and if the interaction input is keyboard-based.
            if (progressRectTransform != null && interactionInput is KeyboardInput keyboardInput)
            {
                // Retrieves the hold progress from 0 to 1.
                float progress = keyboardInput.GetHoldProgress();

                // Calculates the target right value based on the progress.
                float targetRightValue = Mathf.Lerp(startRightValue, endRightValue, progress);

                // Smoothly updates the current right value.
                currentRightValue = Mathf.SmoothDamp(currentRightValue, targetRightValue, ref velocity, smoothTime);

                // Updates the RectTransform's right offset.
                Vector2 offsetMax = progressRectTransform.offsetMax;
                offsetMax.x = -currentRightValue;  // Adjusts the right boundary.
                progressRectTransform.offsetMax = offsetMax;
            }
        }
    }
}