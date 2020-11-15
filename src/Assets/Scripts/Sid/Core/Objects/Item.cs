using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField]
        private Image m_icon;

        [SerializeField]
        private ItemTypes m_type;

        public ItemTypes Type
        {
            get { return m_type; }
        }
    }
}