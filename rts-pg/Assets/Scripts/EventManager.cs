using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CEvents {
    public const string POINTER_MODE = "POINTER_MODE";
    public const string BUILD = "BUILD";
    public const string MOUSE_POSITION = "MOUSE_POSITION";
    public const string CAMERA_MOVE = "CAMERA_MOVE";
}

public class BaseEvent : UnityEvent<BaseMessage> {
}

public class BaseMessage {
}

public class StringMessage : BaseMessage {
    public string value;

    public StringMessage(string value) {
        this.value = value;
    }
}

public class BoolMessage : BaseMessage {
    public bool value;

    public BoolMessage(bool value) {
        this.value = value;
    }
}

public class PositionMessage : BaseMessage {
    public Vector3 position;

    public PositionMessage(Vector3 position) {
        this.position = position;
    }
}

public class CameraMessage : BaseMessage {
    public Direction direction;

    public CameraMessage(Direction direction) {
        this.direction = direction;
    }
}

public class BuildMessage : BaseMessage {
    public GameObject obj;

    public BuildMessage(GameObject obj) {
        this.obj = obj;
    }
}


public class EventManager : MonoBehaviour {

    private Dictionary<string, BaseEvent> eventDictionary;
    private static EventManager eventManager;
    private List<string> ignoredOutputEvents = new List<string>();

    public void Awake() {
        if (eventManager != null && eventManager != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public static EventManager Instance {
        get {
            if (eventManager == null) {
                eventManager = FindObjectOfType<EventManager>();
                if (eventManager == null) {
                    Utils.Log("EventManager: Object not found in the scene!", LogType.ERROR);
                    return null;
                }
                eventManager.Init();
            }
            return eventManager;
        }
    }

    public static void IgnoreEvent(string eventName) {
        Instance.ignoredOutputEvents.Add(eventName);
    }

    public static void UnIgnoreEvent(string eventName) {
        Instance.ignoredOutputEvents.Remove(eventName);
    }

    void Init() {
        Utils.Log("EventManager: Init");
        if (eventDictionary == null) {
            eventDictionary = new Dictionary<string, BaseEvent>();
        }
    }

    public static void StartListening(string eventName, UnityAction<BaseMessage> listener) {
        BaseEvent thisEvent = null;
        if (!Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            thisEvent = new BaseEvent();
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
        thisEvent.AddListener(listener);
    }

    public static void StopListening(string eventName, UnityAction<BaseMessage> listener) {
        if (eventManager != null) {
            BaseEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
                thisEvent.RemoveListener(listener);
            }
        }
    }

    public static void TriggerEvent(string eventName, BaseMessage msg = null) {
        BaseEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent)) {
            // TODO should cache this check, or simple use a dict
            if (!Instance.ignoredOutputEvents.Contains(eventName)) {
                Utils.Log("EventManager: Triggering `" + eventName + "`");
            }
            thisEvent.Invoke(msg);
        } else {
            Utils.Log("EventManager: No listeners for:  `" + eventName + "`", LogType.WARNING);
        }
    }

}
