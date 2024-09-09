using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public GameObject pistol;
    void Start()
    {
        pistol = GameObject.FindGameObjectWithTag("Pistol"); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pistol.GetComponent<Pistol>().BagAmmo = pistol.GetComponent<Pistol>().BagAmmo + 10;
       
            Destroy(gameObject);
        }
    }
}
