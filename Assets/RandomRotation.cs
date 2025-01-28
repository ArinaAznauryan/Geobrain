using UnityEngine;

public class ClockRotation : MonoBehaviour
{
    public float rotationSpeed = 40f; // 60 degrees per second for one rotation

    void Update()
    {
        // Rotate the object around its local Z-axis at a constant speed
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
