using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject[] prefabLevel;
    static public int currentLevel = 1;

    void Start()
    {
        LoadLevel();
    }

    public void LoadLevel()
    {
        for (int i = 0; i < prefabLevel.Length; i++)
        {
            prefabLevel[i].SetActive(false);
        }

        prefabLevel[currentLevel - 1].SetActive(true);
    }
}
