using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using Utils;

public class LevelController : MonoBehaviour {

    public GameObject ghostTemplate;

    [SerializeField] private float levelBoundsMinX = -20;
    [SerializeField] private float levelBoundsMaxX = 20;
    [SerializeField] private float levelBoundsMinY = -3;
    [SerializeField] private float levelBoundsMaxY = 5;
    
    [SerializeField] private int timeLeft = 120;
    private int ghostCount = 0;


    public void targetHit(Collider2D other) {
        Debug.Log("target hit");
        if (other.CompareTag($"Enemy")) {
            Destroy(other.gameObject);
        }
    }
    
    
}
