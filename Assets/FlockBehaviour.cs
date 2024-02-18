using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we wil never instantiate flock behaviour, but will instantiate things like avoidance and cohesion
public abstract class FlockBehaviour : ScriptableObject
{
    // transform context for "what neighbors are around me"
    // the reason why we use transforms and not other agents is because we might avoid things like obstacles or boundaries
    public abstract Vector2 CalculateMove (FlockAgent agent, List<Transform> context, Flock flock);
}
