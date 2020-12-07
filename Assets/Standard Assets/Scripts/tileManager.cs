using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{
    public GameObject[] prefabs;
    public float zSpawn = 0;
    public float tileLength = 30;
    public int numberOfTiles = 5;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List <GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, prefabs.Length));
            }
        }
    }

    // Update is called once per frames
    void Update()
    {
             if ( playerTransform.position.z -35> zSpawn - (numberOfTiles * tileLength)) // player.z > 0 - (5*30) --> player.z > -150
           {
            SpawnTile(Random.Range(0, prefabs.Length));
            DeleteTile();

           }
    }

    public void SpawnTile(int tileIndex)
    {
        // re-learn (list) in C# tutorial NOW !
       GameObject go = Instantiate(prefabs[tileIndex], transform.forward * zSpawn, transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
