using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour
{
    private float accelX = -10f;

    private float currentSpeedX = 0f;

    private float deltaX;

    private BoatPlayerBehaviour boatPlayerBehaviour;

    private ScoreController scoreController;

    // Start is called before the first frame update

    private void Awake()
    {
        boatPlayerBehaviour = GameObject.Find("Player").GetComponent<BoatPlayerBehaviour>();
        scoreController = GameObject.Find("Global Scripts").GetComponent<ScoreController>();
    }
    void Start()
    {
        currentSpeedX += accelX * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        deltaX = currentSpeedX * Time.deltaTime + accelX * Mathf.Pow(Time.deltaTime, 2) / 2;

        gameObject.transform.Translate(new Vector3(deltaX, 0f));

        currentSpeedX += accelX * Time.deltaTime;

    }

    private  void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Enemy" && other.gameObject.name == "Player")
        {
            boatPlayerBehaviour.OnHitted();
        }
        else
        {
            scoreController.AddEssence(gameObject.tag);
            Destroy(gameObject);
        }
    }
}
