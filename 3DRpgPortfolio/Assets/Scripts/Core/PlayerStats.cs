using Rpg;
using Rpg.Inventories;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Equipment))]
public class PlayerStats : MonoBehaviour
{
    private Equipment playerEquipment;
    private PlayerData player;

    public static Action<PlayerData> OnStatsUpdate;

    private void Start()
    {
        playerEquipment = GetComponent<Equipment>();
        playerEquipment.equipmentUpdated += UpdateStats;
        playerEquipment.onCharacterSwitch += ChangedPlayer;
        playerEquipment.onRemoveItem += ItemRemoved;

        OnStatsUpdate?.Invoke(playerEquipment.selectedPlayer);
    }

    private void UpdateStats()
    {
        player = playerEquipment.selectedPlayer;
        foreach(var item in playerEquipment.GetEquipedItems().Values)
        {
            player.AddStats(item.HealthBonus, item.DamageBonus, item.SpeedBonus);
        }
        OnStatsUpdate?.Invoke(player);
    }

    private void ChangedPlayer()
    {
        OnStatsUpdate.Invoke(playerEquipment.selectedPlayer);
    }

    private void ItemRemoved(EquipableItem item)
    {
        player = playerEquipment.selectedPlayer;
        player.RemoveStats(item.HealthBonus, item.DamageBonus, item.SpeedBonus);
        OnStatsUpdate.Invoke(player);
    }

    
}
