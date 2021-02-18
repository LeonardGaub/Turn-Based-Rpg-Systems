using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEndHandler : MonoBehaviour
{
    private void Awake()
    {
        BattleOverUI.OnContinueButtonPressed += OnContinue;
        BattleOverUI.OnLoadButtonPressed += OnLoad;
    }

    private void OnContinue()
    {
        BattleData.state = BattleData.TransitionState.OutBattleWon;
        Reset();
        SceneManager.LoadScene(0);
    }

    private void OnLoad()
    {
        BattleData.state = BattleData.TransitionState.OutBattleLost;
        Reset();
        SceneManager.LoadScene(0);
    }

    private void Reset()
    {
        BattleOverUI.OnContinueButtonPressed -= OnContinue;
        BattleOverUI.OnLoadButtonPressed -= OnLoad;
    }
}
