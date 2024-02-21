using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AreaCollisionScript : MonoBehaviour
{
    public HashSet<string> ObjectsInBoundary;
    void Start()
    {
        ObjectsInBoundary = new HashSet<string>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // we only care about the flock
        if (collision.name.Contains("Agent"))
        {
            ObjectsInBoundary.Add(collision.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Agent"))
        {
            ObjectsInBoundary.Remove(collision.name);
        }
    }
}
