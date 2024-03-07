using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdSpeedMeasure : SpeedMeasure
{
    public Flock flock;
    List<Vector2> lastPositions;

    public override float getCurSpeed()
    {
        if (flock.AgentPositionVec.Count == 0) return 0;
        float distanceVectorsTotal = 0;
        var validMoveVec = 0;
        for (int i = 0; i < flock.AgentPositionVec.Count; i++)
        {
            var lastAgentPosition = Vector2.zero;
            if (lastPositions != null) lastAgentPosition = lastPositions[i];
            var agentMovement = Vector2.Distance(flock.AgentPositionVec[i], lastAgentPosition);
            if (agentMovement > 0)
            {
                distanceVectorsTotal += Vector2.Distance(flock.AgentPositionVec[i], lastAgentPosition);
            }
            validMoveVec++;
        }

        lastPositions = flock.AgentPositionVec;
        return distanceVectorsTotal / (validMoveVec * Time.deltaTime);
    }

    // Start is called before the first frame update
    void Start()
    {
        type = "Herd";
    }
}
