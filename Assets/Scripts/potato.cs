using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class potato : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Kingdom2");
            Destroy(this.gameObject);
        }
    }
}
