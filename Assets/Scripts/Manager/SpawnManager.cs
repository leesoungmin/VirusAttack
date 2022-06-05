using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Text txt_BossHp;
    [SerializeField] Image img_BossHp;

    [SerializeField] Text StageStartPanel;
    [SerializeField] Text StageClearPanel;

    [SerializeField] GameObject GameClearPanel;
    [SerializeField] Text GameClearScore;

    public int stage;

    [SerializeField] Transform[] enemyPoints;
    [SerializeField] GameObject[] enemyTypies;

    public float curSpawnTime;
    float maxSpawnTime;

    public int firstStageEnemyCnt;
    public int secStageEnemyCnt;

    public bool isBoss;
    public bool isSpawn;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        L.Start();
        isBoss = false;
        isSpawn = true;
        stage = 1;
        firstStageEnemyCnt = 0;
        secStageEnemyCnt = 0;
        player = GameObject.FindWithTag("Player");
        StageStart();
    }

    // Update is called once per frame
    void Update()
    {
        L.Update();

        if (curSpawnTime > maxSpawnTime && !L.ingameManager.isPlaying)
        {
            switch (stage)
            {
                case 1:
                    maxSpawnTime = Random.Range(0.5f, 1.3f);
                    break;
                case 2:
                    maxSpawnTime = Random.Range(0.3f, 1f);
                    break;
            }
            EnemySpawn();
            curSpawnTime = 0;
        }
        else
        {
            Reload();
        }

        GameClearScore.text = string.Format("{0:000,000,000}", L.scoreManager.curScore);

    }

    public void StageStart()
    {
        StartCoroutine(StageStartCor());
    }

    public void StageClear()
    {

        L.painManager.curPainPerset = 30;
        StartCoroutine(StageClearCor());

    }

    public void ItemrCreate()
    {
        Instantiate(enemyTypies[3], enemyPoints[4].position, Quaternion.identity);
    }

    public void PaindownCreate()
    {
        Instantiate(enemyTypies[4], enemyPoints[2].position, Quaternion.identity);
    }
    IEnumerator StageClearCor()
    {

        stage += 1;

        player.transform.position = new Vector3(-3, 0, -4);

        StageClearPanel.gameObject.SetActive(true);
        StageClearPanel.text = "Stage " + stage + "\nClear!!";

        if (stage >= 3)
            GameClear();

        yield return new WaitForSeconds(2f);

        StageClearPanel.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        StageStart();

        isSpawn = true;



    }

    IEnumerator StageStartCor()
    {
        
        StageStartPanel.gameObject.SetActive(true);
        StageStartPanel.text = "Stage " + stage + "\nStart!!";
        yield return new WaitForSeconds(1.5f);
        StageStartPanel.gameObject.SetActive(false);
    }

    public void GameClear()
    {
        L.ingameManager.isPlaying = true;
        GameClearPanel.SetActive(true);
    }

    void EnemySpawn()
    {
        if (isBoss)
        {
            isSpawn = false;
            Instantiate(enemyTypies[6], enemyPoints[2].position, Quaternion.identity);
            isBoss = false;
        }
        switch (stage)
        {
            case 1:
                if (firstStageEnemyCnt < 100 && !isBoss && isSpawn && !L.ingameManager.isPlaying)
                {
                    int ranPoints = Random.Range(0, enemyPoints.Length);
                    int ranEnemies = Random.Range(0, enemyTypies.Length - 1);

                    Instantiate(enemyTypies[ranEnemies], enemyPoints[ranPoints].position, Quaternion.identity);
                    ++firstStageEnemyCnt;
                }
                else if (firstStageEnemyCnt >= 100)
                {
                    isBoss = true;
                    firstStageEnemyCnt = 0;
                }
                break;
            case 2:
                if (secStageEnemyCnt < 200 && !isBoss && isSpawn && !L.ingameManager.isPlaying)
                {
                    int ranPoints = Random.Range(0, enemyPoints.Length);
                    int ranEnemies = Random.Range(0, enemyTypies.Length - 1);

                    Instantiate(enemyTypies[ranEnemies], enemyPoints[ranPoints].position, Quaternion.identity);
                    ++secStageEnemyCnt;
                }
                else if (secStageEnemyCnt >= 200)
                {
                    isBoss = true;
                    secStageEnemyCnt = 0;
                }
                break;
        }

    }

    void Reload()
    {
        curSpawnTime += Time.deltaTime;
    }
}
