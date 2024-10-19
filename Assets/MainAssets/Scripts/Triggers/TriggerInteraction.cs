using cdvproject.PromptInteraction;
using SGS29.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace cdvproject.Trigger
{
    /// <summary>
    /// This class handles interaction triggers, prompting the player for input and managing UI feedback 
    /// through a RectTransform progress indicator.
    /// </summary>
    public class TriggerInteraction : MonoBehaviour
    {
        [SerializeField] private InteractionType interactionType;    // The type of interaction to be displayed.
        [SerializeField] private UnityEvent Event_OnInteractionInput; // The event triggered on interaction input.
        [SerializeField] private bool useInputLoading;               // Indicates if input loading is used (not currently implemented).
        [SerializeField] private KeyCode keyCode;                    // The key to trigger the interaction.
        [SerializeField] private bool isMobileControl;               // Indicates if mobile controls are used.
        [SerializeField] private string nameText;                    // The text displayed in the interaction prompt.
        [SerializeField] private RectTransform progressRectTransform; // The RectTransform for the progress UI element.
        [SerializeField] private float startRightValue = 122f;      // The starting value for the RectTransform's right offset.
        [SerializeField] private float endRightValue = 0f;          // The ending value for the RectTransform's right offset.
        [SerializeField] private float smoothTime = 0.1f;           // The time for smoothing the transition.

        private IInteractionInput _interactionInput;                // Interface for handling input.
        private float currentRightValue;                             // The current right offset value for the RectTransform.
        private float velocity = 0f;                                 // The velocity for smoothing transitions.

        private void Start()
        {
            // Initializes the interaction input and sets the starting right value.
            _interactionInput = GetInput();
            currentRightValue = startRightValue;
        }

        /// <summary>
        /// Displays the interaction prompt to the player.
        /// </summary>
        public void ShowPrompt()
        {
            // Creates interaction info and shows the prompt.
            InteractionInfo info = new InteractionInfo(interactionType, nameText, GetInput(), OnInteractionInput);
            SM.Instance<InteractionPromptController>().ShowPrompt(info);
        }

        /// <summary>
        /// Hides the interaction prompt from the player.
        /// </summary>
        public void HidePrompt()
        {
            // Hides the interaction prompt if it exists.
            if (SM.HasSingleton<InteractionPromptController>())
            {
                SM.Instance<InteractionPromptController>().HidePrompt();
            }
        }

        /// <summary>
        /// Gets the appropriate input method based on the control type.
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionInput"/> for handling input.</returns>
        private IInteractionInput GetInput()
        {
            return isMobileControl ? new MobileMenuInput() : new KeyboardInput(keyCode);
        }

        /// <summary>
        /// Invokes the interaction input event.
        /// </summary>
        private void OnInteractionInput()
        {
            Event_OnInteractionInput.Invoke();
        }

        /// <summary>
        /// Updates the right offset of the RectTransform based on the input progress.
        /// </summary>
        private void UpdateProgressRectTransform()
        {
            // Checks if progressRectTransform is set and if the interaction input is keyboard-based.
            if (progressRectTransform != null && _interactionInput is KeyboardInput keyboardInput)
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

        /// <summary>
        /// Called when another collider enters the trigger collider attached to this object.
        /// </summary>
        /// <param name="other">The other collider that entered the trigger.</param>
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                ShowPrompt(); // Show prompt if the player enters the trigger.
            }
        }

        /// <summary>
        /// Called when another collider exits the trigger collider attached to this object.
        /// </summary>
        /// <param name="other">The other collider that exited the trigger.</param>
        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                HidePrompt(); // Hide prompt if the player exits the trigger.
            }
        }

        /// <summary>
        /// Updates the progress RectTransform each frame.
        /// </summary>
        private void Update()
        {
            UpdateProgressRectTransform(); // Updates the RectTransform based on input progress.
        }
    }
}