using UnityEngine;
using System.Collections;

public class AcelerAuto : MonoBehaviour 
{
	public float AcelPorSeg = 0;
	float Velocidad = 0;
	public float VelMax = 0;
	ReductorVelColl Obstaculo = null;
	
	
	bool Avil = true;
	public float TiempRecColl = 0;
	float Tempo = 0;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if(velocidad < VelMax)
		{
			velocidad += AcelPorSeg * Time.deltaTime;
		}
		*/
		
		//Debug.Log("velocidad: "+rigidbody.velocity.magnitude);
		
		if(Avil)
		{
			Tempo += Time.deltaTime;
			if(Tempo > TiempRecColl)
			{
				Tempo = 0;
				Avil = false;
			}
		}
	}
	
	void FixedUpdate () 
	{
		/*
		//this.rigidbody.MovePosition(this.transform.position + this.transform.forward * velocidad);
		if(rigidbody.velocity.magnitude < VelMax)
			rigidbody.velocity += transform.forward * AcelPorSeg * Time.deltaTime;
			*/
		
		
		/*
		if(velocidad < VelMax)
		{
			velocidad += AcelPorSeg * Time.fixedDeltaTime;
		}
		
		rigidbody.MovePosition(this.transform.position + this.transform.forward * velocidad);
		*/
		
		if(Velocidad < VelMax)
		{
			Velocidad += AcelPorSeg * Time.fixedDeltaTime;
		}
		
		GetComponent<Rigidbody>().AddForce(this.transform.forward * Velocidad);
	}
	
	 void OnCollisionEnter(Collision collision)
	{
		if(!Avil)
		{
			Obstaculo = collision.transform.GetComponent<ReductorVelColl>();
			if(Obstaculo != null)
			{
				
				//velocidad -= Obstaculo.ReduccionVel;
				
				//if(velocidad < 0)
					//velocidad = 0;
					
				GetComponent<Rigidbody>().velocity /= 2;
			}
			Obstaculo = null;
		}
	}
	
	public void Chocar(ReductorVelColl obst)
	{
		GetComponent<Rigidbody>().velocity /= 2;
	}
	
}
