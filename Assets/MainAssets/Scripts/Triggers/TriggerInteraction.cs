using PixelCrushers.DialogueSystem;
using SGS29.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace cdvproject.PromptInteraction
{
    /// <summary>
    /// Manages interaction triggers, prompts the player for input, and manages UI feedback 
    /// with a RectTransform progress indicator.
    /// </summary>
    [AddComponentMenu("")] // Use wrapper.
    public class TriggerInteraction : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private InteractionType interactionType;      // Type of interaction for the displayed prompt.
        [SerializeField] private Usable usable;                        // The usable object for interaction.
        [SerializeField] private UnityEvent Event_OnInteractionInput;  // Event triggered on interaction input.
        [SerializeField] private bool useInputLoading;                 // Indicates if input loading is used (not yet implemented).
        [SerializeField] private KeyCode keyCode;                      // Key to trigger the interaction.
        [SerializeField] private bool isMobileControl;                 // Indicates if mobile controls are used.
        [SerializeField] private string nameText;                      // Text displayed in the interaction prompt.
        [SerializeField] private bool respondOnlyTrigger = true;      // Indicates if only trigger interactions are responded to.

        #endregion

        #region Private Fields

        private IInteractionInput _interactionInput;                   // Interface for handling input.
        private Transform actorTransform = null;                       // Transform of the interacting actor.

        #endregion

        #region Unity Lifecycle

        private void Start()
        {
            // Initializes the interaction input method.
            _interactionInput = GetInput();
        }

        private void Update()
        {
            // Updates the RectTransform based on input progress.
            SM.Instance<ProgressInteraction>().UpdateProgressRectTransform(_interactionInput);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Executes logic if the player enters the trigger area.
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                HandleInteraction(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Hides prompt if the player exits the trigger.
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                HandleDeselection(other);
            }
        }

        #endregion

        #region Interaction Prompt

        /// <summary>
        /// Displays the interaction prompt to the player.
        /// </summary>
        public void ShowPrompt()
        {
            InteractionInfo info = new InteractionInfo(interactionType, nameText, GetInput(), OnInteractionInput);
            SM.Instance<InteractionPromptController>().ShowPrompt(info);
        }

        /// <summary>
        /// Hides the interaction prompt from the player.
        /// </summary>
        public void HidePrompt()
        {
            if (SM.HasSingleton<InteractionPromptController>())
            {
                SM.Instance<InteractionPromptController>().HidePrompt();
            }
        }

        /// <summary>
        /// Gets the appropriate input method based on control type.
        /// </summary>
        /// <returns>An instance of <see cref="IInteractionInput"/> for handling input.</returns>
        private IInteractionInput GetInput()
        {
            return isMobileControl ? new MobileMenuInput() : new KeyboardInput(keyCode);
        }

        #endregion

        #region Interaction Events

        /// <summary>
        /// Invokes the interaction input event.
        /// </summary>
        private void OnInteractionInput()
        {
            Event_OnInteractionInput.Invoke();
            UseCurrentSelection();
        }

        /// <summary>
        /// Uses the current selection if a usable object and actor are present.
        /// </summary>
        public void UseCurrentSelection()
        {
            if (usable != null && actorTransform != null)
            {
                usable.OnUseUsable();
                usable.gameObject.BroadcastMessage("OnUse", actorTransform, SendMessageOptions.DontRequireReceiver);
            }
        }

        /// <summary>
        /// Called when a usable object is selected.
        /// </summary>
        protected void OnSelectedUsableObject()
        {
            usable?.OnSelectUsable();
        }

        /// <summary>
        /// Called when a usable object is deselected.
        /// </summary>
        protected void OnDeselectedUsableObject()
        {
            usable?.OnDeselectUsable();
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Handles interaction logic when the player enters the trigger.
        /// </summary>
        /// <param name="other">The collider of the object entering the trigger.</param>
        private void HandleInteraction(Collider2D other)
        {
            // Check if interaction is allowed based on conditions.
            if (CanInteract(other))
            {
                ShowPrompt();
                OnSelectedUsableObject();
                actorTransform = other.transform;
            }
        }

        /// <summary>
        /// Handles deselection logic when the player exits the trigger.
        /// </summary>
        /// <param name="other">The collider of the object exiting the trigger.</param>
        private void HandleDeselection(Collider2D other)
        {
            // Check if deselection is allowed based on conditions.
            if (CanInteract(other))
            {
                HidePrompt();
                OnDeselectedUsableObject();
                actorTransform = null;
            }
        }

        /// <summary>
        /// Determines if interaction is allowed based on the trigger conditions.
        /// </summary>
        /// <param name="other">The collider of the object to check.</param>
        /// <returns>True if interaction is allowed; otherwise, false.</returns>
        private bool CanInteract(Collider2D other)
        {
            return !respondOnlyTrigger || (respondOnlyTrigger && other.isTrigger);
        }

        #endregion
    }
}