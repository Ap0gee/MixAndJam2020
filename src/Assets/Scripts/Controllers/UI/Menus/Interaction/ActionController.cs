using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DuloGames.UI;
using DuloGames.UI.Tweens;
using TMPro;
using System;
using GameJam.Managers;
using UnityEngine.EventSystems;

namespace GameJam
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private UIProgressBar m_background;
        [SerializeField] private TMP_Text m_text;
        [SerializeField] private UIProgressBar m_slot;
        [SerializeField] private Image m_icon;
        [SerializeField] private TweenEasing m_TransitionEasing = TweenEasing.InOutQuint;
        [NonSerialized] private readonly TweenRunner<FloatTween> m_FloatTweenRunner;

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

        public Vector3 LocalBackgroundPos
        {
            get { return m_background.transform.localPosition; }
            set { m_background.transform.localPosition = value; }
        }

        public float BackgroundFill
        {
            get { return m_background.fillAmount; }
            set { m_background.fillAmount = value; }
        }

        public string Text
        {
            get { return m_text.text; }
            set { m_text.text = value; }
        }

        public float SlotFill
        {
            get { return m_slot.fillAmount; }
            set { m_slot.fillAmount = value >= 1f ? 1f : value; }
        }

        public Sprite Icon
        {
            get { return m_icon.sprite; }
            set { m_icon.sprite = value; }
        }

        public UnityEvent CallbackEvent
        {
            get { return m_actionCallbackEvent == null ? new UnityEvent() : m_actionCallbackEvent; }
            set { m_actionCallbackEvent = value; }
        }

        public string CallbackKey { get { return Enum.GetName(typeof(ActionKeys), m_callbackKey); } }

        public float CallbackDelay { get; set; }

        protected ActionController()
        {
            if (m_FloatTweenRunner == null)
                m_FloatTweenRunner = new TweenRunner<FloatTween>();
            m_FloatTweenRunner.Init(this);
        }

        private void Awake()
        {
            m_menuInteraction = UIManager.UI.MenusController.MenuInteraction;
            m_backgroundRect = m_background.transform.GetComponent<RectTransform>();
            m_backgroundTweenPos = m_backgroundRect.anchoredPosition;
        }

        void OnEnable()
        {
            LocalScale = new Vector3(1, 1, 1);
            ResetFillValues();
            CallbackDelay = m_action.actionCallbackDelay;
        }

        public void OnTweenChange(float value)
        {
            float fill = 1 / m_action.tweenTarget * value;

            m_backgroundTweenPos.x = value;
            m_backgroundRect.anchoredPosition = m_backgroundTweenPos;

            if (fill > m_action.backgroundStartFill)
            {
                BackgroundFill = fill;
            }
        }

        public void StartTween(bool ignoreTimeScale)
        {
            ResetFillValues();
            var floatTween = new FloatTween { duration = m_action.tweenDuration, startFloat = m_action.tweenStart, targetFloat = m_action.tweenTarget };
            floatTween.AddOnChangedCallback(OnTweenChange);
            floatTween.ignoreTimeScale = ignoreTimeScale;
            floatTween.easing = m_TransitionEasing;
            m_FloatTweenRunner.StartTween(floatTween);
        }

        public void ShouldListenForInput(bool shouldListen)
        {
            m_listenForInput = shouldListen;
        }

        public void ResetFillValues() {
            BackgroundFill = m_action.backgroundStartFill;
            SlotFill = 0f;
        }

        public void Update()
        {
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
                    ResetFillValues();
                    CallbackEvent.Invoke();
                }
            }
        }
    }
}