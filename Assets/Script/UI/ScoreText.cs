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
