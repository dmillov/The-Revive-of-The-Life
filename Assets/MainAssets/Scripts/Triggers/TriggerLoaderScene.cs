using UnityEngine;

namespace cdvproject
{
    /// <summary>
    /// Class that triggers scene loading when a player enters a specified trigger area.
    /// </summary>
    public class TriggerLoaderScene : MonoBehaviour
    {
        [SerializeField] private string targetSceneName; // The name of the scene to load.

        /// <summary>
        /// Loads the specified scene.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        public void LoadScene(string sceneName)
        {
            SceneLoader.LoadScene(sceneName);
        }

        /// <summary>
        /// Called when another collider enters the trigger collider attached to this object.
        /// </summary>
        /// <param name="other">The collider that triggered the event.</param>
        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(GameConst.PLAYER_TAG))
            {
                LoadScene(targetSceneName); // Load the target scene if the player enters the trigger.
            }
        }
    }
}