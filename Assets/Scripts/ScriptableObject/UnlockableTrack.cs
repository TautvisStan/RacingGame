using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable Track", menuName = "Unlockables/Track")]
public class UnlockableTrack : ScriptableObject
{
    public GameObject Track;
    public GameObject TrackDisplay;
    public int price;
    public bool unlocked = false;
}
