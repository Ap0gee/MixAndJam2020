﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameJam.Managers;
using UnityEngine.Events;

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
            m_selectedRecipe = m_recipes[0];
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

        public virtual void UnUse() {
            //show menu
        }

        public override void Focus()
        {
            Item heldItem = GameManager.Player.HeldItem;

            if (heldItem != null)
            {
                //remove current actions for player use machines
                m_menuInteraction.RequestActionsToLayoutGroup(heldItem.GrabActionModels, ActionGroups.Inactive);

                if (ItemIsReqired(heldItem))
                {
                    m_foucsModels = new ActionModel[] {
                        new ActionModel { actionType = ActionTypes.ActionConfirmAdd, callbackEvent=AddItemEvent }
                    };
                }
                else
                {
                    m_foucsModels = new ActionModel[] {
                        new ActionModel { actionType = ActionTypes.ActionNoneInvalid, callbackEvent=new UnityEvent() }
                    };
                }
             
            } else {
                m_foucsModels = FocusActionModels;
            }

            m_menuInteraction.RequestActionsToLayoutGroup(m_foucsModels, ActionGroups.Primary);
            m_menuInteraction.Show();
        }

        public override void UnFocus()
        {
            m_menuInteraction.Hide();
            m_menuInteraction.RequestActionsToLayoutGroup(m_foucsModels, ActionGroups.Inactive);

            //restore player actions
            Item heldItem = GameManager.Player.HeldItem;
            if (heldItem != null)
            {
                m_menuInteraction.RequestActionsToLayoutGroup(heldItem.GrabActionModels, ActionGroups.Primary);
            }
        }
    }
}