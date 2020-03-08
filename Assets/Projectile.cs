using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime = 5f;
    public float birthtime = 0;
    public LayerMask layerMask;
    public int damage = 1;

    private void Start()
    {
        birthtime = Time.time;
    }
    void FixedUpdate()
    {
        if (Time.time > birthtime + lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        RaycastHit hitinfo = new RaycastHit();
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, speed * Time.deltaTime, layerMask))
        {
            var hitCharacter = hitinfo.transform.GetComponent<CharacterEntity>();
            if (hitCharacter != null)
            {
                Debug.Log("hit " + hitinfo.transform.name);
                hitCharacter.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
        }
            transform.position += transform.forward * speed * Time.deltaTime;
        
    }
}
