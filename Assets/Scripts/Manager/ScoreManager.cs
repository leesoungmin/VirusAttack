using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text txt_Score;

    public int curScore;

    float firstTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        L.Start();
        firstTime = 1 + L.worldTime;
    }

    // Update is called once per frame
    void Update()
    {
        L.Update();
        txt_Score.text = string.Format("{0:000,000,000}", curScore);
        ScoreUp();
    }

    void ScoreUp()
    {
        if(L.worldTime > firstTime && !L.ingameManager.isPlaying)
        {
            firstTime = 1 + L.worldTime;
            curScore += 1;
        }
    }

    public void ScorePlus(int _num)
    {
        curScore += _num;
    }
}
