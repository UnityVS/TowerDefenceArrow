using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image _imageFilled;
    public void TakeDamage(float damage, Tower towerAttack, Enemy enemy, string invokerObjectID)
    {
        _imageFilled.fillAmount -= damage;
        if (_imageFilled.fillAmount == 0)
        {
            towerAttack.SetState(enemy, TowerStates.Search, invokerObjectID);
            Destroy(gameObject);
        }
    }
}
