using UnityEngine;
using UnityEngine.Events;

public class DayCycleSun : MonoBehaviour
{
    public float cycleDuration = 90f;

    public float warmTemp = 2000f;
    public float coldTemp = 6500f;

    public UnityEvent onCycle1Finished;
    public UnityEvent onCycle2Finished;

    private float timer;
    private Light sun;
    public float day = 1;

    private bool play = true;

    private Quaternion startRotation;

    void Start()
    {
        sun = GetComponent<Light>();
        startRotation = transform.rotation;
    }
    void OnEnable()
    {
        transform.rotation = startRotation;
    }

    void Update()
    {
        if (!play) return;

        timer += Time.deltaTime;

        float t = timer / cycleDuration;
        float angle = Mathf.Lerp(180f, -50f, t);

        transform.rotation = Quaternion.Euler(angle, -50f, 0f);

        float dist = Mathf.Abs(angle - 90f) / 90f;
        sun.colorTemperature = Mathf.Lerp(coldTemp, warmTemp, dist);

        if (timer >= cycleDuration && day == 2)
        {
            onCycle2Finished?.Invoke();
            stoptime();
        }

        if (timer >= cycleDuration && day == 1)
        {
            onCycle1Finished?.Invoke();
            stoptime();
        }
    }

    public void stoptime()
    {
        day++;
        play = false;
    }

    public void playtime()
    {
        timer = 0f;
        transform.rotation = startRotation;
        play = true;
    }
}