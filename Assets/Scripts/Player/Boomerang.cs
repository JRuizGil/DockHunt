using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [Header("Elementos principales")]
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotation = boom.GetComponent<BoomerangRotation>();
        rotation.enabled = false;

        boom.transform.parent = boomPos;
        boom.transform.localPosition = Vector3.zero;
        boom.transform.localRotation = Quaternion.identity;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))                   //MIRAR ESTO SUPER URGENTE (INPUT SYSTEM) MANDO MANDO MANDOOOOO
        {
            if (isThrown || isReturning) return;
            {
                Distance();
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
}
