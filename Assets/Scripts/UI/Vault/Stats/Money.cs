using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This Class takes care of showing the amount of money the player
/// currently has and all the money operations
/// <list>
///     <term>Displaying amount of money</term>
///     <term>spending and earning money</term>
/// </list>
/// </summary>
public class Money : MonoBehaviour
{
    static int amount;
    public static int Amount { get { return amount; } }

    [SerializeField] GameObject _money;
    TextMeshProUGUI _moneyText;

    private void OnEnable()
    {
        VaultBuilding.ConfirmTransaction += SetAmount;
    }

    private void OnDisable()
    {
        VaultBuilding.ConfirmTransaction -= SetAmount;
    }

    private void Awake()
    {
        amount = 400;
        _moneyText = _money.GetComponent<TextMeshProUGUI>();
        _moneyText.SetText($"{amount} $");
    }

    /// <summary>
    /// Function sets the amount of money depending on parameters
    /// and displays the updated amount
    /// Function is private and must be triggered through an <event>
    /// <see cref="MoneyOperation"/>
    /// </summary>
    void SetAmount(MoneyOperation operation, int value)
    {
        if (operation == MoneyOperation.SET)
            amount = value;

        if (operation == MoneyOperation.EARN)
            amount += value;

        if (operation == MoneyOperation.SPEND)
            amount -= value;

        _moneyText.SetText($"{amount} $");
    }
}
