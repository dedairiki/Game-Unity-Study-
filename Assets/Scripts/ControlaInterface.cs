using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{

    private MovimentacaoPersonagem scriptControlaJogador;
    public Slider sliderVidadoJogador;
    public TextMeshProUGUI textoPontuacao;
    public GameObject PainelDeGameOver;
    public TextMeshProUGUI TextoSobrevivencia;
    public TextMeshProUGUI TextoMaximo;

    private float tempoMaximoSalvo;

    private int pontos = 0;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Joogador").GetComponent<MovimentacaoPersonagem>();
        sliderVidadoJogador.maxValue = scriptControlaJogador.statusJogador.Vida;
        tempoMaximoSalvo = PlayerPrefs.GetFloat("PontuacaoMaxima");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AtualizaVidaJogador()
    {
        sliderVidadoJogador.value = scriptControlaJogador.statusJogador.Vida;
    }

    public void AtualizaPontuação(int _pontos)
    {
        pontos += _pontos;
        textoPontuacao.text = pontos.ToString("0000000");

    }

    public void GameOver()
    {
        PainelDeGameOver.SetActive(true);
        int minutos = (int)( Time.timeSinceLevelLoad / 60);
        int segundos = (int)(Time.timeSinceLevelLoad % 60);
        TextoSobrevivencia.text = $"Voce Sobrevivel {minutos} minutos e {segundos} segundos.";

        AjustarPontuacao(minutos, segundos);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("SampleScene");
    }


    void AjustarPontuacao( int minutos, int segundos)
    {
        if (Time.timeSinceLevelLoad > tempoMaximoSalvo)
        {
            tempoMaximoSalvo = Time.timeSinceLevelLoad;
            TextoMaximo.text = $"Voce Sobrevivel {minutos} minutos e {segundos} segundos.";
            PlayerPrefs.SetFloat("PontuacaoMaxima", tempoMaximoSalvo);
        }

        if (TextoMaximo.text == "")
        {
            minutos = (int)(tempoMaximoSalvo / 60);
            segundos = (int)(tempoMaximoSalvo % 60);

            TextoMaximo.text = $"Maior tempo: {minutos} minutos e {segundos} segundos.";

        }
        
    }

}
