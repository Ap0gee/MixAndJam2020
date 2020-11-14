using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;
using GameJam.Managers;
using UnityEngine.EventSystems;

namespace GameJam
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private Image m_slot;

        [SerializeField]
        private SOAction m_action;

        private bool m_listenForInput = false;
        private Vector3 m_backgroundTweenPos;
        private RectTransform m_backgroundRect;

        private UnityEvent m_actionCallbackEvent;
        private MenuInteractionController m_menuInteraction;

        public enum ActionKeys
        {
            Confirm,
            Action1,
            Action2,
            Action3
        }

        [SerializeField]
        private ActionKeys m_callbackKey;

        public Transform Parent
        {
            get { return transform.parent; }
            set { transform.SetParent(value); }
        }

        public Vector3 LocalScale
        {
            get { return transform.localScale; }
            set { transform.localScale = value; }
        }

        public float SlotFill
        {
            get { return m_slot.fillAmount; }
            set { m_slot.fillAmount = value >= 1f ? 1f : value; }
        }

        public UnityEvent CallbackEvent
        {
            get { return m_actionCallbackEvent == null ? new UnityEvent() : m_actionCallbackEvent; }
            set { m_actionCallbackEvent = value; }
        }

        public string CallbackKey { get { return Enum.GetName(typeof(ActionKeys), m_callbackKey); } }

        public float CallbackDelay { get; set; }

        private void Awake()
        {
            m_menuInteraction = UIManager.UI.MenusController.MenuInteraction;
        }

        void OnEnable()
        {
            ShouldListenForInput(true);
            SlotFill = 0f;
            CallbackDelay = m_action.actionCallbackDelay;
        }

        public void ShouldListenForInput(bool shouldListen)
        {
            m_listenForInput = shouldListen;
        }

        public void Update()
        {
            Debug.Log(InputManager.GetButton(CallbackKey));
            if (m_listenForInput)
            {
                if (InputManager.GetButton(CallbackKey)) {
                    SlotFill += 1 / CallbackDelay * Time.deltaTime;
                }
                else {
                    SlotFill = 0f;
                }

                if (SlotFill == 1f) {
                    ShouldListenForInput(false);
                    m_menuInteraction.Hide(true);
                    SlotFill = 0f;
                    CallbackEvent.Invoke();
                }
            }
        }
    }
}