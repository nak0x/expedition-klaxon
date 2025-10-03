using Scriptables;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Scriptables.Tracks;

public class SceneController: SingletonMonoBehaviour<SceneController>
{
    private bool _isTrackSet = false;
    private bool _isDifficultySet = false;
    private bool _isPlaySceneLoaded = false;

    void Awake()
    {
        GameController.Instance.OnInitEvent += LoadStartScene;
        GameController.Instance.OnStartEvent += LoadPlayScene;
        
        TrackController.Instance.OnTrackSetEvent += HandleTrackSet;
        DifficultyController.Instance.OnSetDifficultyEvent += HandleDifficultySet;
    }

    void HandleTrackSet(Track track)
    {
        this._isTrackSet = true;
    }

    void HandleDifficultySet(Difficulty difficulty)
    {
        this._isDifficultySet = true;
    }

    bool IsPlaySceneAllowed()
    {
        return this._isTrackSet && this._isDifficultySet;;
    }
    
    public void LoadStartScene()
    {
        if (this._isPlaySceneLoaded)
            SceneManager.UnloadSceneAsync("Scenes/PlayScene");
        SceneManager.LoadScene("Scenes/StartScene", LoadSceneMode.Additive);
    }

    void LoadPlayScene()
    {
        if (this.IsPlaySceneAllowed())
        {
            SceneManager.UnloadSceneAsync("Scenes/StartScene");
            SceneManager.LoadScene("Scenes/PlayScene", LoadSceneMode.Additive);
            this._isPlaySceneLoaded = true;
        } else
            Debug.LogError("SceneController: Cannot load play scene");
    }
}