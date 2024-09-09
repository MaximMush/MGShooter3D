using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Camera cam;

    public float RayLenght = 50;

    public float MarkDamage = 10;

    public ParticleSystem ShootEffect;

    public ParticleSystem HitEffect;

    public AudioSource ShootAudio;

    public float ImpactForce = 1000;

    public int BagAmmo = 10;
    
    public int MaxAmmo = 10;

    public int CurrentAmmo;

    public float ReloadTime = 2f;

    private bool isReloading = false;

    public float FireRate = 0.3f;

    public float NextTimeToFire = 0f;

    public int ZombieDamage = 50;

    private void Start()
    {
        StartCoroutine(Reload());
    }
    private void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (CurrentAmmo <=0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetMouseButtonDown(0) && Time.time >= NextTimeToFire)
        {
            Shoot();

            NextTimeToFire = Time.time + FireRate;
        }
    }

     IEnumerator Reload()
    {
        if (BagAmmo > 0)
        {

            isReloading = true;

            Debug.Log("Reloading...");

            yield return new WaitForSeconds(ReloadTime);

            CurrentAmmo = MaxAmmo;

            BagAmmo = BagAmmo - MaxAmmo;

            isReloading = false;

        }
    }
    void Shoot() 
    {
        CurrentAmmo --;

        ShootEffect.Play();

        ShootAudio.Play();

        RaycastHit hit;
        
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, RayLenght))
        {
            Debug.Log(hit.collider.gameObject.name);

            Target target = hit.transform.GetComponent<Target>();

            ZombieMove zombieMove = hit.transform.GetComponent<ZombieMove>();

            if (target != null) 
            {
                target.TakeDamage(MarkDamage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            if (zombieMove != null)
            {
                zombieMove.TakeDamage(ZombieDamage);
            }

            ParticleSystem CreateHit = Instantiate(HitEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(CreateHit.gameObject, 0.2f);
        }
    }
}
