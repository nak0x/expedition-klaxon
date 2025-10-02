using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Scriptables.Tracks
{
    [CreateAssetMenu(fileName = "track", menuName = "Tracks/Track", order = 0)]
    public class Track : ScriptableObject
    {
        public string title;
        public string slug;
        public string description;
        public AudioClip trackAudio;
        public float bpm;
        public bool isDefault = false;

    }
}