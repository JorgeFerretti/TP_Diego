using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{

[SerializeField] private float velocidad;

[SerializeField] private float daño;



    void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
       
    }

        private void OnTriggerEnter2D(Collider2D other){
            if(other.CompareTag("Enemigo")){
                other.GetComponent<Enemigo>().TomarDaño(daño);
                Destroy(gameObject);
            }
        
        }


}
