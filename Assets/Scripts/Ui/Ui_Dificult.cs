using UnityEngine;
using UnityEngine.UI;
public class Ui_Dificult : MonoBehaviour
{
    private int difficult;
    public Slider sliderDificult;
    
    public void UpdateDificult()
    {
        GameMaster.Get().difficult = (int)sliderDificult.value;
    }
}