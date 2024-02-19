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
            avoidanceMove += new Vector2(flock.BottomLeftBorder.x, agent.transform.position.y) - (Vector2)agent.transform.position;
        }

        if (agent.transform.position.y < flock.BottomLeftBorder.y)
        {
            boundaryForceCount++;
            avoidanceMove += new Vector2(agent.transform.position.x, flock.BottomLeftBorder.y) - (Vector2)agent.transform.position;
        }

        if (agent.transform.position.x > flock.TopRightBorder.x)
        {
            boundaryForceCount++;
            avoidanceMove += new Vector2(flock.TopRightBorder.x, agent.transform.position.y) - (Vector2)agent.transform.position;
        }

        if (agent.transform.position.y > flock.TopRightBorder.y)
        {
            boundaryForceCount++;
            avoidanceMove += new Vector2(agent.transform.position.x, flock.TopRightBorder.y) - (Vector2)agent.transform.position;
        }

        if (boundaryForceCount > 0) avoidanceMove /= boundaryForceCount;

        return avoidanceMove;
    }
}
