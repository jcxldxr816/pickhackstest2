
using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using TMPro;

public class StateButtons : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = transform.Find("PlayButton")?.GetComponent<Button>();
        HideButton();

    }
    
    void HideButton()
    {
        button.gameObject.SetActive(false);
    }
    public void SetSleepButton()
    {
        button.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => GameManager.Instance.SleepStage());
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Continue";
    }
    public void SetBattleButton()
    {
        button.gameObject.SetActive(true);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => GameManager.Instance.BattleStage());
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Fight!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
