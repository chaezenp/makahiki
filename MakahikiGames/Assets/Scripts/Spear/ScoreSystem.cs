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
    public float fivePt = 1f;
    public float fourPt = 2f;
    public float threePt = 3f;
    public float twoPt = 4f;
    public float onePt = 5f;

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

        if (distance <= fivePt)
        {
            AddScore(5);
            Debug.Log("dist: " + distance);

        }
        if (distance <= fourPt && distance > fivePt)
        {
            AddScore(4);
            Debug.Log("dist: " + distance);
        }
        if (distance <= threePt && distance > fourPt)
        {
            AddScore(3);
            Debug.Log("dist: " + distance);
        }
        if (distance <= twoPt && distance > threePt)
        {
            AddScore(2);
            Debug.Log("dist: " + distance);
        }
        if (distance <= onePt && distance > twoPt)
        {
            AddScore(1);
            Debug.Log("dist: " + distance);
        }
        if (distance > onePt)
        {
            Debug.Log("Miss");
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

        Gizmos.DrawWireSphere(target.transform.position, onePt);
        Gizmos.DrawWireSphere(target.transform.position, twoPt);
        Gizmos.DrawWireSphere(target.transform.position, threePt);
        Gizmos.DrawWireSphere(target.transform.position, fourPt);
        Gizmos.DrawWireSphere(target.transform.position, fivePt);

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
