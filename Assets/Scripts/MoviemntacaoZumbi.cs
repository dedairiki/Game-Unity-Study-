using UnityEngine;
using UnityEngine.Android;

public class MoviemntacaoZumbi : MonoBehaviour
{

    public GameObject Jogador;
    public float velocidade;
    public int Vida;
    public int Pontos;
    public AudioClip SomMorte;

    private AnimacaoPersonagem animaPersonagem;
    private Rigidbody rigidbodyZumbi;
    private Vector3 direcao;

    //private float contadorVagar;
    //private float tempoEntrePosicoesAleatorias = 4;
    private Vector3 PosicaoAleatoria;
    private float distancia;
    //private int vagarParado = 0;
    //private Vector3 distanciaAnterior;



    // Start is called before the first frame update
    void Start()
    {
        Jogador = GameObject.FindWithTag("Joogador");
        animaPersonagem = GetComponent<AnimacaoPersonagem>();

        rigidbodyZumbi = GetComponent<Rigidbody>();
        // animatorZumbi = GetComponent<Animator>();

        ConfigurandoZumbiAleatorio();

        Vida = Random.Range(10, 60);
        Pontos = Vida;

        velocidade = Random.Range(1, 10);
        PosicaoAleatoria = BuscaPosicaoAleatoria();

    }

    private void ConfigurandoZumbiAleatorio()
    {
        int geraTipoZumbi = Random.Range(1, 28);
        transform.GetChild(geraTipoZumbi).gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {


        distancia = Vector3.Distance(transform.position, Jogador.transform.position);



        if (distancia >= 15)
        {
             Vagar();

        }
        else if (distancia > 2.5)
        {

            direcao = Jogador.transform.position - transform.position;

            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            rigidbodyZumbi.MoveRotation(novaRotacao);

            rigidbodyZumbi.MovePosition(rigidbodyZumbi.position + (direcao.normalized * velocidade * Time.deltaTime));
            // animatorZumbi.SetBool("Atacando", false);
            animaPersonagem.Atacar(false);
        }
        else
        {
            Quaternion novaRotacao = Quaternion.LookRotation(direcao);
            rigidbodyZumbi.MoveRotation(novaRotacao);
            //animatorZumbi.SetBool("Atacando", true);
            animaPersonagem.Atacar(true);
        }

    }

    private void Vagar()
    {
        direcao = PosicaoAleatoria - transform.position;

        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidbodyZumbi.MoveRotation(novaRotacao);


        if (Vector3.Distance(transform.position, PosicaoAleatoria) > 0.05)
        {
            rigidbodyZumbi.MovePosition(rigidbodyZumbi.position + (direcao.normalized * velocidade * Time.deltaTime));
            animaPersonagem.Atacar(false);

        }
        else
        {
            PosicaoAleatoria = BuscaPosicaoAleatoria();
        }


    }

    private Vector3 BuscaPosicaoAleatoria()
    {
        Vector3 posicao = Random.insideUnitSphere * 5;
        posicao += transform.position;
        posicao.y = transform.position.y;

        return posicao;
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
