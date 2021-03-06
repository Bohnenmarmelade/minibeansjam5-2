﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class EventManager: MonoBehaviour {

        [System.Serializable]
        public class GameKeyEvent : UnityEvent<string>
        {
        }

        private Dictionary<string, GameKeyEvent> eventDictionary;

        private static EventManager eventManager;

        public static EventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!eventManager)
                    {
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                    }
                    else
                    {
                        eventManager.Init();
                    }
                }

                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, GameKeyEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction<string> listener)
        {
            GameKeyEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new GameKeyEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction<string> listener)
        {
            if (eventManager == null) return;
            GameKeyEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName, string payload="")
        {
            GameKeyEvent thisEvent = new GameKeyEvent();
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke(payload);
            }
        }

    }
}