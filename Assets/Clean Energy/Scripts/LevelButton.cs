using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int level;
    public Button button;

    void Start()
    {
        if (!IsLevelUnlocked(level))
        {
            button.interactable = false;
        }
    }

    bool IsLevelUnlocked(int level)
    {
        return PlayerPrefs.GetInt("Level" + level, 0) == 1;
    }
}
