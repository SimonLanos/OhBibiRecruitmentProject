using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterEntity : MonoBehaviour
{
    public bool isEnnemy = false;
    public bool isCitizen = false;
    public int health = 10;
    public int maxHealth = 10;
    public float visionDistance = 0f;
    public LayerMask opposantLayerMask;
    public float speed;
    public float coolDown = 1f;
    float lastShotTime;
    public ParticleSystem deathFX;

    public Projectile projectile;

    public NavMeshAgent navMeshAgent;

    public GameObject destinationIconPrefab;
    GameObject destinationIcon;
    public LineRenderer pathLinePrefab;
    LineRenderer pathLine;

    bool attack = false;
    public Vector3 attackOffset;
    public Vector3 attackExtent;

    bool moving;
    Vector3 destination;
    private void Start()
    {
        if (destinationIconPrefab != null)
        {
            destinationIcon = Instantiate(destinationIconPrefab);
            destinationIcon.name = name + "_destinationIcon";
            destinationIcon.SetActive(false);
        }
        if (pathLinePrefab != null)
        {
            pathLine = Instantiate(pathLinePrefab);
            pathLine.name = name + "_pathLine";
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
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

    public void AttackContact()
    {
        if (Time.time > lastShotTime + coolDown)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y + transform.forward * attackOffset.z, attackExtent, transform.rotation, opposantLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterEntity hitCharacter = colliders[i].transform.GetComponentInParent<CharacterEntity>();
                if (hitCharacter != null)
                {
                    hitCharacter.TakeDamage(1);
                }
            }
            attack = true;
            lastShotTime = Time.time;
        }
    }

    public bool AttackContactWillLand()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y + transform.forward * attackOffset.z, attackExtent, transform.rotation, opposantLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterEntity hitCharacter = colliders[i].transform.GetComponentInParent<CharacterEntity>();
            if (hitCharacter != null)
            {
                return true;
            }
        }
        return false;
    }
    
    public void MoveToPosition(Vector3 position)
    {
        navMeshAgent.SetDestination(position);
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
            MoveToPosition(targetsInVisionRange[indexOfClosetTarget].transform.position);
        }
    }

    private void Update()
    {
        if (navMeshAgent.hasPath)
        {
            if (destinationIcon != null)
            {
                destinationIcon.transform.position = navMeshAgent.destination;
                destinationIcon.SetActive(true);
            }
            if (pathLine != null)
            {
                pathLine.positionCount = navMeshAgent.path.corners.Length;
                pathLine.SetPositions(navMeshAgent.path.corners);
            }
        }
        else
        {
            if (destinationIcon != null && destinationIcon.activeSelf)
            {
                destinationIcon.SetActive(false);
            }
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
            if (deathFX != null)
            {
                ParticleSystem fxObject =Instantiate(deathFX, transform.position, Quaternion.Euler(-90,0,0));
                Destroy(fxObject.gameObject, 2f);
            }
            CameraShake.Shake(0.5f);
            if (isEnnemy)
            {
                Score.AddScore(10);
                EnnemyFactory.Restock(this);
            }
            else 
            {
                if (!isCitizen)
                {
                    AllyAI.numberOfAlliesInGame--;
                    if (AllyAI.numberOfAlliesInGame <= 0)
                    {
                        Debug.Log("GameOver\n" + Score.score);
                        Score.ResetScore();
                        SceneManager.LoadScene(0);
                    }
                }
                Destroy(gameObject);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = visionGizmoColor;
        Gizmos.DrawWireSphere(transform.position, visionDistance);

        Gizmos.color = Color.yellow;
        if (navMeshAgent != null && navMeshAgent.path != null && navMeshAgent.path.corners.Length>0)
        {
            Gizmos.DrawCube(navMeshAgent.destination, Vector3.one);
            Gizmos.DrawCube(navMeshAgent.path.corners[0], Vector3.one / 2f);
            for (int i = 1; i< navMeshAgent.path.corners.Length;i++)
            {
                Gizmos.DrawLine(navMeshAgent.path.corners[i-1], navMeshAgent.path.corners[i]);
                Gizmos.DrawCube(navMeshAgent.path.corners[i], Vector3.one/2f);
            }
        }
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        if (attack)
        {
            Gizmos.DrawCube(attackOffset, attackExtent*2f);
            attack = false;
        }
        Gizmos.DrawWireCube(attackOffset, attackExtent*2f);
    }
}
