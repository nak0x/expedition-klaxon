using System;
using System.Collections;
using System.Collections.Generic;
using Scriptables.Tracks;
using UnityEngine;
using Utils;

public class CityController : SingletonMonoBehaviour<CityController>
{
    [SerializeField] private float speed;
    [SerializeField] private float globalSpeedFactor = 3f;
    [SerializeField] private float maxSpeed = 2f;
    
    public delegate void SpeedEvent(float speed);
    public SpeedEvent OnSpeedChange;
    
    void Awake()
    {
        TrackController.Instance.OnTrackSetEvent += SetupFromTrack;
        Track currentTrack = TrackController.Instance.currentTrack;
        this.SetupFromTrack(currentTrack);
    }
    void SetupFromTrack(Track track)
    {
        float bmpRowSpeed = DifficultyController.Instance.currentDifficulty.bpmToSpeed.Evaluate(track.bpm);
        this.speed = bmpRowSpeed * globalSpeedFactor;
        OnSpeedChange?.Invoke(this.CurrentSpeed());
    }

    public float CurrentSpeed()
    {
        Debug.Log("City current speed : "  + this.speed);
        return Mathf.Min(speed, maxSpeed);
    }
}
