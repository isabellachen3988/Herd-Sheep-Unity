using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask; // access physics layer

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach(Transform item in original)
        {
            if (mask == (mask | (1 << item.gameObject.layer)))
            {
                // item is on same layer of mask that we chose
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
