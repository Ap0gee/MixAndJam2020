using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerPlayer : MonoBehaviour
{
    public float PlayerSpeed = 1;
    protected Rigidbody rigidbody= null;

    // Start is called before the first frame update
    void Start()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float axisVertical = Input.GetAxis("Vertical");
        float axisHorizontal = Input.GetAxis("Horizontal");

        Debug.Log("Vertical " + axisVertical + " Horizontal: " + axisHorizontal);


        this.transform.SetPositionAndRotation(
            new Vector3(
                this.transform.position.x + this.PlayerSpeed * axisHorizontal * Time.deltaTime ,
                this.transform.position.y ,
                this.transform.position.z + this.PlayerSpeed * axisVertical * Time.deltaTime
            ),
            Quaternion.identity
        );
    }
}
