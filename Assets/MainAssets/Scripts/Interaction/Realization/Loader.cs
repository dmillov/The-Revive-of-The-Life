using UnityEngine;

namespace cdvproject
{
    /// <summary>
    /// Class responsible for loading scenes in Unity.
    /// </summary>
    public class Loader : MonoBehaviour
    {
        /// <summary>
        /// Loads the specified scene.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        public void LoadScene(string sceneName)
        {
            SceneLoader.LoadScene(sceneName);
        }
    }
}