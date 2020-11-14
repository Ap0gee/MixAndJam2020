using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;

namespace GameJam
{
    public class ControllerPlayer : MonoBehaviour
    {
        public float PlayerSpeed = 1;
        protected Rigidbody rigidbody = null;

        // Start is called before the first frame update
        void Start()
        {
            this.rigidbody = this.GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            float axisVertical = InputManager.GetAxis("Vertical");
            float axisHorizontal = InputManager.GetAxis("Horizontal");

            this.transform.SetPositionAndRotation(
                new Vector3(
                    this.transform.position.x + this.PlayerSpeed * axisHorizontal * Time.deltaTime,
                    this.transform.position.y,
                    this.transform.position.z + this.PlayerSpeed * axisVertical * Time.deltaTime
                ),
                Quaternion.identity
            );
        }
    }
}