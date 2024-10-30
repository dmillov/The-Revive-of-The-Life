using SGS29.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using cdvproject.PromptInteraction.Text;

namespace cdvproject.PromptInteraction
{
    /// <summary>
    /// Class for displaying and handling interaction prompts in the game.
    /// Inherits from a singleton base class to ensure only one instance exists.
    /// </summary>
    public class InteractionPromptController : MonoSingleton<InteractionPromptController>
    {
        [SerializeField] private GameObject promptPanel; // Panel for the interaction prompt
        [SerializeField] private TextMeshProUGUI promptText; // Text component for the prompt
        [SerializeField] private Button mobileButton; // Button for mobile interface

        private InteractionInfo _currentMenuInfo; // Holds the current interaction information

        private void Start()
        {
            HidePrompt(); // Hide the prompt at the start
        }

        /// <summary>
        /// Displays the interaction prompt with the specified interaction information.
        /// </summary>
        /// <param name="menuInfo">The interaction information to display.</param>
        public void ShowPrompt(InteractionInfo menuInfo)
        {
            _currentMenuInfo = menuInfo;
            promptText.text = GetFormatText(menuInfo);
            promptPanel.SetActive(true); // Show the prompt panel

            // If the interaction input is from MobileMenuInput, initialize it with the mobile button
            if (menuInfo.InteractionInput is MobileMenuInput mobileInput)
            {
                mobileButton.interactable = true; // Enable the mobile button
                mobileInput.Initialize(mobileButton); // Initialize mobile input
            }
        }

        /// <summary>
        /// Formats the text for the interaction prompt based on the interaction type.
        /// </summary>
        /// <param name="menuInfo">The interaction information.</param>
        /// <returns>Formatted prompt text.</returns>
        private string GetFormatText(InteractionInfo menuInfo)
        {
            string formatText = "";

            switch (menuInfo.InteractionType)
            {
                case InteractionType.ZoneTransition:
                    formatText = PromptFormatter.GetZoneTransitionText(menuInfo);
                    break;

                case InteractionType.ItemPickup:
                    formatText = PromptFormatter.GetItemPickupText(menuInfo);
                    break;

                default:
                    formatText = $"Press to interact with “{menuInfo.NameText}”";
                    break;
            }

            return formatText;
        }

        /// <summary>
        /// Hides the interaction prompt panel.
        /// </summary>
        public void HidePrompt()
        {
            _currentMenuInfo = null;
            promptPanel.SetActive(false); // Hide the prompt panel
            mobileButton.interactable = false; // Disable the mobile button
        }

        /// <summary>
        /// Checks for user input and triggers the interaction if received. 
        /// </summary>
        private void Update()
        {
            // Перевірка на наявність `_currentMenuInfo` перед викликом `IsInputReceived`
            if (_currentMenuInfo != null && _currentMenuInfo.InteractionInput != null && _currentMenuInfo.InteractionInput.IsInputReceived())
            {
                _currentMenuInfo.OnInteraction?.Invoke(); // Викликає callback для взаємодії
                HidePrompt(); // Ховає prompt після виконання дії
                _currentMenuInfo = null; // Скидає поточну інформацію про меню
            }
        }
    }
}