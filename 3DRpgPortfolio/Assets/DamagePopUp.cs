using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI damageText;

    public void SetUp(int damage)
    {
        damageText.text = damage.ToString();
        anim.SetTrigger("Activate");
    }

    public void DestroyPopUp()
    {
        Destroy(this.transform.parent.gameObject);
    }
}
