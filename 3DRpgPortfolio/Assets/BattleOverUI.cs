using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Rpg.BattleSystem;
using Rpg.BattleSystem.Reward;
using System;

public class BattleOverUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI result;
    [SerializeField] TextMeshProUGUI rewardsText;

    [SerializeField] GameObject rewardsGameObject;
    [SerializeField] Button continueButton;

    public static Action OnContinueButtonPressed;
    public static Action OnLoadButtonPressed;

    private void Awake()
    {
        BattleHandler.onUIShow += Setup;
        Reset();
    }

    public void Setup(bool won, RewardData data)
    {
        Reset();
        result.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
        SetUpContinueButton(won);
        if (won)
        {
            result.text = "Victory";
            rewardsGameObject.SetActive(true);
            rewardsText.gameObject.SetActive(true);
            SetRewards(data);
        }
        else
        {
            result.text = "Defeat";
        }

    }

    private void SetUpContinueButton(bool playersWon)
    {
        if (playersWon)
        {
            continueButton.GetComponentInChildren<Text>().text = "Continue";
            continueButton.onClick.AddListener(Continue);
        }
        else
        {
            continueButton.GetComponentInChildren<Text>().text = "Load latest Save";
            continueButton.onClick.AddListener(LoadLatestSave);
        }
    }

    private void Continue()
    {
        OnContinueButtonPressed?.Invoke();
    }

    private void LoadLatestSave()
    {
        OnLoadButtonPressed?.Invoke();
    }

    private void Reset()
    {
        rewardsGameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        rewardsText.gameObject.SetActive(false);
        result.gameObject.SetActive(false);
    }

    private void SetRewards(RewardData data)
    {
        rewardsText.text = "";
        rewardsText.text = "You gained: " + data.Xp + " Xp \n"
            + "\n" 
            + "Items obtained: " + "\n";
        foreach(var item in data.Items)
        {
            rewardsText.text += item.GetDisplayName() + "\n";
        }
    }
}
