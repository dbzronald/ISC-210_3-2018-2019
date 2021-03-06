﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    private bool? isLeftPlayer;

    private float _startingSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        UpadateBallState();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftPlayer != null && Input.GetButtonDown("Fire1"))
        {
            if (gameObject.transform.parent.transform.position.x < gameObject.transform.position.x)
            {
                _startingSpeed = -_startingSpeed;
            }

            gameObject.transform.SetParent(null);

            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-_startingSpeed,_startingSpeed * (Random.Range(0,2) == 0 ? 1 : -1));

            isLeftPlayer = null;
        }
        
    }

    public void UpadateBallState()
    {
        switch (transform.parent.name)
        {
            case "LeftPlayer":
                isLeftPlayer = true;
                break;
            case "RightPlayer":
                isLeftPlayer = false;
                break;
            default:
                isLeftPlayer = null;
                break;

        }
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}
