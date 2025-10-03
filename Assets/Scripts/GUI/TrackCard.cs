using Scriptables.Tracks;
using TMPro;
using UnityEngine;

public class TrackCard : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    
    private Track _track;
    public void Setup(Track track)
    {
        title.text = track.title;
        description.text = track.description;
        _track = track;
    }

    public void Select()
    {
        GameController.Instance.SetTrackEvent?.Invoke(_track.slug);
    }
}