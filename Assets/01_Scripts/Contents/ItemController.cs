using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemController : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer _sprite;

    private Define.ItemList _type;
    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    public void SpawnInit(int index)
    {
        switch(index)
        {
            case 0:
                _type = Define.ItemList.Exp;
                break;

        }
        _sprite.sprite = sprites[index];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        switch(_type)
        {
            case Define.ItemList.Exp:
                collision.GetComponent<PlayerController>().GetExp(5);          
                break;
        }

        gameObject.SetActive(false);
    }
}
