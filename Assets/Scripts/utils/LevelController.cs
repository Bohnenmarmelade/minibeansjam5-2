using System.Collections;
using System.Collections.Generic;
using Gate;
using Ghost;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Utils {
    public class LevelController : MonoBehaviour {

        public GateController gateController;
        public GameObject ghostTemplate;

        public UI.TimeIndicator timeIndicator;

        [SerializeField] private float levelBoundsMinX = -20;
        [SerializeField] private float levelBoundsMaxX = 20;
        [SerializeField] private float levelBoundsMinY = -3;
        [SerializeField] private float levelBoundsMaxY = 5;
        [SerializeField] private int initialGhostsAmount = 10;
    
        [SerializeField] private float initialTime = 10;

        private List<GameObject> _ghosts;
    
        private int _ghostCount = 0; //how many ghosts did the player slay
        private float _timeLeft;
    

        private void Awake() {
            _ghosts = new List<GameObject>();
            StartCoroutine(nameof(InitSpawnGhosts));
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
        }

        private void CountDown() {
            _timeLeft -= Time.fixedDeltaTime;
            timeIndicator.TimeLeft = _timeLeft;
            if (_timeLeft <= 0) {
                EventManager.TriggerEvent(Events.GAME_OVER, ""+_ghostCount);
            }
        }

        IEnumerator InitSpawnGhosts() {
            GameObject g;
            for (int i = 0; i < initialGhostsAmount; i++) {
                var timeBetweenTargets = Random.Range(2f, 5f);
                var wobbleSpeed = Random.Range(3f, 5f); // 3 - 5
                var wobbleAmplitude = Random.Range(.35f, .55f); // .35 .55
                g = Instantiate(ghostTemplate,
                    new Vector2(Random.Range(levelBoundsMinX, levelBoundsMaxX),
                        Random.Range(levelBoundsMinY, levelBoundsMaxY)), Quaternion.identity);
                g.GetComponent<GhostAiMovement>().TimeBetweenTargets = timeBetweenTargets;
                g.GetComponent<GhostController>().WobbleSpeed = wobbleSpeed;
                g.GetComponent<GhostController>().WobbleAmplitude = wobbleAmplitude;

                _ghosts.Add(g);
                yield return null;
            }   
        }


    }
}
