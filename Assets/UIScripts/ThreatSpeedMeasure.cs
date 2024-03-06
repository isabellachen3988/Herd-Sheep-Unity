using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThreatSpeedMeasure : SpeedMeasure
{
    public GameObject Obstacle;
    // Start is called before the first frame update
    Vector2 lastPosition;
    public void Start()
    {
        type = "Threat";
    }
    public override float getCurSpeed()
    {
        var speed = Vector2.Distance(Obstacle.transform.position, lastPosition) / Time.deltaTime;
        lastPosition = Obstacle.transform.position;
        return speed;
    }

}
