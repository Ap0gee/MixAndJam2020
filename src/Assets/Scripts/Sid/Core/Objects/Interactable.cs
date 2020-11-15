using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GameJam.Managers;
using UnityEngine.EventSystems;

namespace GameJam {

    [DisallowMultipleComponent]
    public class Interactable : MonoBehaviour, IFocusable
    {
        protected MenuInteractionController m_menuInteraction;

        [SerializeField]
        private List<ActionModel> m_FocusActionModels = new List<ActionModel>();

        public virtual ActionModel[] FocusActionModels
        {
            get { return m_FocusActionModels.ToArray(); }
        }

        public virtual void Awake()
        {
            m_menuInteraction = UIManager.UI.MenusController.MenuInteraction;
        }

        public virtual void Focus()
        {
            m_menuInteraction.RequestActionsToLayoutGroup(FocusActionModels, ActionGroups.Primary);
            m_menuInteraction.Show();
        }

        public virtual void UnFocus()
        {
            m_menuInteraction.Hide();
            m_menuInteraction.RequestActionsToLayoutGroup(FocusActionModels, ActionGroups.Inactive);
        }
    }
}