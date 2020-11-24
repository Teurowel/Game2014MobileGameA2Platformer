//SourceFileName : GlobalData.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.24, 2020
//Program description : This script handles global data such as life or scroe
//Revision History : Nov.24, 2020 Created, Added UnityEvent for onScoreCHanged and onLifeChanged
//                                When game level loaded, reset score and life

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        if (life <= 0)
        {
            SceneManager.LoadScene("GameOverScreen");
        }
        else
        {
            if (onLifeChanged != null)
            {
                onLifeChanged.Invoke();
            }
        }
    }


    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Level Loaded");
        Debug.Log(scene.name);
        Debug.Log(mode);

        //If we loaded game level, reset score and life
        if(scene.name == "GameLevel01")
        {
            score = 0;
            life = 3;
        }
    }
}
