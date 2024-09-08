using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    public GameObject myGun; // The gun prefab to be instantiated
    public int minGunsPerRoad = 6; // Minimum number of guns per road
    public int maxGunsPerRoad = 12; // Maximum number of guns per road

    public List<GameObject> roadList; // List of road GameObjects

    private void Start()
    {
        foreach (GameObject road in roadList)
        {
            MeshCollider meshCollider = road.GetComponent<MeshCollider>();
            if (meshCollider != null)
            {
                int gunCount = Random.Range(minGunsPerRoad, maxGunsPerRoad);
                for (int i = 0; i < gunCount; i++)
                {
                    Vector3 randomPosition = GetRandomPointInMesh(meshCollider);
                    Instantiate(myGun, randomPosition, Quaternion.identity);
                }
            }
        }
    }

    private Vector3 GetRandomPointInMesh(MeshCollider meshCollider)
    {
        Bounds bounds = meshCollider.bounds;
        Vector3 randomPoint = Vector3.zero;
        bool pointInMesh = false;

        while (!pointInMesh)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomZ = Random.Range(bounds.min.z, bounds.max.z);
            Vector3 randomPosition = new Vector3(randomX, bounds.center.y, randomZ);

            if (IsPointInMesh(randomPosition, meshCollider))
            {
                randomPoint = randomPosition;
                pointInMesh = true;
            }
        }

        return randomPoint;
    }

    private bool IsPointInMesh(Vector3 point, MeshCollider meshCollider)
    {
        Vector3 origin = new Vector3(point.x, point.y + 100f, point.z);
        Ray ray = new Ray(origin, Vector3.down);
        RaycastHit hit;

        if (meshCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.point.y < point.y + 0.1f && hit.point.y > point.y - 0.1f)
            {
                return true;
            }
        }

        return false;
    }
}
