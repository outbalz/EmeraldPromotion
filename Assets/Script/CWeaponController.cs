using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CWeaponController : MonoBehaviour
{
    #region inspector
    [SerializeField] private CArrowFactory _arrowFactory;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _pivotTransform;
    #endregion

    #region privat var
    private Camera _camera;
    private readonly int _hashDraw = Animator.StringToHash("tDraw");
    private readonly int _hashRelese = Animator.StringToHash("tRelese");
    private bool _isBowDrawn = false;
    #endregion

    private void Awake()
    {
        _camera = Camera.main;

        if(_arrowFactory == null)
        {
            Debug.LogWarning("Missing CArrowFactory");
            enabled = false;
            return;
        }


        if (_animator == null)
        {
            Debug.LogWarning("Missing Animator");
            enabled = false;
            return;
        }
    }


    void Update()
    {
        AimBow();

        if (Input.GetMouseButton(0))
        {
            if (!_isBowDrawn)
            {
                DrawBow();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleseBow();
        }
    }

    private void AimBow()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 rot = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        _pivotTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void DrawBow()
    {
        _animator.SetTrigger(_hashDraw);
        _isBowDrawn = true;
    }

    private void ReleseBow()
    {
        _animator.SetTrigger(_hashRelese);
    }

    private void ShotArrow()
    {
        CArrowController arrow = _arrowFactory.CreateArrow(_pivotTransform.position, _pivotTransform.rotation);
        _isBowDrawn = false;
    }


}
