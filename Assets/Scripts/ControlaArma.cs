using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Bala;
    public GameObject CanoArma;
    public AudioClip SomdoTiro;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Instantiate(Bala, CanoArma.transform.position, CanoArma.transform.rotation);
            ControlaAudio.instancia.PlayOneShot(SomdoTiro);

        }
    }
}
