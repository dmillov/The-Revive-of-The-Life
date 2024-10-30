namespace cdvproject
{
    /// <summary>
    /// Enumeration for different types of interactions in the game.
    /// This enum is used to define the type of interaction that can occur
    /// in various gameplay scenarios, allowing for a clear distinction between
    /// different player actions and their corresponding responses.
    /// </summary>
    public enum InteractionType
    {
        /// <summary>
        /// Represents the action of picking up an item.
        /// This interaction type is typically triggered when the player
        /// collides with an item that can be collected, allowing the
        /// player to add it to their inventory.
        /// </summary>
        ItemPickup,

        /// <summary>
        /// Represents the action of transitioning to another zone.
        /// This interaction type is usually triggered when the player
        /// enters a designated area or interacts with an object that
        /// facilitates movement to a different location in the game world.
        /// </summary>
        ZoneTransition,
    }
}