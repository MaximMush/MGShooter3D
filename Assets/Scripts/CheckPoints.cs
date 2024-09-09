using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using Unity.VisualScripting;

public class CheckPoints : MonoBehaviour
{
    public GameObject Lvl2;

    private void Start()
    {
        Lvl2 = Resources.Load("Prefabs/CheckPoint") as GameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Lvl2)
        {
            EditorSceneManager.LoadScene("Mountains");
        }
    }
}
