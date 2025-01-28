using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GetAsiaFlag : MonoBehaviour
{
    public List<Sprite> pngFiles = new List<Sprite>();
    public Image rend;
    public TextMeshProUGUI flagsShownText;
    private List<Sprite> remainingFlags = new List<Sprite>();
    public int flagsShownCount = 0; // Make this public
    private int totalFlags = 45;

    void Awake()
    {
        rend = GetComponent<Image>();

        // Add your PNG file names to this list
        List<string> fileNames = new List<string> {
            "Algeria", "Azerbaijan", "Afghanistan", "Brunei", "Kyrgyzstan", "Cambodia", "Hong Kong", "Iran",
            "Laos", "Iraq", "Macao", "Maldives", "Mongolia", "Kuwait", "Kazakhstan", "Nepal", "North Korea",
            "Oman", "Palestine", "Saudi Arabia", "Sri Lanka", "Taiwan", "Tajikistan", "Turkey", "Turkmenistan",
            "Uzbekistan", "Yemen", "Bahrain", "Bangladesh", "Bhutan", "China", "Georgia", "India", "Israel",
            "Japan", "Lebanon", "Malaysia", "Myanmar", "Philippines", "Qatar", "Singapore", "South Korea",
            "Thailand", "UAE", "Vietnam"
        };

        // Load the PNG files from Resources and add them to the list
        foreach (string fileName in fileNames) {
            Sprite file = Resources.Load<Sprite>(fileName);
            if (file != null) {
                pngFiles.Add(file);
                remainingFlags.Add(file); // Add to remainingFlags list
            } else {
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
        if (remainingFlags.Count > 0) {
            int randomIndex = Random.Range(0, remainingFlags.Count);
            rend.sprite = remainingFlags[randomIndex];
            remainingFlags.RemoveAt(randomIndex);

            flagsShownCount++;
            UpdateFlagsShownText();
        } else {
            // All flags have been shown, perform your stopping logic here
            SceneManager.LoadScene("GoodJob");
        }
    }
    private void UpdateFlagsShownText()
    {
        flagsShownText.text = flagsShownCount + "/" + totalFlags;
    }
}
