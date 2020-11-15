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
        [SerializeField]
        private float m_throwForce = 500f;
        private Item m_heldItem;
        private float m_playerSizeY;
        public float heldItemPos;
        public bool throwing = false;
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
            MeshCollider collider = item.GetComponent<MeshCollider>();
            Rigidbody rb = item.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            collider.enabled = false;
            //transform object
            m_heldItem.OnGrabbed();
        }

        public void DropItem()
        {
            if (m_heldItem != null)
            {
                MeshCollider collider = m_heldItem.GetComponent<MeshCollider>();
                Rigidbody rb = m_heldItem.GetComponent<Rigidbody>();
                collider.enabled = true;
                rb.isKinematic = false;
                float sizeX = m_heldItem.GetComponent<Renderer>().bounds.size.x;
                rb.AddForce(transform.forward * m_throwForce);
               

                m_heldItem.OnDropped();
                
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
            if (m_heldItem && !throwing)
            {
                float itemSizeY = m_heldItem.GetComponent<Renderer>().bounds.size.y;
                m_heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + heldItemPos + m_heldItem.heldPosOffset, transform.position.z);
            }
        }
    }
}