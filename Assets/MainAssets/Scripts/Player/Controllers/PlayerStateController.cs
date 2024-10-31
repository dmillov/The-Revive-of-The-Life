using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using SGS29.Utilities;

namespace cdvproject.Player
{
    /// <summary>
    /// Manages the player's state and notifies registered listeners of any state changes.
    /// Allows for integration with Lua scripts through registration of Freeze and UnFreeze methods.
    /// </summary>
    public class PlayerStateController : MonoSingleton<PlayerStateController>
    {
        #region Fields

        private PlayerState currentState;        // The current state of the player
        private PlayerState lastState;           // The last non-freeze state of the player
        private List<IPlayerStateListener> listeners = new List<IPlayerStateListener>(); // List of registered listeners

        #endregion

        #region State Management

        /// <summary>
        /// Changes the player's state and notifies listeners.
        /// </summary>
        /// <param name="newState">The new state to transition to.</param>
        public void ChangeState(PlayerState newState)
        {
            // If the state hasn't changed or if the current state is Freeze, exit method
            if (currentState == newState || currentState == PlayerState.Freeze) return;

            // Update last non-freeze state for restoration after unfreeze
            if (newState != PlayerState.Freeze)
            {
                lastState = newState;
            }

            // Update current state
            currentState = newState;

            // Notify all listeners of the state change
            NotifyListeners(currentState);
        }

        /// <summary>
        /// Returns the current state of the player.
        /// </summary>
        /// <returns>The current player state.</returns>
        public PlayerState GetCurrentState()
        {
            return currentState;
        }

        /// <summary>
        /// Freezes the player state.
        /// </summary>
        public void Freeze()
        {
            ChangeState(PlayerState.Freeze);
        }

        /// <summary>
        /// Unfreezes the player, restoring the last non-freeze state.
        /// </summary>
        public void UnFreeze()
        {
            currentState = lastState;
            NotifyListeners(currentState);
        }

        #endregion

        #region Listener Management

        /// <summary>
        /// Registers a new listener for state changes.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(IPlayerStateListener listener)
        {
            if (!listeners.Contains(listener))
            {
                listeners.Add(listener);
            }
        }

        /// <summary>
        /// Unregisters a listener from state changes.
        /// </summary>
        /// <param name="listener">The listener to remove.</param>
        public void UnregisterListener(IPlayerStateListener listener)
        {
            if (listeners.Contains(listener))
            {
                listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Notifies all registered listeners of a state change.
        /// </summary>
        /// <param name="newState">The new state to notify listeners of.</param>
        private void NotifyListeners(PlayerState newState)
        {
            foreach (var listener in listeners)
            {
                listener.OnPlayerStateChanged(newState);
            }
        }

        #endregion

        #region Unity Event Methods

        private void OnEnable()
        {
            OnRegister();
        }

        private void OnDisable()
        {
            OnUnregister();
        }

        #endregion

        #region Lua Registration

        /// <summary>
        /// Registers Lua functions for Freeze and UnFreeze to allow external Lua scripts to control player state.
        /// </summary>
        private void OnRegister()
        {
            Lua.RegisterFunction("Freeze", this, SymbolExtensions.GetMethodInfo(() => Freeze()));
            Lua.RegisterFunction("UnFreeze", this, SymbolExtensions.GetMethodInfo(() => UnFreeze()));
        }

        /// <summary>
        /// Unregisters the Lua functions upon disabling.
        /// </summary>
        private void OnUnregister()
        {
            Lua.UnregisterFunction("Freeze");
            Lua.UnregisterFunction("UnFreeze");
        }

        #endregion
    }
}