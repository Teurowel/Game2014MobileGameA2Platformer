using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;

    public int score = 0;
    public int life = 3;

    //score
    public UnityEvent<int> onScoreChanged; //ScoreText will subscribe this
    public UnityEvent onLifeChanged; //LifeCounter will subscribve this

    private void Awake()
    {
        //Make sure there is only one instance of SoundManager;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Make it persist through levels
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int addingScore)
    {
        score += addingScore;

        if(onScoreChanged != null)
        {
            onScoreChanged.Invoke(score);
        }
    }

    public void DecreaseLife()
    {
        life -= 1;

        if(onLifeChanged != null)
        {
            onLifeChanged.Invoke();
        }
    }
}
