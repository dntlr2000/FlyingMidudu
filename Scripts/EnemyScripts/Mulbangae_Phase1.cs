using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mulbangae_Phase1 : Enemy_Boss
{
    [Header("프리팹 할당")] 
    public GameObject MechHand_L; // 왼손 프리팹
    public GameObject MechHand_R; // 오른손 프리팹
    public GameObject HeavyMachinegun; //헤비머신건
    public GameObject HOS; //히오스


    protected override void Start()
    {
        //base.Start();
        //물방개 보스는 Start 메서드를 시간을 두고 실행시켜야 함
    }
}
