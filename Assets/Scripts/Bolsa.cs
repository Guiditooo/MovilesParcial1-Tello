using System;
using UnityEngine;
public class Bolsa : MonoBehaviour
{
	public Pallet.Valores Monto;
	public string TagPlayer = "";
	public Texture2D ImagenInventario;
    private Player Pj;
    private bool Desapareciendo;
	public GameObject Particulas;
	public float TiempParts = 2.5f;
    private Renderer renderer;
    private Collider collider;
	private void Awake()
    {
        renderer = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }
    void Start () 
	{
		Monto = Pallet.Valores.Valor2;
        if(Particulas != null)
			Particulas.SetActive(false);
    }
	void Update ()
	{
        if(Desapareciendo)
		{
			TiempParts -= Time.deltaTime;
			if(TiempParts <= 0)
			{
				renderer.enabled = true;
                collider.enabled = true;
                Particulas.GetComponent<ParticleSystem>().Stop();
				gameObject.SetActive(false);
			}
		}
    }
    void OnTriggerEnter(Collider coll)
	{
		if(coll.CompareTag(TagPlayer))
		{
			Pj = coll.GetComponent<Player>();
            if (Pj.AgregarBolsa(this))
                Desaparecer();
        }
	}
    public void Desaparecer()
	{
		Particulas.GetComponent<ParticleSystem>().Play();
		Desapareciendo = true;
        renderer.enabled = false;
		collider.enabled = false;
		
		if(Particulas != null)
		{
			Particulas.GetComponent<ParticleSystem>().Play();
		}
    }
}