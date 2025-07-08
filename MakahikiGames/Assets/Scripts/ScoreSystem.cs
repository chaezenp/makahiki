using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int score = 0; // Player's score

    public UIManager uiManager;
    public GameObject target;
    public GameObject spear;

    void Start()
    {
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
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(target.transform.position, 4f);
        Gizmos.DrawWireSphere(target.transform.position, 1f);
        Gizmos.DrawWireSphere(target.transform.position, 2f);
        Gizmos.DrawWireSphere(target.transform.position, 3f);

    }
}
