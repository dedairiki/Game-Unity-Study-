using UnityEngine;

public class AnimacaoPersonagem : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Atacar(bool estado)
    {
        animator.SetBool("Atacando", estado);
    }



}
