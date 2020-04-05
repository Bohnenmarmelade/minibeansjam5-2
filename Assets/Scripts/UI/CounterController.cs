using System;
using System.Collections.Generic;
using System.Numerics;
using Gate;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace UI {
    public class CounterController : MonoBehaviour {

        private int _currentCount = 5;
        
        public GameObject counter1;
        public GameObject counter2;
        public GameObject counter3;
        public GameObject counter4;
        public GameObject counter5;

        private List<GameObject> counters;
        public Camera _camera;
        [SerializeField]private Vector3 _offset = new Vector3(0, 0, 0);
        private void Awake() {
            counters = new List<GameObject>();
            
        }

        private void Update() {
            Vector3 pos = _camera.transform.position + _offset;
            pos.z = 0;
            transform.position = pos;
        }

        private void InstCounter(int count, int row) {
            Vector3 o = new Vector3(.1f * row, 0,0);
            if (count == 5) {
                var g = Instantiate(counter5, Vector3.zero + _offset + o, Quaternion.identity, this.transform);
                counters.Add(g);
            } else if (count == 4) {
                var g = Instantiate(counter4, Vector3.zero + _offset + o, Quaternion.identity, this.transform);
                counters.Add(g);
            } else if (count == 3) {
                var g = Instantiate(counter3, Vector3.zero + _offset + o, Quaternion.identity, this.transform);
                counters.Add(g);
            } else if (count == 2) {
                var g = Instantiate(counter2, Vector3.zero + _offset + o, Quaternion.identity, this.transform);
                counters.Add(g);
            } else if (count == 1) {
                var g = Instantiate(counter1, Vector3.zero + _offset + o, Quaternion.identity, this.transform);
                counters.Add(g);
            }
        }

        public void SetCurrentCount(int count) {
            _currentCount = count;
            Vector3 o = Vector3.zero;

            foreach (GameObject c in counters) {
                DestroyImmediate(c);
            }

            int times = (int)Math.Floor((float)_currentCount / 5f);
            int left = _currentCount % 5;

            for (int i = 0; i < times; i++) {
                InstCounter(5, i);
            }

            if (left > 0) {
                InstCounter(left, times);
            }

        }
    }
}
