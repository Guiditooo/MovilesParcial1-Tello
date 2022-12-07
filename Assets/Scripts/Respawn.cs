using UnityEngine;
public class Respawn : MonoBehaviour
{
    private CheakPoint CPAct;
    private CheakPoint CPAnt;
    public float angMax = 90;
    private int verifPorCuadro = 20;
    private int contador;
    public float rangMinDer;
	public float rangMaxDer;
    private bool ignorandoColision;
	public float tiempDeNoColision = 2;
    private float tempo;
    public CarController carController;
    private Rigidbody rb;
    private Visualizacion visualizacion;

    private void Awake()
    {
        visualizacion = GetComponent<Visualizacion>();
        rb = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
    }
	void Start ()
    {
        Physics.IgnoreLayerCollision(8, 9, false);
    }
	void Update ()
	{
		if(CPAct)
		{
			contador++;
			if(contador == verifPorCuadro)
			{
				contador = 0;
				if(angMax < Quaternion.Angle(transform.rotation, CPAct.transform.rotation))
				{
					Respawnear();
				}
			}
		}
        if(ignorandoColision)
		{
			tempo += Time.deltaTime;
			if(tempo > tiempDeNoColision)
			{
				IgnorarColision(false);
			}
		}
    }
	public void Respawnear()
	{
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        carController.SetGiro(0.0f);
        if (CPAct.Habilitado())
        {
            if (visualizacion.LadoAct == Visualizacion.Lado.Der)
                transform.position = CPAct.transform.position + CPAct.transform.right * Random.Range(rangMinDer, rangMaxDer);
            else
                transform.position = CPAct.transform.position + CPAct.transform.right * Random.Range(rangMinDer * (-1), rangMaxDer * (-1));
            transform.forward = CPAct.transform.forward;
        }
        else if (CPAnt != null)
        {
            if (visualizacion.LadoAct == Visualizacion.Lado.Der)
                transform.position = CPAnt.transform.position + CPAnt.transform.right * Random.Range(rangMinDer, rangMaxDer);
            else
                transform.position = CPAnt.transform.position + CPAnt.transform.right * Random.Range(rangMinDer * (-1), rangMaxDer * (-1));
            transform.forward = CPAnt.transform.forward;
        }
        IgnorarColision(true);
        Invoke(nameof(ResetRb), 0.1f);
    }
    void ResetRb()
    {
        rb.constraints = RigidbodyConstraints.None;
    }
    public void Respawnear(Vector3 pos)
	{
        rb.velocity = Vector3.zero;
        carController.SetGiro(0.0f);
        transform.position = pos;
        IgnorarColision(true);
	}
    public void Respawnear(Vector3 pos, Vector3 dir)
	{
        rb.velocity = Vector3.zero;
        carController.SetGiro(0.0f);
        transform.position = pos;
		transform.forward = dir;
        IgnorarColision(true);
	}
    public void AgregarCP(CheakPoint cp)
	{
		if(cp != CPAct)
		{
			CPAnt = CPAct;
			CPAct = cp;
		}
	}
    void IgnorarColision(bool b)
	{
		Physics.IgnoreLayerCollision(8,9,b);
		ignorandoColision = b;	
		tempo = 0;
	}
}