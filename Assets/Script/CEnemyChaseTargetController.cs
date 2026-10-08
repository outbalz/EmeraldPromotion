using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyChaseTargetController : MonoBehaviour
{
    #region inspector
    [SerializeField] private CCharacterMovementController _movementController;
    [SerializeField] private CCharacterStateController _state;
    [SerializeField] private Transform _targetTr;
    [SerializeField] private float _attackDamage;
    #endregion

    #region private var
    private bool _playerDeathBinded = false;
    #endregion


    private void Awake()
    {
        if (_movementController == null)
        {
            if (!TryGetComponent<CCharacterMovementController>(out _movementController))
            {
                Debug.LogWarning("Missing Movement Controller");
                enabled = false;
                return;
            }
        }

        if (_state == null)
        {
            if (!TryGetComponent<CCharacterStateController>(out _state))
            {
                Debug.LogWarning("Missing State Controller");
                enabled = false;
                return;
            }
        }

    }

    private void Start()
    {
        if(_playerDeathBinded == true || _targetTr == null)
        {
            return;
        }

        CCharacterStateController playerState = _targetTr.GetComponent<CCharacterStateController>();

        if (playerState != null)
        {
            playerState.OnDeathEvent += OnPlayerDeath;
            _playerDeathBinded = true;
        }
    }

    private void OnEnable()
    {
        if(_playerDeathBinded == true || _targetTr == null)
        {
            return;
        }

        CCharacterStateController playerState = _targetTr.GetComponent<CCharacterStateController>();

        if (playerState != null)
        {
            playerState.OnDeathEvent += OnPlayerDeath;
            _playerDeathBinded = true;
        }        
    }

    private void OnDisable()
    {
        if(_playerDeathBinded == false || _targetTr == null)
        {
            return;
        }

        CCharacterStateController playerState = _targetTr.GetComponent<CCharacterStateController>();

        if (playerState != null)
        {
            playerState.OnDeathEvent -= OnPlayerDeath;
            _playerDeathBinded = false;
        }         
    }


    private void OnPlayerDeath()
    {
        _movementController.MoveChararcter(Vector2.zero, false);
        enabled = false;
    }

    private void FixedUpdate()
    {
        if (_targetTr == null)
        {
            enabled = false;
            return;
        }

        if(_state.State == CCharacterStateController.ECharacterState.TakeDamaged)
        {
            return;
        }

        Vector2 moveVector = _targetTr.position - transform.position;

        if(moveVector.sqrMagnitude < 2)
        {
            moveVector = Vector2.zero;
            _state.ChangeState(CCharacterStateController.ECharacterState.Attack);
        }

        bool isRunning = (moveVector.sqrMagnitude < 5 * 5);

        _movementController.MoveChararcter(Vector2.ClampMagnitude(moveVector, 1), isRunning);
    }

    private void AttackTarget()
    {
        if (_targetTr == null)
        {
            enabled = false;
            return;
        }

        float sqrDistance = (_targetTr.position - transform.position).sqrMagnitude;

        if(sqrDistance > 5)
        {
            return;
        }

        CHPController enemyHP;

        if(_targetTr.TryGetComponent<CHPController>(out enemyHP))
        {
            enemyHP.TakeDamage(_attackDamage);
        }
    }

}
