using UnityEngine;

public abstract class BandSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject bandMemberPrefab;
    private BandManager bm;
    private RecruitmentManager rm;

    void Awake()
    {
        bm = BandManager.Instance;
        rm = RecruitmentManager.Instance;
    }

    void Start()
    {
        int i = 0;
        foreach (string id in bm.bandMembers.Keys)
        {
            SpawnMember(rm.GetBandMemberInstance(id), i);
            i++;
        }
    }

    // Overridden in children
    protected abstract void SpawnMember(BandMemberInstance data, int index);
}