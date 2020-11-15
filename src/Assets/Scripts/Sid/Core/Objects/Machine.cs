using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameJam
{
    [Serializable]
    struct Requirements
    {
        public ItemTypes type;
        public float amount;
    }

    [Serializable]
    struct Recipe
    {
        public ItemTypes makeType;
        public List<Requirements> requirements;
    }

    public class Machine : Interactable, IUseable
    {
        [SerializeField]
        private List<Item> m_producablePrefabs = new List<Item>();

        [SerializeField]
        private List<Recipe> recipes = new List<Recipe>();

        public virtual void Use()
        {
            Debug.Log("using machine.");
        }
        public virtual void UnUse() { }
    }
}