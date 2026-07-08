using UnityEngine;

public class VenueBandMemberSpawner : BandSpawner

{
    [SerializeField] private Transform[] stageAnchors;

    protected override void SpawnMember(BandMemberInstance data, int index)
    {
        if (index >= stageAnchors.Length) return;

        Transform anchor = stageAnchors[index];
        
        // Set anchor as the parent
        GameObject bandMember = Instantiate(bandMemberPrefab, anchor);
        
        // Set local position/rotation to zero
        bandMember.transform.localPosition = Vector3.zero;
        bandMember.transform.localRotation = Quaternion.identity;
        
        bandMember.GetComponent<BandMember>().PopulateBandMemberInstance(data);
    }
}