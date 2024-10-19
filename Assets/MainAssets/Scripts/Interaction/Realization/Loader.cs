using UnityEngine;

public class Loader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneLoader.LoadScene(sceneName);
    }
}
