using UnityEngine;
// Script para hacer que el barco flote de manera realista, con un movimiento de subida y bajada lento y una rotación limitada para simular el balanceo del mar.
//Aportacion josu 
public class FloatingBoat : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f; // Altura del movimiento de flotación
    [SerializeField] private float frequency = 0.5f; // Frecuencia del movimiento (más bajo = más lento)
    [SerializeField] private float maxAngle = 10f; // Ángulo máximo de rotación en grados

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float phaseOffset;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        phaseOffset = Random.Range(0f, 2f * Mathf.PI); // Offset aleatorio para desincronizar
    }

    void Update()
    {
        // Movimiento de flotación vertical lento
        float yOffset = Mathf.Sin((Time.time + phaseOffset) * frequency) * amplitude;
        transform.position = initialPosition + Vector3.up * yOffset;

        // Rotación limitada para simular balanceo
        float angleX = Mathf.Sin((Time.time + phaseOffset) * frequency * 0.8f) * maxAngle;
        float angleZ = Mathf.Sin((Time.time + phaseOffset) * frequency * 1.2f) * maxAngle;
        // Aplicar rotación limitada
        transform.rotation = initialRotation * Quaternion.Euler(angleX, 0, angleZ);
    }
}
