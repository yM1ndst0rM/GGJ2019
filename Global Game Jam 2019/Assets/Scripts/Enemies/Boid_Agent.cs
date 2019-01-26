using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid_Agent : MonoBehaviour
{

    private Boid_Manager manager;

    private float      mass                = 1f;
    private float      drag                = 1.0f;
    private Vector3    avoidance           = Vector3.zero;
    public  float      avoidanceWeight     = 1f;
    private Vector3    cohesion            = Vector3.zero;
    public  float      cohesionWeight      = 1f;
    private Vector3    alignment           = Vector3.zero;
    public  float      alignmentWeight     = 1f;

    private Vector3    chase               = Vector3.zero;
    public  float      chaseWeight         = 1f;

    private Vector3    runaway             = Vector3.zero;
    public  float      runawayWeight       = 1f;


    private Vector3    resultingForce      = Vector3.zero;
    private Vector3    currentVelocity     = Vector3.zero;
    public float       maximumForce        = 1;
    public float       runawayForce        = 5;
    private Vector2Int currentGridIdx      = Vector2Int.zero;

    public LifeForceController lfc;
    public float dps                       = 0.005f;
    public float rotationSpeed             = 10.00f;
    public bool runningAway                = false;

    private EnemyAnimationController eac;

    void Awake()
    {
        lfc    = GameObject.FindObjectOfType<LifeForceController>();
        eac    = GetComponent<EnemyAnimationController>();
    }
    
    

    void Start()
    {
       Boid_Manager[] managers = GameObject.FindObjectsOfType<Boid_Manager>();
        if (managers.Length > 1 || managers.Length == 0)
            Debug.LogError("There HAS to be exactly ONE BoidManager in the scene!");
        else
            manager = managers[0];

        currentGridIdx  = manager.sortBoidIntoGrid(this);
    }

    void constrainForcesUpAxis()
    {
        avoidance.y = 0;
        alignment.y = 0;
        cohesion.y  = 0;
        chase.y     = 0;
        runaway.y   = 0;
    }

    public Vector2Int getCurrentGridIdx()
    {
        return currentGridIdx;
    }

    void updateBehaviourVectors()
    {
        Vector3 aggregatedAvoidance = Vector3.zero;
        Vector3 aggregatedCohesion  = Vector3.zero;
        chase                       = Vector3.zero;
        runaway                     = Vector3.zero;

        Vector3 tempVec;
        int neighbourCount = 0;

        currentGridIdx = manager.sortBoidIntoGrid(this);
        int x = currentGridIdx.x;
        int y = currentGridIdx.y;

        List<Boid_Agent> currentNeighbours = new List<Boid_Agent>();
        currentNeighbours.AddRange(manager.boid_Grid[x, y]);

        for(int i = -1; i <= 1; i++)
        {
            y = currentGridIdx.y + i;
            x = currentGridIdx.x + Random.Range(-1, 0);

            x = Mathf.Clamp(x, 0, manager.getGridResolution() - 1);
            y = Mathf.Clamp(y, 0, manager.getGridResolution() - 1);
            currentNeighbours.AddRange(manager.boid_Grid[x, y]);
        }

        
        


        foreach (Boid_Agent boid in currentNeighbours)
        {
            if (boid != this)
            {
                Vector3 otherBoidPos = boid.gameObject.transform.position;
                Vector3 myPos = transform.position;
                float distanceToBoid = Vector3.Distance(otherBoidPos, myPos);

                tempVec = (myPos - otherBoidPos).normalized;
                aggregatedAvoidance += tempVec * Mathf.Pow((1f / distanceToBoid), 1);
                aggregatedCohesion  -= tempVec;
                neighbourCount++;
            }
        }


        avoidance = aggregatedAvoidance;
        cohesion  = aggregatedCohesion * 1f/Mathf.Max(1f,neighbourCount);

        Vector3 towardPlayer = lfc.transform.position - transform.position;
        float distance = Vector3.Magnitude(towardPlayer);
        if (distance < lfc.LifeForce)
        {
            runningAway = true;
            runaway    += -towardPlayer * Mathf.Pow((1f / distance), 1);
        }
        else
        {
            runningAway = false;
            chase      += towardPlayer;
        }
    }
    
    void updateResultingForce()
    {
        
        resultingForce = avoidance*avoidanceWeight + cohesion*cohesionWeight + chase*chaseWeight + runaway*runawayWeight;
        float forceMagnitude = resultingForce.magnitude;
        if(!runningAway)
            forceMagnitude = Mathf.Clamp(forceMagnitude, 0, maximumForce);
        else
            forceMagnitude = Mathf.Clamp(forceMagnitude, 0, runawayForce); 

        resultingForce.Normalize();
        resultingForce *= forceMagnitude;


        //Drag / Friction
        Vector3 frictionForce = -1f * drag*currentVelocity.magnitude * currentVelocity.normalized;
        resultingForce += frictionForce;  
    }

    void updateAnimation()
    {
        eac.moveMagnitude = Mathf.Clamp(currentVelocity.magnitude, 0, 1);
    }

    void Update()
    {
        currentGridIdx = manager.sortBoidIntoGrid(this);
        updateBehaviourVectors();
        constrainForcesUpAxis();
        updateResultingForce();
        updateVelocity();
        move();
        updateAnimation();
        damagePlayer();

    }

    void damagePlayer()
    {
        float dmgRadius = 0.05f;

        float distance   = Vector3.Magnitude(lfc.transform.position - transform.position);
        if ((distance - dmgRadius) < lfc.LifeForce)
        {
            float damage = dps * Time.deltaTime;
            lfc.LifeForce = Mathf.Max(lfc.LifeForce - damage, 0f);
        }
    }

    void updateVelocity()
    {
        currentVelocity += (resultingForce / mass)*Time.deltaTime;
    }

    void move()
    {
        Vector3 translationVector = new Vector3(currentVelocity.x, 0, currentVelocity.z);
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);

        //Rotate & Animate
        Vector3 newDir = Vector3.RotateTowards(transform.forward, translationVector, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public Vector2 getAlignment()
    {
        return alignment;
    }
}
