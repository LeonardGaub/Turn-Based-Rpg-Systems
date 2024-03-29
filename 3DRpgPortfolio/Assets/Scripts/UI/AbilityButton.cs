﻿using Rpg.BattleSystem.Actions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.BattleSystem.UI
{
    public class AbilityButton : MonoBehaviour
    {
        private BaseAction action;

        public BaseAction Action => action;
        public void SetUp(BaseAction action)
        {
            this.action = action;
            GetComponentInChildren<TextMeshProUGUI>().text = action.name;
        }
    }

}