using UnityEngine;

public class LookAtCameraNPC : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Transform player;
    private float rotationSpeed = 5f;
    private Vector3 lastPosition;

    void Start()
    {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        if (player == null) player = transform;
        lastPosition = player.position;
    }

    void LateUpdate()
    {
        Camera cam = Camera.main;

        // Posición de la cámara pero con la misma altura que el objeto
        Vector3 targetPosition = cam.transform.position;
        targetPosition.y = transform.position.y;

        Quaternion targetRotation = Quaternion.LookRotation(transform.position - targetPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,rotationSpeed * Time.deltaTime);

        // Flip según movimiento
        Vector3 delta = player.position - lastPosition;

        if (delta.x > 0.01f)
            sprite.flipX = false;
        else if (delta.x < -0.01f)
            sprite.flipX = true;

        lastPosition = player.position;
    }
}