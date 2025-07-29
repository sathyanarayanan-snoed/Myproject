using UnityEngine;

public class CameraShake : MonoBehaviour
{

    public Transform camTransform;    // Camera transform to shake
    public float shakeDuration = 0.2f;  // Duration of the shake in seconds
    public float shakeAmount = 0.3f;  // Amplitude of the shake
    public float decreaseFactor = 2.0f; // Rate at which shake duration decreases

    Vector3 originalPos;
    float originalShakeDuration;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        originalShakeDuration = shakeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            // Apply random shake offset within a sphere, scaled by shakeAmount
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }
    
    public void ShakeCamera(float duration, float amount)
    {
        shakeDuration = duration;
        shakeAmount = amount;
    }
}
