using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GetAfricaFlag : MonoBehaviour
{
    public List<Sprite> pngFiles = new List<Sprite>();
    public Image rend;
    public TextMeshProUGUI flagsShownText;
    private List<Sprite> remainingFlags = new List<Sprite>();
    public int flagsShownCount = 0; // Make this public
    private int totalFlags = 50;

    void Awake()
    {
        rend = GetComponent<Image>();

        // Add your PNG file names to this list
        List<string> fileNames = new List<string> {
            "Burkina Faso", "Egypt", "Angola", "Benin", "Botswana", "Burundi", "Cameroon", 
            "Capo Verde", "Central Africa", "Chad", "Djibouti", "Equatorial Guinea", 
            "Gambia", "Ghana", "Guinea", "Lesotho", "Liberia", "Mali", "Mauritania", 
            "Mauritius", "Morocco", "Namibia", "Niger", "Nigeria", "Rwanda", 
            "Sao Tome and Principe", "Sierra Leone", "Sudan", "Tanzania", "Togo", 
            "Tonga", "Tunisia", "Eritrea", "Eswatini", "Ethiopia", "Gabon", "Jordan", 
            "Kenya", "Libya", "Madagascar", "Malawi", "Mozambique", "Senegal", 
            "Somalia", "South Africa", "South Sudan", "The Republic Of Congo", 
            "Uganda", "Zambia", "Zimbabwe"
        };

        // Load the PNG files from Resources and add them to the list
        foreach (string fileName in fileNames)
        {
            Sprite file = Resources.Load<Sprite>(fileName);
            if (file != null)
            {
                pngFiles.Add(file);
                remainingFlags.Add(file); // Add to remainingFlags list
            }
            else
            {
                Debug.LogWarning("Could not load file: " + fileName);
            }
        }

        // Get a random file from remainingFlags
        UpdateFlagsShownText();
        GetRandomFlag();
        Debug.Log("Sprite assigned to sprite renderer: " + (rend.sprite != null ? rend.sprite.name : "null"));
    }

    public void GetRandomFlag()
    {
        if (remainingFlags.Count > 0)
        {
            int randomIndex = Random.Range(0, remainingFlags.Count);
            rend.sprite = remainingFlags[randomIndex];
            remainingFlags.RemoveAt(randomIndex);

            flagsShownCount++;
            UpdateFlagsShownText();
        }
        else
        {
            // All flags have been shown, perform your stopping logic here
            PlayerPrefs.SetInt("FlagsShownCount", flagsShownCount);
            PlayerPrefs.SetInt("TotalFlags", totalFlags);
            SceneManager.LoadScene("GoodJob");
        }
    }

    private void UpdateFlagsShownText()
    {
        flagsShownText.text = flagsShownCount + "/" + totalFlags;
    }
}
