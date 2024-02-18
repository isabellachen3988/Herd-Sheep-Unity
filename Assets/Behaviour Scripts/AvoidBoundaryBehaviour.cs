using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoid Boundary")]
public class AvoidBoundaryBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        var boundaryForceCount = 0;
        // we ignore the context for now
        var avoidanceMove = Vector2.zero;

        if (agent.transform.position.x < flock.BottomLeftBorder.x)
        {
            boundaryForceCount++;
            var forceVector = flock.BottomLeftBorder;
            forceVector.y = agent.transform.position.y;
            avoidanceMove += forceVector - (Vector2)agent.transform.position;
        }


        if (agent.transform.position.y < flock.BottomLeftBorder.y)
        {
            boundaryForceCount++;
            var forceVector = flock.BottomLeftBorder;
            forceVector.x = agent.transform.position.x;
            avoidanceMove += forceVector - (Vector2)agent.transform.position;
        }

        if (boundaryForceCount > 0) avoidanceMove /= boundaryForceCount;

        return avoidanceMove;
    }
}
