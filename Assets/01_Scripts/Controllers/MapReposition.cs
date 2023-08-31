using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapReposition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
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

        switch(transform.tag)
        {
            case "Ground":
                float width = GetComponent<CompositeCollider2D>().bounds.size.x;
                float height = GetComponent<CompositeCollider2D>().bounds.size.y;
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * (width * 2));
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * (height * 2));
                }
                break;

            case "Monster":
                if(coll.enabled)
                {
                    Vector3 dir = new Vector3(dirX, dirY,0);
                    transform.Translate(dir.normalized * 10 + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0));
                }
                break;
            
        }
        
    }
}

