using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class InApp : MonoBehaviour
{
    public static InApp singletone;
    public string priceCode;

    [DllImport("__Internal")]
    private static extern void ShowPrice();
    private void Start()
    {
        ShowPrice();
    }

    private void Awake()
    {
        singletone = this;
    }

    
    public void SetPriceCode(string code)
    {
        priceCode = code;
    }
}
