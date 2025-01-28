using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Hint : MonoBehaviour
{
    public TMP_InputField answerInput;
    public UnityEvent RunAD;
    public bool starthint = true;
    //public bool once = false; // Add this field
    // Start is called before the first frame update
    void Start()
    {
       starthint = true;
    }

    void Update()
    {
        // You can add any update logic here if needed
    }

    public void GetHint()
    {
        
        //FindObjectOfType<RewardedAdManager>().once = true;
        GameObject triangleObject = GameObject.Find("Triangle");
        //Debug.Log("hint written");

        if (triangleObject != null)
        {
            Image triangleRenderer = triangleObject.GetComponent<Image>();

            if (triangleRenderer != null)
            {
                string fullGovernmentName = triangleRenderer.sprite.name.ToLower();
                char[] nameCharacters = fullGovernmentName.ToCharArray();

                for (int i = 0; i < nameCharacters.Length; i++)
                {
                    if (i % 2 == 0 && i%3 != 0)
                    {
                        nameCharacters[i] = '-';
                    }
                }

                answerInput.text = new string(nameCharacters);
                //Debug.Log("hint written");
            }
            else
            {
                Debug.LogError("Triangle Renderer component not found.");
            }
        }
        else
        {
            Debug.LogError("Triangle GameObject not found.");
        }
    }
}
