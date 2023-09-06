using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoviemntacaoZumbi : MonoBehaviour
{

    public GameObject Jogador;
    public float velocidade = 5;
    public int Vida;
    public int Pontos;
    public AudioClip SomMorte;
    

    private Rigidbody rigidbodyZumbi;
    private Animator animatorZumbi;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Joogador");
        

        rigidbodyZumbi = GetComponent<Rigidbody>();
        animatorZumbi = GetComponent<Animator>();

        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);

        Vida = Random.Range(10, 60);
        Pontos = Vida;

    }

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {


        float distrancia = Vector3.Distance(transform.position, Jogador.transform.position);
        Vector3 direcao = Jogador.transform.position - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyZumbi.MoveRotation(novaRotacao);

        if (distrancia > 2.5)
        {
            rigidbodyZumbi.MovePosition(rigidbodyZumbi.position + (direcao.normalized * velocidade * Time.deltaTime));
            animatorZumbi.SetBool("Atacando", false);
        }
        else
        {
            animatorZumbi.SetBool("Atacando", true);

        }

    }


    public void TomaDano(int dano)
    {
        Vida -= dano;

        if (!EstaVivo())
        {

            ControlaAudio.instancia.PlayOneShot(SomMorte);

            // atualizando a pontuacao
            ControlaInterface scriptControlaInterface = FindObjectOfType<ControlaInterface>();
            if (scriptControlaInterface != null)
            {
                scriptControlaInterface.AtualizaPontuação(Pontos);
            }



    Destroy(this.gameObject);
        }
    }

    private bool EstaVivo()
    {
        return Vida > 0;
    }

    void AtacaJogador()
    {
        int dano = Random.Range(0, 30);

        Jogador.GetComponent<MovimentacaoPersonagem>().TomarDano(dano);
    }
}
