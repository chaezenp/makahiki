using UnityEngine;
using TMPro; 

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; //reference to your score text

    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString(); //update the text
    }
}
