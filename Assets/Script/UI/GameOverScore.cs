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
    TMPro.TextMeshProUGUI text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();

        if (GlobalData.instance != null)
        {
            if (text != null)
            {
                text.text = "Score: " + GlobalData.instance.score.ToString();
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
