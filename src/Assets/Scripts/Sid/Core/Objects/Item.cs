using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public class Item : MonoBehaviour
    {
        public ItemTypes Type { get; set; }
    }
}