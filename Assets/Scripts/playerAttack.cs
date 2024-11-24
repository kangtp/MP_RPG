using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public bool can = false;
    public float attackRadius = 5f; // 플레이어 공격 반경
    public int attackDamage = 10;  // 공격 데미지
    public LayerMask bossLayer;    // 보스 레이어 필터

    public void AttackBoss()
    {
        if(can)
        {
        // 반경 내의 모든 콜라이더 감지
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, bossLayer);

        foreach (Collider hit in hits)
        {
            BossMonster boss = hit.GetComponent<BossMonster>();
            if (boss != null)
            {
                boss.curHp -= 10;
                boss.HandleHp();

            }
        }
        }
    }

    public void AttackBoss1()
    {
        if(can)
        {
        // 반경 내의 모든 콜라이더 감지
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRadius, bossLayer);

        foreach (Collider hit in hits)
        {
            BossMonster2 boss = hit.GetComponent<BossMonster2>();
            if (boss != null)
            {
                boss.curHp -= 10;
                boss.HandleHp();

            }
        }
        }
    }
}
