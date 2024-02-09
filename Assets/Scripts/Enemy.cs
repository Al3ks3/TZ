using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Enemy : MonoBehaviour
{
    private Action<Enemy> _killAction;
    [SerializeField] private float _enemyHealth;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyDamage;
    [SerializeField] private Image _enemyHealthBar;
    [SerializeField] private Image _enemyBackHealthBar;
    private GameObject _player;
    private GameObject _spawner;
    public float _curHealth;

    void Awake()
    {
        _enemyHealthBar.fillAmount = 1;
        _curHealth = _enemyHealth;
        _spawner = GameObject.FindGameObjectWithTag("Spawner");
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    public void Inits(Action<Enemy> killAction)
    {
        _killAction = killAction;
    }

    void Update()
    {
        transform.LookAt(_player.transform.position);
        transform.position = Vector3.MoveTowards( transform.position, _player.transform.position , _enemySpeed * Time.deltaTime);
    }
    public void OnTakeDamage(float damage)
    {
       
        _curHealth -= damage;
        _enemyHealthBar.fillAmount = _curHealth / _enemyHealth;
        if (_curHealth <= 0)
        {
            _spawner.GetComponent<EnemySpawner>().RealeseEnemy(this);
            _curHealth = _enemyHealth;
            _enemyHealthBar.fillAmount = 1;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _player.GetComponent<PlayerHealth>().OnTakeDamage(_enemyDamage);
        }
    }
}
