using System;

namespace cdvproject
{
    /// <summary>
    /// Class for storing information about an interaction in the menu.
    /// This class encapsulates details required for displaying interaction prompts and handling input.
    /// </summary>
    [Serializable]
    public class InteractionInfo
    {
        /// <summary>
        /// Gets the type of interaction.
        /// </summary>
        public InteractionType InteractionType { get; }

        /// <summary>
        /// Gets the name text for the interaction.
        /// </summary>
        public string NameText { get; }

        /// <summary>
        /// Gets the interaction input instance.
        /// </summary>
        public IInteractionInput InteractionInput { get; }

        /// <summary>
        /// Gets the action to be performed upon interaction.
        /// </summary>
        public Action OnInteraction { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionInfo"/> class.
        /// </summary>
        /// <param name="interactionType">The type of interaction.</param>
        /// <param name="nameText">The text to be displayed for the interaction.</param>
        /// <param name="interactionInput">The input method used for the interaction.</param>
        /// <param name="onInteraction">The action to execute when the interaction occurs.</param>
        public InteractionInfo(InteractionType interactionType, string nameText, IInteractionInput interactionInput, Action onInteraction)
        {
            InteractionType = interactionType;  // Set the interaction type
            NameText = nameText;                // Set the display text
            InteractionInput = interactionInput; // Set the interaction input
            OnInteraction = onInteraction;       // Set the interaction action
        }
    }
}