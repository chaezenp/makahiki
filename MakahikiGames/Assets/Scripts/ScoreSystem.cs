using System;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class ScoreSystem : MonoBehaviour
{
    private int score = 0; // Player's score

    public UIManager uiManager;
    public ThrowSpear throwSpear;
    public GameObject target;
    public GameObject spear;
    public int scoreToBeat = 5;
    public string nextScene;
    private bool timerOn;
    public int waitime = 5;
    public int savedWait;
    private float timer = 0.0f;
    private int seconds = 0;

    void Start()
    {
        uiManager.ScoreToBeat(scoreToBeat);
        timerOn = false;
        savedWait = waitime;
    }

    public void Hit()
    {
        float distance = Vector3.Distance(target.transform.position, spear.transform.position);

        if (distance < 1)
        {
            AddScore(5);
            Debug.Log("dist: " + distance);

        }
        if (distance < 2 && distance > 1)
        {
            AddScore(4);
            Debug.Log("dist: " + distance);
        }
        if (distance < 3 && distance > 2)
        {
            AddScore(3);
            Debug.Log("dist: " + distance);
        }
        if (distance < 5 && distance > 3)
        {
            AddScore(1);
            Debug.Log("dist: " + distance);
        }

        Debug.Log(score);
    }
    void AddScore(int points)
    {
        score += points;
        uiManager.UpdateScore(score); // Call the UpdateScore method
        if (score >= scoreToBeat)
        {
            uiManager.YouWin(true);
            throwSpear.isWin = true;
            timerOn = true;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(target.transform.position, 4f);
        Gizmos.DrawWireSphere(target.transform.position, 1f);
        Gizmos.DrawWireSphere(target.transform.position, 2f);
        Gizmos.DrawWireSphere(target.transform.position, 3f);

    }

        void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            seconds = (int)(timer % 60);
            if (seconds > waitime)
            {
                timerOn = false;
                timer = 0;
                waitime = savedWait;
                SceneManager.LoadScene(nextScene);

            }
        }
    }
}
