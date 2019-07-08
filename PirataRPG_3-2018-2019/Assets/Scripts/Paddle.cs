using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Vector3 _deltaPos;
    public float speed;
    const  float LEFTLIMIT = -6f, RIGHTLIMIT = 6f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _deltaPos = new  Vector3(Input.GetAxis("Horizontal")*Time.deltaTime* speed, -4.47f, 0);

        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, LEFTLIMIT, RIGHTLIMIT), -4.47f,0);
    }
}
