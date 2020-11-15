using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameJam.Managers;
using System.Linq;

namespace GameJam
{
    public enum ItemTypes
    {
        Ingot,
        Scraps,
        Wires,
        Plate,
        Gear
    }
    public class Item : Interactable
    {
        public float heldPosOffset;
        private bool m_held;

        [SerializeField]
        private Sprite m_icon;

        [SerializeField]
        private ItemTypes m_type;

        private float m_yOrigin;

        public float YOrigin
        {
            get { return m_yOrigin;  }
        }
        
        public ItemTypes Type
        {
            get { return m_type; }
        }

        private ActionModel[] m_grabModels;

        public ActionModel[] GrabActionModels
        {
            get { return m_grabModels; }
        }

        public override void Awake()
        {
            m_yOrigin = transform.position.y;

            base.Awake();
        }

        public void OnGrabbed()
        {
            m_held = true;

            ControllerPlayer player = GameManager.Player;
            m_menuInteraction.RequestActionsToLayoutGroup(m_grabModels, ActionGroups.Primary);
        }

        public void OnDropped()
        {
            m_held = false;
            m_menuInteraction.RequestActionsToLayoutGroup(m_grabModels, ActionGroups.Inactive);
            m_menuInteraction.Hide();
        }

        public void OnDestroyed()
        {

        }

        public void Update()
        {
            if (m_held)
            {
                m_menuInteraction.Show();
            }
        }
    }
}