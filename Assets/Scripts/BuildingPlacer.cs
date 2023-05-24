using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] Camera _mainCamera;
    [SerializeField] Tower _towerPrefab;
    [SerializeField] BuildPlacement _buildPlacement;
    BuildPlacement _buildPlacementRay;
    Material _originalMaterial;
    [SerializeField] Material _canBuild, _cantBuild;
    private List<Vector3> occupiedPositions = new List<Vector3>();

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            DestroyBuildPlacementRay();
        }

        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            HandleBuildPlacement(hit);
            bool canBuild = CanBuildOnPlacement(hit.point);
            if (hit.collider.GetComponent<BuildPlacement>() is BuildPlacement placeForBuild)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    BuildTower(placeForBuild);
                }
            }
            if (hit.collider.GetComponent<PlaceForBuild>() && !hit.collider.GetComponent<BuildPlacement>() && !hit.collider.GetComponent<Tower>())
            {
                HandleBuildPlacementMaterial(canBuild);

                if (Input.GetMouseButtonUp(0) && canBuild)
                {
                    PlaceBuild(hit.point);
                }
            }
            else
            {
                HandleBuildPlacementMaterial(false);
            }
        }
    }

    void DestroyBuildPlacementRay()
    {
        if (_buildPlacementRay != null)
        {
            Destroy(_buildPlacementRay.gameObject);
            _buildPlacementRay = null;
        }
    }

    void HandleBuildPlacement(RaycastHit hit)
    {
        if (hit.transform == null) return;
        if (_buildPlacementRay == null && Input.GetKeyDown(KeyCode.B))
        {
            _buildPlacementRay = Instantiate(_buildPlacement, hit.point, Quaternion.identity);
            _originalMaterial = _buildPlacementRay.meshRenderer.material;
        }

        if (_buildPlacementRay != null)
        {
            _buildPlacementRay.transform.position = hit.point;
        }
    }

    void HandleBuildPlacementMaterial(bool canBuild)
    {
        if (_buildPlacementRay != null)
        {
            _buildPlacementRay.meshRenderer.material = canBuild ? _canBuild : _cantBuild;
        }
    }

    bool CanBuildOnPlacement(Vector3 position)
    {
        return !occupiedPositions.Contains(position);
    }

    void PlaceBuild(Vector3 position)
    {
        if (_buildPlacementRay != null)
        {
            _buildPlacementRay.meshRenderer.material = _originalMaterial;
            _buildPlacementRay.SetBuild();
            _buildPlacementRay = null;

            occupiedPositions.Add(position);
        }
    }

    void BuildTower(BuildPlacement placeForBuild)
    {
        Instantiate(_towerPrefab, placeForBuild.transform.position, Quaternion.identity);
        Destroy(placeForBuild.gameObject);
    }
}
