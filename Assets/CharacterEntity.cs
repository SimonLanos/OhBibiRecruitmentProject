using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEntity : MonoBehaviour
{
    public int health = 10;
    public float visionDistance = 0f;
    public LayerMask opposantLayerMask;
    public float speed;
    public float coolDown = 1f;
    float lastShotTime;

    public Projectile projectile;

    bool moving;
    Vector3 destination;

    public Color visionGizmoColor = Color.cyan;
    public void Shoot(Vector3 direction)
    {
        ShootAt(transform.position + direction);
    }

    public void ShootAt(Vector3 targetPosition)
    {
        transform.LookAt(targetPosition);
        if (Time.time>lastShotTime+coolDown)
        {
            ProjectileFactory.Create(opposantLayerMask, transform.position, transform.rotation);
            lastShotTime = Time.time;
        }
    }


    public void ShootNearestTarget()
    {
        var targetsInVisionRange = Physics.OverlapSphere(transform.position, visionDistance, opposantLayerMask);
        if (targetsInVisionRange.Length > 0)
        {
            int indexOfClosetTarget = 0;
            for(int i = 1; i< targetsInVisionRange.Length; i++)
            {
                if((targetsInVisionRange[i].transform.position - transform.position).sqrMagnitude< (targetsInVisionRange[indexOfClosetTarget].transform.position - transform.position).sqrMagnitude)
                {
                    indexOfClosetTarget = i;
                }
            }
            ShootAt(targetsInVisionRange[indexOfClosetTarget].transform.position);
        }
    }

    public void Move(Vector3 direction)
    {
        transform.position+=(direction * speed * Time.deltaTime);
    }

    public void MoveToPosition(Vector3 position)
    {
        destination = position;
        moving = true;
    }

    private void Update()
    {
        if (moving)
        {
            Vector3 direction = destination - transform.position;
            float magnitude = direction.magnitude;
            direction = direction.normalized * Mathf.Clamp01(magnitude);
            Move(direction);
            if (magnitude <= 0.1f)
            {
                moving = false;
            }
        }
    }

    public void MoveTowardNearestOpponent() {
        var targetsInVisionRange = Physics.OverlapSphere(transform.position, visionDistance, opposantLayerMask);
        if (targetsInVisionRange.Length > 0)
        {
            int indexOfClosetTarget = 0;
            for (int i = 1; i < targetsInVisionRange.Length; i++)
            {
                if ((targetsInVisionRange[i].transform.position - transform.position).sqrMagnitude < (targetsInVisionRange[indexOfClosetTarget].transform.position - transform.position).sqrMagnitude)
                {
                    indexOfClosetTarget = i;
                }
            }
            Move((targetsInVisionRange[indexOfClosetTarget].transform.position-transform.position).normalized);
        }
    }

    public float SqrDistanceToNearestOpponent()
    {
        var targetsInVisionRange = Physics.OverlapSphere(transform.position, visionDistance, opposantLayerMask);
        if (targetsInVisionRange.Length > 0)
        {
            int indexOfClosetTarget = 0;
            for (int i = 1; i < targetsInVisionRange.Length; i++)
            {
                if ((targetsInVisionRange[i].transform.position - transform.position).sqrMagnitude < (targetsInVisionRange[indexOfClosetTarget].transform.position - transform.position).sqrMagnitude)
                {
                    indexOfClosetTarget = i;
                }
            }
            return (targetsInVisionRange[indexOfClosetTarget].transform.position - transform.position).sqrMagnitude;
        }
        return float.MaxValue;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = visionGizmoColor;
        Gizmos.DrawWireSphere(transform.position, visionDistance);
    }
}
