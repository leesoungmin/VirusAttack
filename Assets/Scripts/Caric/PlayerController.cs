using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MoveRange
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

public class PlayerController : Caric
{

    public AudioSource fireSound;

    [Header("플레이어 총")]

    [SerializeField] GameObject normalBullet;
    [SerializeField] GameObject targetBullet;
    [SerializeField] GameObject firePos;

    float curShootTime;
    public float maxShootTime;

    [Header("플레이어 피격 이펙트")]
    [SerializeField] GameObject HitEffect;

    [Header("아이템")]
    [SerializeField] GameObject shieldEffect;
    public bool isAttacked = false; //무적인지 확인

    public float curShieldDelay;
    public float maxShieldDelay;

    public float curAttackUpDelay;
    public float maxAttackUpDelay;

    public float curSpeedUpDelay;
    public float maxSpeedUpDelay;

    public bool isShieldOn = false;
    public bool isAttakUp = false;
    public bool isSpeedUp = false;

    public bool isBugShield = false;

    public MoveRange moveRange;
    Rigidbody rigidbody;
    EnemyBase enemyBase;
    void Init()
    {
        hp = 5;
        moveSpeed = 10;
        dmg = 1;
        fireSpeed = 14;
        power = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        rigidbody = GetComponent<Rigidbody>();
        enemyBase = FindObjectOfType(typeof(EnemyBase)) as EnemyBase;

        curShieldDelay = maxShieldDelay;
        curAttackUpDelay = maxAttackUpDelay;
        curSpeedUpDelay = maxSpeedUpDelay;
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Reload();

        UseShield();
        UseAttackUp();
        UseSpeedUp();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!L.ingameManager.isPlaying)
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(h, 0, v);
            rigidbody.velocity = movement * moveSpeed;
            rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, moveRange.xMin, moveRange.xMax)
                , 0
                , Mathf.Clamp(rigidbody.position.z, moveRange.zMin, moveRange.zMax));
            rigidbody.rotation = Quaternion.Euler(0, 0, rigidbody.velocity.x * -2);

        }
    }

    void Fire()
    {
        if (!Input.GetButton("Fire1"))
            return;
        if (curShootTime < maxShootTime)
            return;
        fireSound.Play();
        switch (power)
        {
            case 1:
                PowerFire1();
                break;
            case 2:
                PowerFire2();
                break;
            case 3:
                PowerFire3();
                break;
            case 4:
                PowerFire4();
                break;
            case 5:
                PowerFire5();
                break;
        }

        curShootTime = 0;
    }
    #region 총알 파워마다 발사
    void PowerFire1()
    {
        GameObject bullet = Instantiate(normalBullet, firePos.transform.position, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);
    }

    void PowerFire2()
    {
        GameObject bullet = Instantiate(normalBullet, firePos.transform.position + Vector3.left * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet2 = Instantiate(normalBullet, firePos.transform.position + Vector3.right * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        rb2.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);
    }

    void PowerFire3()
    {
        GameObject bullet = Instantiate(normalBullet, firePos.transform.position + Vector3.left * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet2 = Instantiate(normalBullet, firePos.transform.position, Quaternion.Euler(90, 0, 0));
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        rb2.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet3 = Instantiate(normalBullet, firePos.transform.position + Vector3.right * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb3 = bullet3.GetComponent<Rigidbody>();
        rb3.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);
    }
    void PowerFire4()
    {
        GameObject bullet = Instantiate(normalBullet, firePos.transform.position + Vector3.left * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet2 = Instantiate(normalBullet, firePos.transform.position + Vector3.right * 0.4f, Quaternion.Euler(90, 0, 0));
        Rigidbody rb2 = bullet2.GetComponent<Rigidbody>();
        rb2.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet3 = Instantiate(targetBullet, firePos.transform.position, Quaternion.identity);
    }
    void PowerFire5()
    {
        GameObject bullet = Instantiate(normalBullet, firePos.transform.position, Quaternion.Euler(90, 0, 0));
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * fireSpeed, ForceMode.Impulse);

        GameObject bullet2 = Instantiate(targetBullet, firePos.transform.position + Vector3.left * 0.4f, Quaternion.identity);
        GameObject bullet3 = Instantiate(targetBullet, firePos.transform.position + Vector3.right * 0.4f, Quaternion.identity);
    }
    #endregion

    void Reload()
    {
        if (!L.ingameManager.isPlaying)
            curShootTime += Time.deltaTime;
    }

    void OnHit()
    {
        Instantiate(HitEffect, transform.position, Quaternion.identity);
        L.ingameManager.DecreaseHp(1);
        isShieldOn = true;
    }

    #region 아이템 
    public void UseShield()
    {
        if (isShieldOn && !L.ingameManager.isPlaying)
        {
            curShieldDelay -= Time.deltaTime;
            isAttacked = true;
            shieldEffect.SetActive(true);
            if (curShieldDelay <= 0)
            {
                curShieldDelay = maxShieldDelay;
                isAttacked = false;
                shieldEffect.SetActive(false);
                isShieldOn = false;
            }

        }
    }

    public void OnShield()
    {
        isAttacked = true;
        shieldEffect.SetActive(true);
    }

    public void OffShield()
    {
        isAttacked = false;
        shieldEffect.SetActive(false);
    }

    public void UseSpeedUp()
    {
        if (isSpeedUp && !L.ingameManager.isPlaying)
        {
            curSpeedUpDelay -= Time.deltaTime;
            moveSpeed = 15;
            if (curSpeedUpDelay <= 0)
            {
                isSpeedUp = false;
                curSpeedUpDelay = maxSpeedUpDelay;
                moveSpeed = 10;
            }
        }
    }

    public void UseAttackUp()
    {
        if (isAttakUp && !L.ingameManager.isPlaying)
        {
            curAttackUpDelay -= Time.deltaTime;
            dmg = 2;
            if (curAttackUpDelay <= 0)
            {
                isAttakUp = false;
                dmg = 1;
                curAttackUpDelay = maxSpeedUpDelay;
            }
        }
    }


    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isAttacked)
                return;

            OnHit();
        }

        if (other.gameObject.tag == "EnemyBullet")
        {
            if (isAttacked)
                return;

            Destroy(other.gameObject);
            OnHit();
        }
    }
}
