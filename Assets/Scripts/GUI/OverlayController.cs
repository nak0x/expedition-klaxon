using Scriptables.Tracks;
using TMPro;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [SerializeField] TMP_Text scoreLabel;
    [SerializeField] TMP_Text trackLabel;
    [SerializeField] TMP_Text endTrackScore;
    
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inGameOverlay;
    [SerializeField] GameObject endGameOverlay;
    
    private bool _isPaused = false;
    void Awake()
    {
        GameController.Instance.SetScoreEvent += SetScore;
        GameController.Instance.OnPauseEvent += PerformUiPause;
        GameController.Instance.OnResumeEvent += PerformUiPause;
        GameController.Instance.OnEndEvent += PerformUiEnd;
        TrackController.Instance.OnTrackSetEvent += SetTrack;
        this.SetTrack(TrackController.Instance.currentTrack);
        this.SetScore(GameController.Instance.Score);
    }

    private void PerformUiEnd()
    {
        endTrackScore.text = GameController.Instance.Score.ToString();
        endGameOverlay.SetActive(true);
        pauseMenu.SetActive(false);
        inGameOverlay.SetActive(false);
    }

    private void PerformUiPause()
    {
        _isPaused = !_isPaused;
        pauseMenu.SetActive(_isPaused);
        inGameOverlay.SetActive(!_isPaused);
    }

    private void ToggleUiPause()
    {
        GameController.Instance?.TogglePause();
    }

    public void GoMainMenu()
    {
        GameController.Instance?.LoadMainMenu();
    }

    void SetTrack(Track track)
    {
        trackLabel.text = track.title;
    }

    void SetScore(int score)
    {
        scoreLabel.text = score.ToString();
    }
}