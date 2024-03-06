using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class SpeedMeasure : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI speedText;
    float totalSpeed;
    int iteration;
    protected string type;

    void Start()
    {
        totalSpeed = 0;
        iteration = 0;
    }

    abstract public float getCurSpeed();
    // Update is called once per frame
    void Update()
    {
        var curSpeed = getCurSpeed();
        if (curSpeed > 0)
        {
            totalSpeed += curSpeed;
            iteration++;
        }

        UpdateThreatSpeed();
    }
    void UpdateThreatSpeed()
    {
        if (iteration % 100 == 0)
        {
            speedText.text = type + " Speed: " + (totalSpeed / iteration).ToString("0.00") + " px/s";
        }
    }
}
