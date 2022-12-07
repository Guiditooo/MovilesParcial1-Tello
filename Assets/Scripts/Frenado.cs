using UnityEngine;
public class Frenado : MonoBehaviour
{
    public string tagDeposito = "Deposito";
    public bool frenando;
    public float velEntrada;
    private float tiempoFrenado = 0.5f;
    private float tempo;
    private int cantMensajes = 10;
    private int contador;
    private CarController carController;
    private ControlDireccion KInput;
    private Vector3 destino;
    private ControlDireccion controlDireccion;
    private Rigidbody rb;
    private Player player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        controlDireccion = GetComponent<ControlDireccion>();
        carController = GetComponent<CarController>();
    }
    private void Start()
    {
        Frenar();
    }
    private void FixedUpdate()
    {
        if (frenando)
        {
            tempo += Time.deltaTime;
            if (tempo >= tiempoFrenado / cantMensajes * contador) contador++;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDeposito))
        {
            Deposito2 dep = other.GetComponent<Deposito2>();
            if (!dep.Vacio) return;

            if (player.ConBolasas())
            {
                dep.Entrar(player);
                destino = other.transform.position;
                transform.forward = destino - transform.position;
                Frenar();
            }
        }
    }
    public void Frenar()
    {
        controlDireccion.enabled = false;
        carController.SetAcel(0);
        rb.velocity = Vector3.zero;

        frenando = true;

        tempo = 0;
        contador = 0;
    }
    public void RestaurarVel()
    {
        controlDireccion.enabled = true;
        carController.SetAcel(1);
        frenando = false;
        tempo = 0;
        contador = 0;
    }
}