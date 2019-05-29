using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerBehaviour : MonoBehaviour
{
    private bool _isLeftPlayer;

    public float _speed;

    private Vector3 _deltaPos;

    private const float UPPPERLIMIT = 3.5f, LOWERLIMIT = -3.5f;
    // Start is called before the first frame update

    private void Awake()
    {
        _isLeftPlayer = gameObject.name == "LeftPlayer";
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _deltaPos = new Vector3(0, (_isLeftPlayer ? Input.GetAxis("LeftPlayer") : Input.GetAxis("Vertical")) * _speed * Time.deltaTime);
        transform.Translate(_deltaPos);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, LOWERLIMIT, UPPPERLIMIT), transform.position.z);
    }
}
