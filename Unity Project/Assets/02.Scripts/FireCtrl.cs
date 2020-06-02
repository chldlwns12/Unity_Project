using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerSfx
{
    public AudioClip[] fire;
    public AudioClip[] reload;
}

public class FireCtrl : MonoBehaviour
{
    //무기타입
    public enum WeaponType
    {
        RIFLE = 0,
        SHOTGUN
    }

    //주인공이 현재 들고 있는 무기를 저장할 변수
    public WeaponType currWeapon = WeaponType.RIFLE;

    //총알 프리팹
    public GameObject bullet;
    //탄피 추출 파티클
    public ParticleSystem cartridge;
    //총구 화염 파티클
    public ParticleSystem muzzleFlash;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource _audio;

    //총알 발사 좌표
    public Transform firePos;
    //오디오 클립을 저장할 변수
    public PlayerSfx playerSfx;

    void Start()
    {
        muzzleFlash = firePos.GetComponentInChildren<ParticleSystem>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        //마우스 왼쪽 버튼을 클릭했을 때 Fire 함수 호출
        if(Input.GetMouseButton(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        //Bullet 프리팹을 동적으로 생성
        Instantiate(bullet, firePos.position, firePos.rotation);
        //파티클 실행
        cartridge.Play();
        //총구 화염 파티클 실행
        muzzleFlash.Play();
        //사운드 발생
        FireSfx();
    }

    private void FireSfx()
    {
        Debug.Log("재생");
        //현재 들고 있는 무기의 오디오 클립을 가져옴

        var _sfx = playerSfx.fire[(int)currWeapon];
        
        //사운드 발생
        _audio.PlayOneShot(_sfx, 1.0f);
    }
}
