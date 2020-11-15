using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameJam.Managers;
using UnityEngine.Events;

namespace GameJam
{
    public class ControllerPlayer : MonoBehaviour
    {
        public float PlayerSpeed = 1;
        private float m_rotSpeed = 1000f;
        protected Rigidbody rigidbody = null;

        private Item m_heldItem;
        private float m_playerSizeY;
        public float heldItemPos;

        public UnityEvent dropItemEvent = new UnityEvent();

        public Item HeldItem
        {
            get { return m_heldItem; }
        }

        private void Awake()
        {
            m_playerSizeY = transform.GetComponent<BoxCollider>().bounds.size.y;
            dropItemEvent.AddListener(DropItem);
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
            if (axisVertical != 0 || axisHorizontal != 0)
            {
                Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                transform.position = new Vector3(
                        this.transform.position.x + this.PlayerSpeed * axisHorizontal * Time.deltaTime,
                        this.transform.position.y,
                        this.transform.position.z + this.PlayerSpeed * axisVertical * Time.deltaTime
                    );
                transform.rotation = Quaternion.LookRotation(moveDirection);
            }
           
        }

        public void PickupItem(Item item)
        {
            m_heldItem = item;

            //diable collider
            BoxCollider collider = item.GetComponent<BoxCollider>();
            collider.enabled = false;
            //transform object
            m_heldItem.OnGrabbed();
        }

        public void DropItem()
        {
            if (m_heldItem != null)
            {
                BoxCollider collider = m_heldItem.GetComponent<BoxCollider>();
                float sizeX = m_heldItem.GetComponent<Renderer>().bounds.size.x;
                m_heldItem.transform.position = transform.forward * 5f + transform.position;
                collider.enabled = true;
                m_heldItem.OnDropped();
                m_heldItem = null;
            }
        }

        public void DestroyHeldItem()
        {
            if (m_heldItem)
            {
                m_heldItem.OnDestroyed();
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