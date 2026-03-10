using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;      
    public Vector3 offset;        
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;        
        Vector3 targetPosition = player.position + offset;
        //retardo de movimiento
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothPosition;
    }
}