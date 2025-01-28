using UnityEngine;
using UnityEngine.UI;

public class SideToSideLevitation : MonoBehaviour
{
    public float speed = 1f; 
    public float distance = 10f; 

    private RectTransform rectTransform;
    private float startX;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startX = rectTransform.anchoredPosition.x;
    }

    void Update()
    {
        // Calculate the new X position using a sine wave
        float newX = startX + Mathf.Sin(Time.time * speed) * distance;

        // Update the object's anchored position
        rectTransform.anchoredPosition = new Vector2(newX, rectTransform.anchoredPosition.y);
    }
}
