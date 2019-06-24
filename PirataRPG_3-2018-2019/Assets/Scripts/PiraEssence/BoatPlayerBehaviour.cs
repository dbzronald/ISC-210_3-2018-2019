using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatPlayerBehaviour : MonoBehaviour
{
    private float _speed = 5f;

    private Vector3 _deltaPos;

    private const float VERTICALUPPERLIMIT = 4f, VERTICALLOWERLIMIT = -4f;

    public int HitPoints;


    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _deltaPos = new Vector3(0, Input.GetAxis("Vertical")* _speed * Time.deltaTime);

        animator.SetFloat("Orientation", _deltaPos.y);

        gameObject.transform.Translate(_deltaPos);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, Mathf.Clamp(gameObject.transform.position.y,
            VERTICALLOWERLIMIT, VERTICALUPPERLIMIT));
    }

    public void OnHitted()
    {
        HitPoints--;
        Destroy(GameObject.Find("HitPoints").transform.GetChild(0).gameObject);

        if (HitPoints == 0)
        {
            //GameOver
            Destroy(gameObject);
        }
    }
}
