using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Vector2Int[] Levels; 

    public void Exit() 
    {
        Application.Quit();
    }

    public void Play(int level)
    {
        GameInitializer.MazeSize = Levels[level];
        SceneManager.LoadScene("Maze", LoadSceneMode.Single);
    }
}
