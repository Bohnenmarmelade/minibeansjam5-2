using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Utils;

namespace Gate {
    public class GateController : MonoBehaviour {

        [SerializeField] private float doorDelay = .5f;

        public Sprite spriteGateClosed;
        public Sprite spriteGateOpen;

        private SpriteRenderer _spriteRenderer;


        private float _changeTime;
        private bool _shouldOpen = true;
        private bool _shouldClose = true;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = spriteGateClosed;
        }

        private void FixedUpdate() {
            if (_shouldOpen && Time.time > _changeTime) {
                _spriteRenderer.sprite = spriteGateOpen;
                _shouldOpen = false;
            } else if (_shouldClose && Time.time > _changeTime) {
                _spriteRenderer.sprite = spriteGateClosed;
                _shouldClose = false;
            }
        }

        public void Open() {
            EventManager.TriggerEvent(Events.SFX_GATE);
            _changeTime = Time.time + doorDelay;
            _shouldOpen = true;
            _shouldClose = false;
        }
    
        public void Close() {
            EventManager.TriggerEvent(Events.SFX_GATE);
            _changeTime = Time.time + doorDelay;
            _shouldClose = true;
            _shouldOpen = false;
        }
    
    }
}
