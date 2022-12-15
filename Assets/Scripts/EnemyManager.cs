using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    float _timer = 0f;
    [SerializeField] float _enemySpeed = 0.1f;
    [SerializeField] float _maxGenerationEnemyTimer = 2f;
    [SerializeField] int _maxEnemyCount = 5;
    float _startWaveTimer = 2f;
    bool _waveStart = false;
    int _currentEnemyWave = 0;
    [SerializeField] Enemy _enemyPrefab;
    [SerializeField] Transform _startPoint;
    [SerializeField] Transform _navigationPoints;
    public List<Enemy> _enemyList = new List<Enemy>();
    public static EnemyManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _startWaveTimer && !_waveStart)
        {
            _waveStart = true;
        }
        if (_waveStart)
        {
            if (_timer > _maxGenerationEnemyTimer && _maxEnemyCount != _currentEnemyWave)
            {
                _currentEnemyWave++;
                Enemy newEnemy = Instantiate(_enemyPrefab, _startPoint.position, Quaternion.identity);
                _enemyList.Add(newEnemy);
                _timer = 0f;
            }
        }
    }
    public void RemoveEnemyFromList(Enemy enemy)
    {
        _enemyList.Remove(enemy);
    }
    public float GetEnemyTurnSpeed()
    {
        return _enemySpeed;
    }
    public List<Transform> GetNavigationPoints()
    {
        List<Transform> listOfPoints = new List<Transform>();
        for (int i = 0; i < _navigationPoints.childCount; i++)
        {
            listOfPoints.Add(_navigationPoints.GetChild(i));
            if (_navigationPoints.childCount == i)
            {
                return listOfPoints;
            }
        }
        if (_navigationPoints.childCount == 0)
        {
            listOfPoints.Add(_startPoint);
            return listOfPoints;
        }
        else
        {
            return listOfPoints;
        }
    }
}
