using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletController : MonoBehaviour
{
    public Transform _target;
    private float _damage;
    public float bulletSpeed;
    private Vector3 _dir;
    private int _count;

    public void SetTarget(Transform target,WeaponData data,int count)
    {
        transform.position = GameManager.Instance.Player.transform.position;
        //float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        _target = target;
        _damage = data.Damage;
        _count = count;
        bulletSpeed = count == 5 ? 20.0f :10.0f; // 레벨이 5이면 bullet속도증가
        _dir = _target.position - transform.position;

    }
    private void Update()
    {
        if(_target != null)
        {
            transform.Translate(_dir.normalized * bulletSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Monster")) return;

        _count--;
        
        collision.GetComponent<CreatureController>().OnDamge(_damage);

        if(_count <= 0)
            gameObject.SetActive(false);
    }
}
