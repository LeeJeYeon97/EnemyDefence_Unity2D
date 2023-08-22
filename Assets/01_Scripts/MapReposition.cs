using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    private float width;
    private float height;

    private void Start()
    {
        width = GetComponent<CompositeCollider2D>().bounds.size.x;
        height = GetComponent<CompositeCollider2D>().bounds.size.y;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 myPos = transform.position;
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
   
        // 차이가 -값이 나오지않도록 절대값을 적용
        float diffX = Mathf.Abs(myPos.x - playerPos.x);
        float diffY = Mathf.Abs(myPos.y - playerPos.y);

        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        dirX = dirX < 0 ? -1 : 1;
        dirY = dirY < 0 ? -1 : 1;
        // 플레이어가 가로방향의 collider와 충돌했다.
        if (diffX > diffY)
        {
            Debug.Log("Right" + dirX + "," + dirY);
            transform.Translate(Vector3.right * dirX * (width * 2));
        }
        else if (diffX < diffY)
        {
            Debug.Log("Up" + dirX + "," + dirY);
            transform.Translate(Vector3.up * dirY * (height * 2));
        }
    }
}

