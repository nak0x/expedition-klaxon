using UnityEngine;
using Utils;


public class GameController : SingletonMonoBehaviour<GameController>
{

    public delegate void PlayPauseEvent();
    
    public PlayPauseEvent OnPauseEvent;
    public PlayPauseEvent OnResumeEvent;

    public delegate void EndStartEvent();
    public EndStartEvent OnStartEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        OnStartEvent?.Invoke();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            this.PauseGame();
    }

    void PauseGame()
    {
        OnPauseEvent?.Invoke();
    }
}
