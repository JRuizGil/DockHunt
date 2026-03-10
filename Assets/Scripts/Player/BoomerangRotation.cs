using UnityEngine;

public class BoomerangRotation : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float speedZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speedX, speedY, speedZ);
    }
}
