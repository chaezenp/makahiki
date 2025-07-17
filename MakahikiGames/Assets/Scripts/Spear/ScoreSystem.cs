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
    public Vector3 spearTip;
    public GameObject spearHead;

    public int scoreToBeat = 5;
    public string nextScene;
    public bool isPractice;
    private bool timerOn;
    public int waitime = 5;
    public int savedWait;
    private float timer = 0.0f;
    private int seconds = 0;
    public float maxPt = 1f;
    public float sndPt = 2f;
    public float thrdPT = 3f;
    public float lastPt = 4f;

    void Start()
    {
        uiManager.ScoreToBeat(scoreToBeat);
        timerOn = false;
        savedWait = waitime;
        isPractice = throwSpear.isPracticeMode;
    }

    public void Hit(Vector3 impactPoint)
    {
        float distance = Vector3.Distance(impactPoint, target.transform.position);

        if (distance < maxPt)
        {
            AddScore(5);
            Debug.Log("dist: " + distance);

        }
        if (distance < sndPt && distance > maxPt)
        {
            AddScore(3);
            Debug.Log("dist: " + distance);
        }
        if (distance < thrdPT && distance > sndPt)
        {
            AddScore(2);
            Debug.Log("dist: " + distance);
        }
        if (distance < lastPt && distance > thrdPT)
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
        if (score >= scoreToBeat && !isPractice)
        {
            uiManager.YouWin(true);
            throwSpear.isWin = true;
            timerOn = true;
        }
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(target.transform.position, lastPt);
        Gizmos.DrawWireSphere(target.transform.position, maxPt);
        Gizmos.DrawWireSphere(target.transform.position, sndPt);
        Gizmos.DrawWireSphere(target.transform.position, thrdPT);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(spearTip, target.transform.position);

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
        spearTip = spearHead.transform.position;
    }
}
