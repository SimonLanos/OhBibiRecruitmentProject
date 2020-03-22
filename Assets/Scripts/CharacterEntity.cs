using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CharacterEntity : MonoBehaviour
{
    public bool isEnnemy = false;
    public int health = 10;
    public int maxHealth = 10;
    public float visionDistance = 0f;
    public LayerMask opposantLayerMask;
    public float speed;
    public float coolDown = 1f;
    float lastAttackTime;
    Animation attackAnimation;

    public Projectile projectile;

    public ParticleSystem deathFX;

    public NavMeshAgent navMeshAgent;

    public GameObject destinationIconPrefab;
    GameObject destinationIcon;
    public LineRenderer pathLinePrefab;
    LineRenderer pathLine;

    bool attack = false;
    public Vector3 attackOffset;
    public Vector3 attackExtent;


    public Color visionGizmoColor = Color.cyan;
    Color baseColor;
    public Color selectedColor;

    private void Start()
    {
        if (destinationIconPrefab != null)
        {
            if (destinationIcon == null)
            {
                destinationIcon = Instantiate(destinationIconPrefab);
                destinationIcon.name = name + "_destinationIcon";
            }
            destinationIcon.SetActive(false);
        }
        if (pathLinePrefab != null)
        {
            if (pathLine == null)
            {
                pathLine = Instantiate(pathLinePrefab);
                pathLine.name = name + "_pathLine";
            }
            pathLine.gameObject.SetActive(true);
        }
        navMeshAgent = GetComponent<NavMeshAgent>();
        attackAnimation = GetComponent<Animation>();
    }
   
    private void Update()
    {
        UpdatePathUI();
    }

    private void FixedUpdate()
    {
        UpdateSpeedDependingOnGroundType();
    }

    void UpdateSpeedDependingOnGroundType()
    {
        NavMeshHit navHit;
        if (NavMesh.SamplePosition(transform.position, out navHit, 1f, NavMesh.AllAreas))
        {
            int mask1 = navHit.mask;
            int index = 0;

            while ((mask1 >>= 1) > 0)
            {
                index++;
            }
            float areaCost = NavMesh.GetAreaCost(index);
            navMeshAgent.speed = speed / areaCost;
        }
    }
    
    public void ShootAt(Vector3 targetPosition)
    {
        transform.LookAt(targetPosition);
        if (Time.time>lastAttackTime+coolDown)
        {
            ProjectileFactory.Create(opposantLayerMask, transform.position, transform.rotation);
            lastAttackTime = Time.time;
        }
    }


    public void AttackContact()
    {
        if (Time.time > lastAttackTime + coolDown)
        {
            if (attackAnimation != null)
            {
                attackAnimation.Play();
            }
            Collider[] colliders = Physics.OverlapBox(transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y + transform.forward * attackOffset.z, attackExtent, transform.rotation, opposantLayerMask);
            for (int i = 0; i < colliders.Length; i++)
            {
                CharacterEntity hitCharacter = colliders[i].transform.GetComponent<CharacterEntity>();
                if (hitCharacter != null)
                {
                    hitCharacter.TakeDamage(1);
                }
            }
            attack = true;
            lastAttackTime = Time.time;
        }
    }

    public bool AttackContactWillLand()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + transform.right * attackOffset.x + transform.up * attackOffset.y + transform.forward * attackOffset.z, attackExtent, transform.rotation, opposantLayerMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            CharacterEntity hitCharacter = colliders[i].transform.GetComponent<CharacterEntity>();
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


    public Transform GetClosestTargetTransformInVisionRange()
    {
        Collider[] targetsInVisionRange = Physics.OverlapSphere(transform.position, visionDistance, opposantLayerMask);
        if (targetsInVisionRange.Length > 0)
        {
            int indexOfClosestTarget = 0;
            for (int i = 1; i < targetsInVisionRange.Length; i++)
            {
                if ((targetsInVisionRange[i].transform.position - transform.position).sqrMagnitude < (targetsInVisionRange[indexOfClosestTarget].transform.position - transform.position).sqrMagnitude)
                {
                    indexOfClosestTarget = i;
                }
            }
            return targetsInVisionRange[indexOfClosestTarget].transform;
        }
        return null;
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
            CameraShaker.Shake(0.5f);
            //TODO: make betterFactory to prepare for different types of ennemies and to not have to use this unelegant bool
            if (isEnnemy)
            {
                Score.AddScore(1);
                AttackerFactory.Restock(GetComponent<AttackerAI>());
            }
            else 
            {
                Destroy(gameObject);
            }
        }
    }

    void UpdatePathUI()
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
    public void ShowSelection()
    {
        baseColor = GetComponent<Renderer>().material.color;
        GetComponent<Renderer>().material.color = selectedColor;
    }
    public void HideSelection()
    {
        GetComponent<Renderer>().material.color = baseColor;
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

    private void OnDestroy()
    {
        if (pathLine)
        {
            Destroy(pathLine.gameObject);
        }
        if (destinationIcon)
        {
            Destroy(destinationIcon.gameObject);
        }
    }

    private void OnDisable()
    {
        if (pathLine)
        {
            pathLine.gameObject.SetActive(false);
        }
        if (destinationIcon)
        {
            destinationIcon.gameObject.SetActive(false);
        }
    }

}
