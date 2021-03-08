using Rpg.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class PlayerGroup : MonoBehaviour, ISaveable
    {
        [SerializeField] List<PlayerActor> players = new List<PlayerActor>();
        [SerializeField] List<PlayerData> datas = new List<PlayerData>();

        public List<PlayerActor> Players => players;

       [HideInInspector] public Vector3 originalPosition => gameObject.transform.position;

        public object CaptureState()
        {
            List<ActorStats> stats = new List<ActorStats>();
            foreach(var data in datas)
            {
                stats.Add(new ActorStats(data.health, data.damage, data.speed));
            }
            return stats;
        }

        public void RestoreState(object state)
        {
            var stats = state as List<ActorStats>;
            if (stats != null)
            {
                for (int i = 0; i < datas.Count; i++)
                {
                    print("Set");
                    datas[i].SetStats(stats[i].health, stats[i].damage, stats[i].speed);
                }
            }
        }
    }

}
