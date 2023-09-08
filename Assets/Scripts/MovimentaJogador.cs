using UnityEngine;

public class MovimentaJogador : MovimentacaoPersonagem
{


    public void RotacionarJogador(LayerMask MascaraChao)
    {

        Ray raio = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Exibe uma linha da camera para onde o mouse esta apontando. 
        Debug.DrawRay(raio.origin, raio.direction * 100, Color.red);

        RaycastHit impacto;

        if (Physics.Raycast(raio, out impacto, 100, MascaraChao))
        {
            Vector3 posicaoMiraJogador = impacto.point - transform.position;

            posicaoMiraJogador.y = transform.position.y;

            Quaternion novaRotacao = Quaternion.LookRotation(posicaoMiraJogador, Vector3.up);
            
            Rotacionar(posicaoMiraJogador);
        }

    }



}
