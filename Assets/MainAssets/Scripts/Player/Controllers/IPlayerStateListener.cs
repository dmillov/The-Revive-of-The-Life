namespace cdvproject.Player
{
    #region Enums and Interfaces

    /// <summary>
    /// Interface for objects that listen to player state changes.
    /// </summary>
    public interface IPlayerStateListener
    {
        /// <summary>
        /// Called when the player state changes.
        /// </summary>
        /// <param name="newState">The new player state.</param>
        void OnPlayerStateChanged(PlayerState newState);
    }

    #endregion
}