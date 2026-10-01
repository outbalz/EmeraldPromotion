using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrowFactory : MonoBehaviour
{

    #region inspector
    [SerializeField] private CArrowController _arrowPrefab;
    [SerializeField] private Transform _parent;
    #endregion

    

    public CArrowController CreateArrow(Vector3 pos, Quaternion rot)
    {
        if (_arrowPrefab == null)
        {
            Debug.LogWarning("Missing prefab");

            return null;
        }

        Transform p = (_parent != null) ? _parent : null;


        CArrowController inst = Instantiate(_arrowPrefab, pos, rot, p);

        return inst;
    }

    public CArrowController CreateArrow(Vector3 pos)
    {
        return CreateArrow(pos, Quaternion.identity);
    }

}
