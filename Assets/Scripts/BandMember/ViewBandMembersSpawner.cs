using UnityEngine;

public class ViewBandMembersSpawner : BandSpawner
{
    [SerializeField] private Vector2 spawnArea = new(9.5f, 5.1f);
    [SerializeField] protected Vector3 scale = new(0.5f, 0.5f, 0.5f);


    protected override void SpawnMember(BandMemberInstance data, int index)
    {
        Vector3 randomPos = new(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
        GameObject spawnedBandMember = Instantiate(bandMemberPrefab, randomPos, Quaternion.identity);

        spawnedBandMember.GetComponent<BandMember>().PopulateBandMemberInstance(data);
        spawnedBandMember.transform.localScale = scale;

        // Add ambulation
        Ambulate amb = spawnedBandMember.AddComponent<Ambulate>();
        amb.speed = Random.Range(1f, 4f);
        amb.SetupBoundaries(spawnArea);
    }
}