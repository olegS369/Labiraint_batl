using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    protected GameObject player;
    protected Animator anim;
    protected Rigidbody rb;
    protected float distance;
    protected float timer;
    bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (!dead)
        {
            Attack();
        }
    }

    public virtual void Move()
    {
    }
    public virtual void Attack()
    {
    }

    private void FixedUpdate()
    {
        if (!dead)
        {
            Move();
        }
    }

    public void ChangeHealth(int count)
    {
        //отнимаем здоровье
        health -= count;
        //если здоровье меньше, либо равно нулю, то..
        if (health <= 0)
        {
            //меняем значение булевой переменной(перестают работать методы Attack и Move
            dead = true;
            //отключаем коллайдер врага
            GetComponent<Collider>().enabled = false;
            //включаем анимацию смерти
            anim.SetBool("Die", true);
        }
    }
}
