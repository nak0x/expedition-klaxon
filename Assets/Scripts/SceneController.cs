using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class SceneController: SingletonMonoBehaviour<SceneController>
{

    void Awake()
    {
        GameController.Instance.OnInitEvent += LoadStartScene;
        GameController.Instance.OnStartEvent += LoadPlayScene;
    }
    
    void LoadStartScene()
    {
        SceneManager.LoadScene("Scenes/StartScene", LoadSceneMode.Additive);
    }

    void LoadPlayScene()
    {
        SceneManager.UnloadSceneAsync("Scenes/StartScene");
        SceneManager.LoadScene("Scenes/PlayScene", LoadSceneMode.Additive);
    }
}