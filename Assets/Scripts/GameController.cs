using System;
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

    void Start()
    {
        OnInitEvent?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_gamePaused)
            this.PauseGame();

        if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused)
            this.ResumeGame();
    }

    void PauseGame()
    {
        _gamePaused = true;
        OnPauseEvent?.Invoke();
    }

    void ResumeGame()
    {
        _gamePaused = false;
        OnResumeEvent?.Invoke();
    }
}
