using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class L : MonoBehaviour
{
    public static float worldTime = 0;

    public static IngameManager ingameManager = null;
    public static SpawnManager spawnManager = null;
    public static ScoreManager scoreManager = null;
    public static PainManager painManager = null;
    public static StageManager stageManager = null;
    // Start is called before the first frame update
    public static void Start()
    {
        ingameManager = GameObject.Find("Manager").transform.Find("Ingame").GetComponent<IngameManager>();
        spawnManager = GameObject.Find("Manager").transform.Find("Spawn").GetComponent<SpawnManager>();
        scoreManager = GameObject.Find("Manager").transform.Find("Score").GetComponent<ScoreManager>();
        painManager = GameObject.Find("Manager").transform.Find("Pain").GetComponent<PainManager>();
        stageManager = GameObject.Find("Manager").transform.Find("Stage").GetComponent<StageManager>();
    }

    // Update is called once per frame
    public static void Update()
    {
        worldTime = Time.time;
    }
}
