using UnityEngine;

public class Ui_RotateSelf : MonoBehaviour
{
    public float speed = 0.1f;
    
    private void Update()
    {
        transform.Rotate(Vector3.up, speed);
    }
}