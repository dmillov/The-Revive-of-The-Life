using cdvproject.PromptInteraction;
using SGS29.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace cdvproject.Trigger
{
    public class TriggerInteraction : MonoBehaviour
    {
        [SerializeField] private InteractionType interactionType;
        [SerializeField] private UnityEvent Event_OnInteractionInput;
        [SerializeField] private KeyCode keyCode;
        [SerializeField] private bool isMobileControl;
        [SerializeField] private string nameText;

        public void ShowPrompt()
        {
            InteractionInfo info = new InteractionInfo(interactionType, nameText, GetInput(), OnInteractionInput);
            SM.Instance<InteractionPromptController>().ShowPrompt(info);
        }

        public void HidePrompt()
        {
            SM.Instance<InteractionPromptController>().HidePrompt();
        }

        private IInteractionInput GetInput()
        {
            if (isMobileControl)
            {
                return new MobileMenuInput();
            }

            return new KeyboardInput(keyCode);
        }

        private void OnInteractionInput()
        {
            Event_OnInteractionInput.Invoke();
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                ShowPrompt();
            }
        }

        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                HidePrompt();
            }
        }
    }
}