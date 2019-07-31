using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CanonBall : MonoBehaviour
{
    public float speed;

    public AudioSource HitFX;

    public TextMesh Score;

    public TextMesh Timer;

    private float tCount;

    private int cont = 0;

    // Start is called before the first frame update
    void Start()
    {
        Respawn();
    }

    // Update is called once per frame
    void Update()
    {
        tCount += Time.deltaTime;

        Timer.text = "Tiempo: " + Mathf.Round(tCount);
    }

    public void Respawn()
    {

        transform.position = Vector3.zero;

        GetComponent<Rigidbody>().velocity = Random.insideUnitCircle.normalized * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("SBlock") || other.gameObject.CompareTag("Block"))
        {
            cont = cont + 10;
        }
        SetCountText();
        HitFX.Play();

        if (cont == 100 )
        {
            gameObject.transform.localScale = new Vector3(0.7f, 0.7f,0);
            speed = 20f;
        }

        if (cont == 150)
        {
            SceneManager.LoadScene("Credits");
        }

        StartCoroutine(PostRequest("http://localhost:3000/api/Scoreboards"));

    }

    void SetCountText()
    {
        Score.text = "PUNTAJE: " + cont.ToString();
    }

    IEnumerator PostRequest(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("puntaje", cont);
        form.AddField("tiempo", (int) tCount);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        Debug.Log("Recibido: " + uwr.downloadHandler.text);

    }

}
