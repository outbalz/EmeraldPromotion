using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CWeaponController : MonoBehaviour
{
    #region inspector
    [SerializeField] private CArrowFactory _arrowFactory;
    [SerializeField] private Animator _animator;
    #endregion

    #region privat var
    private Camera _camera;
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

        if (Input.GetMouseButtonDown(0))
        {
            ShotArrow();
        }
    }

    private void AimBow()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 rot = (mousePos - transform.position).normalized;
        float angle = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShotArrow()
    {
        CArrowController arrow = _arrowFactory.CreateArrow(transform.position, transform.rotation);
    }



}
