using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private Image _healthBar;
    [SerializeField] private GameObject _endPanel;
    public float _health;
    void Start()
    {
        _healthBar.fillAmount = 1;
        _health = _maxHealth;
    }

   public void OnTakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.fillAmount -= damage * 0.01f;
        if (_health <= 0)
        {
            _endPanel.SetActive(true);
            Time.timeScale = 0;
        }
     
    }
}
