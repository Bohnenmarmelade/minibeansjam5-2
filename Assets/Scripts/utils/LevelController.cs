﻿using System.Collections;
using System.Collections.Generic;
using Gate;
using Ghost;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils {
    public class LevelController : MonoBehaviour {

        public GameObject[] flummyTemplates;
        public GameObject gateTemplate;
        public GameObject ghostTemplate;
        public GameObject boxTemplate;
        public GameObject obstacleParent;
        private GateController _gateController;
        
        public UI.TimeIndicator timeIndicator;

        [SerializeField] private float levelBoundsMinX = -20;
        [SerializeField] private float levelBoundsMaxX = 20;
        [SerializeField] private float levelBoundsMinY = -3;
        [SerializeField] private float levelBoundsMaxY = 5;
        [SerializeField] private int initialGhostsAmount = 10;
        [SerializeField] private int bonusSecondsPerGhost = 5;
        [SerializeField] private int initialBoxAmount = 20;
        [SerializeField] private int initialFlummyAmount = 20;
    
        [SerializeField] private float initialTime = 10;

        private List<GameObject> _ghosts;
    
        private int _ghostCount = 0; //how many ghosts did the player slay
        private float _timeLeft;

        private bool _isPlayerAtGate = false;
        

        private void Awake() {
            _ghosts = new List<GameObject>();
            StartCoroutine(nameof(InitSpawnGhosts));
            StartCoroutine(nameof(InitSpawnBoxes));
            StartCoroutine(nameof(InitSpawnFlummies));
            SpawnGate();
            _timeLeft = initialTime;

        }

        public void targetHit(Collider2D other) {
            Debug.Log("target hit");
            if (other.CompareTag($"Enemy")) {
                _ghostCount++;
                other.GetComponent<GhostController>().Die();
                //Destroy(other.gameObject);
            }
        }

        private void FixedUpdate() {
            CountDown();
            if (ShouldSpawnGhost()) {
                SpawnGhost();
            }
        }

        private void CountDown() {
            _timeLeft -= Time.fixedDeltaTime;
            timeIndicator.TimeLeft = _timeLeft;
            if (_timeLeft <= 0) {
                EventManager.TriggerEvent(Events.GAME_OVER, ""+_ghostCount);
            }
        }

        private void PayGhosts() {
            float extraTime = (float) bonusSecondsPerGhost * _ghostCount;
            _ghostCount = 0;
            _timeLeft += extraTime;
        }

        public void GateEntered() {
            if (!_isPlayerAtGate) {
                Debug.Log("yo you entered the gate to heaven");
                _gateController.Open();
                _isPlayerAtGate = true;
                PayGhosts();
            }
        }

        public void GateLeft() {
            if (_isPlayerAtGate) {
                Debug.Log("yo you left the gate :(");
                _gateController.Close();
                _isPlayerAtGate = false;
            }
        }

        private void SpawnGate() {
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(levelBoundsMinX, levelBoundsMaxX);

            var i = Instantiate(gateTemplate, pos, Quaternion.identity);
            _gateController = i.GetComponent<GateController>();
        }

        private bool ShouldSpawnGhost() {
            return _ghosts.Count < 8;
        }

        private void SpawnGhost() {
            var timeBetweenTargets = Random.Range(2f, 5f);
            var wobbleSpeed = Random.Range(3f, 5f); // 3 - 5
            var wobbleAmplitude = Random.Range(.35f, .55f); // .35 .55
            GameObject g = Instantiate(ghostTemplate,
                new Vector2(Random.Range(levelBoundsMinX, levelBoundsMaxX),
                    Random.Range(levelBoundsMinY, levelBoundsMaxY)), Quaternion.identity);
            g.GetComponent<GhostAIMovement>().TimeBetweenTargets = timeBetweenTargets;
            g.GetComponent<GhostController>().WobbleSpeed = wobbleSpeed;
            g.GetComponent<GhostController>().WobbleAmplitude = wobbleAmplitude;

            _ghosts.Add(g);
        }

        private void SpawnBox() {
            GameObject g = Instantiate(boxTemplate, 
                new Vector2(Random.Range(levelBoundsMinX, levelBoundsMaxX),
                    Random.Range(levelBoundsMinY, levelBoundsMaxY)), Quaternion.identity, obstacleParent.transform);
        }

        private void SpawnFlummy() {
            int index = Random.Range(0, flummyTemplates.Length);
            GameObject g = Instantiate(flummyTemplates[index], 
                new Vector2(Random.Range(levelBoundsMinX, levelBoundsMaxX),
                    Random.Range(levelBoundsMinY, levelBoundsMaxY)), Quaternion.identity, obstacleParent.transform);
        }

        IEnumerator InitSpawnGhosts() {
            for (int i = 0; i < initialGhostsAmount; i++) {
                SpawnGhost();
                yield return null;
            }   
        }

        IEnumerator InitSpawnBoxes() {
            for (int i = 0; i < initialBoxAmount; i++) {
                SpawnBox();
                yield return null;
            }
        }

        IEnumerator InitSpawnFlummies() {
            for (int i = 0; i < initialFlummyAmount; i++) {
                SpawnFlummy();
                yield return null;
            }
        }


    }
}
