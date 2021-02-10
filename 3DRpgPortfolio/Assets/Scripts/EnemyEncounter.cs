using Rpg.BattleSystem.Actors;
using Rpg.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class EnemyEncounter : MonoBehaviour, ISaveable
    {
        [SerializeField] EncounterData data;
        private bool wasFought;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.GetComponent<PlayerGroup>() != null && !wasFought)
            {
                var players = other.gameObject.GetComponent<PlayerGroup>();
                wasFought = true;
                FindObjectOfType<SavingSystem>().Save("sav.data");
                BattleHandler.Instance.SetUpBattle(players.Players, data.Enemies, players.originalPosition);
            }
        }
        public object CaptureState()
        {
            return wasFought;
        }

        public void RestoreState(object state)
        {
            print(state);
            wasFought = (bool)state;
            UpadteState();
        }

        private void UpadteState()
        {
            if (wasFought)
            {
                Destroy(gameObject);
            }
        }

    }
}
