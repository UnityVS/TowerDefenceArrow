using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Tower _towerBrefab;
    [SerializeField] BuildPlacement _buildPlacement;
    BuildPlacement _buildPlacementRay;
    Material _originalMaterial;
    [SerializeField] Material _canBuild, _cantBuild;
    void Update()
    {

        if (_buildPlacementRay != null && Input.GetMouseButton(1))
        {
            Destroy(_buildPlacementRay);
        }
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (_buildPlacementRay == null && Input.GetKeyDown(KeyCode.B))
            {
                if (_buildPlacementRay == null)
                {
                    _buildPlacementRay = Instantiate(_buildPlacement, hit.point, Quaternion.identity);
                    _originalMaterial = _buildPlacementRay.meshRenderer.material;
                }
            }
            if (_buildPlacementRay != null)
            {
                _buildPlacementRay.transform.position = hit.point;
            }
            if (hit.collider.GetComponent<PlaceForBuild>())
            {
                if (_buildPlacementRay != null)
                {
                    _buildPlacementRay.meshRenderer.material = _canBuild;
                }
                if (Input.GetMouseButtonUp(0) && _buildPlacementRay != null)
                {
                    _buildPlacementRay.meshRenderer.material = _originalMaterial;
                    _buildPlacementRay = null;
                }
            }
            else
            {
                if (_buildPlacementRay != null)
                {
                    _buildPlacementRay.meshRenderer.material = _cantBuild;
                }
            }
        }
    }
}
