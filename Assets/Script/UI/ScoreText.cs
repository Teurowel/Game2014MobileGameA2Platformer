//SourceFileName : ScoreText.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles Score text ui inside game screen
//Revision History : Nov.24, 2020 Created, Added OnScoreChanged so that when score changes, ui text changes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TMPro.TextMeshProUGUI text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();    

        if(GlobalData.instance != null)
        {
            GlobalData.instance.onScoreChanged.AddListener(OnScoreChanged);
        }
        else
        {
            Debug.LogWarning("GlobalData is null, check ScoreText");
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    void OnScoreChanged(int newScore)
    {
        if(text != null)
        {
            text.text = "Score: " + newScore.ToString();
        }
        else
        {
            Debug.LogWarning("TMPro.TextMeshProUGUI is null, check ScoreText");
        }
    }
}
