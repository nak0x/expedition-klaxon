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

    private float _currentSpeed;
    
    public delegate void SpeedEvent(float speed);
    public SpeedEvent OnSpeedChange;
    
    void Awake()
    {
        GameController.Instance.OnPauseEvent += StopCity;
        GameController.Instance.OnResumeEvent += ResumeCity;
        TrackController.Instance.OnTrackSetEvent += SetupFromTrack;
        Track currentTrack = TrackController.Instance.currentTrack;
        this.SetupFromTrack(currentTrack);
    }

    void StopCity()
    {
        this._currentSpeed = this.speed;
        this.speed = 0f;
        this.OnSpeedChange?.Invoke(this.speed);
        Time.timeScale = 0f;
    }

    void ResumeCity()
    {
        this.speed = this._currentSpeed;
        this._currentSpeed = 0f;
        this.OnSpeedChange?.Invoke(this.speed);
        Time.timeScale = 1f;
    }
    void SetupFromTrack(Track track)
    {
        float bmpRowSpeed = DifficultyController.Instance.currentDifficulty.bpmToSpeed.Evaluate(track.bpm);
        this.speed = bmpRowSpeed * globalSpeedFactor;
        OnSpeedChange?.Invoke(this.CurrentSpeed());
    }

    public float CurrentSpeed()
    {
        return Mathf.Min(speed, maxSpeed);
    }
}
