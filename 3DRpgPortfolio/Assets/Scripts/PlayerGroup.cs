using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem.Actors
{
    public class PlayerGroup : MonoBehaviour
    {
        [SerializeField] List<PlayerActor> players = new List<PlayerActor>();

        public List<PlayerActor> Players => players;

       [HideInInspector] public Vector3 originalPosition => gameObject.transform.position;
    }

}
