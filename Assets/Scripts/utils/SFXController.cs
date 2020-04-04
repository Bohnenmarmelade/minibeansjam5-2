using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SFXController : MonoBehaviour {
    public AudioClip scytheSlay;
    public AudioClip ghostDie;

    private AudioSource _source;

    private void OnEnable() {
        EventManager.StartListening(Events.SFX_SCYTHE, OnPlayScytheSlay);
        EventManager.StartListening(Events.SFX_GHOST_DIE, OnPlayGhostDie);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.SFX_SCYTHE, OnPlayScytheSlay);
        EventManager.StopListening(Events.SFX_GHOST_DIE, OnPlayGhostDie);
    }

    private void Awake() {
        _source = GetComponent<AudioSource>();
    }

    private void OnPlayScytheSlay(string payload) {
        _source.PlayOneShot(scytheSlay);
    }

    private void OnPlayGhostDie(string payload) {
        _source.PlayOneShot(ghostDie);
    }
}
