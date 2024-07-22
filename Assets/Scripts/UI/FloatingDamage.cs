using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{   
    [SerializeField] float moveSpeed; //텍스트 이동속도
    [SerializeField] float alphaSpeed; //투명도 전환 속도
    [SerializeField] float destroyTime; // 수명
    TextMeshPro tmp;
    MeshRenderer meshRenderer;
    Color alpha;
    float damage;

    public int testVal = 10;

    void Start()
    {     
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.sortingLayerName = "UI";
        tmp = GetComponent<TextMeshPro>();
        tmp.text = damage.ToString();
        alpha = tmp.color;
        Invoke("DestroyText", destroyTime);
    }

    void Update()
    {   
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        tmp.color = alpha;
    }

    void DestroyText()
    {
        Destroy(gameObject);
    }

    public void SetDamageText(float amount)
    {
        damage = amount;
    }
}
