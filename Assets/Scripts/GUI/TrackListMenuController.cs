using UnityEngine;
using Scriptables.Tracks;
using UnityEditor;

public class TrackListMenuController : MonoBehaviour
{
    public TrackList tracks;
    public GameObject trackCard;

    void Start()
    {
        foreach (Track track in tracks.tracks)
        {
            GameObject newCard = GameObject.Instantiate(trackCard, this.transform);
            newCard.GetComponent<TrackCard>().Setup(track);
        }
    }
}