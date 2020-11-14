using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

namespace GameJam.Managers
{
    public class EventManager : MonoBehaviour
    {
        private static EventManager instance;

        private Dictionary<string, UnityEvent> eventDictionary = new Dictionary<string, UnityEvent>();

        private static Dictionary<string, UnityEvent> EventDictionary
        {
            get { return instance.eventDictionary; }
        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            if (EventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                EventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            if (EventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            if (EventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        protected void Awake()
        {
            Object.DontDestroyOnLoad(gameObject);

            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        protected void OnDestroy()
        {
            if (instance != null)
            {
                instance = null;
            }
        }
    }
}