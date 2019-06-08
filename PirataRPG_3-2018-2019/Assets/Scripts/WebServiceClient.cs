using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebServiceClient : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            StartCoroutine(SendResquest("https://my.api.mockaroo.com/my_saved_schema.json?key=6b179840"));
        }

        if (Input.GetButtonDown("Fire2"))
        {
            StartCoroutine(PostRequest("https://localhost:44311/api/values"));
        }
    }

    IEnumerator PostRequest(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("PlayerName", "Mario");

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        Debug.Log("Recibido: " + uwr.downloadHandler.text);

    }



    IEnumerator SendResquest(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);

        yield return webRequest.SendWebRequest();

        Debug.Log(webRequest.downloadHandler.text);
    }
}
