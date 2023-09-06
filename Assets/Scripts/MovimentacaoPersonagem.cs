using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentacaoPersonagem : MonoBehaviour
{

    public float velocidade = 5;
    Vector3 direcao;
    public LayerMask MascaraChao;
    public GameObject gameOver;
    public int Vida = 100;
    public ControlaInterface scriptControlaInterface;

    private Rigidbody rigidBodyPersonagem;
    private Animator AnimatorPersonagem;
    public AudioClip SomDeDano;


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        rigidBodyPersonagem = GetComponent<Rigidbody>();
        AnimatorPersonagem = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovendoPersonagem();

        AlterandoAnimacaoPersonagem();
    }

    private void FixedUpdate()
    {
        rigidBodyPersonagem.MovePosition(rigidBodyPersonagem.position + (direcao * Time.deltaTime * velocidade));


        //Rotacionar Personagem de acordo com o mouse
        RotacionaPersonagem();

    }

    private void MovendoPersonagem()
    {
        float eixoX = Input.GetAxis("Horizontal");
        float eixoY = Input.GetAxis("Vertical");
        direcao = new Vector3(eixoX, 0, eixoY);
    }

    private void AlterandoAnimacaoPersonagem()
    {
        if (direcao != Vector3.zero)
        {
            AnimatorPersonagem.SetBool("Movendo", true);

        }
        else
        {
            AnimatorPersonagem.SetBool("Movendo", false);
        }

        if (!EstaVivo())
        {
            if (Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("SampleScene");

            }
        }
    }

    private void RotacionaPersonagem()
    {
        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Exibe uma linha da camera para onde o mouse esta apontando. 
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador);

            rigidBodyPersonagem.MoveRotation(novaRotacao);

        }
    }

    public void TomarDano(int dano)
    {
        ControlaAudio.instancia.PlayOneShot(SomDeDano);
        Vida -= dano;
        scriptControlaInterface.AtualizaVidaJogador();

        if (!EstaVivo())
        {
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }

    }

    private bool EstaVivo()
    {
        return Vida > 0;
    }
}
