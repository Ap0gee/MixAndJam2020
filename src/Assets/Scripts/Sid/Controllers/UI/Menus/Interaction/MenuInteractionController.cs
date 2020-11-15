using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Linq;
using UnityEngine.Events;

namespace GameJam
{
    public enum ActionGroups
    {
        Primary,
        Secondary,
        Inactive
    }

    [Serializable]
    public enum ActionTypes
    {
        ActionConfirmUse,
        ActionConfirmPickup,
        ActionNoneInvalid,
        ActionConfirmAdd,
        ActionConfirmDrop
    }

    [Serializable]
    public struct ActionModel
    {
        public ActionTypes actionType;
        public UnityEvent callbackEvent;
    }

    [DisallowMultipleComponent]
    public class MenuInteractionController : MonoBehaviour
    {
        [SerializeField]
        private GridLayoutGroup m_primaryActionsGroup;

        [SerializeField]
        private GridLayoutGroup m_secondaryActionsGroup;

        [SerializeField]
        private GridLayoutGroup m_inactiveActionsGroup;

        private CanvasGroup m_canvasGroup;

        [SerializeField]
        private List<ActionController> m_actionPrefabs = new List<ActionController>();

        private Dictionary<string, ActionController> m_actionsMap = new Dictionary<string, ActionController>();

        private List<ActionController> m_activeActions = new List<ActionController>();

        public ActionController[] Actions { get { return m_actionsMap.Values.ToArray(); } }

        public ActionController[] ActiveActions { get { return m_activeActions.ToArray(); } }
            
        private void Awake()
        {
            m_canvasGroup = GetComponent<CanvasGroup>();
            RegisterActions();
            Show(true);
            InitializeActions();
            Hide(true);
        }
      
        public void Show(bool instant=false)
        {
            m_canvasGroup.alpha = 1f;
        }

        public void Hide(bool instant=false)
        {
            m_canvasGroup.alpha = 0f;
        }

        private void RegisterActions()
        {
            foreach (ActionController prefab in m_actionPrefabs)
            {
                m_actionsMap.Add(prefab.name, Instantiate(prefab));
            }
        }

        private void InitializeActions()
        {
            foreach (ActionController action in Actions)
            {
                action.Parent = m_inactiveActionsGroup.transform;
                SetActionActive(action, true);
                SetActionActive(action, false);
            }
        }

        private ActionController GetActionInstanceByType(ActionTypes actionType)
        {
            ActionController actionInstance;
            string actionName = Enum.GetName(typeof(ActionTypes), actionType);
            m_actionsMap.TryGetValue(actionName, out actionInstance);
            return actionInstance;
        }

        private void RequestActionToLayoutGroup(ActionModel actionModel, ActionGroups actionsGroup)
        {
            bool active = true;
            GridLayoutGroup group;
            ActionController actionInstance = GetActionInstanceByType(actionModel.actionType);

            if (actionInstance == null)
            {
                return; //No Instance Found.
            }

            actionInstance.CallbackEvent = actionModel.callbackEvent;

            switch (actionsGroup)
            {
                case ActionGroups.Primary:
                    group = m_primaryActionsGroup;
                    break;
                case ActionGroups.Secondary:
                    group = m_secondaryActionsGroup;
                    break;
                case ActionGroups.Inactive:
                default:
                    group = m_inactiveActionsGroup;
                    active = false;
                    break;
            }

            if (active) {
                m_activeActions.Add(actionInstance);
            }
            else {
                m_activeActions.Remove(actionInstance);
            }

            actionInstance.Parent = group.transform;

            SetActionActive(actionInstance, active);
        }

        public void RequestActionsToLayoutGroup(ActionModel[] actionModels, ActionGroups actionsGroup)
        {
            foreach (ActionModel actionModel in actionModels)
            {
                RequestActionToLayoutGroup(actionModel, actionsGroup);
            }
        }

        private void SetActionActive(ActionController action, bool active)
        {
            action.gameObject.SetActive(active);
        }
        /// <summary>
        ///     Temporarily set the callback delay on an action type instance. 
        ///     The delay will be reset to its global value when the action is enabled again.
        /// </summary>
        /// <param name="actionType"></param>
        /// <param name="callbackDelay"></param>
        public void RequestTempOverrideCallbackDelay(ActionTypes actionType, float callbackDelay)
        {
            ActionController actionInstance = GetActionInstanceByType(actionType);
            if (actionInstance != null) {
                actionInstance.CallbackDelay = callbackDelay;
            }
        }
    }
}