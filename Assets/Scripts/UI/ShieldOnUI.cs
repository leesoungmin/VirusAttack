using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldOnUI : MonoBehaviour
{
    Image img_ShieldOn;

    PlayerController player;

    float maxTime = 5;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
        img_ShieldOn = GetComponent<Image>();
        player.curShieldDelay = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!L.ingameManager.isPlaying)
            img_ShieldOn.fillAmount = player.curShieldDelay / maxTime;
    }
}
