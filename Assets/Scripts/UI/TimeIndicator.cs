using System;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace UI {
    public class TimeIndicator : MonoBehaviour {
        private TextMeshProUGUI _textMesh;

        private float _timeLeft;


        private void Awake() {
            _textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        }

        public float TimeLeft {
            get => _timeLeft;
            set => _timeLeft = value;
        }

        private void FixedUpdate() {
            _textMesh.text = ((int) Math.Floor(_timeLeft)) + "";
        }
    }
}
