using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Tracks;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class TrackController : SingletonMonoBehaviour<TrackController>
{
    [SerializeField]
    public TrackList trackList;
    public Track currentTrack;
    
    private AudioSource _audioSource;
    
    public delegate void OnTrackEvent(Track track);
    public OnTrackEvent OnTrackSetEvent;
    public OnTrackEvent OnTrackEnd;

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
        Track track = TrackList.FindTrackBySlug(trackList.tracks, trackSlug);
        if (track == null)
        {
            return;
        }
        SetTrack(track);
        OnTrackSetEvent?.Invoke(track);
    }

    private void StartTrack()
    {
        float length = _audioSource.clip.length;
        _audioSource.Play();
        StartCoroutine(StartMethod(length));
    }

    private IEnumerator StartMethod(float length)
    {
        yield return new WaitForSeconds(length);
        TrackEnd();
    }

    private void TrackEnd()
    {
        OnTrackEnd?.Invoke(currentTrack);
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
        this.currentTrack = track;
    }
}
