using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Baddie baddieToSpawn;
    [SerializeField]
    private float spawnAreaWidth, spawnTime, spawnSpeed;
    List<GameObject> spawnedBaddies = new List<GameObject>();
    public Transform baddiesParents;
    public IEnumerator StartSpawning()
    {

        spawnTime = Random.Range(1f, 3f);
        //spawn enemy
        SpawnBaddie();
        yield return new WaitForSeconds(spawnTime);
        //spawn enemy
        StartCoroutine(StartSpawning());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    Vector3 GetRandomPosition()
    {
        float randX = Random.Range(-spawnAreaWidth, spawnAreaWidth);
        return new Vector3(randX, transform.position.y, transform.position.z);
    }
    void SpawnBaddie()
    {

        spawnSpeed = Random.Range(1f, 3f);
        Baddie baddieInstance = Instantiate(baddieToSpawn, GetRandomPosition(), Quaternion.identity);
        baddieInstance.transform.parent = baddiesParents;
        baddieInstance.AssignTarget(BaseGameManager.instance.GetRandomBase(), spawnSpeed);
        spawnedBaddies.Add(baddieInstance.gameObject);
    }
    public void RemoveAllBaddies()
    {
        foreach (GameObject baddie in spawnedBaddies)
        {
            Destroy(baddie);
        }
    }
}
