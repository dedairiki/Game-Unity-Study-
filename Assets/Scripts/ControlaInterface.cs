using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlaInterface : MonoBehaviour
{

    private MovimentacaoPersonagem scriptControlaJogador;
    public Slider sliderVidadoJogador;
    public TextMeshProUGUI textoPontuacao;

    private int pontos = 0;

    // Start is called before the first frame update
    void Start()
    {
        scriptControlaJogador = GameObject.FindWithTag("Joogador").GetComponent<MovimentacaoPersonagem>();
        sliderVidadoJogador.maxValue = scriptControlaJogador.Vida;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizaVidaJogador()
    {
        sliderVidadoJogador.value = scriptControlaJogador.Vida;
    }

    public void AtualizaPontuação(int _pontos)
    {
        pontos += _pontos;
        textoPontuacao.text = pontos.ToString("0000000");

    }
}
