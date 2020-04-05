using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class SFXController : MonoBehaviour {
    public AudioClip scytheSlay;
    public AudioClip ghostDie;
    public AudioClip gate;

    private AudioSource _source;

    private void OnEnable() {
        EventManager.StartListening(Events.SFX_SCYTHE, OnPlayScytheSlay);
        EventManager.StartListening(Events.SFX_GHOST_DIE, OnPlayGhostDie);
        EventManager.StartListening(Events.SFX_GATE, OnPlayGate);
    }

    private void OnDisable() {
        EventManager.StopListening(Events.SFX_SCYTHE, OnPlayScytheSlay);
        EventManager.StopListening(Events.SFX_GHOST_DIE, OnPlayGhostDie);
        EventManager.StopListening(Events.SFX_GATE, OnPlayGate);
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

    private void OnPlayGate(string payload) {
        _source.PlayOneShot(gate);
    }
}
