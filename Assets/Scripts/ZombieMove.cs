using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    public Waves _waves;

    private GameObject Player;

    private GameObject ammoBox;

    public float ZombiesSpeed = 2f;

    private Animator ZombieAnimator;

    public int ZombieHealth = 100;

    public List<Rigidbody> GetRigidbodies = new List<Rigidbody>();

    private AnimatorStateInfo ZombieStateInfo;

    private void Start()
    {
        _waves = FindObjectOfType<Waves>();

        ZombieAnimator = gameObject.GetComponent<Animator>();

        Player = GameObject.FindGameObjectWithTag("Player");

        ammoBox = Resources.Load("Prefabs/SmgAmmo_Box") as GameObject;

        RigidbodyIsKinematicOn();

        gameObject.GetComponent<CapsuleCollider>().enabled = true;

        ZombieHealth = 100;

        ZombiesSpeed = 2f;
    }

    void Update()
    {
        ZombieController();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ZombieAnimator.SetBool("attack", true);

            collision.gameObject.GetComponent<PlayerHealth>().TakePlayerDamage(10);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ZombieAnimator.SetBool("attack", false);
        }
    }

    void RigidbodyIsKinematicOn()
    {
        ZombieAnimator.enabled = true;
        
        for (int i = 0; i < GetRigidbodies.Count; i++)
        {
            GetRigidbodies[i].isKinematic = true;
        }
    }

    void RigidbodyIsKinematicOff()
    {
        ZombieAnimator.enabled = false;
        
        for (int i = 0; i < GetRigidbodies.Count; i++)
        {
            GetRigidbodies[i].isKinematic = false;
        }
    }

    void ZombieController()
    {
        ZombieStateInfo = ZombieAnimator.GetCurrentAnimatorStateInfo(0);

          if (ZombieHealth > 0)
          {
                if (ZombieStateInfo.IsName("Zombie Attack") || ZombieStateInfo.IsName("Zombie Biting"))
                {
                    ZombiesSpeed = 0;
                }
                else
                {
                    ZombiesSpeed = 2f;
                    
                    gameObject.transform.LookAt(Player.transform.position);
                    
                    gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, Player.transform.position, ZombiesSpeed * Time.deltaTime);
                }
          }
    }

    public void TakeDamage(int damage) 
    {
        ZombieHealth = ZombieHealth - damage;
        
        ZombieAnimator.SetTrigger("hit");
        
        if (ZombieHealth <= 0)
        {
            RigidbodyIsKinematicOff();

            gameObject.GetComponent<CapsuleCollider>().enabled = false;

            Instantiate(ammoBox.gameObject, gameObject.transform.position, Quaternion.identity);

            _waves.ZombieKillsOnWave++;
        }
    }
}
