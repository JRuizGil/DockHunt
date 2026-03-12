using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{

    public GameObject AttackRangeObject;
    public SphereCollider AttackCollider;
    public MeshRenderer AttackRenderer;
    private Material mat;
    bool attacking = false;
    private AudioSource clip;
    private void Start()
    {
        AttackCollider = GetComponentInChildren<SphereCollider>(); 
        clip = GetComponent<AudioSource>();
        var m =AttackRangeObject.GetComponent<MeshRenderer>();
        mat = m.material;
        mat.color = new Color(0f, 1f, 0f, 0.3f);
        //desactivo el collider al inicio
        AttackCollider.enabled = false;
    }
    void Update()
    {
        //input action de ataque, el boton de click izquierdo, si ya esta atacando no deja para que no se hagan miles de ataques en un segundo
        if (Mouse.current.leftButton.wasPressedThisFrame && !attacking)
        {
            StartCoroutine(AttackDo());
        }
    }
    // la corutina del ataque, para poder meter cosas entre medias como cambiar el material o en este caso, activar y desactivar el collider
    IEnumerator AttackDo()
    {
        attacking = true;
        AttackCollider.enabled = true;
        clip.Play();
        mat.color = new Color(1f, 0f, 0f, 0.3f);        
        yield return new WaitForSeconds(0.2f);
        AttackCollider.enabled = false;
        mat.color = new Color(0f, 1f, 0f, 0.3f);
        attacking = false;
    }
}
