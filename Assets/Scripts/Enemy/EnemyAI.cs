using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("References")]
    public Transform target;            // اللاعب
    public Animator animator;           // Animator العدو
    public NavMeshAgent agent;          // NavMeshAgent على العدو
    public CharacterController characterToPush; // اختياري

    [Header("Movement")]
    public float detectionRange = 15f;
    public float attackRange = 8f;
    public float rotationSpeed = 12f;
    public float idleStopDistance = 0.1f;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireCooldown = 0.4f;
    private float fireTimer = 0f;

    [Header("Aiming (like player)")]
    public bool aimLikePlayer = true;
    public float aimOffsetY = 1.2f;
    public float aimRayDistance = 200f;
    public LayerMask aimMask = ~0;

    [Header("Anim Params")]
    public string moveXParam = "MoveX";
    public string moveYParam = "MoveY";
    public string isMovingParam = "IsMoving";
    public string isSprintingParam = "IsSprinting";
    public string attackTrigger = "Attack";

    [Header("Audio")]
    public AudioSource audioSource;     // AudioSource مضاف على العدو
    public AudioClip shootClip;         // صوت الطلقة

    const float DEAD = 0.1f;

    void Reset()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        if (agent) agent.updateRotation = false;
    }

    void Update()
    {
        if (!target || !agent || !animator) return;

        fireTimer -= Time.deltaTime;

        float dist = Vector3.Distance(transform.position, target.position);

        if (dist > detectionRange)
        {
            agent.ResetPath();
            UpdateAnimFromVelocity(Vector3.zero);
            return;
        }

        if (dist > attackRange)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);
            FaceTowards(agent.desiredVelocity.sqrMagnitude > 0.001f ? transform.position + agent.desiredVelocity : target.position);
            UpdateAnimFromVelocity(agent.velocity);
        }
        else
        {
            agent.isStopped = true;
            agent.ResetPath();
            FaceTowards(target.position);
            UpdateAnimFromVelocity(Vector3.zero);
            TryShoot();
        }
    }

    void UpdateAnimFromVelocity(Vector3 worldVel)
    {
        Vector3 local = transform.InverseTransformDirection(worldVel);
        float moveX = local.x;
        float moveY = local.z;

        animator.SetFloat(moveXParam, moveX, 0.1f, Time.deltaTime);
        animator.SetFloat(moveYParam, moveY, 0.1f, Time.deltaTime);

        bool moving = worldVel.magnitude > idleStopDistance;
        animator.SetBool(isMovingParam, moving);
    }

    void FaceTowards(Vector3 worldPoint)
    {
        Vector3 dir = (worldPoint - transform.position);
        dir.y = 0f;
        if (dir.sqrMagnitude < 0.0001f) return;

        Quaternion targetRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
    }

    void TryShoot()
    {
        if (fireTimer > 0f) return;

        if (!string.IsNullOrEmpty(attackTrigger))
            animator.SetTrigger(attackTrigger);

        if (bulletPrefab && firePoint)
        {
            Vector3 dir = firePoint.forward;

            if (aimLikePlayer && target)
            {
                Vector3 aimPoint = target.position + Vector3.up * aimOffsetY;
                dir = (aimPoint - firePoint.position).normalized;

                if (Physics.Raycast(firePoint.position, dir, out RaycastHit hit, aimRayDistance, aimMask, QueryTriggerInteraction.Ignore))
                {
                    dir = (hit.point - firePoint.position).normalized;
                }
            }

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(dir));
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.isKinematic = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
                rb.velocity = dir * bulletSpeed;
            }

            // 🎵 شغل صوت الطلقة
            if (audioSource && shootClip)
                audioSource.PlayOneShot(shootClip);

            Debug.DrawRay(firePoint.position, dir * 5f, Color.green, 1.5f);
            Destroy(bullet, 3f);
        }

        fireTimer = fireCooldown;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (characterToPush == null) return;
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb != null && !rb.isKinematic)
        {
            rb.AddForce(hit.moveDirection * 5f, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}