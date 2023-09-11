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

    
    private Rigidbody2D _rb;
    public void SetTarget(WeaponData data,int count,Transform target, GameObject[] sideBullets = null)
    {
        _rb = GetComponent<Rigidbody2D>();
        transform.localPosition = GameManager.Instance.Player.transform.position;

        _target = target;

        _damage = data.Damage;
        _count = count;

        _dir = (_target.position - transform.position).normalized;
        
        // 왜 up방향인지..?
        transform.rotation = Quaternion.FromToRotation(Vector3.up,_dir);

        bulletSpeed = count == 5 ? 20.0f : 10.0f; // 레벨이 5면 bullet속도증가
        _rb.velocity = _dir * bulletSpeed;

        var quat = Quaternion.Euler(0, 0, 30);
        var quat2 = Quaternion.Euler(0, 0, -30);
        // 각도에 따라 오브젝트의 방향을 구하는법은 Quaternion을 하나 만들어서 기존 방향벡터를 곱하면
        // 각도가 나온다 단 Quaternion을 먼저 곱해줘야한다.
        Vector3 _dir1 = quat * _dir;
        Vector3 _dir2 = quat2 * _dir;

        if (sideBullets != null)
        {
            for(int i = 1; i < sideBullets.Length; i++)
            {
                sideBullets[i].transform.position = GameManager.Instance.Player.transform.position;
                sideBullets[i].GetComponent<BulletController>()._damage = data.Damage;
                sideBullets[i].GetComponent<BulletController>()._count = count;
                sideBullets[i].GetComponent<BulletController>().bulletSpeed = count == 5 ? 20.0f : 10.0f;
                
            }
            sideBullets[1].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir1);
            sideBullets[1].GetComponent<Rigidbody2D>().velocity = _dir1 * bulletSpeed;
            sideBullets[2].transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir2);
            sideBullets[2].GetComponent<Rigidbody2D>().velocity = _dir2 * bulletSpeed;
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        gameObject.SetActive(false);
    }
}

