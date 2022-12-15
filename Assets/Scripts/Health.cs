using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image _imageFilled;
    public void TakeDamage(float damage)
    {
        _imageFilled.fillAmount -= damage;
        if (_imageFilled.fillAmount == 0)
        {
            Destroy(gameObject);
        }
    }
}
