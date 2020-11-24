//SourceFileName : GameOverScore.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles score text inside game over screen
//Revision History : Nov.24, 2020 Created, when game over screen start, get score from global data and change text

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScore : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI scoreText = null;
    [SerializeField] TMPro.TextMeshProUGUI lifeBonusText = null;
    [SerializeField] TMPro.TextMeshProUGUI finalScoreText = null;

    // Start is called before the first frame update
    void Start()
    {
        if (GlobalData.instance != null)
        {
            if (scoreText != null)
            {
                scoreText.text = "Score: " + GlobalData.instance.score.ToString();
            }

            if (lifeBonusText != null)
            {
                lifeBonusText.text = "Life Bonus: " + GlobalData.instance.life.ToString() + " X 100";
            }

            if (finalScoreText != null)
            {
                finalScoreText.text = "Final Score: " + (GlobalData.instance.score + (GlobalData.instance.life * 100)).ToString();
            }
        }
        else
        {
            Debug.LogWarning("GlobalData is null, check GameOverScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
