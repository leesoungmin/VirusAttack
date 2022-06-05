using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Animator ani_StageStart;

    public int stage;

    float curStageOneTimer;
    public float maxStageOneTimer;

    float curStageTwoTimer;
    public float maxStageTwoTimer;

    public bool stageOneClear;
    public bool stageTwoClear;
    // Start is called before the first frame update
    void Start()
    {
        L.Start();
        ani_StageStart = GetComponent<Animator>();
        stageOneClear = false;
        stageTwoClear = false;
        StageStart();
    }

    // Update is called once per frame
    void Update()
    {
        L.Update();
    }

    public void StageStart()
    {
        

    }

    public void StageClear()
    {

    }

}
