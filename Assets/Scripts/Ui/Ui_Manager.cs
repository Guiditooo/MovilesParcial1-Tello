using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Ui_Manager : MonoBehaviour
{
    public GameObject Player01;
    public GameObject Player02;
    private CarController carController;
    public List<CanvasGroup> panelsPlayer = new List<CanvasGroup>();
    private TextMeshProUGUI textMoneyP1;
    private TextMeshProUGUI textMoneyP2;
    
    void Start()
    {
        if (GameMaster.Get().IsSinglePlayer())
        {
            panelsPlayer[1].gameObject.SetActive(false);
        }
    }
}