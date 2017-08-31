using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

    public GameObject options;
    public Button play;
    public Button settings;
    public Button exit;

    public GameObject sure;
    public Button yes;
    public Button no;

    public GameObject setSettings;
    public Button back;
    public Slider audioSlider;


    public void PlayPress()
    {
        //Lobby wird aufgerufen
    }

    public void ExitPress()
    {
        options.SetActive(false);
        sure.SetActive(true);
    }

    public void YesPress()
    {
        Application.Quit();
    }

    public void NoPress()
    {
        options.SetActive(true);
        sure.SetActive(false);
    }


    public void SettingsPress()
    {
        options.SetActive(false);
        setSettings.SetActive(true);
    }

    public void AudioSliderMove()
    {

    }

    public void BackPress()
    {
        options.SetActive(true);
        setSettings.SetActive(false);
    }

}
