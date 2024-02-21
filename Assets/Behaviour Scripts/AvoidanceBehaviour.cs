using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : DoubleFilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        var SOFTNESS_FACTOR = 1;
        var SMALL_VALUE = 0.01;
        // if no neighbors, return no adjustment
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        if (filteredContext.Count == 0) return Vector2.zero;

        // add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                var movementIntensity = Mathf.Pow((float)(Vector2.Distance(agent.transform.position, item.position) / SOFTNESS_FACTOR + SMALL_VALUE), -2);
                avoidanceMove += 10 * movementIntensity * (Vector2)(agent.transform.position - item.position);
            }
        }
        if (nAvoid > 0) avoidanceMove /= nAvoid; // average it

        List<Transform> filteredThreatContext = (filter == null) ? context : filter2.Filter(agent, context);
        var sigmoid = 1f;
        if (filteredThreatContext.Count > 0)
        {
            sigmoid = (float)(1 + (1 / Math.PI * Math.Atan((flock.SquareNeighborRadius - Vector2.Distance(agent.transform.position, filteredThreatContext[0].position)) / flock.mSteepness) + 0.5) * flock.AvoidEmotionWeight);
        }
        avoidanceMove *= sigmoid;

        return avoidanceMove;
    }
}
