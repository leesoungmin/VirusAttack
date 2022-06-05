using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] GameObject SpeedUpEffect;
    [SerializeField] GameObject AttackUpEffect;
    [SerializeField] GameObject HealEffect;
    [SerializeField] GameObject PainDownEffect;
    [SerializeField] GameObject PowerUpEffect;
    [SerializeField] GameObject PowerDownEffect;
    [SerializeField] GameObject ShieldOnEffect;

    public ITEMTYPE itemType;

    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            switch(itemType)
            {
                case ITEMTYPE.HEAL:
                    L.ingameManager.IncraseHp(1);
                    Instantiate(HealEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.SHIELDON:
                    player.isShieldOn = true;
                    player.curShieldDelay = player.maxShieldDelay;
                    Instantiate(ShieldOnEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.PAINDOWN:
                    L.painManager.DecreasePain(5);
                    Instantiate(PainDownEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.POWERUP:
                    player.power += 1;
                    if (player.power > 5)
                        player.power = 5;
                    Instantiate(PowerUpEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.POWERDOWN:
                    player.power -= 1;
                    if (player.power < 1)
                        player.power = 1;
                    Instantiate(PowerDownEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.ATTACKUP:
                    player.isAttakUp = true;
                    player.curAttackUpDelay = player.maxAttackUpDelay;
                    Instantiate(AttackUpEffect, transform.position, Quaternion.identity);
                    break;
                case ITEMTYPE.SPEEDUP:
                    player.isSpeedUp = true;
                    player.curSpeedUpDelay = player.maxSpeedUpDelay;
                    Instantiate(SpeedUpEffect, transform.position, Quaternion.identity);
                    break;
            }

            Destroy(gameObject);
        }
    }
}
