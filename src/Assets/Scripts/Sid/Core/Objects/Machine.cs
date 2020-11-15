using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Managers;
using UnityEngine.Events;

namespace GameJam
{
    [Serializable]
    public struct Requirements
    {
        public ItemTypes type;
        public int amount;
        [System.NonSerialized]
        public int held;
        [System.NonSerialized]
        public bool complete;
    }

    [Serializable]
    public struct Recipe
    {
        public ItemTypes makeType;
        public List<Requirements> requirements;
    }

    public class Machine : Interactable
    {
        private UnityEvent AddItemEvent = new UnityEvent() { };
 
        [SerializeField]
        private List<Item> m_producablePrefabs = new List<Item>();

        private List<Item> m_heldItems = new List<Item>();

        [SerializeField]
        private List<Recipe> m_recipes = new List<Recipe>();

        private Recipe m_selectedRecipe;

        private ActionModel[] m_foucsModels;

        public override void Awake()
        {
            base.Awake();
            //m_selectedRecipe = m_recipes[0];
        }

        public bool ItemIsReqired(Item item)
        {
            foreach (Requirements requirement in m_selectedRecipe.requirements)
            {
                if (requirement.type == item.Type)
                {
                    return true;
                }
            }

            return false;
        }


        public void SelectNextRecipe()
        {
            int cur_index = m_recipes.IndexOf(m_selectedRecipe);
            Recipe nextRecipe;

            try
            {
                nextRecipe = m_recipes[cur_index + 1];

                if (nextRecipe.makeType != m_selectedRecipe.makeType)
                {
                    UpdateRequirements();
                    //update menu;
                }


            }
            catch { }
        }

        public void SelectPreviousRecipe()
        {
            int cur_index = m_recipes.IndexOf(m_selectedRecipe);
            Recipe nextRecipe;

            try
            {
                nextRecipe = m_recipes[cur_index - 1];

                if (nextRecipe.makeType != m_selectedRecipe.makeType)
                {
                    UpdateRequirements();
                    //update menu;
                }
            }
            catch { }
        }

        public void UpdateRequirements()
        {
            foreach (Requirements requirement in m_selectedRecipe.requirements)
            {
                int held = 0;
                foreach (Item item in m_heldItems)
                {
                    if (item.Type == requirement.type)
                    {
                        held += 1;
                    }
                }
                int index = m_selectedRecipe.requirements.IndexOf(requirement);
                Requirements r = m_selectedRecipe.requirements[index];
                r.held = held;
                if (r.held == r.amount)
                {
                    r.complete = true;
                }
            }
        }

        public bool RecipeComplete(Recipe recipe)
        {
            foreach (Requirements requirement in m_selectedRecipe.requirements)
            {
                if (!requirement.complete)
                {
                    return false;
                }
            }

            return true;
        }

        public void AddItem(Item item)
        {
            UpdateRequirements();
            if (RecipeComplete(m_selectedRecipe))
            {
                //start making item
            }
        }

        public virtual void OnUse() {
            UpdateRequirements();
            //show Menu
        }

        public override void Focus()
        {

        }

        public override void UnFocus()
        {
            m_menuInteraction.Hide();
            m_menuInteraction.RequestActionsToLayoutGroup(m_foucsModels, ActionGroups.Inactive);
        }
    }
}