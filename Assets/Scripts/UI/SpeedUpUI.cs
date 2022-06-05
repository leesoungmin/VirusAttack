using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpUI : MonoBehaviour
{
    Image img_SpeedUp;

    PlayerController player;

    float maxTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        img_SpeedUp = GetComponent<Image>();
        player.curSpeedUpDelay = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!L.ingameManager.isPlaying)
            img_SpeedUp.fillAmount = player.curSpeedUpDelay / maxTime;
    }
}
