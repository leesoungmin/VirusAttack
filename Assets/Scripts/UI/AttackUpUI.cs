using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackUpUI : MonoBehaviour
{
    Image img_AttackUp;

    float maxTime = 10;

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        img_AttackUp = GetComponent<Image>();
        player.curAttackUpDelay = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!L.ingameManager.isPlaying)
            img_AttackUp.fillAmount = player.curAttackUpDelay / maxTime;
    }
}
