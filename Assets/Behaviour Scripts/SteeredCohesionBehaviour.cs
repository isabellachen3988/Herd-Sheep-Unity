using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehavior : DoubleFilteredFlockBehaviour
{

    Vector2 currentVelocity;
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

        List<Transform> filteredThreatContext = (filter == null) ? context : filter2.Filter(agent, context);
        
        //create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        // if the threat is nearby, then we want a strong cohesion
        // TODO - figure out the stronger cohesion problem.. or not
        // lets create the playing field next
        cohesionMove = Vector2.SmoothDamp(agent.transform.up * flock.StaticMovementMultiplier, cohesionMove, ref currentVelocity, agentSmoothTime);
        
        return cohesionMove;
    }
}