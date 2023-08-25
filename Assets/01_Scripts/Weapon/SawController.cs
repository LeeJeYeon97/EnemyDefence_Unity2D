using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : WeaponController
{
    public float RotationSpeed;
    public float sawRotationSpeed;

    private Transform[] saws;

    public int count;

    private void Awake()
    {
        RotationSpeed = 200.0f;
        sawRotationSpeed = 700.0f;
        attackPower = 1;
        saws = new Transform[count];
        CreateSaws();
    }

    void Update()
    {
        SawsRotation();
    }
    private void SawsRotation()
    {
        // 캐릭터 주변 회전
        //transform.eulerAngles += new Vector3(0, 0, RotationSpeed * Time.deltaTime);
        transform.Rotate(Vector3.back * RotationSpeed * Time.deltaTime);

        // 칼날 회전
        for (int i = 0; i< saws.Length; i++)
        {
            saws[i].Rotate(Vector3.forward * sawRotationSpeed * Time.deltaTime);
        }
    }

    private void CreateSaws()
    {
        GameObject prefabs = Resources.Load<GameObject>("Weapon/Saw");
        for (int i = 0; i < count; i++)
        {
            GameObject saw = Instantiate<GameObject>(prefabs);
            saw.transform.parent = transform;

            Vector3 rotVec = Vector3.forward * 360 * i / count;
            saw.transform.Rotate(rotVec);
            saw.transform.Translate(saw.transform.up * 1.5f, Space.World);

            saws[i] = saw.transform;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
            collision.GetComponent<CreatureController>().OnDamge(attackPower);
    }
}
