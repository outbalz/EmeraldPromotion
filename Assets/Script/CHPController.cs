using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CHPController : MonoBehaviour
{
    #region inspector
    [SerializeField] private float _hp;
    [SerializeField] private float _maxhp;
    [SerializeField] private Slider _hpBar;
    #endregion

    private void Awake()
    {
        if(_hpBar != null)
        {
            _hpBar.maxValue = _maxhp;
            _hpBar.SetValueWithoutNotify(_hp);
        }
    }

    public void TakeDamage(float damage)
    {
        _hp -= damage;

        if( _hp < 0)
        {
            gameObject.SetActive(false);
            return;
        }

        if (_hpBar != null)
        {
            _hpBar.SetValueWithoutNotify(_hp);
        }
    }


}
