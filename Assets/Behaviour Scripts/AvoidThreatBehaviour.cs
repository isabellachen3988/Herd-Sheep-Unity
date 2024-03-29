using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoid Threat Behaviour")]
public class AvoidThreatBehaviour : FilteredFlockBehaviour
{
    public float agentSmoothTime = 1f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        var SOFTNESS_FACTOR = 1;
        var SMALL_VALUE = 0.01;
        
        // if no neighbors, return no adjustment
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        if (filteredContext.Count == 0)
        {
            return agent.transform.up * flock.StaticMovementMultiplier;
        }

        // add all points together and average
        Vector2 avoidanceMove = Vector2.zero;

        // we only have one shepherd at a time
        if (filteredContext.Count > 0 && Vector2.SqrMagnitude(filteredContext[0].position - agent.transform.position) < flock.SquareNeighborRadius)
        {
            var threat = filteredContext[0];
            var movementIntensity = Mathf.Pow((float)(Vector2.Distance(agent.transform.position, threat.position) / SOFTNESS_FACTOR + SMALL_VALUE), -2);
            avoidanceMove = 10 * movementIntensity * (Vector2)(agent.transform.position - threat.position) / Vector2.Distance(agent.transform.position, threat.position);
            agent.SetOutlinedSprite(true);
        } else
        {
            agent.SetOutlinedSprite(false);
        }

        return avoidanceMove;
    }
}
