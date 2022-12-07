using UnityEngine;
using UnityEngine.SceneManagement;
public class VidIntrMgr : MonoBehaviour 
{
	void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("GamePlay");
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if(Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene("GameOver");
		}
	}
}
