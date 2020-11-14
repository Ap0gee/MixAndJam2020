using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCamera : MonoBehaviour
{
    public float CameraHeight = 18;
    public float CameraDistance = 10;
    public Transform Target;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        this.transform.position = new Vector3(
            this.Target.position.x,
            Target.position.y + this.CameraHeight,
            Target.position.z - this.CameraDistance
        );

        this.transform.LookAt(Target);

    }
}
