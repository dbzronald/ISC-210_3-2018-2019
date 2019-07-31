using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CollectableBehaviour : MonoBehaviour
{
    private float accelX = -10f;

    private float currentSpeedX = 0f;

    private float deltaX;

    private BoatPlayerBehaviour boatPlayerBehaviour;

    private ScoreController scoreController;

    private int cont = 0;

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
        if (other.gameObject.name != "Player")
        {
            return;
        }

        if (gameObject.tag == "Enemy")
        {
            boatPlayerBehaviour.OnHitted();
        }

        else
        {
            scoreController.AddEssence(gameObject.tag);
            cont = cont + 1;
            Destroy(gameObject);
        }

        StartCoroutine(PostRequest("http://localhost:3000/api/Scoreboards"));
    }


    IEnumerator PostRequest(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("puntaje", cont);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        Debug.Log("Recibido: " + uwr.downloadHandler.text);

    }
}
