using UnityEngine;
public class ScreenShake : MonoBehaviour
{
    public float shakeAmount = 0.7f;
    float shakeTime = 0.0f;
    Vector3 initialPosition;

    public void VibrateForTime(float time)
    {
        shakeTime = time;
    }
    void UpdatePosition()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            UpdatePosition();
            transform.position = Random.insideUnitSphere * shakeAmount + initialPosition;
            shakeTime -= Time.deltaTime;
        }
    }
}