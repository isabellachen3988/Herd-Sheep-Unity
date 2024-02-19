using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : DoubleFilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return no adjustment
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // add all points together and average
        Vector2 alignmentMove = Vector2.zero;
        int nAlign = 0;
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareCohesionAlignmentRadius)
            {
                nAlign++;
                alignmentMove += (Vector2)item.transform.up * flock.StaticMovementMultiplier; // whichever way the "up" is pointing
            }
        }
        // average it out again (since we want middle point between all the neighbors)
        if (nAlign > 0) alignmentMove /= nAlign;

        List<Transform> filteredThreatContext = (filter == null) ? context : filter2.Filter(agent, context);
        var sigmoid = 1f;
        if (filteredThreatContext.Count > 0)
        {
            sigmoid = (float)(1 + (1 / Math.PI * Math.Atan((flock.SquareNeighborRadius - Vector2.Distance(agent.transform.position, filteredThreatContext[0].position)) / flock.mSteepness) + 0.5) * flock.AlignEmotionWeight);
        }
        alignmentMove *= sigmoid;

        return alignmentMove;
    }
}
