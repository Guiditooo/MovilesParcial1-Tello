using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoEscMgr : MonoBehaviour 
{
	bool JuegoFinalizado = false;
	public float TiempoEsperaFin = 25;
	float Tempo = 0;
	
	bool JuegoIniciado = false;
	public float TiempoEsperaInicio = 120;
	float Tempo2 = 0;

    void Update () 
	{
		if(JuegoFinalizado)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempoEsperaFin)
			{
				Tempo = 0;
                SceneManager.LoadScene("MainMenu");
			}
		}
		
		if(!JuegoIniciado)
		{
			Tempo2 += Time.deltaTime;
			if(Tempo > TiempoEsperaInicio)
			{
				Tempo2 = 0;
                SceneManager.LoadScene("MainMenu");
			}
		}

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
		
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
    public void JuegoFinalizar() => JuegoFinalizado = true;
    public void JuegoIniciar() => JuegoIniciado = true;
}