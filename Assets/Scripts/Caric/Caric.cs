using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caric : MonoBehaviour
{
    public int hp;
    public int dmg;
    public float moveSpeed;

    [Header("파워 레벨")]
    public int power;

    [Header("적 타입")]
    public ENEMYTYPE enemyType;

    [Header("적 공격 속도")]
    public float fireSpeed;
}
