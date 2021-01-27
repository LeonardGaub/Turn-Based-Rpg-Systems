using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rpg.BattleSystem
{
    public class Actor : MonoBehaviour
    {
        public IEnumerable ActionFinshed()
        {
            print("Action");
            yield return StartCoroutine(WaitForAction());
        }
        private IEnumerator WaitForAction()
        {
            print("test");
            new WaitUntil(() => Input.GetKeyDown(KeyCode.B));
            Debug.Log("Hey");
            return null;
        }
    }
}