using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterStateController : MonoBehaviour
{
    public enum ECharacterState
    {
        Idle,
        OnMove,
        Running,
        TakeDamaged,
        Death
    }

    #region inspector
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _hasInvincibleTime = true;
    [SerializeField] private bool _stunOnHit = false;
    #endregion

    #region private var
    private readonly int _hashVelocityX = Animator.StringToHash("fVelocityX");
    private readonly int _hashVelocityY = Animator.StringToHash("fVelocityY");
    private readonly int _hashOnMove = Animator.StringToHash("bOnMove");
    private readonly int _hashIsRunning = Animator.StringToHash("bIsRunning");
    private readonly int _hashTakeDamage = Animator.StringToHash("tTakeDamage");

    private ECharacterState _state;
    private bool _isInvincible = false;
    #endregion

    public bool IsInvincible
    {
        get
        {
            if (_hasInvincibleTime)
            {
                return _isInvincible;
            }

            return false;
        }
    }


    private void OnEnterState(ECharacterState state)
    {
        switch (_state)
        {
            case ECharacterState.Idle:
                break;
            case ECharacterState.OnMove:
                _animator.SetBool(_hashOnMove, true);
                break;
            case ECharacterState.Running:
                _animator.SetBool(_hashOnMove, true);
                _animator.SetBool(_hashIsRunning, true);
                break;
            case ECharacterState.TakeDamaged:
                _animator.SetTrigger(_hashTakeDamage);
                _isInvincible = true;
                break;
            case ECharacterState.Death:
                break;
            default:
                break;
        }
    }

    private void OnExitState(ECharacterState state)
    {

        switch (_state)
        {
            case ECharacterState.Idle:
                break;
            case ECharacterState.OnMove:
                _animator.SetBool(_hashOnMove, false);
                break;
            case ECharacterState.Running:
                _animator.SetBool(_hashOnMove, false);
                _animator.SetBool(_hashIsRunning, false);
                break;
            case ECharacterState.TakeDamaged:
                break;
            case ECharacterState.Death:
                break;
            default:
                break;
        }

    }


    private void ChangeState(ECharacterState state)
    {
        if (_state == state)
        {
            return;
        }

        OnExitState(_state);

        _state = state;

        OnEnterState(_state);
    }

    public void OnMove(Vector2 moveVector, bool isRunning)
    {
        _animator.SetFloat(_hashVelocityX, moveVector.x);
        _animator.SetFloat(_hashVelocityY, moveVector.y);

        if (isRunning)
        {
            ChangeState(ECharacterState.Running);
        }
        else
        {
            ChangeState(ECharacterState.OnMove);
        }
    }

    public void TakeDamege()
    {
        ChangeState(ECharacterState.TakeDamaged);
    }

    private void InvincibleTimeEnd()
    {
        _isInvincible = false;
        ChangeState(ECharacterState.Idle);
    }
}