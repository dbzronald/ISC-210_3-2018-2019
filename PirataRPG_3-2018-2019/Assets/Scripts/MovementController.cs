using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementController : MonoBehaviour
{
    Vector3 maxWalkSpeed = new Vector3(2f, 2f), maxRunSpeed = new Vector3(5f, 5f), currentMovementSpeed;
    bool isAttacking, isRunning, lookingRight = true;
    Animator currentAnimator;
    Vector3 maxEnemyWalkSpeed = new Vector3(3f,3f);
    SpriteRenderer spriteRenderer;
    private GameObject _player;
    private const float _ATTACKDISTANCE = 1f;
    private const float ENEMYDETECTIONDISTANCE = 4f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentAnimator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player"); // For the enemies
    }


    void Update()
    {
        Physics.gravity = new Vector3(0, -9800f*Time.deltaTime, 0);
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector2(0, 80), ForceMode.Impulse);
        }

        if (gameObject.tag == "Player")
        {
            isRunning = Input.GetButton("Fire3");
            isAttacking = Input.GetButton("Fire1");

            currentMovementSpeed = new Vector3(Input.GetAxis("Horizontal") * (isRunning ? maxRunSpeed.x : maxWalkSpeed.x),
                Input.GetAxis("Vertical") * (isRunning ? maxRunSpeed.y : maxWalkSpeed.y));

            gameObject.transform.Translate(currentMovementSpeed * Time.deltaTime*2f);


        }
        else if (gameObject.tag == "Enemy")
        {
            if (Vector3.Distance(_player.transform.position, transform.position) <= _ATTACKDISTANCE)
                isAttacking = true;
            else
                isRunning = false; //Enemies dont run
            if (Vector3.Distance(_player.transform.position, transform.position) <= ENEMYDETECTIONDISTANCE)
            {
                currentMovementSpeed = new Vector3(((_player.transform.position.x < transform.position.x) ? -1 : 1) *maxEnemyWalkSpeed.x, 
                ((_player.transform.position.y < transform.position.y) ? -1 : 1)* maxEnemyWalkSpeed.y);
            }
            else
            {
                currentMovementSpeed = Vector3.zero;
            }
        }
        currentAnimator.SetBool("IsAttacking", isAttacking);
        currentAnimator.SetFloat("Speed", currentMovementSpeed.magnitude);

        if (currentMovementSpeed.x < 0)
            lookingRight = false;
        else if (currentMovementSpeed.x > 0)
            lookingRight = true;

        //gameObject.transform.rotation = new Quaternion(0, lookingRight ? 0 : 180, 0, 0);
        spriteRenderer.flipX = lookingRight ? false : true;

        gameObject.GetComponent<Rigidbody>().velocity = currentMovementSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
