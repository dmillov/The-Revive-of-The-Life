using UnityEngine;

public class TriggerLoaderScene : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    public void LoadScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GameConst.PLAYER_TAG))
        {
            LoadScene(targetSceneName);
        }
    }
}
