using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameJam
{
    public class Machine : Interactable, IUseable
    {
        [SerializeField]
        private List<Item> m_producablePrefabs = new List<Item>();

        public virtual void Use()
        {
            Debug.Log("using machine.");
        }
        public virtual void UnUse() { }
    }
}