using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; //reference to your score text
    public TextMeshProUGUI BeatThisText;
    public TextMeshProUGUI ammoLeft;
    public TextMeshProUGUI TestWinScreen;
    public ThrowSpear throwSpear;
    public bool isPractice;

    public void Start()
    {
        TestWinScreen.enabled = false;
        isPractice = throwSpear.isPracticeMode;
        if (isPractice)
        {
            ammoLeft.enabled = false;
            BeatThisText.enabled = false;
        }
        if (!isPractice)
        {
            ammoRemaining(throwSpear.ammoRemaining);
        }
    }
    public void UpdateScore(int playerScore)
    {
        scoreText.text = "Score: " + playerScore.ToString(); //update the text
    }
    public void ScoreToBeat(int ScoreBeat)
    {
        BeatThisText.text = "Need to Beat: " + ScoreBeat.ToString();
    }
    public void ammoRemaining(int ammoRemains)
    {
        ammoLeft.text = "Spears left: " + ammoRemains.ToString();
    }

    public void YouWin(bool Win)
    {
        if (Win)
        {
            TestWinScreen.enabled = true;
        }
    }
}
