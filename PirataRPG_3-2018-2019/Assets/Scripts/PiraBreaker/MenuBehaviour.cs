using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    public TextMesh playText;
    public GameObject playT;
    public GameObject exitT;

    public TextMesh exitText;
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
        if (gameObject.name == "Play")
        {

            SceneManager.LoadScene("Brick");

        }

        if (gameObject.name == "Exit")
        {

            Application.Quit();

        }
    }
}
