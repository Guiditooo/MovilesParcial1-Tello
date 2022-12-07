using UnityEngine;
using UnityEngine.EventSystems;

public class Ui_ButtonFx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float scaleMultiply = 3;
    [SerializeField] private float limit = 1.2f;
    private enum State { None, Increase, Decrease }

    private State state;
    private Vector3 initialScale;
    private Vector3 scale;

    private void Awake()
    {
        state = State.None;
        initialScale = transform.localScale;
    }
    private void OnEnable()
    {
        transform.localScale = initialScale;
        state = State.None;
    }
    private void Update()
    {
        ChangeScale();
    } 
    private void ChangeScale()
    {
        float timeStep = 0;
        if (state != State.None)
        {
            timeStep = scaleMultiply * Time.unscaledDeltaTime;
            scale = transform.localScale;
        }

        switch (state)
        {
            case State.None:
                break;
            case State.Increase:
                if (transform.localScale.x < limit)
                {
                    scale = new Vector3(scale.x + timeStep, scale.y + timeStep, scale.z + timeStep);
                    transform.localScale = scale;
                }
                else
                {
                    transform.localScale = new Vector3(limit, limit, limit);
                }
                break;
            case State.Decrease:
                if (transform.localScale.x > initialScale.x)
                {
                    scale = new Vector3(scale.x - timeStep, scale.y - timeStep, scale.z - timeStep);
                    transform.localScale = scale;
                }
                else
                {
                    transform.localScale = new Vector3(initialScale.x, initialScale.y, initialScale.z);
                }
                break;
            default:
                Debug.LogWarning("State fuera de rango!");
                break;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        state = State.Increase;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        state = State.Decrease;
    }
}