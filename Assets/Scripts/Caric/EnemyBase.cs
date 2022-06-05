using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyBase : Caric
{
    [Header("�� �߻�")]

    [SerializeField] GameObject mediumFirePos;

    [SerializeField] GameObject mediumBullet;

    [SerializeField] GameObject largeBullet;

    float curFireTime;
    public float maxFireTime;

    [SerializeField] GameObject hitEffect;

    [SerializeField] GameObject[] Item;

    [SerializeField] GameObject DeItem;

    public GameObject player;

    Rigidbody rigidbody;

    [Header("����")]

    [SerializeField] GameObject BossBullet;

    [SerializeField] GameObject BossBulletPos;

    public int maxBossHp;

    public int PatternIndex;
    public int curPatternCnt;
    public int[] maxPatternCnt;

    [SerializeField] Transform[] enemySpawnPos;
    [SerializeField] GameObject[] enemySpawnType;

    float boss_PatternDelay;
    int boss_ShotCnt;
    int boss_ShotCnt2;

    void Init()
    {
        switch (enemyType)
        {
            case ENEMYTYPE.SMALL:
                hp = 3;
                dmg = 1;
                moveSpeed = 8;
                break;
            case ENEMYTYPE.MEDIUM:
                hp = 6;
                dmg = 1;
                moveSpeed = 5;
                fireSpeed = 7;
                curFireTime = 1;
                break;
            case ENEMYTYPE.LARGE:
                hp = 10;
                dmg = 1;
                moveSpeed = 3;
                fireSpeed = 4;
                curFireTime = 2;
                break;
            case ENEMYTYPE.PAINER:
                hp = 3;
                dmg = 1;
                moveSpeed = 3;
                break;
            case ENEMYTYPE.ITEMER:
                hp = 5;
                dmg = 1;
                moveSpeed = 6;
                break;
            case ENEMYTYPE.DEITEMER:
                hp = 6;
                dmg = 1;
                moveSpeed = 6;
                break;
            case ENEMYTYPE.BOSS:
                moveSpeed = 2;
                break;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        Init();
        rigidbody = GetComponent<Rigidbody>();

        if (!L.ingameManager.isPlaying)
        {
            rigidbody.velocity = Vector3.back * moveSpeed;
        }
        player = GameObject.FindWithTag("Player");

        

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyType == ENEMYTYPE.BOSS)
            return;
        
        

        Reload();
        Fire();

    }

    private void OnEnable()
    {
        if (enemyType == ENEMYTYPE.BOSS)
        {
            Invoke("Stop", 1.5f);

            switch (L.spawnManager.stage)
            {
                case 1:
                    boss_PatternDelay = 4f;
                    boss_ShotCnt = 4;
                    boss_ShotCnt2 = 6;
                    hp = 100;
                    break;
                case 2:
                    boss_PatternDelay = 2f;
                    boss_ShotCnt = 7;
                    boss_ShotCnt2 = 10;
                    hp = 500;
                    break;
            }
        }
    }

    void Stop()
    {
        if (!gameObject.activeSelf)
            return;

        rigidbody.velocity = Vector3.zero;
        Invoke("Think", 1.5f);
    }

    void Think()
    {
        PatternIndex = PatternIndex == 5 ? 0 : PatternIndex + 1;
        curPatternCnt = 0;


        switch (PatternIndex)
        {
            case 0:
                FowardFire();
                break;
            case 1:
                LefttargetFire();
                break;
            case 2:
                RighttargetFire();
                break;
            case 3:
                EnemySpawnPattern();
                break;
            case 4:
                ShotFire();
                break;
            case 5:
                ShotFire2();
                break;
        }
    }
    void FowardFire()
    {

        curPatternCnt++;

        GameObject bullet = Instantiate(BossBullet, BossBulletPos.transform.position, Quaternion.identity);
        rigidbody = bullet.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet2 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 0.8f, Quaternion.identity);
        rigidbody = bullet2.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet3 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 0.8f, Quaternion.identity);
        rigidbody = bullet3.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);

        GameObject bullet4 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 1.5f, Quaternion.identity);
        rigidbody = bullet4.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet5 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 1.5f, Quaternion.identity);
        rigidbody = bullet5.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);


        if (curPatternCnt < maxPatternCnt[PatternIndex])
        {
            Invoke("FowardFire", 1f);
        }
        else
        {
            Invoke("Think", boss_PatternDelay);
        }

    }

    void LefttargetFire()
    {

        curPatternCnt++;

        GameObject bullet = Instantiate(BossBullet, BossBulletPos.transform.position, Quaternion.identity);
        rigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 dir = player.transform.position - transform.position;
        rigidbody.AddForce(dir.normalized * 5, ForceMode.Impulse);
        GameObject bullet7 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 0.8f, Quaternion.identity);
        rigidbody = bullet7.GetComponent<Rigidbody>();
        Vector3 dir2 = player.transform.position - transform.position;
        rigidbody.AddForce(dir2.normalized * 5, ForceMode.Impulse);
        GameObject bullet2 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 0.8f, Quaternion.identity);
        rigidbody = bullet2.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet3 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 1.5f, Quaternion.identity);
        rigidbody = bullet3.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet4 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 2.2f, Quaternion.identity);
        rigidbody = bullet4.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet5 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 2.9f, Quaternion.identity);
        rigidbody = bullet5.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet6 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.left * 3.6f, Quaternion.identity);
        rigidbody = bullet6.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);

        if (curPatternCnt < maxPatternCnt[PatternIndex])
        {
            Invoke("LefttargetFire", 0.4f);
        }
        else
        {
            Invoke("Think", boss_PatternDelay);
        }

    }

    void RighttargetFire()
    {
        curPatternCnt++;


        GameObject bullet = Instantiate(BossBullet, BossBulletPos.transform.position, Quaternion.identity);
        rigidbody = bullet.GetComponent<Rigidbody>();
        Vector3 dir = player.transform.position - transform.position;
        rigidbody.AddForce(dir.normalized * 5, ForceMode.Impulse);
        GameObject bullet6 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 0.8f, Quaternion.identity);
        rigidbody = bullet6.GetComponent<Rigidbody>();
        Vector3 dir2 = player.transform.position - transform.position;
        rigidbody.AddForce(dir2.normalized * 5, ForceMode.Impulse);
        GameObject bullet2 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 0.8f, Quaternion.identity);
        rigidbody = bullet2.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet3 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 1.5f, Quaternion.identity);
        rigidbody = bullet3.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet4 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 2.2f, Quaternion.identity);
        rigidbody = bullet4.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);
        GameObject bullet5 = Instantiate(BossBullet, BossBulletPos.transform.position + Vector3.right * 2.9f, Quaternion.identity);
        rigidbody = bullet5.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.back.normalized * 7, ForceMode.Impulse);

        if (curPatternCnt < maxPatternCnt[PatternIndex])
        {
            Invoke("RighttargetFire", 0.4f);
        }
        else
        {
            Invoke("Think", boss_PatternDelay);
        }
    }
    void EnemySpawnPattern()
    {
        curPatternCnt++;

        int ranEnemy = Random.Range(0, enemySpawnType.Length - 1);
        int ranPoints = Random.Range(0, enemySpawnPos.Length);

        Instantiate(enemySpawnType[ranEnemy], enemySpawnPos[ranPoints].position, Quaternion.identity);

        if (curPatternCnt < maxPatternCnt[PatternIndex])
        {
            Invoke("EnemySpawnPattern", 0.4f);
        }
        else
        {
            Invoke("Think", boss_PatternDelay);
        }

    }

    void ShotFire()
    {
        curPatternCnt++;

        for (int i = 0; i < boss_ShotCnt; i++)
        {
            GameObject bullet = Instantiate(BossBullet, BossBulletPos.transform.position, Quaternion.identity);
            rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 l_Vec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector3(Random.Range(-4, 4), 0, Random.Range(0, 4));
            l_Vec += ranVec;
            rigidbody.AddForce(l_Vec.normalized * 4, ForceMode.Impulse);
        }

        if (curPatternCnt < maxPatternCnt[PatternIndex])
        {
            Invoke("ShotFire", 0.5f);
        }
        else 
        {
            Invoke("Think", boss_PatternDelay);
        }
    }

    void ShotFire2()
    {
        curPatternCnt++;
        for (int i = 0; i < boss_ShotCnt2; i++)
        {
            GameObject bullet = Instantiate(BossBullet, BossBulletPos.transform.position, Quaternion.identity);
            rigidbody = bullet.GetComponent<Rigidbody>();
            Vector3 l_Vec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector3(Random.Range(-7, 7), 0, Random.Range(0, 8));
            l_Vec += ranVec;
            rigidbody.AddForce(l_Vec.normalized * 4, ForceMode.Impulse);
        }
        if (curPatternCnt < maxPatternCnt[PatternIndex] && !L.ingameManager.isPlaying)
        {
            Invoke("ShotFire2", 1f);
        }
        else if (curPatternCnt > maxPatternCnt[PatternIndex] && !L.ingameManager.isPlaying)
        {
            Invoke("Think", boss_PatternDelay);
        }

    }

    void Fire()
    {
        if (curFireTime < maxFireTime)
            return;

        switch (enemyType)
        {
            case ENEMYTYPE.MEDIUM:
                MediumFire();
                break;
            case ENEMYTYPE.LARGE:
                LargeFire();
                break;
        }
    }
    void MediumFire()
    {

        GameObject m_bullet = Instantiate(mediumBullet, mediumFirePos.transform.position, Quaternion.identity);
        rigidbody = m_bullet.GetComponent<Rigidbody>();
        Vector3 m_Vec = player.transform.position - transform.position;
        rigidbody.AddForce(m_Vec.normalized * fireSpeed, ForceMode.Impulse);


        curFireTime = 0;

    }
    void LargeFire()
    {

        for (int i = 0; i < 4; i++)
        {
            GameObject l_bullet = Instantiate(largeBullet, transform.position, Quaternion.identity);
            rigidbody = l_bullet.GetComponent<Rigidbody>();
            Vector3 l_Vec = player.transform.position - transform.position;
            Vector3 ranVec = new Vector3(Random.Range(-3, 3), 0, Random.Range(0, 3));
            l_Vec += ranVec;
            rigidbody.AddForce(l_Vec.normalized * fireSpeed, ForceMode.Impulse);
        }


        curFireTime = 0;
    }

    void Reload()
    {
        if (!L.ingameManager.isPlaying)
            curFireTime += Time.deltaTime;
    }

    void ItemDrop()
    {
        int ranItem = Random.Range(0, 6);
        switch (ranItem)
        {
            case 0:
                Instantiate(Item[0], transform.position, Quaternion.identity);
                break;
            case 1:
                Instantiate(Item[1], transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(Item[2], transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(Item[3], transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(Item[4], transform.position, Quaternion.identity);
                break;
            case 5:
                Instantiate(Item[5], transform.position, Quaternion.identity);
                break;
        }
    }

    void DeItemDrop()
    {
        Instantiate(DeItem, transform.position, Quaternion.identity);

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BorderEnemy")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            --hp;
            if (hp <= 0)
            {
                hp = 0;
                switch (enemyType)
                {
                    case ENEMYTYPE.SMALL:
                        L.scoreManager.ScorePlus(50);
                        break;
                    case ENEMYTYPE.MEDIUM:
                        L.scoreManager.ScorePlus(200);
                        break;
                    case ENEMYTYPE.LARGE:
                        L.scoreManager.ScorePlus(500);
                        break;
                    case ENEMYTYPE.PAINER:
                        L.painManager.IncreasePain(5);
                        break;
                    case ENEMYTYPE.ITEMER:
                        ItemDrop();
                        break;
                    case ENEMYTYPE.DEITEMER:
                        DeItemDrop();
                        break;
                    case ENEMYTYPE.BOSS:
                        L.scoreManager.ScorePlus(1000);
                        if (L.spawnManager.stage >= 3)
                        {
                            L.spawnManager.GameClear();
                        }
                        else
                        {
                            L.spawnManager.StageClear();
                        }
                        break;
                }
                Destroy(gameObject);
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }

        }
    }

}