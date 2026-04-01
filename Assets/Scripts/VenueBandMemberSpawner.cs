using UnityEngine;

public class VenueBandMemberSpawner : BandSpawner

{
    [SerializeField] private Transform[] stageAnchors;

    protected override void SpawnMember(BandMemberData data, int index)
    {
        if (index >= stageAnchors.Length) return;

        Transform anchor = stageAnchors[index];
        
        // Set anchor as the parent
        GameObject sbm = Instantiate(bandMemberPrefab, anchor);
        
        // Set local position/rotation to zero
        sbm.transform.localPosition = Vector3.zero;
        sbm.transform.localRotation = Quaternion.identity;
        
        sbm.GetComponent<BandMember>().PopulateBandMemberData(data);
    }
}