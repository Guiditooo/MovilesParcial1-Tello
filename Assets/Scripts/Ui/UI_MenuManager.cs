using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UI_MenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] menues;
    [SerializeField] private float timeTransition = 1;

    void Start()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public enum Menues { Main, Single, Multi, Credits, Tutorial, Exit }
    private Menues menuActual = Menues.Main;
    private float onTime;

    public void OffPanel()
    {
        menues[(int)menuActual].blocksRaycasts = false;
        menues[(int)menuActual].interactable = false;
        StartCoroutine(PanelOff(timeTransition, (int)menuActual));
    }
    public void SwitchPanel(int otherMenu)
    {
        menues[(int) menuActual].blocksRaycasts = false;
        menues[(int) menuActual].interactable = false;
        StartCoroutine(SwitchPanel(timeTransition, otherMenu, (int) menuActual));
    }
    IEnumerator SwitchPanel(float maxTime, int onMenu, int offMenu)
    {
        CanvasGroup on = menues[onMenu];
        CanvasGroup off = menues[offMenu];

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float fade = onTime / maxTime;
            on.alpha = fade;
            off.alpha = 1 - fade;
            yield return null;
        }
        on.blocksRaycasts = true;
        on.interactable = true;
        onTime = 0;

        menuActual = (Menues)onMenu;
    }
    IEnumerator PanelOff(float maxTime, int offMenu)
    {
        CanvasGroup off = menues[offMenu];

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float fade = onTime / maxTime;
            off.alpha = 1 - fade;
            yield return null;
        }
        onTime = 0;
    }

    IEnumerator QuitGame(float maxTime, int offMenu)
    {
        CanvasGroup off = menues[offMenu];

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float fade = onTime / maxTime;
            off.alpha = 1 - fade;
            yield return null;
        }
        onTime = 0;
        Application.Quit();
    }

    IEnumerator FadeScene(float maxTime, int offMenu, string scene)
    {
        CanvasGroup off = menues[offMenu];

        while (onTime < maxTime)
        {
            onTime += Time.deltaTime;
            float fade = onTime / maxTime;
            off.alpha = 1 - fade;
            yield return null;
        }
        onTime = 0;
        SceneManager.LoadScene(scene);
    }

    public void ChangeScene(string scene)
    {
        StartCoroutine(FadeScene(timeTransition, (int)menuActual, scene));
    }
    public void ExitGame()
    {
        StartCoroutine(QuitGame(timeTransition, (int)menuActual));
    }
}