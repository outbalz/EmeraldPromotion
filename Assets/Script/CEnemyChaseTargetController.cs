using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEnemyChaseTargetController : MonoBehaviour
{
    #region inspector
    [SerializeField] private CCharacterMovementController _movementController;
    [SerializeField] private Transform _targetTr;
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

    }


    private void FixedUpdate()
    {
        if (_targetTr == null)
        {
            return;
        }

        Vector2 moveVector = _targetTr.position - transform.position;
        if(moveVector.sqrMagnitude < 2)
        {
            moveVector = Vector2.zero;
        }

        bool isRunning = (moveVector.sqrMagnitude < 5 * 5);

        _movementController.MoveChararcter(moveVector.normalized, isRunning);
    }

}
