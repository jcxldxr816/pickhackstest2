using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public bool inOptionsMenu = false;

    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button returnButton;
    public Slider volumeSlider;

    void Start()
    {
        playButton = transform.Find("PlayButton")?.GetComponent<Button>();
        optionsButton = transform.Find("OptionsButton")?.GetComponent<Button>();
        quitButton = transform.Find("QuitButton")?.GetComponent<Button>();
        
        returnButton = transform.Find("ReturnButton")?.GetComponent<Button>();
        returnButton.gameObject.SetActive(false);
        
        volumeSlider = transform.Find("VolumeSlider")?.GetComponent<Slider>();
        volumeSlider.gameObject.SetActive(false);
    }
    
    public void playGame()
    {
        SceneManager.LoadScene("SampleScene"); //TODO change this
    }

    public void optionsMenu()
    {
        if (!inOptionsMenu) //Main menu
        {
            inOptionsMenu = true;
            
            playButton.gameObject.SetActive(false);
            optionsButton.gameObject.SetActive(false);
            quitButton.gameObject.SetActive(false);
            
            returnButton.gameObject.SetActive(true);
            volumeSlider.gameObject.SetActive(true);
        }
        else //Options menu
        {
            inOptionsMenu = false;
            
            playButton.gameObject.SetActive(true);
            optionsButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
            
            returnButton.gameObject.SetActive(false);
            volumeSlider.gameObject.SetActive(false);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
