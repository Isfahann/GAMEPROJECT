using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFollow : MonoBehaviour
{

    [SerializeField] float health, maxHealth = 30f;
    [SerializeField] FloatingHealthbar healthbar;
    private Transform target;
    public Rigidbody2D rb;
    private Animator myAnimator;


    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public GameObject arrow;
    public GameObject arrowParent;
    private Transform player;
    public float rotateSpeed = 0.5f;
    public float fireRate = 1f;
    private float nextFireTime;

    public void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHealthbar>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = player;
        health = maxHealth;
        healthbar.UpdateHealthbar(health, maxHealth);
    }

    

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            // Optionally handle what happens when the player is destroyed
            return; // Exit the Update method if the player is null
        }
        // Calculate the distance from the enemy to the player
        float distanceFromPlayer = Vector2.Distance(transform.position, player.position);

        // Check if the player is within line of sight
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            // Move towards the player
            RotateTowardsTarget();
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(arrow, arrowParent.transform.position, Quaternion.identity); 
            nextFireTime = Time.time + fireRate;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1f);
            Destroy(other.gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthbar.UpdateHealthbar(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
