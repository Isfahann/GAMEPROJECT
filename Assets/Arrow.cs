using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody arrowRB;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] float health, maxHealth = 30f;
    [SerializeField] FloatingHealthbar healthbar;
    private float canAttack;

    private void Start()
    {
        arrowRB = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        arrowRB.linearVelocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
           Console.WriteLine("damage dealed");
           other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
           Destroy(gameObject);
           canAttack = 0f;
           
        }
    }

}
