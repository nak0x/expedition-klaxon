using System;
using Scriptables;
using Scriptables.Tracks;
using UnityEngine;
using Utils;


public class GameController : SingletonMonoBehaviour<GameController>
{
    // GameStatus
    private bool _gamePaused = false;

    // Events
    public delegate void PlayPauseEvent();
    
    public PlayPauseEvent OnPauseEvent;
    public PlayPauseEvent OnResumeEvent;

    public delegate void EndStartEvent();
    public EndStartEvent OnStartEvent;
    public EndStartEvent OnInitEvent;
    
    public delegate void TrackEvent(string trackSlug);
    public TrackEvent SetTrackEvent;

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
    }

    void HandleUserAction(UserActions action)
    {
        if (action == UserActions.TogglePause)
            this.TogglePause();
    }

    void TogglePause()
    {
        if  (_gamePaused)
            OnResumeEvent?.Invoke();
        else
            OnPauseEvent?.Invoke();
        _gamePaused = !_gamePaused;
    }
}