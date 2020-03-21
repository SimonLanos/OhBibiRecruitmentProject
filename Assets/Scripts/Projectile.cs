using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed= 10f;
    public float range = 10f;
    float lifeTime { get { return range / speed; } }
    public int damage = 1;

    float birthtime = 0;
    
    public LayerMask targetLayerMask;
    public LayerMask wallMask;
    
    RaycastHit hitinfo = new RaycastHit();

    public void Init(Vector3 position, Quaternion rotation,LayerMask layerMask)
    {
        gameObject.SetActive(true);
        transform.position = position;
        transform.rotation = rotation;
        birthtime = Time.time;
        targetLayerMask = layerMask;
    }

    void FixedUpdate()
    {
        if (Time.time > birthtime + lifeTime)
        {
            ProjectileFactory.Restock(this);
            return;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, speed * Time.fixedDeltaTime, targetLayerMask|wallMask))
        {
            CharacterEntity hitCharacter = hitinfo.transform.GetComponent<CharacterEntity>();
            if (hitCharacter != null)
            {
                hitCharacter.TakeDamage(damage);
            }

            ProjectileFactory.Restock(this);
            return;
        }
        transform.position += transform.forward * speed * Time.fixedDeltaTime;

    }
}
