using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private int health;
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] float ShiftSpeed = 10f;
    [SerializeField] float JumpForce = 7f;
    bool isGrounded = true;
    [SerializeField] Animator anim;
    float stamina = 5f;
    private int health;



    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
        anim = GetComponent<Animator>();

        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        if (direction.x != 0 || direction.z != 0)
        {
            anim.SetBool("Run", true);
        }
        if (direction.x == 0 && direction.z == 0)
        {
            anim.SetBool("Run", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("Jump", true);
            //Отключаем звук бега
            //Создаем временный источник звука для прыжка
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina > 0)
            {
                stamina -= Time.deltaTime;
                currentSpeed = ShiftSpeed;
            }
            else
            {
                currentSpeed = movementSpeed;
            }
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            stamina += Time.deltaTime;
            currentSpeed = movementSpeed;
        }

        if (stamina > 5f)
        {
            stamina = 5f;
        }
        else if (stamina < 0)
        {
            stamina = 0;
        }

        if (health <= 0)
        {
            health = 0;
            anim.SetBool("Die", true);
            //Убираем оружие
            //ChooseWeapon(Weapons.None);
            //отключаем скрипт PlayerController, чтобы персонаж не мог передвигаться
            this.enabled = false;
        }
    }

    public void ChangeHealth(int count)
    {
        //вычитаем здоровье
        health -= count;
        //если здоровье меньше либо равно нулю, то...
        if (health <= 0)
        {
            //что-то произойдет
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }

}
