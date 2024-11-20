using System;
using UnityEngine;

public class HomingArrow : MonoBehaviour
{
    public float speed = 5;
    Transform player;

    GameObject target;
    Rigidbody arrowRB;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] float health, maxHealth = 30f;
    [SerializeField] FloatingHealthbar healthbar;
    private float canAttack;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed*Time.deltaTime);
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
