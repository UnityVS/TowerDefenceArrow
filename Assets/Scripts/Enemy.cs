using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Transform> _navigationPoints = new List<Transform>();
    [SerializeField] Health _health;
    int _currentNavigationPoint = 0;
    float _walkSpeed = 0.1f;
    private void Start()
    {
        _navigationPoints = EnemyManager.Instance.GetNavigationPoints();
        _walkSpeed = EnemyManager.Instance.GetEnemyTurnSpeed();
        StartCoroutine(Walk());
    }
    public Health GetHealthInfo()
    {
        return _health;
    }
    IEnumerator Walk()
    {
        while (true)
        {
            transform.Translate((_navigationPoints[_currentNavigationPoint].position - transform.position).normalized * _walkSpeed);
            yield return new WaitForSeconds(0.007f);
            if (Vector3.Distance(transform.position, _navigationPoints[_currentNavigationPoint].position) < 0.5f)
            {
                if (_currentNavigationPoint == _navigationPoints.Count - 1)
                {
                    break;
                }
                else
                {
                    _currentNavigationPoint++;
                }
                yield return null;
            }
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        EnemyManager.Instance.RemoveEnemyFromList(this);
    }
}
