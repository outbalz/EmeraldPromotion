using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 화살에 오브젝트 풀링을 구현하지 않았지만, 이후 구현해서 수정한다면, 수정하기 쉽도록 팩토리 사용
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
