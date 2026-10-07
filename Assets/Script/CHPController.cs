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
    [SerializeField] private CCharacterStateController _stateController;
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
        if (_stateController != null)
        {
            if (_stateController.IsInvincible)
            {
                return;
            }

            else
            {
                _stateController.TakeDamege();
            }
        }

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
