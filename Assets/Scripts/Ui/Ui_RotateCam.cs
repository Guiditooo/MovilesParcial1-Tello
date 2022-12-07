using UnityEngine;

public class Ui_RotateCam : MonoBehaviour
{
    public Transform cam;
    public Transform center;
    public float speed = 0.1f;
    
    private void Update()
    {
        cam.transform.RotateAround(center.position, Vector3.up, speed);
    }
}