using UnityEngine;

public class BuildPlacement : MonoBehaviour
{
    public Renderer meshRenderer;
    [SerializeField] Collider _collider;
    bool builded = false;
    private void Update()
    {
        if (_collider.enabled == true) return;
        if (builded)
        {
            _collider.enabled = true;
        }
    }
    public void SetBuild()
    {
        builded = true;
    }
}
