using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster2 : MonoBehaviour
{
    public GameObject carrot;          // 당근 프리팹
    public Transform player;           // 플레이어 Transform
    public Transform firepos;
    public float carrotSpeed = 20f;    // 당근 속도
    public float fireRate = 0.2f;      // 당근 발사 간격
    public int carrotCount = 10;       // 한 번에 발사할 당근 개수
    private float maxHp = 100;
    public float curHp = 100;

    public GameObject npc;

    private Animator animator;

    public Slider hpBar;

    void Start()
    {
        hpBar.value = curHp / maxHp;
        animator = GetComponent<Animator>();
    }

    public void HandleHp()
    {
        hpBar.value = curHp / maxHp;
        if(curHp < 1)
        {
            animator.SetBool("Die",true);
            StartCoroutine(OnNpc());
        }
    }

    /// <summary>
    /// 애니메이션 이벤트에서 호출될 당근 10개 발사 함수
    /// </summary>
    public void FireCarrotSequence()
    {
        StartCoroutine(FireCarrotsCoroutine());
    }

    private IEnumerator FireCarrotsCoroutine()
    {
        if (player == null) yield break;

        for (int i = 0; i < carrotCount; i++)
        {
            // 플레이어 방향 계산
            Vector3 direction = (player.position - transform.position).normalized;

            // 당근 생성 및 발사
            GameObject newCarrot = Instantiate(carrot, firepos.position, Quaternion.identity);
            Rigidbody rb = newCarrot.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * carrotSpeed;
            }

            // 발사 간격 대기
            yield return new WaitForSeconds(fireRate);
        }
    }

    private IEnumerator OnNpc()
    {
        yield return new WaitForSeconds (3.5f);
        npc.SetActive(true);
    }
}
