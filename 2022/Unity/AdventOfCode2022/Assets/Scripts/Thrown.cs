using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrown : MonoBehaviour
{

    public Vector3 start;
    public Vector3 target;

    private Vector3 previousTarget;
    
    // https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html

    // Movement speed in units per second.
    public float speed = 3.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public bool moving = false;

    void BeginJourney() {
        start = transform.position;

        moving = true;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(start, target);
    }

    void Start()
    {
        if (transform.position != target) {
            BeginJourney();
        } else {
            previousTarget = target;
        }
    }

    // Move to the target end position.
    void Update()
    {
        if (moving) {
            if (previousTarget != target) {
                BeginJourney();
            }

            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            if (journeyLength != 0) {
                transform.position = Vector3.Lerp(start, target, fractionOfJourney);
            }

            if ((transform.position - target).magnitude < 0.001) {
                transform.position = target;
                moving = false;
            }
        } else if (transform.position != target) {
            BeginJourney();
        }
        previousTarget = target;
    }
}
