using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;
    private Node[] nodes;
    public Transform nodesSpawnParentObj;



    public void OnNodesSpawned()
    {
        FetchNodes();
        
    }

    private void FetchNodes()
    {
        nodes = FindObjectsOfType<Node>();
        if (nodes != null)
        {
            CheckLevelCompletion();
        }
        Debug.Log("Nodes fetched: " + nodes.Length);
    }

    public void NotifyNodeStateChange()
    {
        CheckLevelCompletion();
    }

    private void CheckLevelCompletion()
    {
        bool allNodesGreen = true;

        foreach (Node node in nodes)
        {
            if (!node.isGreen)
            {
                allNodesGreen = false;
                break; // Exit early if any node is not green
            }
        }

        if (allNodesGreen)
        {
            DisableNodeTouching();
            CompleteLevel();
        }
    }
    void DisableNodeTouching()
    {
        foreach (Node node in nodes)
        {
            node.disabletouch = true;
        }

    }
    public void CompleteLevel()
    {
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.Save();
        Debug.Log("Level " + currentLevel + " completed.");

        UnlockNextLevel();
    }

    public bool IsLevelUnlocked()
    {
        return PlayerPrefs.GetInt("Level", 0) == 1;
    }

    public void UnlockNextLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
        PlayerPrefs.Save();
        Debug.Log("Level " + currentLevel + " unlocked.");
    }

    public bool IsCurrentLevelCompleted()
    {
        return PlayerPrefs.GetInt("Level" + currentLevel, 0) == 1;
    }
}
