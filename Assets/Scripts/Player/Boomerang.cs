using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Boomerang : MonoBehaviour
{
    [Header("Elementos propios boomerang")]
    [SerializeField] GameObject boom;
    [SerializeField] Transform boomPos;
    [SerializeField] Transform boomRot;
    [SerializeField] float boomDist;
    [SerializeField] float boomSpeed;
    [SerializeField] private LayerMask layMask;
    private bool isThrown;
    private bool isReturning;
    private Vector3 DistPos;
    private BoomerangRotation rotation;

    public CapsuleCollider boomCollider;
    public MeshRenderer boomRenderer;
    private bool attacking = false; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rotation = boom.GetComponent<BoomerangRotation>();
        boomCollider = GetComponent<CapsuleCollider>();
        boomRenderer = GetComponent<MeshRenderer>();
        rotation.enabled = false;

        boom.transform.parent = boomPos;
        boom.transform.localPosition = Vector3.zero;
        boom.transform.localRotation = Quaternion.identity;
        boomCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        Lanzar();

        if (isThrown)
        {
            Vector3 newPosition = Vector3.MoveTowards(boom.transform.position, DistPos, boomSpeed * Time.deltaTime);
            boom.transform.position = newPosition;
            if (boom.transform.position == DistPos)
            {
                isThrown = false;
                isReturning = true;
            }
        }
        if (isReturning)
        {
            Vector3 newPos = Vector3.MoveTowards(boom.transform.position, boomPos.position, boomSpeed * Time.deltaTime);        
            boom.transform.position = newPos;

            if (boom.transform.position == boomPos.position)
            {
                isReturning = false;
                rotation.enabled = false;
                boom.transform.parent = boomPos;
                boom.transform.rotation = boomRot.rotation;
            }
        }

    }
    void Lanzar()       //Con todo el tema de la distancia
    {
        if (Mouse.current.leftButton.wasPressedThisFrame || Gamepad.current != null &&Gamepad.current.buttonWest.wasPressedThisFrame)                   //MIRAR ESTO SUPER URGENTE (INPUT SYSTEM) MANDO MANDO MANDOOOOO
        {
            if (isThrown || isReturning) return;
            {
                Distance();
                StartCoroutine(Attack());
            }
        }
    }
    void Distance() //Se va a la distancia que metas
    {
        
            DistPos = boomPos.position + boomPos.forward * boomDist;
            boom.transform.parent = null;
            rotation.enabled = true;
            isThrown = true;
        
    }
    IEnumerator Attack()
    {
        attacking = true;
        boomCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        boomCollider.enabled = false;
        attacking = false;
    }
}
