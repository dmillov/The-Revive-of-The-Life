namespace cdvproject.PromptInteraction.Text
{
    /// <summary>
    /// The <c>PromptFormatter</c> class provides static methods to format
    /// interaction prompts for different types of game inputs (mobile or keyboard).
    /// This class is useful for dynamically generating user interaction instructions
    /// for in-game events like item pickups and zone transitions.
    /// </summary>
    public static class PromptFormatter
    {
        /// <summary>
        /// Generates a formatted text prompt for item pickup interactions.
        /// Based on the input type, the prompt will adapt to either a mobile
        /// button press or a keyboard input.
        /// </summary>
        /// <param name="menuInfo">An <see cref="InteractionInfo"/> object containing 
        /// details about the interaction, such as the input type and the item's name.</param>
        /// <returns>
        /// A string instructing the player how to pick up the item, based on their 
        /// input method (mobile or keyboard). Returns an error message if the 
        /// input type is invalid.
        /// </returns>
        /// <remarks>
        /// This method assumes <see cref="InteractionInfo.InteractionInput"/> can either 
        /// be a <see cref="MobileMenuInput"/> for mobile interactions or a 
        /// <see cref="KeyboardInput"/> for keyboard interactions. If the input is invalid, 
        /// an appropriate error message is returned.
        /// </remarks>
        public static string GetItemPickupText(InteractionInfo menuInfo)
        {
            if (menuInfo.InteractionInput is MobileMenuInput)
            {
                // For mobile inputs, display a generic instruction for button press.
                return $"Press this button to pick up the “{menuInfo.NameText}” item.";
            }
            else if (menuInfo.InteractionInput is KeyboardInput keyboardInput)
            {
                // For keyboard inputs, display the specific key required to pick up the item.
                return $"Press “{keyboardInput.GetInputAsString()}” to pick up the “{menuInfo.NameText}” item.";
            }
            return "Invalid input type for item pickup.";
        }

        /// <summary>
        /// Generates a formatted text prompt for zone transition interactions.
        /// This method adapts the prompt based on the input type, providing 
        /// instructions for either mobile or keyboard input.
        /// </summary>
        /// <param name="menuInfo">An <see cref="InteractionInfo"/> object containing 
        /// details about the interaction, such as the input type and the zone's name.</param>
        /// <returns>
        /// A string instructing the player how to transition to a new zone, based 
        /// on their input method (mobile or keyboard). Returns an error message 
        /// if the input type is invalid.
        /// </returns>
        /// <remarks>
        /// This method expects <see cref="InteractionInfo.InteractionInput"/> to either 
        /// be a <see cref="MobileMenuInput"/> or <see cref="KeyboardInput"/>.
        /// If the input is invalid or unrecognized, the method returns a generic 
        /// error message.
        /// </remarks>
        public static string GetZoneTransitionText(InteractionInfo menuInfo)
        {
            if (menuInfo.InteractionInput is MobileMenuInput)
            {
                // For mobile inputs, display a generic instruction for transitioning zones.
                return $"Press this to go to the “{menuInfo.NameText}” location.";
            }
            else if (menuInfo.InteractionInput is KeyboardInput keyboardInput)
            {
                // For keyboard inputs, display the specific key required for the zone transition.
                return $"Press “{keyboardInput.GetInputAsString()}” to go to the “{menuInfo.NameText}” location.";
            }
            return "Invalid input type for zone transition.";
        }

        /// <summary>
        /// Generates a formatted text prompt for NPC interaction.
        /// This method adapts the prompt based on the input type, providing 
        /// instructions for either mobile or keyboard input.
        /// </summary>
        /// <param name="menuInfo">An <see cref="InteractionInfo"/> object containing 
        /// details about the interaction, such as the input type and the NPC's name.</param>
        /// <returns>
        /// A string instructing the player how to interact with the NPC, based on 
        /// their input method (mobile or keyboard). Returns an error message if 
        /// the input type is invalid.
        /// </returns>
        /// <remarks>
        /// This method expects <see cref="InteractionInfo.InteractionInput"/> to either 
        /// be a <see cref="MobileMenuInput"/> or <see cref="KeyboardInput"/>.
        /// If the input is invalid or unrecognized, the method returns a generic 
        /// error message.
        /// </remarks>
        public static string GetNpcInteractionText(InteractionInfo menuInfo)
        {
            if (menuInfo.InteractionInput is MobileMenuInput)
            {
                // For mobile inputs, display a generic instruction for NPC interaction.
                return $"Press this button to talk to “{menuInfo.NameText}”.";
            }
            else if (menuInfo.InteractionInput is KeyboardInput keyboardInput)
            {
                // For keyboard inputs, display the specific key required to interact with the NPC.
                return $"Press “{keyboardInput.GetInputAsString()}” to talk to “{menuInfo.NameText}”.";
            }
            return "Invalid input type for NPC interaction.";
        }
    }
}