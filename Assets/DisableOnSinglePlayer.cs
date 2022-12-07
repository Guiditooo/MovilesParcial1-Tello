using UnityEngine;
public class DisableOnSinglePlayer : MonoBehaviour
{
    [Header("Objetos por SinglePlayer:")]
    [SerializeField] private GameObject[] objectsDisableSinglePlayer;
    [SerializeField] private Camera[] camsP1ToExpand;
    [SerializeField] private GameObject[] camsP2ToDisable;
    private Rect rectCameraExpand = new Rect(0, 0, 1, 1);

    [Space(20)] [Header("Objetos por Dificultad:")]
    private int difficult;
    public GameObject[] objectsOnDificult01;
    public GameObject[] objectsOnDificult02;

    private void Start()
    {
        SetObjectsSinglePlayer();
        SetObjectsDificult();
    }
    void SetObjectsSinglePlayer()
    {
        if (GameMaster.Get().IsSinglePlayer())
        {
            foreach (GameObject go in objectsDisableSinglePlayer)
            {
                go.SetActive(false);
            }
            foreach (GameObject camsP2 in camsP2ToDisable)
            {
                camsP2.SetActive(false);
            }
            foreach (Camera camsP1 in camsP1ToExpand)
            {
                camsP1.rect = rectCameraExpand;
            }
        }
    }

    void SetObjectsDificult()
    {

    }
}