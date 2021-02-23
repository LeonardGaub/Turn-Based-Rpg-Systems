using Rpg.BattleSystem.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpUI : MonoBehaviour
{
    private Vector2 uiOffset;
    [SerializeField] private GameObject damagePopUp;

    private void Awake()
    {
           
    }
    public void SpawnDamagePopUp(int damage , Actor target)
    {
        var popUp = Instantiate(damagePopUp, this.transform);
        popUp.GetComponentInChildren<DamagePopUp>().SetUp(damage);
        MovePopUp(target.transform.position);
    }

    private void MovePopUp(Vector3 popUpPosition)
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(popUpPosition);  

        GetComponent<RectTransform>().anchorMin = viewportPoint;
        GetComponent<RectTransform>().anchorMax = viewportPoint;
    }
}
    