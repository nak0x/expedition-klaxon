using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Scriptables.Tracks;

public class SceneController: SingletonMonoBehaviour<SceneController>
{
    private bool _allowPlayScene = false;

    void Awake()
    {
        GameController.Instance.OnInitEvent += LoadStartScene;
        GameController.Instance.OnStartEvent += LoadPlayScene;
        TrackController.Instance.OnTrackSetEvent += HandleTrackSet;
    }

    void HandleTrackSet(Track track)
    {
        this._allowPlayScene = true;
    }
    
    void LoadStartScene()
    {
        SceneManager.LoadScene("Scenes/StartScene", LoadSceneMode.Additive);
    }

    void LoadPlayScene()
    {
        if (this._allowPlayScene)
        {
            SceneManager.UnloadSceneAsync("Scenes/StartScene");
            SceneManager.LoadScene("Scenes/PlayScene", LoadSceneMode.Additive);
        } else
            Debug.Log("Cannot play without a track selected");
    }
}