using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBullet : MonoBehaviour
{
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetBulletFire();
    }

    void TargetBulletFire()
    {
        var col = Physics.OverlapSphere(transform.position, Speed, 1 << 8);
        if(col.Length != 0)
        {
            GameObject target = col[0].gameObject;
            float dis = Vector3.Distance(transform.position, target.transform.position);
            foreach(var found in col)
            {
                float _dis = Vector3.Distance(transform.position, found.transform.position);
                if(_dis < dis)
                {
                    target = found.gameObject;
                    dis = _dis;
                }
            }
            var Dir = (target.transform.position - transform.position).normalized;
            transform.Translate(Dir * Speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BorderBullet")
        {
            Destroy(gameObject);
        }

    }
}
