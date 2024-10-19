using SGS29.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cdvproject.PromptInteraction
{
    // Клас для відображення і обробки меню взаємодії
    public class InteractionPromptController : MonoSingleton<InteractionPromptController>
    {
        [SerializeField] private GameObject promptPanel; // Панель для підказки
        [SerializeField] private TextMeshProUGUI promptText;        // Текстова підказка на панелі
        [SerializeField] private Button mobileButton;    // Кнопка для мобільного інтерфейсу

        private InteractionInfo _currentMenuInfo;

        private void Start()
        {
            HidePrompt();
        }

        // Метод для показу підказки
        public void ShowPrompt(InteractionInfo menuInfo)
        {
            _currentMenuInfo = menuInfo;
            promptText.text = GetFormatText(menuInfo);
            promptPanel.SetActive(true);

            // Якщо вибрано MobileMenuInput, ініціалізуємо його з мобільною кнопкою
            if (menuInfo.InteractionInput is MobileMenuInput mobileInput)
            {
                mobileButton.interactable = true;
                mobileInput.Initialize(mobileButton);
            }
        }

        private string GetFormatText(InteractionInfo menuInfo)
        {
            string formatText = "";

            switch (menuInfo.InteractionType)
            {
                case InteractionType.ZoneTransition:
                    formatText = GetZoneTransitionText(menuInfo);
                    break;

                case InteractionType.ItemPickup:
                    formatText = GetItemPickupText(menuInfo);
                    break;

                default:
                    formatText = $"Press to interact with “{menuInfo.NameText}”";
                    break;
            }

            return formatText;
        }

        // Метод для форматування тексту для переходу в іншу зону
        private string GetZoneTransitionText(InteractionInfo menuInfo)
        {
            if (menuInfo.InteractionInput is MobileMenuInput)
            {
                return $"Press this to go to the “{menuInfo.NameText}” location";
            }
            else if (menuInfo.InteractionInput is KeyboardInput keyboardInput)
            {
                return $"Press “{keyboardInput.GetInputAsString()}” to go to the “{menuInfo.NameText}” location";
            }
            return "Invalid input type for zone transition";
        }

        // Метод для форматування тексту для підняття предмета
        private string GetItemPickupText(InteractionInfo menuInfo)
        {
            if (menuInfo.InteractionInput is MobileMenuInput)
            {
                return $"Press this button to pick up the “{menuInfo.NameText}” item.";
            }
            else if (menuInfo.InteractionInput is KeyboardInput keyboardInput)
            {
                return $"Press “{keyboardInput.GetInputAsString()}” to pick up the “{menuInfo.NameText}” item.";
            }
            return "Invalid input type for item pickup";
        }

        // Метод для приховування підказки
        public void HidePrompt()
        {
            promptPanel.SetActive(false);
            mobileButton.interactable = false; // Приховуємо кнопку для мобільного інтерфейсу
        }

        // Щоденна перевірка стану вводу і взаємодії
        private void Update()
        {
            if (_currentMenuInfo != null && _currentMenuInfo.InteractionInput.IsInputReceived())
            {
                _currentMenuInfo.OnInteraction?.Invoke(); // Виклик callback
                HidePrompt(); // Приховуємо підказку після виконання дії
                _currentMenuInfo = null;
            }
        }
    }
}