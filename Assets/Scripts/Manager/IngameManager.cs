using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    [SerializeField] Text[] txt_Hp;

    [SerializeField] Text txt_Power;

    [SerializeField] GameObject GameOverPanel;
    [SerializeField] Text gameover_ScoreText;

    public int maxHp;

    public bool isPlaying = false;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        L.Start();
        GameOverPanel.SetActive(false);
        playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        playerController.hp = maxHp;
        HpUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        L.Update();
        txt_Power.text = string.Format("{0:0}", playerController.power);
        gameover_ScoreText.text = string.Format("{0:000,000,000}", L.scoreManager.curScore);
        HotKey();
    }

    void HotKey()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            IncraseHp(1);
        }
        if(Input.GetKeyDown(KeyCode.J))
        {
            playerController.power += 1;
            if (playerController.power >= 5)
                playerController.power = 5;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            playerController.OnShield();
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            playerController.OffShield();
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            L.spawnManager.ItemrCreate();
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            L.spawnManager.PaindownCreate();
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            L.painManager.DecreasePain(5);
        }
    }

    void GameOver()
    {
        GameOverPanel.SetActive(true);
        isPlaying = true;
    }

    void HpUpdate()
    {
        for(int i =0; i< txt_Hp.Length; i++)
        {
            if (i < playerController.hp)
                txt_Hp[i].gameObject.SetActive(true);
            else
                txt_Hp[i].gameObject.SetActive(false);
        }
    }

    public void IncraseHp(int _num)
    {
        playerController.hp += _num;
        if (playerController.hp >= maxHp)
            playerController.hp = maxHp;
        HpUpdate();
    }

    public void DecreaseHp(int _num)
    {
        playerController.hp -= _num;
        if (playerController.hp <= 0)
        {
            playerController.hp = 0;
            GameOver();
            Debug.Log("ÇìÇì Á×À½");
        }
        HpUpdate();
    }
}
