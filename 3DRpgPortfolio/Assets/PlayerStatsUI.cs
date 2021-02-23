using Rpg.Inventories;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rpg.UI.Stats
{
    public class PlayerStatsUI : MonoBehaviour
    {
        [SerializeField] Slider hpBar;
        [SerializeField] TextMeshProUGUI HpText;
        [SerializeField] TextMeshProUGUI attackText;
        [SerializeField] TextMeshProUGUI speedText;

        private void Start()
        {
            PlayerStats.OnStatsUpdate += Redraw;
        }

        private void Redraw(PlayerData data)
        {
            var currentHpPercent = System.Math.Round((float)data.health / (float)data.maxHealth, 1);
            hpBar.value = (float)currentHpPercent * 100;
            attackText.text = "Attack: " + data.damage;
            speedText.text = "Speed: " + data.speed;
            HpText.text = data.health + "/" + data.maxHealth;
        }
    }
}
