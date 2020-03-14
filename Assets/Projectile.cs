using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed= 10f;
    public float range = 10f;
    float lifeTime { get { return range / speed; } }
    public float birthtime = 0;
    public LayerMask layerMask;
    public LayerMask wallMask;
    public int damage = 1;

    void FixedUpdate()
    {
        if (Time.time > birthtime + lifeTime)
        {
            ProjectileFactory.Restock(this);
            return;
        }
        RaycastHit hitinfo = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, speed * Time.deltaTime, layerMask|wallMask))
        {
            var hitCharacter = hitinfo.transform.GetComponent<CharacterEntity>();
            if (hitCharacter != null)
            {
                //Debug.Log("hit " + hitinfo.transform.name);
                hitCharacter.TakeDamage(damage);
            }

            ProjectileFactory.Restock(this);
            return;
        }
        transform.position += transform.forward * speed * Time.deltaTime;

    }
}
