using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    //폭발 효과 프리팹을 저장할 변수
    public GameObject expEffect;
    //총알이 맞은 횟수
    private int hitCount = 0;
    //rigidbody 컴포넌트를 저장할 변수
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //충돌이 발생했을 때 한 번 호출되는 태그를 비교
    private void OnCollisionEnter(Collision coll)
    {
        //충돌한 게임오브젝트의 태그를 비교
        if(coll.collider.CompareTag("BULLET"))
        {
            //총알의 충돌 횟수를 증가시키고 3발 이상 맞았는지 확인
            if(++hitCount ==3)
            {
                ExpBarrel();
            }
        }
    }

    private void ExpBarrel()
    {
        //폭발 효과 프리팹을 동적으로 생성
        Instantiate(expEffect, transform.position, Quaternion.identity);
        //Rigidbody 컴포넌트의 mass를 1.0으로 수정해 무게를 가볍게 함
        rb.mass = 1.0f;
        //위로 솟구치는 힘을 가함
        rb.AddForce(Vector3.up * 1000.0f);
    }
}
