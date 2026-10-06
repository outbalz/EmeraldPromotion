using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CArrowController : MonoBehaviour
{
    [SerializeField] private float _damage = 1f;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            CHPController _ememyHP = collision.collider.GetComponent<CHPController>();

            _ememyHP.TakeDamage(_damage);
            Debug.Log("!!");
            Destroy(gameObject);
        }
    }

}
