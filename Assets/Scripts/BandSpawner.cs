using UnityEngine;

public abstract class BandSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject bandMemberPrefab;

    void Start()
    {
        if (BandManager.Instance == null) return;

        var members = BandManager.Instance.bandMembers;
        for (int i = 0; i < members.Count; i++)
        {
            SpawnMember(members[i], i);
        }
    }

    // Overridden in children
    protected abstract void SpawnMember(BandMemberInstance data, int index);
}