using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialBlock : MonoBehaviour
{
    private int cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        cont++;
        if (cont >= 2)
        {
            Destroy(gameObject);
        }
    }
}
