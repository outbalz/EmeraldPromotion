using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHealingItemController : MonoBehaviour
{
    [SerializeField] private float _healAmount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }


        CHPController chara = collision.GetComponent<CHPController>();

        if (chara == null)
        {
           return;
        }

        if(chara.HP >= chara.MaxHP)
        {
            return;
        }

        chara.TakeDamage(-_healAmount);

        Destroy(gameObject);
    }

}
