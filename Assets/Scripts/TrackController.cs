using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Tracks;
using UnityEngine;
using Utils;

public class TrackController : SingletonMonoBehaviour<TrackController>
{
    [SerializeField]
    public TrackList trackList;
    
    private AudioSource _audioSource;
    
    public delegate void OnTrackEvent(Track track);
    public OnTrackEvent OnTrackSetEvent;

    void Awake()
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
        GameController.Instance.SetTrackEvent += SetCurrentTrack;
    }

    private void SetCurrentTrack(string trackSlug)
    {
        Debug.Log("SetCurrentTrack " +  trackSlug);
        Track track = TrackList.FindTrackBySlug(trackList.tracks, trackSlug);
        if (track == null)
        {
            Debug.Log("Cannot set track " + trackSlug);
            return;
        }
        SetTrack(track);
        OnTrackSetEvent?.Invoke(track);
    }

    private void StartTrack()
    {
        _audioSource.Play();
    }

    private void PauseTrack()
    {
        _audioSource.Pause();
    }

    private void ResumeTrack()
    {
        _audioSource.UnPause();
    }

    private void SetTrack(Track track)
    {
        _audioSource.clip = track.trackAudio;
    }
}
