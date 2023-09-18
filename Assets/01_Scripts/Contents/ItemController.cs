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

    private float curRadius;

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
            Vector3 dir = GameManager.Instance.Player.transform.position
                - transform.position;

            transform.Translate(dir.normalized * Time.deltaTime * 7.0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isHit && collision.CompareTag("ItemCollider"))
        {
            // 자석을 먹은 상태이면 exp만 자석 영향을 받도록
            if(GameManager.Instance.Player.itemCollider.radius == 100.0f)
            {
                switch (_type)
                {
                    case Define.ItemList.Mag:
                    case Define.ItemList.Health:
                    case Define.ItemList.Chest:
                        break;
                    default:
                        isHit = true;
                        break;
                }
            }
            else
                isHit = true;
        }
        else if(isHit && collision.CompareTag("Player")) // 아이템 획득하기
        {
            switch (_type)
            {
                case Define.ItemList.Mag:
                    GameManager.Instance.Player.itemCollider.radius = 100.0f;
                    gameObject.SetActive(false);
                    break;
                case Define.ItemList.Health:
                    collision.GetComponent<PlayerStatController>().curHp += 30;
                    gameObject.SetActive(false);
                    break;
                case Define.ItemList.Chest:
                    // 무기 랜덤 뽑기
                    if(GameManager.Instance.Player._weaponList.Count 
                        == System.Enum.GetValues(typeof(Define.WeaponType)).Length)
                    {
                        Managers.UI.ShowPopupUI<UI_LevelUpPopup>("LevelUpPopup");
                    }
                    else
                    {
                        Managers.UI.ShowPopupUI<UI_SelectPopup>("SelectPopup");
                    }
                    GameManager.Instance.IsPause = true;
                    gameObject.SetActive(false);
                    break;
                default:
                    collision.GetComponent<PlayerStatController>().GetExp(exp);
                    gameObject.SetActive(false);
                    break;
            }
            SoundManager.instance.PlaySfx(Define.Sfx.GetItem);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    private void RandomItem()
    {
        // 2% 확률로 경험치가 아닌 자석과 체력, 상자 아이템으로 스폰
        int randInt = Random.Range(0, 50);
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
