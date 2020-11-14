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
    [RequireComponent(typeof(HighlightEffectController))]
    public class Interactable : MonoBehaviour, IInteractable, IFocusable
    {
        protected HighlightEffectController m_highlighEffect;
        protected MenuInteractionController m_menuInteraction;

        [SerializeField]
        private string m_name;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        [SerializeField]
        private Sprite m_icon;

        public Sprite Icon {
            get { return m_icon; }
            set { m_icon = value; }
        }

        [SerializeField]
        private List<ActionModel> m_FocusActionModels = new List<ActionModel>();

        protected virtual ActionModel[] FocusActionModels
        {
            get { return m_FocusActionModels.ToArray(); }
        }

        public virtual void Awake()
        { 
        }

        public virtual void Focus()
        {
            m_menuInteraction.Icon = Icon;
            m_menuInteraction.RequestActionsToLayoutGroup(FocusActionModels, ActionGroups.Primary);
            m_menuInteraction.Show();
        }

        public virtual void UnFocus()
        {
            m_menuInteraction.Hide(true);
            m_menuInteraction.RequestActionsToLayoutGroup(FocusActionModels, ActionGroups.Inactive);
        }
    }
}