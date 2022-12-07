using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Ui_Player : MonoBehaviour
{
    public Ui_Manager uiManager;
    public Player player;
    public ControladorDeDescarga controladorDeDescarga;
    public List<Sprite> truckSprites = new List<Sprite>();
    public Image truckImage;
    public TextMeshProUGUI textMoney;
    private int amountBags;
    private float timeWait = 1.0f;
    private float onTimeWait;
    private int currentmaxSprite;

    void Start()
    {
        player.AddMoney += UiUpdateScore;
        controladorDeDescarga.onAddMoneyBonus += UiUpdateScore;
    }
    private void Update()
    {
        if (amountBags > 2)
        {
            onTimeWait += Time.deltaTime;
            if (onTimeWait > timeWait)
            {
                onTimeWait = 0;
                currentmaxSprite = (currentmaxSprite == 0) ? 1 : 0;
                truckImage.sprite = truckSprites[amountBags + currentmaxSprite];
            }
        }
        else
        {
            onTimeWait = 0;
        }
    }
    public void UiUpdateScore(int amount)
    {
        textMoney.text = "$" + amount;
        amountBags = player.CantBolsAct;
        switch (amountBags)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                truckImage.sprite = truckSprites[amountBags];
                break;
            default:
                truckImage.sprite = truckSprites[0];
                break;
        }
    }
}