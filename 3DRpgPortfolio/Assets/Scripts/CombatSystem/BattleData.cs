using Rpg.BattleSystem.Actors;
using Rpg.BattleSystem.Reward;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Combat/BattleData"))]
public class BattleData : ScriptableObject
{
    public enum TransitionState
    {
        InBattle,
        OutBattleWon,
        OutBattleLost,
        InWorld
    }

    public static TransitionState state;
    public List<Actor> players = new List<Actor>();
    public List<Actor> enemies = new List<Actor>();

    public List<Actor> spawnedPlayers = new List<Actor>();
    public List<Actor> spawnedEnemies = new List<Actor>();

    public RewardData reward;

    public Vector3 originalPlayerPosition;

    public void ClearLists()
    {
        players.Clear();
        enemies.Clear();
        spawnedPlayers.Clear();
        spawnedEnemies.Clear();
        reward = null;
    }
}


