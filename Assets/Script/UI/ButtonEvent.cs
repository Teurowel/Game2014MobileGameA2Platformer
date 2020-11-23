using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSFX);
        }
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void PlayButtonSFX()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.Play("ButtonSFX");
        }
        else
        {
            Debug.Log("Should start from main menu scene since SoundManager is only created in menu scene");
        }
    }
}
