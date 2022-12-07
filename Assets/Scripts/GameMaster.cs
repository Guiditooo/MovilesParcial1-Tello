using UnityEngine;


public class GameMaster : MonoBehaviourSingleton<GameMaster>
{
    [SerializeField] private bool singlePlayer;
    public int difficult;

    public void SetSinglePlayer(bool value) => singlePlayer = value;
    public void SetDifficult(int newDifficult) => difficult = newDifficult;
    public bool IsSinglePlayer() => singlePlayer;
    public int moneyP1;
    public int moneyP2;
}