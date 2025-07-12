using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; //reference to your score text
    public TextMeshProUGUI BeatThisText;
    public TextMeshProUGUI ammoLeft;
    public TextMeshProUGUI TestWinScreen;
    public RawImage[] SpearsLeft;
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
            for (int i = 0; i < SpearsLeft.Length; i++)
            {
                SpearsLeft[i].enabled = false;
            }
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
        SpearsLeft[ammoRemains].enabled = false;
    }

    public void YouWin(bool Win)
    {
        if (Win)
        {
            TestWinScreen.enabled = true;
        }
    }
}
