using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyPanel : MonoBehaviour
{
    [SerializeField]
    private int index = 0;
    [SerializeField]
    private int earnPerSeconds = 0;
    [SerializeField]
    private int price = 0;
    [SerializeField]
    private int priceAdd = 0;
    [SerializeField]
    private Text priceText = null;
    [SerializeField]
    private Text earnText = null;

    private void Start()
    {
        price = GameManager.Instance.playerData.watermelonObjectPrice[index];
        earnText.text = string.Format("{0}", earnPerSeconds);
        priceText.text = string.Format("Price : {0}", price);
        StartCoroutine(EarnPerSeconds());
    }
    public void OnPurchase()
    {
        if (GameManager.Instance.playerData.watermelonAmount < price) return;
        GameManager.Instance.playerData.watermelonAmount -= price;
        GameManager.Instance.playerData.watermelonObjectAmount[index]++;
        price += priceAdd;
        priceText.text = string.Format("Price : {0}", price);
        GameManager.Instance.playerData.watermelonObjectPrice[index] = price;
        GameManager.Instance.UpdateUI();
    }

    private IEnumerator EarnPerSeconds()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameManager.Instance.playerData.watermelonAmount += earnPerSeconds * 
                GameManager.Instance.playerData.watermelonObjectAmount[index];
            GameManager.Instance.UpdateUI();
        }
    }
}
