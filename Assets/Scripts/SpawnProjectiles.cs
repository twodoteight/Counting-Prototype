using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    [SerializeField] List<GameObject> projectiles;

    public float spawnRangeX = 5f;
    public float spawnRangeY = 1f;
    public float spawnRangeZ = 2f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnRandomProjectile", 2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomProjectile()
    {
        float x = Random.Range(transform.position.x - spawnRangeX, transform.position.x + spawnRangeX);
        float y = Random.Range(transform.position.y - spawnRangeY, transform.position.y + spawnRangeY);
        float z = Random.Range(transform.position.z - spawnRangeZ, transform.position.z + spawnRangeZ);
        int projectileToSpawn;
        int randomNum = Random.Range(0, 101);
        
        if (randomNum < 50)
        {
            projectileToSpawn = 0;
        }
        else if (randomNum < 85)
        {
            projectileToSpawn = 1;
        }
        else
        {
            projectileToSpawn = 2;
        }
       
        Instantiate(projectiles[projectileToSpawn], new Vector3(x, y, z), projectiles[0].transform.rotation);
        
        Invoke("SpawnRandomProjectile", Random.Range(2f, 5f));
    }
}
