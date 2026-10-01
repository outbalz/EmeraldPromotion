using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCharacterMovementController : MonoBehaviour
{
    #region inspector
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private float _runSpeed = 2.0f;
    #endregion

    #region private var
    private readonly int _hashVelocityX = Animator.StringToHash("fVelocityX");
    private readonly int _hashVelocityY = Animator.StringToHash("fVelocityY");
    private readonly int _hashOnMove = Animator.StringToHash("bOnMove");
    private readonly int _hashIsRunning = Animator.StringToHash("bIsRunning");
    #endregion

    private void Awake()
    {
        if (_rb == null)
        {
            if(!TryGetComponent<Rigidbody2D>(out _rb))
            {
                Debug.LogWarning("Missing Rigidbody2D");
                enabled = false;
                return;
            }
        }

        if (_animator == null)
        {
            if(!TryGetComponent<Animator>(out _animator))
            {
                Debug.LogWarning("Missing Animator");
                enabled = false;
                return;
            }
        }

    }

    private void FixedUpdate()
    {
        MoveChararcter();
    }

    private void MoveChararcter()
    {
        Vector2 moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if(moveVector.sqrMagnitude < 0.0001f)
        {
            _animator.SetBool(_hashOnMove, false);
            _animator.SetBool(_hashIsRunning, false);
            _rb.velocity = Vector2.zero;
            return;
        }

        Vector2.ClampMagnitude(moveVector, 1.0f);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        moveVector *= isRunning ? _runSpeed : _movementSpeed ;

        _rb.velocity = moveVector;

        _animator.SetFloat(_hashVelocityX, moveVector.x);
        _animator.SetFloat(_hashVelocityY, moveVector.y);
        _animator.SetBool(_hashOnMove, true);
        _animator.SetBool(_hashIsRunning, isRunning);
        
    }

}
