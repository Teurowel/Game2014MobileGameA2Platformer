//SourceFileName : HealthUI.cs
//Author's name : Doosung Jang
//Studnet Number : 101175013
//Date last Modified : Nov.23, 2020
//Program description : This script handles health ui
//Revision History : Nov.23, 2020 Created

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab = null;//Health UI prefab

    Transform target = null; //Target that UI has to follow
    Transform ui; //Health UI itself
    Image healthSlider; //Greenpart of Health UI

    Transform cam; //Main camera

    float visibleTime = 5f; //After 5 seconds, ui will be invisible
    float lastVisibleTime = 0f;

    public float yOffset = 2f; //Y offset from target's position

    // Start is called before the first frame update
    void Start()
    {
        //Find camera
        cam = Camera.main.transform;

        //Find worldspace Canvas so that we can add Healt UI
        foreach (Canvas c in FindObjectsOfType<Canvas>())
        {
            if (c.renderMode == RenderMode.WorldSpace)
            {
                //Create Health UI and make it as child of Canvas
                ui = Instantiate(uiPrefab, c.transform).transform;
                //Get green part of Health UI which is child of UI itself
                healthSlider = ui.GetChild(0).GetComponent<Image>();

                //It is not visible at first
                ui.gameObject.SetActive(false);
                break;
            }
        }

        //Subscribe this event
        GetComponent<Stats>().onHealthChanged.AddListener(OnHealthChangedCallback);

        target = gameObject.transform;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void LateUpdate()
    {
        if (ui != null && target != null)
        {
            //Set Health UI's position to target(player ,enemy)'s position
            ui.position = new Vector3(target.position.x, target.position.y + yOffset, target.position.z);

            //Align UI that makes UI always face to camera
            ui.forward = -cam.forward;

            //Check visible time
            if ((ui.gameObject.activeSelf == true) && (Time.time - lastVisibleTime > visibleTime))
            {
                ui.gameObject.SetActive(false);  
            }
        }
    }

    void OnHealthChangedCallback(int health, int maxHealth)
    {
        if (ui != null)
        {
            //Make health UI visible
            ui.gameObject.SetActive(true);
            lastVisibleTime = Time.time; //Save the last time when UI was visible

            //Calculate healthPercent and set it to ui
            float healthPercent = (float)health / (float)maxHealth;
            healthSlider.fillAmount = healthPercent;

            //If character was dead, destroy ui
            if (health <= 0)
            {
                Destroy(ui.gameObject);
            }
        }
    }
}
