using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongPlayerBehaviour : MonoBehaviour
{
    private bool _isLeftPlayer;

    public float _speed;

    private Vector3 _deltaPos;

    private const float UPPPERLIMIT = 3.5f, LOWERLIMIT = -3.5f;

    private static bool _onePlayer;

    private GameObject _ball;
    // Start is called before the first frame update

    private void Awake()
    {
        _isLeftPlayer = gameObject.name == "LeftPlayer";
        _ball = GameObject.Find("Ball");
    }
    void Start()
    {
        _onePlayer = true; // Esto s para probar. Se supone que debe
        // ser elegido por el jugador.
    }

    // Update is called once per frame
    void Update()
    {
        float desde, hasta;
        desde = gameObject.transform.position.y < _ball.transform.position.y
            ? gameObject.transform.position.y
            : _ball.transform.position.y;

        hasta = gameObject.transform.position.y > _ball.transform.position.y
            ? gameObject.transform.position.y
            : _ball.transform.position.y;

        if (_onePlayer && ! _isLeftPlayer)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(Mathf.Lerp(desde, hasta, 0.5f), LOWERLIMIT, UPPPERLIMIT), transform.position.z);
            return;
            
        }
        _deltaPos = new Vector3(0, (_isLeftPlayer ? Input.GetAxis("LeftPlayer") :  Input.GetAxis("Vertical")) * _speed * Time.deltaTime);
        transform.Translate(_deltaPos);
        transform.position = new Vector3(transform.position.x, 
            Mathf.Clamp(transform.position.y, LOWERLIMIT, UPPPERLIMIT), transform.position.z);
    }
}
