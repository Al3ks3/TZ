using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private float _score;
    [SerializeField] private Text _scoreText;
    void Start()
    {
        _score = 0;
    }

    void Update()
    {
        _score += Time.deltaTime * 2;
        _scoreText.text = "" + Mathf.RoundToInt(_score);
            
    }
}
