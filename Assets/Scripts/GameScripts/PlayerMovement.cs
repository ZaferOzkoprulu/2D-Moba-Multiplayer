using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] Rigidbody2D controller;
    [SerializeField] Joystick joystick; 
    [SerializeField] float horizontalMove = 0f;
    [SerializeField] float verticalMove = 0f;  
    [SerializeField] float runSpeed = 40f;
    [SerializeField] internal bool canMove = true;
    [SerializeField] internal bool canShoot = false; 

    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("FixedJoystickTag").GetComponent<FixedJoystick>();
        controller = GetComponent<Rigidbody2D>();
        // GameUIManager.instance.kickButton.onClick.AddListener(() => Shoot());
    }
    void Update()
    {
        if (!canMove) return;
        if (joystick.Horizontal >= .2f)
            horizontalMove = runSpeed;
        else if (joystick.Horizontal <= -.2f)
            horizontalMove = -runSpeed;
        if (joystick.Vertical >= .2f)
            verticalMove = runSpeed;
        else if (joystick.Vertical <= -.2f)
            verticalMove = -runSpeed;
        else
        {
            horizontalMove = 0;
            verticalMove = 0;
        }
        Vector2 newVelo = controller.velocity = new Vector2(joystick.Horizontal, joystick.Vertical) * 5;
        controller.velocity = newVelo;

        // if (Vector2.Distance(gameObject.transform.position, SpawnManager.instance.SkillShotBall.transform.position) < 1)
        //     canShoot = true;
        // else
        //     canShoot = false;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            Vector2 dir = (SpawnManager.instance.SkillShotBall.transform.position - gameObject.transform.position).normalized;
            SpawnManager.instance.SkillShotBall.GetComponent<Rigidbody2D>().AddForce(dir * 500 );
        }
    }
    
}
