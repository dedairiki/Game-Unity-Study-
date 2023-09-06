using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float Velocidade = 20;
    private int contador = 0;
    private GameObject zumbi;
    //public AudioClip somDeMorte;
    


    // Update is called once per frame
    void FixedUpdate()
    {
        GetComponent<Rigidbody>().MovePosition(GetComponent<Rigidbody>().position + transform.forward * Time.deltaTime * Velocidade);

        if (contador >= 100)
        {
            Destroy(gameObject);
        }
        contador++;
    }

    private void OnTriggerEnter(Collider objetoDeColisao)
    {
        if (objetoDeColisao.tag == "Inimigo")
        {
            Destroy(objetoDeColisao.gameObject);

            objetoDeColisao.GetComponent<MoviemntacaoZumbi>().TomaDano(CalculoDano());
            //ControlaAudio.instancia.PlayOneShot(somDeMorte);

            
        }

        Destroy(this.gameObject);
    }


    private int CalculoDano()
    {
        int critico = Random.Range(0, 10);

        if (critico > 8)
        {
            return 60;
        }

        return 30;

    }


}
