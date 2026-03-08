using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject AttackRange;
    public SphereCollider AttackCollider;
    public MeshRenderer AttackRenderer;
    private Material mat;
    bool attacking = false;
    private void Start()
    {
        AttackCollider = GetComponentInChildren<SphereCollider>();
        AttackRenderer = GetComponentInChildren<MeshRenderer>();

        mat = AttackRenderer.material;

        // Empieza verde
        mat.color = Color.green;

        AttackCollider.enabled = false;
    }
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && !attacking)
        {
            StartCoroutine(AttackDo());
        }
    }

    IEnumerator AttackDo()
    {
        attacking = true;

        AttackCollider.enabled = true;
        mat.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        AttackCollider.enabled = false;
        mat.color = Color.green;

        attacking = false;
    }
}
