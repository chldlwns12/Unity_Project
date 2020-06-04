using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    private const string bulletTag = "BULLET";
    private const string enemyTag = "ENEMY";

    private float initHp = 100.0f;
    public float currHp;
    //BloodScreen 텍스처를 저장하기 위한 변수
    public Image bloodScreen;

    //델리게이트 및 이벤트 선언
    public delegate void PlayerDieHandler();
    public static event PlayerDieHandler OnPlayerDie;

    // Start is called before the first frame update
    void Start()
    {
        currHp = initHp;
    }

    private void OnTriggerEnter(Collider coll)
    {
        //충돌한 Collider의 태그가 BULLET이면 Player의 currHp를 차감
        if(coll.tag == bulletTag)
        {
            Destroy(coll.gameObject);

            //혈흔 효과를 표현할 코루틴 함수 호출
            StartCoroutine(ShowBloodScreen());

            currHp -= 5.0f;
            Debug.Log("Player HP = " + currHp.ToString());

            //Player의 생명이 0 이하이면 사망 처리
            if(currHp <= 0.0f)
            {
                PlayerDie();
            }
        }
    }

    IEnumerator ShowBloodScreen()
    {
        //BloodScreen 텍스처의 알파값을 불규칙하게 변경
        bloodScreen.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        //BloodScreen 텍스처의 색상을 모두 0으로 변경
        bloodScreen.color = Color.clear;
    }

    //Player의 사망 처리 루틴
    private void PlayerDie()
    {
        OnPlayerDie();
        //Debug.Log("PlayerDie !");
        ////"ENEMY" 태그로 지정된 모든 적 캐릭ㅌ어를 추출해 배열에 저장
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        //
        ////배열의 처음부터순회하면서 적 캐릭터의 OnPlayerDie 함수를 호출
        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    enemies[i].SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
        //}
    }
}
