using TMPro;
using UnityEngine;

public class SceneLoadingVisualizer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPressToContinue;

    void Update()
    {
        if (SceneLoader.LoadingProgress > 0.9f)
        {
            textPressToContinue.gameObject.SetActive(true);
        }
    }
}
