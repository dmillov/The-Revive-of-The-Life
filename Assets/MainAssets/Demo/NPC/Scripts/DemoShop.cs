using PixelCrushers.DialogueSystem;
using DG.Tweening;
using UnityEngine;
using SGS29.Utilities;
using cdvproject.Player;

namespace cdvproject.Demo
{
    /// <summary>
    /// Manages the shop UI in the demo, providing methods to open and close the shop
    /// with animations and interacting with the PlayerStateController to freeze/unfreeze player actions.
    /// </summary>
    public class DemoShop : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private GameObject shopPanel;    // The panel representing the shop UI
        [SerializeField] private float delayToOpen = 1.0f;       // Delay before the shop opens
        [SerializeField] private float animationDuration = 0.5f; // Duration of the open/close animation

        #endregion

        #region Shop Management

        /// <summary>
        /// Opens the shop UI with an animation and freezes the player state.
        /// </summary>
        public void OpenShop()
        {
            SM.Instance<PlayerStateController>().Freeze();
            DOVirtual.DelayedCall(delayToOpen, AnimationOpenShop);
        }

        /// <summary>
        /// Closes the shop UI with an animation and unfreezes the player state.
        /// </summary>
        public void CloseShop()
        {
            SM.Instance<PlayerStateController>().UnFreeze();
            shopPanel.transform.DOScale(Vector3.zero, animationDuration).OnComplete(() => shopPanel.SetActive(false));
        }

        /// <summary>
        /// Animates the opening of the shop by scaling the panel to full size.
        /// </summary>
        private void AnimationOpenShop()
        {
            shopPanel.SetActive(true);
            shopPanel.transform.localScale = Vector3.zero; // Initial scale for opening animation
            shopPanel.transform.DOScale(Vector3.one, animationDuration); // Scale animation to open shop
        }

        #endregion

        #region Lua Registration

        void OnEnable()
        {
            Lua.RegisterFunction("OpenShop", this, SymbolExtensions.GetMethodInfo(() => OpenShop()));
        }

        void OnDisable()
        {
            Lua.UnregisterFunction("OpenShop");
        }

        #endregion
    }
}
