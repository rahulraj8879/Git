using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public GridManager gridManager;
    public LevelManager levelManager;

    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (levelManager != null)
        {
            levelManager.currentLevel = PlayerPrefs.GetInt("Level", 0);

        }
        if (gridManager != null)
        {
            gridManager.SetupGrid(); 
        }
        else
        {
            Debug.LogError("GridManager is not assigned.");
        }

    }
}
