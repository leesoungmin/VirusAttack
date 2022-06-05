using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PainManager : MonoBehaviour
{
    [SerializeField] GameObject PainOverPanel;

    [SerializeField] Text painover_ScoreText;

    [SerializeField] Text txt_PainPerset;

    float firstTime = 0;

    public int curPainPerset;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        curPainPerset = 10;
        PainOverPanel.SetActive(false);
        L.Start();
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        firstTime = 5 + L.worldTime;
    }

    // Update is called once per frame
    void Update()
    {
        L.Update();
        txt_PainPerset.text = string.Format("{0:0}%", curPainPerset);
        painover_ScoreText.text = string.Format("{0:000,000,000}", L.scoreManager.curScore);
        PainUp();
    }

    void PainUp()
    {
        if (L.worldTime > firstTime && !L.ingameManager.isPlaying)
        {
            firstTime = 5 + L.worldTime;
            curPainPerset += 1;
            if (curPainPerset >= 100)
            {
                curPainPerset = 100;
                PainOver();

            }
        }
    }

    void PainOver()
    {
        L.ingameManager.isPlaying = true;
        PainOverPanel.SetActive(true);
    }
    public void IncreasePain(int _num)
    {
        curPainPerset += _num;
        if (curPainPerset >= 100)
        {
            curPainPerset = 100;
            PainOver();

            Debug.Log("PAIN DEAD");
        }
    }

    public void DecreasePain(int _num)
    {
        curPainPerset -= _num;
        if (curPainPerset <= 0)
            curPainPerset = 0;
    }
}
