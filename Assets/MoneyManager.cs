using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MoneyManager : MonoBehaviour
{
    public UnityEvent RunAD;
    public bool givemoney = true;
    public TextMeshProUGUI budgetText; // Reference to the TextMeshProUGUI component
    public Button WatchAd; // Reference to the UI Button component
    public int budget; // Variable to store the current budget

    private const string BudgetKey = "PlayerBudget"; // Key to store the budget in PlayerPrefs

    void Awake()
    {
        // Load the saved budget from PlayerPrefs or set a default value
        LoadBudget();
    }

    void Start()
    {
        givemoney = true;
        // Initialize the budget display
        UpdateBudgetText();
    }

    // Method to load the budget from PlayerPrefs
    public void LoadBudget()
    {
        budget = PlayerPrefs.GetInt(BudgetKey, 0); // Load the saved budget or set to 0 if not found
        UpdateBudgetText(); // Update the UI Text
    }

    // Method to add money
    public void AddMoney()
    {
        budget += 10; // Add 10 coins to the budget
        UpdateBudgetText(); // Update the UI Text
        SaveBudget(); // Save the updated budget to PlayerPrefs
    }

    public void AddMoneyMore()
    {
        budget += 50; // Add 10 coins to the budget
        UpdateBudgetText(); // Update the UI Text
        SaveBudget(); // Save the updated budget to PlayerPrefs
    }

    public void AddMoneyALot()
    {
        budget += 250; // Add 10 coins to the budget
        UpdateBudgetText(); // Update the UI Text
        SaveBudget(); // Save the updated budget to PlayerPrefs
    }

    // Method to update the budget text
    public void UpdateBudgetText()
    {
        budgetText.text = budget.ToString();
    }

    // Method to save the budget to PlayerPrefs
    public void SaveBudget()
    {
        PlayerPrefs.SetInt(BudgetKey, budget);
        PlayerPrefs.Save();
    }
}
