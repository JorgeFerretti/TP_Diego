using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;

    [SerializeField] private GameObject bala;

   [Header("Animacion")]
    private Animator animator2;



    private void Start(){
       animator2 = GetComponent<Animator>();   
    }


    void Update()
    {      

      if(Input.GetButtonDown("Fire1")){
        Disparar();
        animator2.SetTrigger("disparo");        
      } 
    }

    private void Disparar(){
        Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);     

    }
}
