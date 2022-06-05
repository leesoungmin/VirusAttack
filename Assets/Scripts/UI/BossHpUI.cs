using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUI : MonoBehaviour
{
    Image BossHpBar;

    EnemyBase enemy;

    int hp;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType(typeof(EnemyBase)) as EnemyBase;
        BossHpBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
