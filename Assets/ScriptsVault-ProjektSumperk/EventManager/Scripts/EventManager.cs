using UnityEngine;
using UnityEngine.Events;

namespace ProjektSumperk
{
    public class EventManager : MonoBehaviour
    {
        // Dictionary to store event listeners
        private readonly System.Collections.Generic.Dictionary<string, UnityEvent> eventDictionary = new System.Collections.Generic.Dictionary<string, UnityEvent>();

        private static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("EventManager").AddComponent<EventManager>();
                }
                return instance;
            }
        }


        /// <summary>
        /// Add a listener to an event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="listener">Listener (UnityAction) to add.</param>
        public void AddListener(string eventName, UnityAction listener)
        {
            if (!eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent = new UnityEvent();
                eventDictionary.Add(eventName, thisEvent);
            }
            thisEvent.AddListener(listener);
        }

        /// <summary>
        /// Remove a listener from an event.
        /// </summary>
        /// <param name="eventName">Name of the event.</param>
        /// <param name="listener">Listener (UnityAction) to remove.</param>
        public void RemoveListener(string eventName, UnityAction listener)
        {
            if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// Trigger an event by name.
        /// </summary>
        /// <param name="eventName">Name of the event to trigger.</param>
        public void TriggerEvent(string eventName)
        {
            if (eventDictionary.TryGetValue(eventName, out UnityEvent thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        /// <summary>
        /// Clear all listeners for an event.
        /// </summary>
        /// <param name="eventName">Name of the event to clear.</param>
        public void ClearEventListeners(string eventName)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName].RemoveAllListeners();
            }
        }

        /// <summary>
        /// Clear all listeners for all events.
        /// </summary>
        public void ClearAllEventListeners()
        {
            eventDictionary.Clear();
        }
    }
}