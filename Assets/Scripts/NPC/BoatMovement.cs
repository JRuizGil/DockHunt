using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    private Vector3 StartingPos;
    private Vector3 EndPos;
    public float distance = 300f;
    private float MoveTime = 10f;
    private float CurrentMoveTime = 0;
    void Start()
    {
        StartingPos = transform.position;
        EndPos = transform.position + Vector3.left * distance;
    }
    void Update()
    {
        CurrentMoveTime += Time.deltaTime;
        float t = CurrentMoveTime / MoveTime;
        t = Mathf.SmoothStep(0f, 1f, t);
        transform.position = Vector3.Lerp(StartingPos, EndPos, t);
    }
}
