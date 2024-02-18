using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    // weights of behaviours
    public float[] weights;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if (weights.Length != behaviours.Length) // handle data mismatch
        {
            Debug.LogError("Data mismatch in " + name, this);

            return Vector2.zero; // just don't move if there's a mismatch
        }

        // set up move
        Vector2 move = Vector2.zero;

        // iterate through behaviours
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

            // ensure partial move is limited to extent of the weight
            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    // set to max weight if exceeds
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
