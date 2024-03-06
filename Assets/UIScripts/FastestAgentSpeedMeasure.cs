using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastestAgentSpeedMeasure : SpeedMeasure
{
    public Flock flock;
    List<Vector2> lastPositions;
    public override float getCurSpeed()
    {
        float largestDistance = 0;
        for (int i = 0; i < flock.AgentPositionVec.Count; i++)
        {
            var lastAgentPosition = Vector2.zero;
            if (lastPositions != null && lastPositions.Count == flock.AgentPositionVec.Count)
            {
                lastAgentPosition = lastPositions[i];
            }

            var movementDistance = Vector2.Distance(lastAgentPosition, flock.AgentPositionVec[i]);
            if (movementDistance > largestDistance)
            {
                largestDistance = movementDistance;
            }
        }

        lastPositions = flock.AgentPositionVec;
        return largestDistance / Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        type = "Fastest Agent";
    }
}
