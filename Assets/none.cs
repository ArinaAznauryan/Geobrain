using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class none : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<ButtonMoneyCheck>().LoadChildStates();
        FindObjectOfType<MoneyManager>().UpdateBudgetText();
        FindObjectOfType<MoneyManager>().LoadBudget();
    }

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<MoneyManager>().UpdateBudgetText();
        FindObjectOfType<MoneyManager>().LoadBudget();
    }
}
