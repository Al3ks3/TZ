using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Action<Bullet> _killAction;
    private Rigidbody _bulletRb;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private GameObject[] _enemy;
    [SerializeField] private float _bulletLiveTime;
    [SerializeField] private float _damage;
    public GameObject _closest;
    private void Start()
    {
        _bulletRb = GetComponent<Rigidbody>();
    }
    public void Init(Action<Bullet> killAction)
    {
        
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
        _killAction = killAction;
        FindClosestEnemy();
    }
    GameObject FindClosestEnemy()
    {
        if (_enemy != null)
        {
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;
            foreach (GameObject go in _enemy)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    _closest = go;
                    distance = curDistance;
                }
            }
            return _closest;
        }
        return null;
    }
    public void Update()
    {
       
            Transform nearest = FindClosestEnemy().transform;
            var heading = nearest.position - transform.position;
            var rotaton = Quaternion.LookRotation(heading);
            _bulletRb.MoveRotation(rotaton);
            _bulletRb.velocity = transform.forward * _bulletSpeed;
            var curLiveTime = _bulletLiveTime;
            curLiveTime -= Time.deltaTime;
            if (curLiveTime <= 0)
            {
                transform.position = Vector3.zero;
                curLiveTime = _bulletLiveTime;
                _killAction(this);
            }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _closest.GetComponent<Enemy>().OnTakeDamage(_damage);  
                _killAction(this);
        }
    }
}
