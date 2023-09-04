using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class ItemController : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _sprite;
    private Rigidbody2D _rigidBody;

    private Define.ItemList _type;
    private float[] exps;

    private float exp;
    private bool isHit;
    private bool isFollow;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }
    public void SpawnInit(int index)
    {
        switch(index)
        {
            case 0:
                _type = Define.ItemList.Exp0;
                exp = 5;
                break;
            case 1:
                _type = Define.ItemList.Exp1;
                exp = 7;
                break;
            case 2:
                _type = Define.ItemList.Exp2;
                exp = 10;
                break;
            case 3:
                _type = Define.ItemList.Exp3;
                exp = 12;
                break;
        }
        _sprite.sprite = sprites[index];
        RandomItem();
        isHit = false;
        isFollow = false;
    }
    private void Update()
    {
        if (isHit && !isFollow)
        {
            Vector3 dir = transform.position -
                GameManager.Instance.Player.transform.position;
        
            transform.Translate(dir.normalized * Time.deltaTime * 10.0f);

            Debug.Log("Hit");
            if (dir.magnitude > 1.5f)
            {
                isFollow = true;
            }
        }
        if(isFollow)
        {
            Vector3 dir = GameManager.Instance.Player.transform.position
                - transform.position;
        
            transform.Translate(dir.normalized * Time.deltaTime * 7.0f);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isHit && collision.CompareTag("ItemCollider"))
        {
            isHit = true;
        }
        else if(isHit && collision.CompareTag("Player")) // 아이템 획득하기
        {
            switch (_type)
            {
                case Define.ItemList.Mag:
                    StartCoroutine(CoMagItem());
                    break;
                case Define.ItemList.Health:
                    collision.GetComponent<PlayerStatController>().curHp += 30;
                    break;
                case Define.ItemList.Chest:
                    // 무기 랜덤 뽑기
                    break;
                default:
                    collision.GetComponent<PlayerStatController>().GetExp(exp);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
    IEnumerator CoMagItem()
    {
        float curRadius = GameManager.Instance.Player.itemCollider.radius;
        GameManager.Instance.Player.itemCollider.radius = 100.0f;
        yield return new WaitForSeconds(1.0f);
        GameManager.Instance.Player.itemCollider.radius = curRadius;
    }
    private void RandomItem()
    {
        // 2.5% 확률로 경험치가 아닌 자석과 체력, 상자 아이템으로 스폰
        int randInt = Random.Range(0, 40);
        switch(randInt)
        {
            case 4: // 자석
                _type = Define.ItemList.Mag;
                break;
            case 5: // 체력
                _type = Define.ItemList.Health;
                break;
            case 6: // 상자
                _type = Define.ItemList.Chest;
                break;
            default:
                return;
        }
        _sprite.sprite = sprites[randInt];
    }
}
