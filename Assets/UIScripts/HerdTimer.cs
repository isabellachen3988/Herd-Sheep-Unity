using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using TMPro;
using UnityEngine;

public class HerdTimer : MonoBehaviour
{
    public GameObject BeginSpace;
    public GameObject EndSpace;
    public Flock flock;

    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime  = 0;
    bool complete = false;

    AreaCollisionScript beginSpaceScript;
    AreaCollisionScript endSpaceScript;
    // Start is called before the first frame update
    void Start()
    {
        beginSpaceScript = BeginSpace.GetComponent<AreaCollisionScript>();
        endSpaceScript = EndSpace.GetComponent<AreaCollisionScript>();
    }

    void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = elapsedTime.ToString("0.0") + "s";
    }

    // Update is called once per frame
    void Update()
    {
        if (endSpaceScript.ObjectsInBoundary.Count == flock.startingCount)
        {
            complete = true;
        }

        var justLeftStartingArea = elapsedTime == 0 && beginSpaceScript.ObjectsInBoundary.Count == 0;
        var roaming = elapsedTime > 0 && !complete;
        if (justLeftStartingArea || roaming)
        {
            UpdateTimer();
        }
    }
}
