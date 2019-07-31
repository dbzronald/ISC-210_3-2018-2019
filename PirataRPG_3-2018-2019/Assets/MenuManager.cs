using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (gameObject.name == "PlayText")
        {
            SceneManager.LoadScene("Tavern");
        }

        if (gameObject.name == "OptionsText")
        {
            SceneManager.LoadScene("Options");
        }

        if (gameObject.name == "Credits")
        {
            SceneManager.LoadScene("Credits");
        }

        if (gameObject.name == "Back")
        {
            SceneManager.LoadScene("Test");

        }

        //-------------------------------

        if (gameObject.name == "Explo")
        {
            SceneManager.LoadScene("ExplorationLevel");
        }

        if (gameObject.name == "PiraB")
        {
            SceneManager.LoadScene("Brick");
        }

        if (gameObject.name == "PiraE")
        {
            SceneManager.LoadScene("Essence");
        }

        if (gameObject.name == "PiraP")
        {
            SceneManager.LoadScene("PiraPon");
        }

        //------------------------------

        if (gameObject.name == "Back")
        {
            SceneManager.LoadScene("Test");

        }

        if (gameObject.name == "ExitText")
        {
            Application.Quit();
        }

    }
}
