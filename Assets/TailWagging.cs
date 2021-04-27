using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailWagging : MonoBehaviour
{
    // Start is called before the first frame update
    Quaternion rotation;
    void Start()
    {
        rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        // Reset tail rotation
        // transform.rotation = rotation;
        // Vector3 velocity = transform.parent.GetComponent<Arrive>().boid.velocity;
        // float angle = Mathf.Sin(Mathf.Deg2Rad * 2.0f) * Mathf.Max(Mathf.Max(velocity.x, velocity.y), velocity.z) * 10;
        // transform.rotation = new Quaternion(0.0f, angle, 0.0f, 0.0f);
    }
}
