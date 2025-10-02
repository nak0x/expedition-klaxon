using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Scriptables.Tracks
{
    [CreateAssetMenu(fileName = "trackList", menuName = "Tracks/Track List", order = 1)]
    public class TrackList : ScriptableObject
    {
        public List<Track> tracks;
        
        [CanBeNull]
        public static Track FindTrackBySlug(List<Track> tracks, string slug)
        {
            foreach (var track in tracks)
            {
                if (track.slug == slug) return track;
            }
            return null;
        }

        [CanBeNull]
        public static Track GetDefaultTrack(List<Track> tracks)
        {
            foreach (var track in tracks)
                if (track.isDefault) return track;
            return null;
        }
    }
}