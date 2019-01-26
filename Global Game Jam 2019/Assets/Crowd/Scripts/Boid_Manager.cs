using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Manager : MonoBehaviour
{

    public int spawnNumber = 10;
    public GameObject agentPrototype;
    private const int gridResolution = 128;
    public List<Boid_Agent>[,] boid_Grid = new List<Boid_Agent>[gridResolution, gridResolution];
    private Bounds worldBounds;
    private List<BoxCollider> spawnAreas = new List<BoxCollider>();


    Bounds computeWorldBB()
    {
        Vector3 minExtents = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        Vector3 maxExtents = new Vector3(Mathf.NegativeInfinity, Mathf.NegativeInfinity, Mathf.NegativeInfinity);

        Collider[] colliders = GameObject.FindObjectsOfType<Collider>();

        foreach (Collider c in colliders)
        {
            if (c.bounds.min.x < minExtents.x)
                minExtents.x = c.bounds.min.x;
            if (c.bounds.min.y < minExtents.y)
                minExtents.y = c.bounds.min.y;
            if (c.bounds.min.z < minExtents.z)
                minExtents.z = c.bounds.min.z;

            if (c.bounds.max.x > maxExtents.x)
                maxExtents.x = c.bounds.max.x;
            if (c.bounds.max.y > maxExtents.y)
                maxExtents.y = c.bounds.max.y;
            if (c.bounds.max.z > maxExtents.z)
                maxExtents.z = c.bounds.max.z;
        }

        Bounds result = new Bounds();
        result.SetMinMax(minExtents, maxExtents);

        Debug.Log("WorldBB is: " + minExtents.ToString() + " , " + maxExtents.ToString());

        return result;
    }

    private void gatherSpawnAreas()
    {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag("Spawn");
        for(int i = 0; i < candidates.Length; i++)
        {
            BoxCollider bc = candidates[i].GetComponent<BoxCollider>();
            if (bc != null)
                spawnAreas.Add(bc);
        }
       
    }

    public int getGridResolution()
    {
        return gridResolution;
    }

    void Awake()
    {
        worldBounds = computeWorldBB();
        gatherSpawnAreas();

        for (int i = 0; i < gridResolution; i++)
            for (int j = 0; j < gridResolution; j++)
                boid_Grid[i, j] = new List<Boid_Agent>();
    }


    void Start()
    {
        spawnBoids();
    }

    public Vector2Int sortBoidIntoGrid(Boid_Agent b)
    {
        Vector3 worldPosition = b.transform.position;
        float relativePositionX = worldPosition.x / worldBounds.size.x;
        float relativePositionZ = worldPosition.z / worldBounds.size.z;
        int gridIdxX = (int)(relativePositionX * gridResolution) + (gridResolution / 2) - 1;
        int gridIdxY = (int)(relativePositionZ * gridResolution) + (gridResolution / 2) - 1;

        //Remove old reference from grid
        boid_Grid[b.getCurrentGridIdx().x, b.getCurrentGridIdx().y].Remove(b);

        //Add new reference to grid
        boid_Grid[gridIdxX, gridIdxY].Add(b);

        return new Vector2Int(gridIdxX, gridIdxY);

        //Debug.Log("Sorted into Grid: (" + gridIdxX + "," + gridIdxY + ")");
    }

    void Update()
    {
        
    }


    void spawnBoids()
    {
        foreach (BoxCollider bc in spawnAreas)
        {
            for (int i = 0; i < spawnNumber; i++)
            {
                Vector3 randomPosition = new Vector3(Random.Range(bc.bounds.min.x, bc.bounds.max.x),
                                                     Random.Range(bc.bounds.min.y, bc.bounds.min.y),
                                                     Random.Range(bc.bounds.min.z, bc.bounds.max.z));

                Instantiate(agentPrototype, randomPosition, Quaternion.identity);
            }
        }
    }
}
