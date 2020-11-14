using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class InteractableItem : Interactable, IUseable, IInspectable, IHoldable
    {
        public virtual void Use()
        {
            Debug.Log("Using Item");
        }

        public virtual void UnUse()
        {
            Debug.Log("UnUsing Item");
        }

        public virtual void Inspect()
        {
            Debug.Log("Inspecting Item");
        }

        public virtual void UnInspect()
        {
            Debug.Log("UnInspecting Item");
        }

        public virtual void Pickup()
        {
            Debug.Log("Picking Up Item");
        }

        public virtual void UnPickup()
        {
            Debug.Log("UnPicking Up Item");
        }

        public virtual void Place()
        {
            Debug.Log("Placing Item");
        }

        public virtual void UnPlace()
        {
            Debug.Log("UnPlace Item");
        }
    }
}