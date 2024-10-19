using UnityEngine;
using UnityEngine.Events;

namespace cdvproject
{
    /// <summary>
    /// The Trigger2D class is used to create a 2D trigger collider that can detect when
    /// other colliders, specifically the player, enter or exit its boundaries.
    /// It utilizes Unity Events to allow for custom responses to these interactions.
    /// </summary>
    public class Trigger2D : MonoBehaviour
    {
        /// <summary>
        /// Event invoked when a collider enters the trigger area.
        /// Attach methods in the Unity Inspector to respond to this event.
        /// </summary>
        [SerializeField] private UnityEvent onTriggerEnter;

        /// <summary>
        /// Event invoked when a collider exits the trigger area.
        /// Attach methods in the Unity Inspector to respond to this event.
        /// </summary>
        [SerializeField] private UnityEvent onTriggerExit;

        /// <summary>
        /// Called when another collider enters the trigger.
        /// This method checks if the other collider has the "Player" tag,
        /// and if so, invokes the onTriggerEnter event.
        /// </summary>
        /// <param name="other">The collider that entered the trigger.</param>
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG)) // Check if the other collider has the player tag
            {
                onTriggerEnter.Invoke(); // Trigger the onEnter event
            }
        }

        /// <summary>
        /// Called when another collider exits the trigger.
        /// This method checks if the other collider has the "Player" tag,
        /// and if so, invokes the onTriggerExit event.
        /// </summary>
        /// <param name="other">The collider that exited the trigger.</param>
        public virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG)) // Check if the other collider has the player tag
            {
                onTriggerExit.Invoke(); // Trigger the onExit event
            }
        }
    }
}