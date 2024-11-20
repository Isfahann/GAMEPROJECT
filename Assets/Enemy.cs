using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] float health, maxHealth = 30f;
    public float rotateSpeed = 0.0025f;
    private float canAttack;
    private Transform target;
    public Rigidbody2D rb;
    private Animator myAnimator;

    [SerializeField] FloatingHealthbar healthbar;

    public void Awake()
    {
        healthbar = GetComponentInChildren<FloatingHealthbar>();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthbar.UpdateHealthbar(health, maxHealth);

    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
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

    

    private void FixedUpdate()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1f);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }

    private void RotateTowardsTarget()
    {
        if (target == null) return;
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

}
