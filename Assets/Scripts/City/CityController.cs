using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Tracks;
using UnityEngine;

public class CityController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    public delegate void SpeedEvent(float speed);
    public SpeedEvent OnSpeedChange;
    
    void Awake()
    {
        TrackController.Instance.OnTrackSetEvent += SetupFromTrack;
    }
    void SetupFromTrack(Track track)
    {
        this.speed = DifficultyController.Instance.currentDifficulty.bpmToSpeed.Evaluate(track.bpm);
        OnSpeedChange?.Invoke(this.speed);
    }
}
