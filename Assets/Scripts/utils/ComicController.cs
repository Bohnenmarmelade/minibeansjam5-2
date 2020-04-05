using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utils;

public class ComicController : MonoBehaviour {


    private readonly float _delay = 29.5f;
    private float _nextSceneTime;
    
    private void Awake() {
    }

    private void OnEnable() {
        EventManager.TriggerEvent(Events.MUSIC_COMIC);
        Debug.Log("comic music triggered");
        _nextSceneTime = Time.time + _delay;
    }

    private void FixedUpdate() {
        if (Time.time > _nextSceneTime) {
            EventManager.TriggerEvent(Events.START_GAME);
        }
    }
}
