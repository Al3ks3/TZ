using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform _shootPose;
    [SerializeField] private GameObject _player;
    [SerializeField] private Bullet Prefab;
    [SerializeField] private float _shootSpeed;
    private int _spawnAmount = 10;
    private ObjectPool<Bullet> _pool;
    private float _timeBfrShoots;
    void Start()
    {
        _timeBfrShoots = _shootSpeed;
        _pool = new ObjectPool<Bullet>(() =>
        {
            return Instantiate(Prefab);
        }, bullet =>
        {
            bullet.gameObject.SetActive(true);
        }, bullet =>
        {
            bullet.gameObject.SetActive(false);
        }, bullet =>
        {
            Destroy(bullet.gameObject);
        }, false, _spawnAmount, 20);

    }

    void Update()
    {
        _timeBfrShoots -= Time.deltaTime;
        if ( _timeBfrShoots < 0)
        {
            _timeBfrShoots = _shootSpeed;
            var bullet = _pool.Get();
            bullet.transform.position = _shootPose.transform.position;
            bullet.transform.rotation = _player.transform.rotation;
            bullet.GetComponent<Rigidbody>().AddForce(_player.transform.forward * 800, ForceMode.Acceleration);
            bullet.Init(DeactiveBullet);
        }
    }
    public void DeactiveBullet(Bullet bullet)
    {
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bullet.transform.position = _shootPose.transform.position;
        _pool.Release(bullet);
    }
}
