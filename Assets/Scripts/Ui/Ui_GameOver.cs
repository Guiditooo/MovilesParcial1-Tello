using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ui_GameOver : MonoBehaviour    // Todo: change type name
{
    public TextMeshProUGUI textP1;
    public TextMeshProUGUI textP2;
    public Image playerWinner;
    private bool P1Win; // Todo: Change to lower
    private int moneyP1;
    private int moneyP2;
    public Sprite spriteP1Win;
    public Sprite spriteP2Win;

    public float fadeTime = 0.5f;
    private float onTime;
    private bool upperTime;

    void Start()
    {
        moneyP1 = GameMaster.Get().moneyP1;
        moneyP2 = GameMaster.Get().moneyP2;
        textP1.text = moneyP1.ToString();
        textP2.text = moneyP2.ToString();
        P1Win = moneyP1 > moneyP2;

        playerWinner.sprite = P1Win ? spriteP1Win : spriteP2Win;

        Destroy(GameMaster.Get().gameObject);
    }
    void Update()
    {
        if (upperTime)
        {
            onTime += Time.deltaTime;
            if (onTime > fadeTime)
            {
                upperTime = false;
                onTime = fadeTime;
            }
        }
        else
        {
            onTime -= Time.deltaTime;
            if (onTime < 0)
            {
                upperTime = true;
                onTime = 0;
            }
        }

        float lerp = onTime / fadeTime;
        playerWinner.color = Color.Lerp(Color.white, Color.blue, lerp);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}