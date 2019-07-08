using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnZone : MonoBehaviour
{
    public TextMesh Life;
    private int cont;


    // Start is called before the first frame update
    void Start()
    {
        cont = 4;
        SetCountText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CanonBall>().Respawn();
        SetCountText();
    }

    void SetCountText()
    {
        cont = cont - 1;
        if (cont <= 0)
        {
            cont = 0;
        }
        Life.text = "VIDAS: " + cont.ToString();
    }
}
