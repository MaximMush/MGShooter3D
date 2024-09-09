using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private Slider slider;

    public int health = 100;

    private void Start()
    {
        slider = FindObjectOfType<Slider>();
    }

    void Update()
    {
        slider.value = health;
        Death();
    }

    public void TakePlayerDamage(int PlayerDAmage)
    {
        health -= PlayerDAmage;
    }

    public void Death() 
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
