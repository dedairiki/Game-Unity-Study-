using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentacaoPersonagem : MonoBehaviour
{


    Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject gameOver;
    public ControlaInterface scriptControlaInterface;
    public AudioClip SomDeDano;
    public Status statusJogador;
    private Rigidbody rigidBodyPersonagem;
    private Animator AnimatorPersonagem;
    private MovimentaJogador meuMovimentaJogador;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rigidBodyPersonagem = GetComponent<Rigidbody>();
        AnimatorPersonagem = GetComponent<Animator>();
        meuMovimentaJogador = GetComponent<MovimentaJogador>();
        statusJogador = GetComponent<Status>();
        statusJogador.Vida = 100;
    }

    // Update is called once per frame
    void Update()
    {
        MovendoPersonagem();

        AlterandoAnimacaoPersonagem();
    }

    private void FixedUpdate()
    {
        rigidBodyPersonagem.MovePosition(rigidBodyPersonagem.position + (direcao * Time.deltaTime * statusJogador.Velocidade));

        //Rotacionar Personagem de acordo com o mouse
        meuMovimentaJogador.RotacionarJogador(MascaraChao);

    }

    public void Rotacionar(Vector3 direcao)
    {
        Quaternion novaRotacao = Quaternion.LookRotation(direcao);
        rigidBodyPersonagem.MoveRotation(novaRotacao);

    }

    private void MovendoPersonagem()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoY = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoY);
    }

    private void AlterandoAnimacaoPersonagem()
    {
        //meuMovimentaJogador.AlteraAnimacaoAndarJogador(direcao);


        if (direcao != Vector3.zero)
        {
            AnimatorPersonagem.SetBool("Movendo", true);

        }
        else
        {
            AnimatorPersonagem.SetBool("Movendo", false);
        }

        //if (!EstaVivo())
        //{
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        SceneManager.LoadScene("SampleScene");

        //    }
        //}
    }

    public void TomarDano(int dano)
    {
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        statusJogador.Vida -= dano;
        scriptControlaInterface.AtualizaVidaJogador();

        if (!EstaVivo())
        {
            Morrer();
        }

    }

    private void Morrer()
    {
        Time.timeScale = 0f;
        scriptControlaInterface.GameOver();
        //gameOver.SetActive(true);
    }

    private bool EstaVivo()
    {
        return statusJogador.Vida > 0;
    }
}
