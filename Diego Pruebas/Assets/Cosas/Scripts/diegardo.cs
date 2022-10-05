using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class diegardo : MonoBehaviour
{
    private Rigidbody2D rb2D;

    [Header("Movimiento")]

    private float movimientoHorizontal = 0f;

    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float suavizadoDeMovimiento;
    [Range(0, 1f)][SerializeField] Vector3 velocidad = Vector3.zero;

    private bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;      

    [Header("Dash")]
    [SerializeField] private float velocidadDash;
    [SerializeField] private float tiempoDash;   
    private float gravedadInicial;
    private bool puedeHacerDash = true;
    private bool sePuedeMover = true;      

    [Header("Animacion")]
    private Animator animator;

    private bool salto = false; 

    private void Start(){

       rb2D = GetComponent<Rigidbody2D>();
       animator = GetComponent<Animator>();
       gravedadInicial = rb2D.gravityScale;
    }

    private void Update() {
    movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;
    
    animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

    if(Input.GetButtonDown("Jump")){
        salto = true;
    }

    if(Input.GetKeyDown(KeyCode.LeftShift) && puedeHacerDash){
        StartCoroutine(Dash());
    }

    }

    private void FixedUpdate() {

      enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);  
      animator.SetBool("enSuelo", enSuelo);
      //mover
      if(sePuedeMover){
      Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
      }

      salto = false;
    }

    private void Mover(float mover, bool saltar){
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if(mover>0 && !mirandoDerecha){
        Girar();
        }else if (mover < 0 && mirandoDerecha){
        Girar();
        }

        if(enSuelo && saltar){
         enSuelo = false;
         rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));

        }
      }

    private IEnumerator Dash(){
       
       sePuedeMover = false;
       puedeHacerDash = false;
       rb2D.gravityScale = 0;
       rb2D.velocity = new Vector2(velocidadDash * transform.localScale.x, 0);
       animator.SetTrigger("Dash");

       yield return new WaitForSeconds(tiempoDash);

       sePuedeMover = true;
       puedeHacerDash = true;
       rb2D.gravityScale = gravedadInicial;
    }

    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }   

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}
