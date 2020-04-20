using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static MainMenuData MainMenuData;

    [Header("Game Objects")]
    public GameObject PlayerCloud;
    public int NumberOfBalloons;
    public GameObject BalloonPrefab;

    [Header("UI Elements")]
    public TMPro.TMP_Text ScoreText;
    public GameObject GameOverContainer;
    public TMPro.TMP_Text GameOverScoreText;

    bool playing = true;
    int score = 0;
    GameObject[] balloons;
    public GameObject[] Balloons { get
        {
            return balloons;
        } }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        StartGame();
    }

    public void StartGame()
    {
        if(MainMenuData.Difficulty != Difficulty.NotSet)
        {
            NumberOfBalloons = (int)MainMenuData.Difficulty;
        }

        Cursor.visible = false;
        ScoreText.gameObject.SetActive(true);
        GameOverContainer.SetActive(false);

        PlayerCloud.transform.position = Vector2.zero;
        balloons = new GameObject[NumberOfBalloons];
        for (int i = 0; i < NumberOfBalloons; i++)
        {
            balloons[i] = Instantiate(BalloonPrefab, new Vector2(Random.Range(-2f, 2f), 3f), Quaternion.identity);
        }
        ScoreText.text = "0";
        score = 0;
        playing = true;
        ThreatManager.instance.StartSpawner();
        StartCoroutine(AddScore());
    }

    public void GameOver()
    {
        ThreatManager.instance.Cleanup();
        for (int i = 0; i < NumberOfBalloons; i++)
        {
            Destroy(balloons[i]);
        }
        Cursor.visible = true;
        ScoreText.gameObject.SetActive(false);
        GameOverContainer.SetActive(true);
        GameOverScoreText.text = "SCORE: " + score;
        playing = false;
    }

    public bool IsPlaying()
    {
        return playing;
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator AddScore()
    {
        while(playing)
        {
            score += 1;
            ScoreText.text = score.ToString();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
