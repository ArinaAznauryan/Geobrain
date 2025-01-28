using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GetAllFlag : MonoBehaviour
{
    public List<Sprite> pngFiles = new List<Sprite>();
    public Image rend;
    public TextMeshProUGUI flagsShownText;
    private List<Sprite> remainingFlags = new List<Sprite>();
    public int flagsShownCount = 0; // Make this public
    private int totalFlags = 181;
    void Awake()
    {
        rend = GetComponent<Image>();

        // Add your PNG file names to this list
        List<string> fileNames = new List<string> {
            "Albania", "Armenia", "Andorra", "Austria", "Antigua and Barbuda", "Belarus", 
            "Belgium", "Bosnia", "Bulgaria", "Botswana", "Czech", "Denmark", "Iraq", 
            "Iran", "England", "Estonia", "France", "Germany", "Curacao", "Croatia", 
            "Greece", "Djibouti", "Cyprus", "Ireland", "Equatorial Guinea", "Lithuania", 
            "Norway", "Oman", "Sweden", "Nigeria", "Niger", "North Korea", "North Macedonia", 
            "Spain", "Switzerland", "UK", "Finland", "Burkina Faso", "Egypt", "Eritrea", 
            "Eswatini", "Ethiopia", "French Guiana", "Chad", "Gabon", "Central Africa", 
            "Capo Verde", "Macao", "Luxembourg", "Liechtenstein", "Cameroon", "Cambodia", 
            "Jordan", "Kenya", "Malta", "Mali", "Maldives", "Libya", "Madagascar", "Malawi", 
            "Mozambique", "Sri Lanka", "Senegal", "Slovenia", "Slovakia", "Serbia", "Somalia", 
            "South Africa", "South Sudan", "The Republic Of Congo", "Uganda", "Zambia", 
            "Zimbabwe", "Argentina", "Aruba", "Brunei", "Bolivia", "Brazil", 
            "British Virgin Islands", "Chile", "Colombia", "Dominica", "Ecuador", "Paraguay", 
            "Peru", "Suriname", "Sierra Leone", "Tunisia", "Tonga", "Togo", "Trinidad And Tobago", 
            "Tajikistan", "Taiwan", "Uruguay", "Angola", "Anguilla", "Venezuela", "Algeria", 
            "Azerbaijan", "Bahrain", "Bangladesh", "Bhutan", "Tanzania", "China", "Georgia", 
            "Hungary", "Hong Kong", "Italy", "India", "Kazakhstan", "Israel", "Japan", 
            "Lebanon", "Malaysia", "Myanmar", "Poland", "Portugal", "Philippines", "Qatar", 
            "Singapore", "South Korea", "Thailand", "Turkey", "Turkmenistan", "UAE", 
            "Vietnam", "Bahamas", "Barbados", "Belize", "Canada", "Benin", "Laos", 
            "Kyrgyzstan", "Kuwait", "Costa Rica", "Libya", "Latvia", "Liberia", "Lesotho", 
            "Wales", "Cuba", "Vatican", "Uzbekistan", "Dominican Republic", "El Salvador", 
            "Greenland", "Guatemala", "Guinea", "Guyana", "Yemen", "Saudi Arabia", "San Marino", 
            "Saint Lucia", "Rwanda", "Russia", "Romania", "Sao Tome and Principe", "Ukraine", 
            "Haiti", "Scotland", "Hawaii", "Honduras", "Jamaica", "Mexico", "Montenegro", 
            "Mongolia", "Moldova", "Nicaragua", "Netherlands", "Nepal", "Namibia", "Grenada", 
            "Morocco", "Ghana", "Gambia", "Panama", "Mauritius", "Mauritania", "Palestine", 
            "Puerto Rico", "Seychelles", "USA"
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
