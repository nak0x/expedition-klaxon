using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class SceneController: SingletonMonoBehaviour<SceneController>
{

    void Awake()
    {
        GameController.Instance.OnStartEvent += LoadWorld;
    }

    void LoadWorld()
    {
        Debug.Log("Loading world");
        SceneManager.LoadScene("Scenes/PlayScene", LoadSceneMode.Additive);
    }
}