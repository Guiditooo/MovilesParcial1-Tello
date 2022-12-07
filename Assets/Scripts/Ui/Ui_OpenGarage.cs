using UnityEngine;
public class Ui_OpenGarage : MonoBehaviour
{
    [SerializeField] private float posUp = 17.5f;
    [SerializeField] private float maxTime = 3;
    private float onTime;
    private Vector3 posInit;
    private Vector3 posEnd;

    void Start()
    {
        var position = transform.position;
        posInit = position;
        posEnd = position;
        posEnd.y = posUp;

        if (maxTime == 0)
            DestroyImmediate(this);
    }
    void Update()
    {
        onTime += Time.unscaledDeltaTime;
        if (onTime < maxTime)
        {
            transform.position = Vector3.Lerp(posInit, posEnd, onTime / maxTime);
        }
        else
        {
            Destroy(this);
        }
    }
}