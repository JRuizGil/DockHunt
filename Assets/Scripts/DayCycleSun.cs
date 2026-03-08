using UnityEngine;

public class DayCycleSun : MonoBehaviour
{
    public float cycleDuration = 120f;

    public float warmTemp = 2000f;   // amanecer / atardecer
    public float coldTemp = 6500f;   // mediodía

    private float timer;
    private Light sun;

    void Start()
    {
        sun = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        float t = timer / cycleDuration;
        float angle = Mathf.Lerp(180f, -50f, t);

        transform.rotation = Quaternion.Euler(angle, -50f, 0f);

        // distancia al mediodía (90°)
        float dist = Mathf.Abs(angle - 90f) / 90f;

        // temperatura
        sun.colorTemperature = Mathf.Lerp(coldTemp, warmTemp, dist);

        if (timer >= cycleDuration)
        {
            timer = 0f;
        }
    }
}