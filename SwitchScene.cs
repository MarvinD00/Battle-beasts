using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public string sceneName;

    public void change()
    {
        SceneManager.LoadScene(sceneName);
    }
}
