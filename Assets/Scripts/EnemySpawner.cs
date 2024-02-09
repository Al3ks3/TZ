using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemys;
    [SerializeField] private int _enemyCount;
    private ObjectPool<Enemy> _enemyPool;
    private int _minEnemyCount = 5;
    private float _timeBeforeEnemySpawn = 4;
    private float _curTimeSpawn;

    void Start()
    {

        _curTimeSpawn = _timeBeforeEnemySpawn;

        _enemyPool = new ObjectPool<Enemy>(() =>
        {
            return Instantiate(_enemys[Random.Range(0, _enemys.Length)]);
        }, enemy =>
        {
            enemy.gameObject.SetActive(true);
        }, enemy =>
        {
           enemy.gameObject.SetActive(false);
        }, enemy =>
        {
            Destroy(enemy.gameObject);
        }, false, _minEnemyCount, _enemyCount);

    }

    void Update()
    {
        
        _curTimeSpawn -= Time.deltaTime;
        if(_curTimeSpawn < 0 )
        {
            if (_timeBeforeEnemySpawn > 2)
                _timeBeforeEnemySpawn -= 0.1f;
            else
            {
                _timeBeforeEnemySpawn = 2f;
            }
            _curTimeSpawn = _timeBeforeEnemySpawn;
            
            var enemy = _enemyPool.Get();
            enemy.transform.position = gameObject.transform.position;
        }

    }
    public void RealeseEnemy(Enemy enemy)
    {
        _enemyPool.Release(enemy);
    }

}
