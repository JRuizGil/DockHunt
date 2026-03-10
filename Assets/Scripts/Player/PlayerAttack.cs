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
    private void Start()
    {
        AttackCollider = GetComponentInChildren<SphereCollider>();
        AttackRenderer = GetComponentInChildren<MeshRenderer>();        
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
        //mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        AttackCollider.enabled = false;
        //mat.color = Color.green;
        attacking = false;
    }
}
