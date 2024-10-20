using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace cdvproject
{
    /// <summary>
    /// Class responsible for loading scenes with visual transitions and tracking loading progress.
    /// </summary>
    public static class SceneLoader
    {
        private static float fadeDuration = 1f;  // Duration of the fade animation
        private static bool isFading;            // Indicates if a fade is currently in progress
        private static string nextScene;         // The name of the next scene to load
        public static float LoadingProgress { get; private set; } // Loading progress value (0 to 1)
        public const bool TOUCH_TO_LOAD = false; // Determines if a touch input is required to load the scene
        public const int ARTIFICIAL_SLOWDOWN = 2000; // Artificial delay in milliseconds before loading the next scene

        /// <summary>
        /// Asynchronously loads a specified scene with fade transitions.
        /// </summary>
        /// <param name="targetScene">The name of the target scene to load.</param>
        public static async void LoadScene(string targetScene)
        {
            if (!isFading)
            {
                nextScene = targetScene;
                isFading = true;

                // Fade to black before loading
                await SceneTransitionFader.Fade(0f, 1f, fadeDuration);

                // Load the transition scene
                await LoadSceneAsync("TransitionScene");

                // Fade back in to show the loading screen
                await SceneTransitionFader.Fade(1f, 0f, fadeDuration);

                if (TOUCH_TO_LOAD)
                {
                    // Wait for any key press if touch loading is enabled
                    await WaitForAnyKeyPress();
                }
                else
                {
                    // Artificial delay before loading the next scene
                    await Task.Delay(ARTIFICIAL_SLOWDOWN);
                }

                // Fade to black for loading the target scene
                await SceneTransitionFader.Fade(0f, 1f, fadeDuration);

                // Load the target scene with progress tracking
                await LoadSceneAsync(nextScene, trackProgress: true);

                // Fade back in after loading
                await SceneTransitionFader.Fade(1f, 0f, fadeDuration);
                isFading = false;  // Fade completed
            }
        }

        /// <summary>
        /// Asynchronously loads a scene while tracking its loading progress.
        /// </summary>
        /// <param name="sceneName">The name of the scene to load.</param>
        /// <param name="trackProgress">Whether to track loading progress.</param>
        private static async Task LoadSceneAsync(string sceneName, bool trackProgress = false)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;

            while (!operation.isDone)
            {
                await Task.Yield();

                // Update the loading progress if tracking is enabled
                if (trackProgress)
                {
                    LoadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
                }

                // Allow scene activation when progress reaches 90%
                if (operation.progress >= 0.9f)
                {
                    operation.allowSceneActivation = true;
                }
            }

            // Finalize loading and reset progress
            LoadingProgress = 1f;
        }

        /// <summary>
        /// Waits for any key press from the user.
        /// </summary>
        private static async Task WaitForAnyKeyPress()
        {
            // Wait until the user presses any key
            while (!Input.anyKeyDown)
            {
                await Task.Yield();
            }
        }
    }

    /// <summary>
    /// Class responsible for handling the fade transitions of the screen.
    /// </summary>
    public static class SceneTransitionFader
    {
        private static GameObject canvasObject;
        private static Canvas fadeCanvas;    // Canvas for the fade effect
        private static Image fadePanel;      // Panel used for the fade effect

        // Static constructor for initializing the canvas and fade panel
        static SceneTransitionFader()
        {
            CreateCanvas();
        }

        /// <summary>
        /// Creates a canvas and an image for the fade effect.
        /// </summary>
        private static void CreateCanvas()
        {
            // Create the canvas for the fade effect
            canvasObject = new GameObject("FadeCanvas");
            GameObject.DontDestroyOnLoad(canvasObject);

            fadeCanvas = canvasObject.AddComponent<Canvas>();
            fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

            // Optionally add CanvasScaler and GraphicRaycaster
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();

            // Create the fade panel
            GameObject panelObject = new GameObject("FadePanel");
            fadePanel = panelObject.AddComponent<Image>();
            panelObject.transform.SetParent(canvasObject.transform, false);
            fadePanel.color = new Color(0, 0, 0, 0); // Transparent panel

            // Stretch the panel to cover the entire screen
            RectTransform rectTransform = fadePanel.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;

            fadeCanvas.gameObject.SetActive(true);  // Activate the canvas
        }

        /// <summary>
        /// Fades the panel from a start alpha to an end alpha over a specified duration.
        /// </summary>
        /// <param name="startAlpha">The starting alpha value (0 for transparent).</param>
        /// <param name="endAlpha">The ending alpha value (1 for opaque).</param>
        /// <param name="duration">The duration of the fade in seconds.</param>
        public static async Task Fade(float startAlpha, float endAlpha, float duration)
        {
            float elapsedTime = 0f;
            Color panelColor = fadePanel.color;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                panelColor.a = newAlpha;
                fadePanel.color = panelColor;
                await Task.Yield();  // Wait for the next frame
            }

            // Final update of the alpha channel
            panelColor.a = endAlpha;
            fadePanel.color = panelColor;
        }
    }
}