using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

      [SerializeField] private float tiempoDeVida;

    private void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }


}
