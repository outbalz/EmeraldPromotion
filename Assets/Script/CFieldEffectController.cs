using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CFieldEffectController : MonoBehaviour
{

    #region inspector
    [SerializeField] private float _healAmount;
    #endregion


    #region private var
    private List<CHPController> _effectedCharacters = new List<CHPController>();
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        CHPController chara = collision.GetComponent<CHPController>();

        if (chara != null)
        {
            _effectedCharacters.Add(chara);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        
        CHPController chara = collision.GetComponent<CHPController>();

        if (chara != null)
        {
            if (_effectedCharacters.Contains(chara))
            {
                _effectedCharacters.Remove(chara);
            }
        }
    }

    private void Update()
    {
        if( _effectedCharacters.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < _effectedCharacters.Count; i++)
        {
            _effectedCharacters[i].TakeDamage(-_healAmount * Time.deltaTime, true);
        }
    }
}
