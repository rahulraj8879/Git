using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [System.Serializable]
    public struct PrefabData
    {
        public GameObject prefab;
        public float length;
    }

    public List<PrefabData> prefabs;
    public Transform nodesSpawnParentObj;
    public int numberOfGridsToGenerate = 1;
    public int gridWidth = 4;
    public int gridHeight = 4;
    public float spacing = 1f;

    private GameObject[,] nodes;
    int levelWidth = 2;

    public void SetupGrid()
    {
        levelWidth = PlayerPrefs.GetInt("Level", 0) + 2;

        if (levelWidth<=numberOfGridsToGenerate)
        {
            GameObject levelParent = new GameObject("Level_" + (levelWidth - 1));
            levelParent.transform.SetParent(nodesSpawnParentObj);

            nodes = new GameObject[levelWidth, levelWidth];

            // Calculate center position
            float halfWidth = (levelWidth - 1) * spacing * 0.5f;
            Vector3 centerPosition = new Vector3(-halfWidth, -halfWidth, 0);

            for (int x = 0; x < levelWidth; x++)
            {
                for (int y = 0; y < levelWidth; y++)
                {
                    Vector3 position = centerPosition + new Vector3(x * spacing, y * spacing, 0);

                    // Check if position is occupied
                    if (IsPositionOccupied(position))
                    {
                        continue;
                    }

                    // Randomly select a prefab
                    PrefabData prefabData = prefabs[Random.Range(0, prefabs.Count)];

                    // Instantiate prefab with random rotation
                    GameObject node = Instantiate(prefabData.prefab, position, RandomRotation(), levelParent.transform);
                    nodes[x, y] = node;
                }
            }
        }
        else
        {
            GameObject levelParent = new GameObject("Level_" + (levelWidth - 1));
            levelParent.transform.SetParent(nodesSpawnParentObj);

            nodes = new GameObject[gridWidth, gridHeight];

            // Calculate center position
            float halfWidth = (gridHeight - 1) * spacing * 0.5f;
            Vector3 centerPosition = new Vector3(-halfWidth, -halfWidth, 0);

            for (int x = 0; x < gridHeight; x++)
            {
                for (int y = 0; y < gridHeight; y++)
                {
                    Vector3 position = centerPosition + new Vector3(x * spacing, y * spacing, 0);

                    // Check if position is occupied
                    if (IsPositionOccupied(position))
                    {
                        continue;
                    }

                    PrefabData prefabData = prefabs[Random.Range(0, prefabs.Count)];

                    // Instantiate prefab with random rotation
                    GameObject node = Instantiate(prefabData.prefab, position, RandomRotation(), levelParent.transform);
                    nodes[x, y] = node;
                }
            }
            
        }

  

        // Notify LevelManager that nodes have been spawned
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if (levelManager != null)
        {
            levelManager.OnNodesSpawned();
        }
    }

    bool IsPositionOccupied(Vector3 position)
    {

        return false; // Replace with actual check
    }

    Quaternion RandomRotation()
    {
        // Generate a random rotation angle
        int randomRotation = Random.Range(0, 4) * 90;
        Quaternion rotation = Quaternion.Euler(0, 0, randomRotation);
        return rotation;
    }
}
