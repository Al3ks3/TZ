using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 3f;
    [SerializeField] private Joystick _joystick;
    private GameObject[] _enemy;
    private Rigidbody _rb;
    private GameObject _closest;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        float h = _joystick.Horizontal;
        float v = _joystick.Vertical;
        _rb.velocity = new Vector3(h * _playerSpeed, _rb.velocity.y, v * _playerSpeed);
       
        FindClosestEnemy();
        transform.LookAt(_closest.transform.position);
        Vector3 moveDir = new Vector3(h, 0, v);
        moveDir.Normalize();
        if (h!= 0 || v!=0)
        {
            _anim.SetBool("IsRun",true);
        }
        else
            _anim.SetBool("IsRun", false);
    }
    public GameObject FindClosestEnemy()
    {
        _enemy = GameObject.FindGameObjectsWithTag("Enemy");
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
}
