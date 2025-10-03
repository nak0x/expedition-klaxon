using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Tracks;
using UnityEngine;
using Utils;

public class CityController : SingletonMonoBehaviour<CityController>
{
    [SerializeField] private float speed;
    
    public delegate void SpeedEvent(float speed);
    public SpeedEvent OnSpeedChange;
    
    void Awake()
    {
        TrackController.Instance.OnTrackSetEvent += SetupFromTrack;
        
        Track currentTrack = TrackController.Instance.currentTrack;
        this.speed = DifficultyController.Instance.currentDifficulty.bpmToSpeed.Evaluate(currentTrack.bpm);
    }
    void SetupFromTrack(Track track)
    {
        this.speed = DifficultyController.Instance.currentDifficulty.bpmToSpeed.Evaluate(track.bpm);
        OnSpeedChange?.Invoke(this.speed);
    }
    
    public float CurrentSpeed() => speed;
}
