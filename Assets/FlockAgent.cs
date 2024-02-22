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

    public Transform outlinedSprite;

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>(); // find whatever is attached and will cache for reference
    }

    public void SetOutlinedSprite(bool outlined)
    {
        if (outlinedSprite == null) return;

        var spriteRenderer = outlinedSprite.GetComponent<SpriteRenderer>();
        if (outlined)
        {
            spriteRenderer.enabled = true;
        } else
        {
            spriteRenderer.enabled = false;
        }
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
