using System.Linq;
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
    string _objectID;
    Enemy _currentEnemy;
    float _timer = 0;
    [SerializeField] float _searchDelayTimer = 0.5f;
    [SerializeField] float _attackSpeed = 0.5f;
    [SerializeField] float _attackDamage = 0.1f;
    [SerializeField] float _attackRadius = 5f;
    [SerializeField] Bullet _bullet;
    Bullet _currentFireBullet;

    private void Awake()
    {
        _objectID = GetInstanceID().ToString();
        InvokeRepeating(nameof(SearchEnemy), 0f, _searchDelayTimer);
    }

    void SearchEnemy()
    {
        if (_currentEnemy && Vector3.Distance(_currentEnemy.transform.position, transform.position) > _attackRadius)
            SetState(null, TowerStates.Search, _objectID);
        if (_currentEnemy != null) return;
        _currentEnemy = EnemyManager.Instance._enemyList
        .Where(enemy => Vector3.Distance(enemy.transform.position, transform.position) < _attackRadius)
        .OrderBy(enemy => Vector3.Distance(enemy.transform.position, transform.position))
        .FirstOrDefault();
        if (_currentEnemy && Vector3.Distance(_currentEnemy.transform.position, transform.position) < _attackRadius)
            MonoCustom.Instance.StartRepeatingCoroutineWithOUTArg(delegate { AttackEnemy(); }, _attackSpeed, _objectID);
    }

    void AttackEnemy()
    {
        if (_currentEnemy != null)
        {
            if (Vector3.Distance(_currentEnemy.transform.position, transform.position) < _attackRadius)
            {
                Bullet newBullet = Instantiate(_bullet, transform.position, Quaternion.identity);
                _currentFireBullet = newBullet;
                newBullet.StartCoroutine(newBullet.WaitToCollapse(_currentEnemy, _attackDamage, this, _objectID));
                _timer = 0;
            }
            else
                SetState(null, TowerStates.Search, _objectID);
        }
    }

    public void SetState(Enemy enemyValue, TowerStates state, string invokeName)
    {
        MonoCustom.Instance.StopRepeatingCoroutineWithOUTArg(invokeName);
        _currentEnemy = enemyValue;
        _currentTowerState = state;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRadius);
    }
}
