
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
        GameManager.OnRoundBattleEnd += SetSleepButton;
        GameManager.OnRoundPlanEnd += SetBattleButton;

    }
    private void OnDestroy()
    {
        GameManager.OnRoundBattleEnd -= SetBattleButton;
        GameManager.OnRoundSleepEnd -= SetSleepButton;
    }
    void SetSleepButton()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => GameManager.Instance.SleepStage());
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Continue";
    }
    void SetBattleButton()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => GameManager.Instance.BattleStage());
        button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Fight!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
