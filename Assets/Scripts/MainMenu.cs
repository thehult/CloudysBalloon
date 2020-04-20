using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartEasy()
    {
        GameManager.MainMenuData.Difficulty = Difficulty.Easy;
        SceneManager.LoadScene("GameScene");
    }

    public void StartMedium()
    {
        GameManager.MainMenuData.Difficulty = Difficulty.Medium;
        SceneManager.LoadScene("GameScene");
    }

    public void StartHard()
    {
        GameManager.MainMenuData.Difficulty = Difficulty.Hard;
        SceneManager.LoadScene("GameScene");
    }

}

public struct MainMenuData
{
    public Difficulty Difficulty;
}

public enum Difficulty
{
    NotSet = 0,
    Easy = 1,
    Medium = 2,
    Hard = 3
}