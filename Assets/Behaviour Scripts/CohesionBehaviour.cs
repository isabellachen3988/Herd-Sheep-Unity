using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehavior : DoubleFilteredFlockBehaviour
{
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return no adjustment
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        
        if (filteredContext.Count == 0) return Vector2.zero;

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        var nCohere = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareCohesionAlignmentRadius)
            {
                nCohere++;
                cohesionMove += (Vector2)item.position;
            }
        }
        // average it out again (since we want middle point between all the neighbors)
        if (nCohere > 0) cohesionMove /= nCohere;
        
        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        // if a threat is nearby, append the sigmoid
        List<Transform> filteredThreatContext = (filter == null) ? context : filter2.Filter(agent, context);
        var sigmoid = 1f;
        if (filteredThreatContext.Count > 0)
        {
            sigmoid = (float)(1 + (1 / Math.PI * Math.Atan((flock.SquareNeighborRadius - Vector2.Distance(agent.transform.position, filteredThreatContext[0].position)) / flock.mSteepness) + 0.5) * flock.CohereEmotionWeight);
        }
        cohesionMove *= sigmoid;

        return cohesionMove;
    }
}