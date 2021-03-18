using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] levels;
    public Transform player;

    public float zSpawn = 0;
    public float levelLength = 1000;

    public int numberOfLevelsShown = 3;

    List<GameObject> activeLevels = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfLevelsShown; i++)
        {
            SpawnLevel(Random.Range(0, levels.Length));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z - 1050 > zSpawn - (numberOfLevelsShown * levelLength))
        {
            SpawnLevel(Random.Range(0, levels.Length));
            Deletelevel();
        }
    }

    void SpawnLevel(int levelIndex)
    {
        GameObject go = Instantiate(levels[levelIndex], transform.forward * zSpawn, transform.rotation);

        activeLevels.Add(go);

        zSpawn += levelLength;
    }

    void Deletelevel()
    {
        Destroy(activeLevels[0]);
        activeLevels.RemoveAt(0);
    }
}
