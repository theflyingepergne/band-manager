using UnityEngine;

public class ViewBandMembersSpawner : BandSpawner
{
    [SerializeField] private Vector2 spawnArea = new Vector2(9.5f, 5.1f);
    [SerializeField] protected Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);


    protected override void SpawnMember(BandMemberData data, int index)
    {
        Vector3 randomPos = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0); //
        GameObject sbm = Instantiate(bandMemberPrefab, randomPos, Quaternion.identity); //

        sbm.GetComponent<BandMember>().PopulateBandMemberData(data);
        sbm.transform.localScale = scale;

        // Add ambulation
        Ambulate amb = sbm.AddComponent<Ambulate>();
        amb.speed = Random.Range(1f, 4f);
        amb.SetupBoundaries(spawnArea);

    }
}