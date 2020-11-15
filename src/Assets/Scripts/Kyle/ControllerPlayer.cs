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

        private Item m_heldItem;
        private float m_playerSizeY;

        public float heldItemPos;

        private void Awake()
        {
            m_playerSizeY = transform.GetComponent<BoxCollider>().bounds.size.y;
        }

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

        public void PickupItem(Item item)
        {
            m_heldItem = item;

            //diable collider
            BoxCollider collider = item.GetComponent<BoxCollider>();
            collider.enabled = false;

            //transform object
        }

        public void DestroyHeldItem()
        {
            if (m_heldItem)
            {
                Destroy(m_heldItem);
                m_heldItem = null;
            }
        }

        private void Update()
        {
            if (m_heldItem)
            {
                float itemSizeY = m_heldItem.GetComponent<Renderer>().bounds.size.y;
                m_heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + heldItemPos + m_heldItem.heldPosOffset, transform.position.z);
            }
        }
    }
}