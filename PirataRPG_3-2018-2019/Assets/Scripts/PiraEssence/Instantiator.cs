using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    public List<GameObject> Essences;

    public GameObject Spike;

    const int _essenceQuantity = 6;

    private float _nextTime;

    private const float _LOWERTIME = 1f, _UPPERTIME = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InstantiatorCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator InstantiatorCoroutine()
    {
        _nextTime = Random.Range(_LOWERTIME, _UPPERTIME);
    }
}
