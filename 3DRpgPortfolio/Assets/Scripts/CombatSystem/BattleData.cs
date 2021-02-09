using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("RPG/Combat/BattleData"))]
public class BattleData : ScriptableObject
{
    public List<Actor> players = new List<Actor>();
    public List<Actor> enemies = new List<Actor>();

    public List<Actor> spawnedPlayers = new List<Actor>();
    public List<Actor> spawnedEnemies = new List<Actor>();

    public Vector3 originalPlayerPosition;

    public void ClearLists()
    {
        players.Clear();
        enemies.Clear();
        spawnedPlayers.Clear();
        spawnedEnemies.Clear();
    }
}
