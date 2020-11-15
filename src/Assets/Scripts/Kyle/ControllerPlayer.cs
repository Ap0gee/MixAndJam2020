using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameJam
{
    public class ControllerPlayer : MonoBehaviour
    {
        public float PlayerSpeed = 1;
        public Transform RaycastFrom;
        public float ThrowForce = 5;

        public Transform BigItemAttachment;
        public Transform SmallItemAttachment;


        public GameObject pickedUpObject;

        private void Awake()
        {
        }

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            Debug.Log(Input.GetButtonDown("Confirm"));
            if (Input.GetButtonDown("Confirm"))
            {
                if (this.pickedUpObject == null)
                {
                    int layerMask = 1 << 15;

                    Vector3 fwd = this.transform.TransformDirection(Vector3.forward);

                    RaycastHit hit;
                    Debug.DrawRay(this.RaycastFrom.position, fwd * 1f, Color.red);

                    if (Physics.Raycast(this.RaycastFrom.position, fwd, out hit, 1f, layerMask))
                    {
                        this.PickUp(hit.collider.gameObject);
                    }

                } else
                {
                    this.Throw();
                }
            }
        }

        private void LateUpdate()
        {
            float axisVertical = Input.GetAxisRaw("Vertical");
            float axisHorizontal = Input.GetAxisRaw("Horizontal");

            if (this.pickedUpObject != null)
            {
                this.UpdatePickedUpObject();
            }

            if (axisVertical != 0 || axisHorizontal != 0)
            {
                Vector3 MovePosition = new Vector3(
                    axisHorizontal,
                    0f,
                    axisVertical
                );

                transform.rotation = Quaternion.LookRotation(MovePosition);
                transform.Translate(MovePosition * PlayerSpeed * Time.deltaTime, Space.World);
            }
        }

        void Throw()
        {
            Debug.Log("Throwing");
            this.pickedUpObject.transform.parent = null;

            Rigidbody thisrb = this.GetComponent<Rigidbody>();
            Vector3 fwd = this.transform.TransformDirection(Vector3.forward);

            Vector3 ThrowDirection = new Vector3(
                fwd.x * ThrowForce + thisrb.velocity.x,
                1f,
                fwd.z * ThrowForce + thisrb.velocity.z
            );

            Rigidbody rb = this.pickedUpObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;

            rb.velocity = ThrowDirection;
            this.pickedUpObject = null;

        }

        void PickUp(GameObject go)
        {
            this.pickedUpObject = go;
            this.pickedUpObject.transform.parent = this.transform;
        }

        void UpdatePickedUpObject()
        {
            ItemScript itemScript = this.pickedUpObject.GetComponent<ItemScript>();


            if (itemScript.ItemSize == ItemSize.Big)
            {
                this.pickedUpObject.transform.position = this.BigItemAttachment.position;
            }
            else
            {
                this.pickedUpObject.transform.position = this.SmallItemAttachment.position;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Stop moving when we hit the ground
            if (collision.gameObject.layer == 16)
            {
                //this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}