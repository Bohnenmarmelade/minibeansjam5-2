using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour {

    public GameObject ghostTemplate;

    [SerializeField] private float levelBoundsMinX = -20;
    [SerializeField] private float levelBoundsMaxX = 20;
    [SerializeField] private float levelBoundsMinY = -3;
    [SerializeField] private float levelBoundsMaxY = 5;
    [SerializeField] private int initialGhostsAmount = 10;
    
    [SerializeField] private int timeLeft = 120;

    private List<GameObject> _ghosts;
    
    private int ghostCount = 0; //how many ghosts did the player slay


    private void Awake() {
        _ghosts = new List<GameObject>();
        StartCoroutine(nameof(InitSpawnGhosts));
    }

    public void targetHit(Collider2D other) {
        Debug.Log("target hit");
        if (other.CompareTag($"Enemy")) {
            Destroy(other.gameObject);
        }
    }

    private void spawnGhost() {
        

    }

    IEnumerator InitSpawnGhosts() {
        GameObject g;
        float timeBetweenTargets = 1f;
        float wobbleSpeed = 4f; // 3 - 5
        float wobbleAmplitude = 0.45f; // .35 .55
        for (int i = 0; i < initialGhostsAmount; i++) {
            timeBetweenTargets = Random.Range(2f, 5f);
            wobbleSpeed = Random.Range(3f, 5f);
            wobbleAmplitude = Random.Range(.35f, .55f);
            g = Instantiate(ghostTemplate,
                new Vector2(Random.Range(levelBoundsMinX, levelBoundsMaxX),
                    Random.Range(levelBoundsMinY, levelBoundsMaxY)), Quaternion.identity);
            g.GetComponent<GhostAIMovement>().TimeBetweenTargets = timeBetweenTargets;
            g.GetComponent<GhostController>().WobbleSpeed = wobbleSpeed;
            g.GetComponent<GhostController>().WobbleAmplitude = wobbleAmplitude;

            _ghosts.Add(g);
            yield return null;
        }   
    }


}
