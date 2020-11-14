//SourceFileName : GameOver.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.14, 2020
//Program description : This script handles GameOver screen ui
//Revision History : Nov.14, 2020 Created, Added PlayAgin function that is called whan play again button clicked
//                                         Added MainMenu function that is called whan main menu button clicked

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameLevel01");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }
}
