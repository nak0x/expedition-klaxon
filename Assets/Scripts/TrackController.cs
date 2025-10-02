using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{

    public AudioClip track;
    private AudioSource _audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        GameController.Instance.OnStartEvent += StartTrack;
        GameController.Instance.OnPauseEvent += PauseTrack;
        GameController.Instance.OnResumeEvent += ResumeTrack;
    }

    private void StartTrack()
    {
        _audioSource.PlayOneShot(this.track);
    }

    private void PauseTrack()
    {
        _audioSource.Pause();
    }

    private void ResumeTrack()
    {
        _audioSource.UnPause();
    }
}
