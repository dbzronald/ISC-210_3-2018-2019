using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TablesController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButton("Submit"))
        {
            switch (other.gameObject.name)
            {
                case "TT1":
                    SceneManager.LoadScene("Pirapon");
                    break;
                case "TT2":
                    SceneManager.LoadScene("Brick");
                    break;
                case "TT3":
                    SceneManager.LoadScene("Essence");
                    break;
                case "TT4":
                    SceneManager.LoadScene("ExplorationLevel");
                    break;
            }
        }
    }
}
