using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrowController : MonoBehaviour
{

    private float _lifetime = 5;

    private void Update()
    {
        if (_lifetime > 0)
        {
            _lifetime -= Time.deltaTime;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position +=  transform.rotation * Vector3.right * 1f;
    }

}
