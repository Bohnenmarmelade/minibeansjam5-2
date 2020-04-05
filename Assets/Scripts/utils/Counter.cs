using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour {

    private TextMeshProUGUI _textMeshPro;

    private void Awake() {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
        _textMeshPro.text = "0";
    }

    public void SetCount(int count) {
        _textMeshPro.text = count+"";

    } 
}
