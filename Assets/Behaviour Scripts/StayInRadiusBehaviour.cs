using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // based on position from centre, we try to get back to it
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius; // if t is 0, it is at the center, if it is > 1 then we are beyond the radius

        if (t < 0.9f)
        {
            return Vector2.zero; // we don't need any adjustment if we're within the radius
        }

        return centerOffset * t * t;
    }
}
