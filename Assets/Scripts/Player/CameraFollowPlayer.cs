using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    public bool lockX;
    public bool lockY;
    public bool lockZ;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPosition = player.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        Vector3 finalPosition = smoothPosition;

        if (lockX) finalPosition.x = transform.position.x;
        if (lockY) finalPosition.y = transform.position.y;
        if (lockZ) finalPosition.z = transform.position.z;

        transform.position = finalPosition;
    }
}