using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZumbi : MonoBehaviour
{

    public GameObject Zumbi;
    float contadorTempo = 0;
    public float tempoGerarZumbi = 1;
    private int quantidadeZumbi = 50;
    public LayerMask LayerZumbi;

    // Start is called before the first frame update
    void Start()
    {
        tempoGerarZumbi = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {

        contadorTempo += Time.deltaTime;

        if (contadorTempo >= tempoGerarZumbi && quantidadeZumbi > 0)
        {
            StartCoroutine(GerarNovoZumbi());
            contadorTempo = 0;

            quantidadeZumbi--;
        }




    }

    private IEnumerator GerarNovoZumbi()
    {
        Vector3 posicaoDeCriacao = AleatorizarPosicao();
        //Collider[] colizores = Physics.OverlapSphere(posicaoDeCriacao, 1);
        Collider[] colisores = Physics.OverlapSphere(posicaoDeCriacao, 1, LayerZumbi);


        while (colisores.Length > 0)
        {
            posicaoDeCriacao = AleatorizarPosicao();
            colisores = Physics.OverlapSphere(posicaoDeCriacao, 1);
            yield return null;
        }

        Instantiate(Zumbi, posicaoDeCriacao, transform.rotation);
    }

    Vector3 AleatorizarPosicao()
    {
        Vector3 posicao = Random.insideUnitSphere * 3;
        posicao += transform.position;
        posicao.y = 0;

        return posicao;

    }
}
