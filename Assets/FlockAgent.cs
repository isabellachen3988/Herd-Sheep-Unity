using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))] // create a collider
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    //float acceleration = 0.01f;
    //float speedMultiplier = 1.0f;
    //public float SpeedMultiplier { get { return  speedMultiplier; } }

    //public void Accelerate ()
    //{
    //    speedMultiplier += acceleration;
    //}

    //public void Deaccelerate()
    //{
    //    speedMultiplier = Mathf.Max(1f, speedMultiplier - acceleration);
    //}

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>(); // find whatever is attached and will cache for reference
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        // turn agent and then move it
        // because our sprite is pointing upwards
        transform.up = velocity; 

        // ensure constant movement regardless of framerate
        // cast velocity to Vector3 since transform.position is a vector3
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
