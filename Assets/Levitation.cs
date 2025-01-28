using UnityEngine;
using UnityEngine.UI;

public class Levitation : MonoBehaviour
{
    public float speed = 1f; 
    public float height = 10f; 

    private RectTransform rectTransform;
    private float startY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startY = rectTransform.anchoredPosition.y;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave
        float newY = startY + Mathf.Sin(Time.time * speed) * height;

        // Update the object's anchored position
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
    }
}
