using UnityEngine;
using UnityEngine.SceneManagement;

public class MngPts : MonoBehaviour 
{
    public float TiempEmpAnims = 2.5f;
    private float Tempo = 0;
    public Vector2[] DineroPos;
	public Vector2 DineroEsc;
    public Vector2 GanadorPos;
	public Vector2 GanadorEsc;
	public Texture2D[] Ganadores;
    public GameObject Fondo;
    public float TiempEspReiniciar = 10;
    public float TiempParpadeo = 0.7f;
    private float TempoParpadeo = 0;
    private bool PrimerImaParp = true;
    public bool ActivadoAnims;

	void Update () 
	{
        TiempEspReiniciar -= Time.deltaTime;
		if(Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Keypad0))
		{
            SwitchToMainMenu();
		}
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (TiempEspReiniciar <= 0)
        {
            SwitchToMainMenu();
        }
        if(ActivadoAnims)
		{
			TempoParpadeo += Time.deltaTime;
			
			if(TempoParpadeo >= TiempParpadeo)
			{
				TempoParpadeo = 0;
				
				if(PrimerImaParp)
					PrimerImaParp = false;
				else
				{
					TempoParpadeo += 0.1f;
					PrimerImaParp = true;
				}
			}
		}
		else
		{
			Tempo += Time.deltaTime;
			if(Tempo >= TiempEmpAnims)
			{
				Tempo = 0;
				ActivadoAnims = true;
			}
		}
    }
    public void SwitchToMainMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }
}