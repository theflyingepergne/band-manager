using UnityEngine;

public abstract class BandSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject bandMemberPrefab;

    void Start()
    {
        if (BandManager.Instance == null) return;

        var members = BandManager.Instance.BandMembers;
        for (int i = 0; i < members.Count; i++)
        {
            SpawnMember(members[i], i);
        }
    }

    // This is the "Blank Space" that children must fill in
    protected abstract void SpawnMember(BandMemberData data, int index);
}