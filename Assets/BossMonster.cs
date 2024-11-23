using System.Collections;
using UnityEngine;

public class BossMonster : MonoBehaviour
{
    public GameObject carrot;          // 당근 프리팹
    public float carrotSpeed = 20f;    // 당근 속도
    public int carrotCount = 10;       // 360도에서 날아가는 당근 개수
    public float attackInterval = 3f;  // 공격 주기
    public float rotationSpeed = 500f; // 공격 중 회전 속도
    private float timeSinceLastAttack;
    private bool isAttacking = false;  // 공격 중 상태 확인용

    public static BossMonster Instance;

    public int Hp = 100;

    private void Awake() {
        Instance = this;
    }

    void Start()
    {
        timeSinceLastAttack = 0f;
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (timeSinceLastAttack >= attackInterval && !isAttacking)
        {
            StartCoroutine(Launch360Carrots());
            timeSinceLastAttack = 0f;
        }

        if (isAttacking)
        {
            RotateBoss();
        }
    }

    void RotateBoss()
    {
        // 보스를 빠르게 회전
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    IEnumerator Launch360Carrots()
    {
        isAttacking = true; // 공격 시작
        float angleStep = 360f / carrotCount;

        for (int i = 0; i < carrotCount; i++)
        {
            float angle = i * angleStep;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;
            ThrowCarrot(direction);

            // 각 당근 발사 후 짧은 대기시간
            yield return new WaitForSeconds(0.1f);
        }

        isAttacking = false; // 공격 종료
    }

    void ThrowCarrot(Vector3 direction)
    {
        // 당근 생성 및 발사
        GameObject newCarrot = Instantiate(carrot, transform.position, Quaternion.identity);
        Rigidbody rb = newCarrot.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * carrotSpeed;
        }
    }
}
