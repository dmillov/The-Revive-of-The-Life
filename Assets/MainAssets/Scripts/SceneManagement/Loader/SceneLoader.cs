using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class SceneLoader
{
    private static float fadeDuration = 1f;  // Тривалість анімації
    private static bool isFading;            // Чи відбувається затемнення
    private static string nextScene;         // Наступна сцена для завантаження
    public static float LoadingProgress { get; private set; } // Прогрес завантаження

    public static async void LoadScene(string targetScene)
    {
        if (!isFading)
        {
            nextScene = targetScene;
            isFading = true;

            // Затемнення екрану перед переходом
            await SceneTransitionFader.Fade(0f, 1f, fadeDuration);

            // Завантаження сцени TransitionScene
            await LoadSceneAsync("TransitionScene");

            // Повертаємося до освітлення на сцені завантаження
            await SceneTransitionFader.Fade(1f, 0f, fadeDuration);

            // Чекаємо на натискання будь-якої кнопки
            await WaitForAnyKeyPress();

            // Затемнення екрану для завантаження цільової сцени
            await SceneTransitionFader.Fade(0f, 1f, fadeDuration);

            // Завантажуємо цільову сцену з відслідковуванням прогресу
            await LoadSceneAsync(nextScene, trackProgress: true);

            // Повертаємося до освітлення після завантаження
            await SceneTransitionFader.Fade(1f, 0f, fadeDuration);
            isFading = false;  // Завершено затухання
        }
    }

    // Метод для завантаження сцени з можливістю відстеження прогресу
    private static async Task LoadSceneAsync(string sceneName, bool trackProgress = false)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            await Task.Yield();

            // Якщо потрібно показати прогрес завантаження
            if (trackProgress)
            {
                // Оновлюємо значення прогресу від 0 до 1
                LoadingProgress = Mathf.Clamp01(operation.progress / 0.9f);
            }

            // Якщо завантаження сцени досягло 90%, дозволяємо її активацію
            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }
        }

        // Завершуємо завантаження і скидаємо прогрес
        LoadingProgress = 1f;
    }

    // Метод для очікування натискання будь-якої кнопки
    private static async Task WaitForAnyKeyPress()
    {
        // Чекаємо, поки користувач натисне будь-яку клавішу
        while (!Input.anyKeyDown)
        {
            await Task.Yield();
        }
    }
}

public static class SceneTransitionFader
{
    private static GameObject canvasObject;
    private static Canvas fadeCanvas;    // Canvas для затемнення
    private static Image fadePanel;      // Панель для затемнення

    // Ініціалізація створення Canvas
    static SceneTransitionFader()
    {
        CreateCanvas();
    }

    // Метод для створення Canvas і Image
    private static void CreateCanvas()
    {
        // Створення Canvas
        canvasObject = new GameObject("FadeCanvas");
        GameObject.DontDestroyOnLoad(canvasObject);

        fadeCanvas = canvasObject.AddComponent<Canvas>();
        fadeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Додати CanvasScaler і GraphicRaycaster (опціонально)
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Створення панелі для затемнення
        GameObject panelObject = new GameObject("FadePanel");
        fadePanel = panelObject.AddComponent<Image>();
        panelObject.transform.SetParent(canvasObject.transform, false);
        fadePanel.color = new Color(0, 0, 0, 0); // Непрозора панель

        // Розтягування панелі на весь екран
        RectTransform rectTransform = fadePanel.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        fadeCanvas.gameObject.SetActive(true);  // Активувати Canvas
    }

    // Метод для зміни прозорості панелі (затемнення і повернення до освітлення)
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
            await Task.Yield();  // Затримка на один кадр
        }

        // Останнє оновлення альфа-каналу
        panelColor.a = endAlpha;
        fadePanel.color = panelColor;
    }
}
