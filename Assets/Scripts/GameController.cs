using System;
using Scriptables;
using Scriptables.Tracks;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;


public class GameController : SingletonMonoBehaviour<GameController>
{
    // GameStatus
    private bool _gamePaused = false;

    private int _score = 0;
    public int Score => _score;

    // Events
    public delegate void PlayPauseEvent();

    public PlayPauseEvent OnPauseEvent;
    public PlayPauseEvent OnResumeEvent;

    public delegate void EndStartEvent();
    public EndStartEvent OnStartEvent;
    public EndStartEvent OnEndEvent;
    public EndStartEvent OnInitEvent;

    public delegate void TrackEvent(string trackSlug);
    public TrackEvent SetTrackEvent;

    public delegate void DifficultyEvent(string difficultySlug);
    public DifficultyEvent SetDifficultyEvent;

    public delegate void ScoreEvent(int score);
    public ScoreEvent SetScoreEvent;

    void Awake()
    {
        OnInitEvent?.Invoke();
        InputController.Instance.OnUserAction += HandleUserAction;
    }

    void Start()
    {
        Track defaultTrack = TrackList.GetDefaultTrack(TrackController.Instance.trackList.tracks);
        if (defaultTrack != null)
            SetTrackEvent?.Invoke(defaultTrack.slug);
        TrackController.Instance.OnTrackEnd += HandleTrackEnd;

        Difficulty defaultDifficulty = Difficulties.GetDefaultDifficulty(DifficultyController.Instance.difficultyList.difficulties);
        if (defaultDifficulty != null)
            SetDifficultyEvent?.Invoke(defaultDifficulty.slug);
    }

    void HandleTrackEnd(Track track)
    {
        TogglePause();
        OnEndEvent?.Invoke();
    }

    void HandleUserAction(UserActions action)
    {
        if (action == UserActions.TogglePause)
            this.TogglePause();
    }

    public void TogglePause()
    {
        if (_gamePaused)
            OnResumeEvent?.Invoke();
        else
            OnPauseEvent?.Invoke();
        _gamePaused = !_gamePaused;
    }

    public void LoadMainMenu()
    {
        SceneController.Instance.LoadStartScene();
    }
    
    public void AddScore(int amount)
    {
        _score += amount;
        SetScoreEvent?.Invoke(_score);
    }
}