using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public GameObject AttackRangeObject;
    public SphereCollider AttackCollider;
    private Material mat;
    public MeshRenderer m;
    bool attacking = false;
    private AudioSource clip;

    public GameObject Fregona;
    public Animator fregonaanim;

    public float eraseCooldownValue = 10f;
    public Slider CooldownSlider;
    public float CooldownRegenValue = 1f;

    
    [Header("Elementos propios boomerang")]
    public bool boomerangIsActive;
    [SerializeField] float boomDist;
    [SerializeField] float boomSpeed;
    private bool isThrown;    
    private BoomerangRotation boomRotation;

    public void Awake()
    {
        CooldownSlider.maxValue = 100f;
        CooldownSlider.value = CooldownSlider.maxValue;
        boomRotation = GetComponentInChildren<BoomerangRotation>();
        AttackCollider = GetComponentInChildren<SphereCollider>();
        clip = GetComponent<AudioSource>();
        //desactivo el collider al inicio
        AttackCollider.enabled = false;
        fregonaanim.SetBool("Boom", false);
    }
    private void Start()
    {                 
        mat = m.material;
        mat.color = new Color(1f, 1f, 1f, 0.1f);      
        
    }
    void Update()
    {
        //input action de ataque, el boton de click izquierdo, si ya esta atacando no deja para que no se hagan miles de ataques en un segundo
        if (Mouse.current.leftButton.wasPressedThisFrame && !attacking || Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame && !attacking)
        {
            if (!boomerangIsActive)
            {
                if (CooldownSlider.value > eraseCooldownValue)
                {
                    CooldownSlider.value -= eraseCooldownValue;
                    
                    StartCoroutine(AttackDo());                    
                }
            }
            else
            {
                if (CooldownSlider.value > eraseCooldownValue)
                {
                    CooldownSlider.value -= eraseCooldownValue;
                    boomThrow();
                }
            }
                       
        }
    }


    public void LateUpdate ()
    {
        RegenCooldown();
    }
    // la corutina del ataque, para poder meter cosas entre medias como cambiar el material o en este caso, activar y desactivar el collider
    IEnumerator AttackDo()
    {         
        attacking = true;
        AttackCollider.enabled = true;
        clip.Play();
        fregonaanim.SetBool("Boom", false);
        fregonaanim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.1f);        
        AttackCollider.enabled = false;
        attacking = false;
    }
    public void RegenCooldown()
    {
        if (CooldownSlider.value < CooldownSlider.maxValue)
        {
            CooldownSlider.value += CooldownRegenValue * Time.deltaTime;
        }
    }    

    public void boomThrow() //Se va a la distancia que metas
    {
        if (isThrown) return;
        {
            StartCoroutine(BoomAttack());
        }
        
    }
    public IEnumerator BoomAttack()
    {
        attacking = true;
        AttackRangeObject.transform.parent = null;
        boomRotation.enabled = true;
        AttackCollider.enabled = true;
        Vector3 thrownDistance = AttackRangeObject.transform.position + gameObject.transform.forward * boomDist;
        fregonaanim.SetBool("Boom", true);
        fregonaanim.SetTrigger("Attack");
        // Ida
        while (Vector3.Distance(AttackRangeObject.transform.position, thrownDistance) > 0.3f)
        {
            AttackRangeObject.transform.position = Vector3.MoveTowards(AttackRangeObject.transform.position,thrownDistance,boomSpeed * Time.deltaTime);
            yield return null; // espera al siguiente frame
        }
        // Vuelta
        while (Vector3.Distance(AttackRangeObject.transform.position, gameObject.transform.position) > 0.3f)
        {
            AttackRangeObject.transform.position = Vector3.MoveTowards(AttackRangeObject.transform.position,gameObject.transform.position,boomSpeed *2 * Time.deltaTime);
            yield return null; // espera al siguiente frame
        }
        isThrown = false;
        boomRotation.enabled = false;
        AttackRangeObject.transform.parent = gameObject.transform;
        AttackCollider.enabled = false;
        attacking = false;
    }
    
}
