using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemObjController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buttonText;

    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Image softCurrencyImage;

    [SerializeField]
    private Image realMoneyImage;

    public string Price { set => priceText.text = value; }
    public string ButtonText { set => buttonText.text = value; }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {

    }

    public void SetCurrency()
    {

    }
}
