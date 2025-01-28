using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonMoneyCheck : MonoBehaviour
{
    public MoneyManager moneyManager; // Reference to the MoneyManager script
    public Button[] buttons; // Array to hold references to the buttons
    public int requiredMoney = 500; // The default required amount of money to enable the buttons
    public int specialButtonIndex = 4; // Index of the button with a different price
    public int specialButtonPrice = 1700; // The required amount of money for the special button

    void Start()
    {
        moneyManager.LoadBudget();
        // Add listeners to each button's click event
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }
    }

    void Update()
    {
        moneyManager.LoadBudget();
        // Enable or disable each button based on player's money and PlayerPrefs state
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            int buttonPrice = (i == specialButtonIndex) ? specialButtonPrice : requiredMoney;
            bool enoughMoney = moneyManager.budget >= buttonPrice;
            bool buttonClicked = PlayerPrefs.GetInt(button.name + "_Clicked", 0) == 1;
            button.interactable = enoughMoney || buttonClicked;
        }
    }

    public void OnButtonClick(Button clickedButton)
    {
        int buttonIndex = System.Array.IndexOf(buttons, clickedButton);
        int buttonPrice = (buttonIndex == specialButtonIndex) ? specialButtonPrice : requiredMoney;
        bool buttonClicked = PlayerPrefs.GetInt(clickedButton.name + "_Clicked", 0) == 1;

        // Perform the button's function only if the player has enough money and the button hasn't been clicked before
        if (moneyManager.budget >= buttonPrice && !buttonClicked)
        {
            // Deduct the money directly from the MoneyManager's budget variable
            moneyManager.budget -= buttonPrice;
            moneyManager.UpdateBudgetText(); // Update the budget UI text
            moneyManager.SaveBudget(); // Ensure the new budget is saved
            PerformButtonAction(clickedButton);

            // Save the button's state as clicked
            PlayerPrefs.SetInt(clickedButton.name + "_Clicked", 1);
            PlayerPrefs.Save(); // Ensure the state is saved immediately
        }
        else if (buttonClicked)
        {
            // If the button has already been clicked, just perform the action without deducting money
            PerformButtonAction(clickedButton);
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    void PerformButtonAction(Button clickedButton)
    {
        Debug.Log($"{clickedButton.name} clicked! Performing action.");
        // Add specific actions for each button if needed
        DestroyChild(clickedButton.gameObject, "coin");
    }

    public void DestroyChild(GameObject parent, string tag)
    {
        foreach (Transform child in parent.transform)
        {
            if (child.tag == tag)
            {
                child.gameObject.SetActive(false);
                PlayerPrefs.SetInt(child.gameObject.name + "_Active", 0);
                PlayerPrefs.Save();
            }
        }
    }

    public void LoadChildStates()
    {
        // Find all game objects in the scene with the tag "coin"
        GameObject[] coins = GameObject.FindGameObjectsWithTag("coin");
        foreach (GameObject coin in coins)
        {
            // Load the active state for each coin
            bool isActive = PlayerPrefs.GetInt(coin.name + "_Active", 1) == 1;
            coin.SetActive(isActive);
        }
    }
}
