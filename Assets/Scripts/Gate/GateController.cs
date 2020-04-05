using System;
using UnityEngine;

namespace Gate {
    public class GateController : MonoBehaviour {

        public Sprite spriteGateClosed;
        public Sprite spriteGateOpen;

        private SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = spriteGateClosed;
        }

        public void Open() {
            _spriteRenderer.sprite = spriteGateOpen;
        }
    
        public void Close() {
            _spriteRenderer.sprite = spriteGateClosed;
        }
    
    }
}
