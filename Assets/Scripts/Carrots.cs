using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrots : MonoBehaviour
{
    void Start()
    {
        // 2.5초 후 이 게임 오브젝트를 삭제
        Destroy(gameObject, 2.5f);
    }

}
