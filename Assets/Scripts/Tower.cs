using UnityEngine;

public enum TowerStates
{
    Idle,
    Search,
    Attack
}

public class Tower : MonoBehaviour
{
    TowerStates _currentTowerState = TowerStates.Idle;
    Enemy _currentEnemy;
    float _timer = 0;
    [SerializeField] float _searchDelayTimer = 0.5f;
    [SerializeField] float _attackSpeed = 0.5f;
    [SerializeField] float _attackDamage = 0.1f;
    [SerializeField] float _attackRadius = 5f;
    [SerializeField] Bullet _bullet;
    Bullet _currentFireBullet;
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _searchDelayTimer && _currentTowerState == TowerStates.Idle && _currentEnemy == null)
        {
            _currentTowerState = TowerStates.Search;
            SearchEnemy();
            return;
        }
        if (_currentEnemy != null && _currentTowerState != TowerStates.Attack)
        {
            _currentTowerState = TowerStates.Attack;
            InvokeRepeating(nameof(AttackEnemy), 0f, _attackSpeed);
        }
        else if (_timer >= _attackSpeed && _currentEnemy == null)
        {
            _timer = 0;
        }
    }
    void SearchEnemy()
    {
        float newDistance;
        float topDistance = 0f;
        for (int i = 0; i < EnemyManager.Instance._enemyList.Count; i++)
        {
            newDistance = Vector3.Distance(EnemyManager.Instance._enemyList[i].transform.position, transform.position);
            if (newDistance < _attackRadius)
            {
                if (topDistance != 0f)
                {
                    if (newDistance < topDistance)
                    {
                        _currentEnemy = EnemyManager.Instance._enemyList[i];
                        topDistance = newDistance;
                    }
                }
                else
                {
                    _currentEnemy = EnemyManager.Instance._enemyList[i];
                    topDistance = newDistance;
                }
            }
        }
        _currentTowerState = TowerStates.Idle;
    }
    void AttackEnemy()
    {
        if (_currentEnemy != null)
        {
            if (Vector3.Distance(_currentEnemy.transform.position, transform.position) < _attackRadius)
            {
                if (_currentFireBullet == null)
                {
                    Bullet newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
                    _currentFireBullet = newBullet;
                    newBullet.StartCoroutine(newBullet.WaitToCollapse(_currentEnemy, _attackDamage));
                    _timer = 0;
                }
            }
        }
        else
        {
            CancelInvoke(nameof(AttackEnemy));
            _currentEnemy = null;
            _currentTowerState = TowerStates.Search;
            SearchEnemy();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
