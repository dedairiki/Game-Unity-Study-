using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZumbi : MonoBehaviour
{

    public GameObject Zumbi;
    float contadorTempo = 0;
    public float tempoGerarZumbi = 1;

    // Start is called before the first frame update
    void Start()
    {
        tempoGerarZumbi = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoGerarZumbi)
        {
            Instantiate(Zumbi, transform.position, transform.rotation);
            contadorTempo = 0;
        }


    }
}
