using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ControlaAudio : MonoBehaviour
{
    private AudioSource audioSource;

    public static AudioSource instancia;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        instancia = audioSource;
    }



    public void Ripo()
    {

    }





}
