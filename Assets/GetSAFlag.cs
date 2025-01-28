using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GetSAFlag : MonoBehaviour
{
    public List<Sprite> pngFiles = new List<Sprite>();
    public Image rend;
    public TextMeshProUGUI flagsShownText;
    private List<Sprite> remainingFlags = new List<Sprite>();
    public int flagsShownCount = 0; // Make this public
    private int totalFlags = 15;

    void Awake()
    {
        rend = GetComponent<Image>();

        List<string> fileNames = new List<string> {
            "Argentina", "Aruba", "Bolivia", "Brazil", "Chile", "Colombia", 
            "Dominica", "Ecuador", "Paraguay", "Guyana", "Peru", "Suriname", 
            "Trinidad And Tobago", "Uruguay", "Venezuela"
        };

        foreach (string fileName in fileNames) {
            Sprite file = Resources.Load<Sprite>(fileName);
            if (file != null) {
                pngFiles.Add(file);
                remainingFlags.Add(file);
            } else {
                Debug.LogWarning("Could not load file: " + fileName);
            }
        }

        UpdateFlagsShownText();
        GetRandomFlag();
        Debug.Log("Sprite assigned to sprite renderer: " + (rend.sprite != null ? rend.sprite.name : "null"));
    }

    public void GetRandomFlag()
    {
        if (remainingFlags.Count > 0) {
            int randomIndex = Random.Range(0, remainingFlags.Count);
            rend.sprite = remainingFlags[randomIndex];
            remainingFlags.RemoveAt(randomIndex);

            flagsShownCount++;
            UpdateFlagsShownText();
        } else {
            SceneManager.LoadScene("GoodJob");
        }
    }

    private void UpdateFlagsShownText()
    {
        flagsShownText.text = flagsShownCount + "/" + totalFlags;
    }
}
