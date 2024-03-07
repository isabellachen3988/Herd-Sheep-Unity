using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

// both responsible for populating the flock with our prefabs
// and handles iterations and executing behaviours on flock agents as they are iterated through
public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();

    // put in scriptable object behaviour
    public FlockBehaviour behaviour;

    // for a slider
    [Range(1, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    // makes our agents move faster despite the little movements
    [Range(0f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.1f;

    [Range(0f, 1f)]
    public float cohesionAlignmentRadiusMultiplier = 0.5f;

    [Range(0f, 1f)]
    public float staticMovementMultiplier = 0.01f;

    [Range(-25f, -10f)]
    public float borderLeft = -17.75f;

    [Range(-25f, -10f)]
    public float borderBottom = -16f;

    [Range(10f, 20f)]
    public float borderRight = 25f;

    [Range(10f, 25f)]
    public float borderTop = 15f;

    [Range(1f, 10f)]
    public float cohereEmotionWeight = 2f;

    [Range(1f, 10f)]
    public float alignEmotionWeight = 2f;

    [Range(1f, 10f)]
    public float avoidEmotionWeight = 3f;

    [Range(1f, 3f)]
    public float mSteepness = 2f;

    // used for utility
    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    float squareCohesionAlignmentRadius;
    List<Vector2> agentPositionVec;

    public float MSteepness { get { return mSteepness; } }
    public float AvoidEmotionWeight { get { return avoidEmotionWeight; } }
    public float AlignEmotionWeight { get { return alignEmotionWeight; } }
    public float CohereEmotionWeight { get { return cohereEmotionWeight; } }
    public float SquareNeighborRadius { get { return squareNeighborRadius; } }
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    public float SquareCohesionAlignmentRadius { get { return squareCohesionAlignmentRadius; } }
    public float StaticMovementMultiplier { get { return staticMovementMultiplier; } }
    public List<Vector2> AgentPositionVec { get { return agentPositionVec; } }

    public Vector2 BottomLeftBorder { 
        get
        {
            var bottomLeftBorder = new Vector2();
            bottomLeftBorder.x = borderLeft;
            bottomLeftBorder.y = borderBottom;
            return bottomLeftBorder;
        }
    }

    public Vector2 TopRightBorder
    {
        get
        {
            var topRightBorder = new Vector2();
            topRightBorder.x = borderRight; 
            topRightBorder.y = borderTop;
            return topRightBorder;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // when we're calculating, we're just comparing current velocity with max speed
        // to do that, we need to get a vector's magnitude (which is a complex operation in a computer)
        // rather than comparing the square roots, we're just going to compare the squares against each other
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        squareCohesionAlignmentRadius = squareNeighborRadius * cohesionAlignmentRadiusMultiplier * cohesionAlignmentRadiusMultiplier;

        // instantiate flock
        for (int i = 0; i < startingCount; i++)
        {
            var spawnArea = Random.insideUnitCircle * startingCount * AgentDensity; // location of spawn - set size of circle is based on # agents, so we have a good density
            spawnArea.x += 20;
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                spawnArea, 
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), // rotation is in quaternion
                transform // parent
            );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this); // now the agent knows that it belongs to a flock

            agents.Add(newAgent);
        }
        agentPositionVec = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        agentPositionVec = new List<Vector2>();
        foreach(var agent  in agents)
        {
            agentPositionVec.Add(agent.transform.position);
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 velocity = behaviour.CalculateMove(agent, context, this);
            velocity *= driveFactor; // to get speedier movement

            // ensure max speed limit
            if (velocity.sqrMagnitude > squareMaxSpeed)
            {
                // reset to one and then multiply by maxSpeed so that it is exactly at the max speed
                velocity = velocity.normalized * maxSpeed;
            }

            agent.Move(velocity);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // creates imaginary circle in space with radius that we choose, and see what colliders are within it
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D c in contextColliders)
        {
            // saves us from running getcomponent every time
            // iterate through all the colliders that are not for the current agent
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
