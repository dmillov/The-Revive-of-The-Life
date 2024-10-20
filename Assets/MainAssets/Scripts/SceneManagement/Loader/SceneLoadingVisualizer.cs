using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cdvproject
{
    /// <summary>
    /// Class responsible for visualizing the loading progress of a scene.
    /// Updates the UI elements such as a slider and text to reflect the loading status.
    /// </summary>
    public class SceneLoadingVisualizer : MonoBehaviour
    {
        [SerializeField] private Slider progressSlider;       // Slider to show loading progress
        [SerializeField] private TextMeshProUGUI progressText; // Text element to display loading percentage

        /// <summary>
        /// Updates the loading visualizations each frame.
        /// Retrieves the loading progress from the SceneLoader and updates the UI elements accordingly.
        /// </summary>
        void Update()
        {
            float progress = SceneLoader.LoadingProgress; // Get the current loading progress

            // Update the text to display the percentage of loading completed
            progressText.text = $"{progress * 100}%";
            // Update the slider value based on the loading progress
            progressSlider.value = progress;
        }
    }
}