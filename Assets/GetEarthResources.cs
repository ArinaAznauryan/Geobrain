using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GetEarthResources : MonoBehaviour
{
    public List<Sprite> pngFiles = new List<Sprite>();
    public Image rend;
    public TextMeshProUGUI resourcesShownText;
    private List<Sprite> remainingResources = new List<Sprite>();
    public int resourcesShownCount = 0; 
    private int totalResources = 42;

    void Awake()
    {
        rend = GetComponent<Image>();

        // Add your PNG file names to this list
        List<string> fileNames = new List<string> {
            "shells", "river", "mountain", "fire", "snow", "gas", "ice", 
            "iceberg", "solar system", "rain", "thunder", "mercury", "uranus", 
            "saturn", "neptune", "galaxy", "jupiter", "mars", "venus", "water", 
            "moon", "meteorite", "animals", "fuel", "plant", "minerals", "wood", 
            "air", "soil", "sand", "stone", "metal", "oil", "coal", "lava", 
            "clouds", "sun", "comet", "rainbow", "star", "earth", "explosion"
        };

        // Load the PNG files from Resources and add them to the list
        foreach (string fileName in fileNames) {
            Sprite file = Resources.Load<Sprite>(fileName);
            if (file != null) {
                pngFiles.Add(file);
                remainingResources.Add(file); // Add to remainingFlags list
            } else {
                Debug.LogWarning("Could not load file: " + fileName);
            }
        }

        // Get a random file from remainingFlags
        UpdateResourcesShownText();
        GetRandomResource();
        Debug.Log("Sprite assigned to sprite renderer: " + (rend.sprite != null ? rend.sprite.name : "null"));
    }

    public void GetRandomResource()
    {
        if (remainingResources.Count > 0) {
            int randomIndex = Random.Range(0, remainingResources.Count);
            rend.sprite = remainingResources[randomIndex];
            remainingResources.RemoveAt(randomIndex);

            resourcesShownCount++;
            UpdateResourcesShownText();
        } else {
            // All flags have been shown, perform your stopping logic here
            SceneManager.LoadScene("GoodJob");
        }
    }
    private void UpdateResourcesShownText()
    {
        resourcesShownText.text = resourcesShownCount + "/" + totalResources;
    }
}
