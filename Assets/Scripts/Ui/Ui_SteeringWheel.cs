using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Ui_SteeringWheel : MonoBehaviour
{
    public PalletMover.MoveType typeInput = PalletMover.MoveType.WASD;
    public Graphic UI_Element;
    public Player player;
    private float onTimeRotation;
    private float maxTimeRotation = 0.5f;

    private Quaternion rotationCurrent;
    private Quaternion rotationLeft = new Quaternion(0.0f, 0.0f, 0.7071068f, 0.7071068f);
    private Quaternion rotationNone = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    private Quaternion rotationRight = new Quaternion(0.0f, 0.0f, -0.7071068f, 0.7071068f);
    
    RectTransform rectT;
    Vector2 centerPoint;
    public float maximumSteeringAngle = 200f;
    public float wheelReleasedSpeed = 200f;
    private float wheelAngle = 0f;
    private float wheelPrevAngle = 0f;
    private bool wheelBeingHeld = false;

    public float GetClampedValue() => wheelAngle / maximumSteeringAngle;
    public float GetAngle() => wheelAngle;
    void Start()
    {
        rectT = UI_Element.rectTransform;
        InitEventsSystem();
    }
    void Update()
    {
#if UNITY_ANDROID 
        if (!wheelBeingHeld && !Mathf.Approximately(0f, wheelAngle))
        {
            float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= deltaAngle;
            else
                wheelAngle += deltaAngle;
        }
        rectT.localEulerAngles = Vector3.back * wheelAngle;

        if (rectT.localEulerAngles.z > 20 && rectT.localEulerAngles.z < 180)
            player.direction = Player.Direction.Left;
        else if (rectT.localEulerAngles.z > 270) 
            player.direction = Player.Direction.Right;
        else
            player.direction = Player.Direction.None;
#else
        if (typeInput == PalletMover.MoveType.WASD)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (player.direction != Player.Direction.Left)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.Left;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (player.direction != Player.Direction.Right)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.Right;
                }
            }
            else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                if (player.direction != Player.Direction.None)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.None;
                }
            }

            if (onTimeRotation < maxTimeRotation)
            {
                onTimeRotation += Time.deltaTime;
                switch (player.direction)
                {
                    case Player.Direction.Left:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationLeft, onTimeRotation / maxTimeRotation);
                        break;
                    case Player.Direction.Right:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationRight, onTimeRotation / maxTimeRotation);
                        break;
                    case Player.Direction.None:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationNone, onTimeRotation / maxTimeRotation);
                        break;
                    default:
                        break;
                }
            }
        }
        else if (typeInput == PalletMover.MoveType.Arrows)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (player.direction != Player.Direction.Left)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.Left;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (player.direction != Player.Direction.Right)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.Right;
                }
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (player.direction != Player.Direction.None)
                {
                    rotationCurrent = rectT.rotation;
                    onTimeRotation = 0;
                    player.direction = Player.Direction.None;
                }
            }

            if (onTimeRotation < maxTimeRotation)
            {
                onTimeRotation += Time.deltaTime;
                switch (player.direction)
                {
                    case Player.Direction.Left:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationLeft, onTimeRotation / maxTimeRotation);
                        break;
                    case Player.Direction.Right:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationRight, onTimeRotation / maxTimeRotation);
                        break;
                    case Player.Direction.None:
                        rectT.rotation = Quaternion.Lerp(rotationCurrent, rotationNone, onTimeRotation / maxTimeRotation);
                        break;
                    default:
                        break;
                }
            }
        }
#endif
    }
    void InitEventsSystem()
    {
        EventTrigger events = UI_Element.gameObject.GetComponent<EventTrigger>();

        if (events == null)
            events = UI_Element.gameObject.AddComponent<EventTrigger>();

        if (events.triggers == null)
            events.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
        UnityAction<BaseEventData> functionCall = new UnityAction<BaseEventData>(PressEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(DragEvent);
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.Drag;
        entry.callback = callback;

        events.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        callback = new EventTrigger.TriggerEvent();
        functionCall = new UnityAction<BaseEventData>(ReleaseEvent);//
        callback.AddListener(functionCall);
        entry.eventID = EventTriggerType.PointerUp;
        entry.callback = callback;

        events.triggers.Add(entry);
    }
    public void PressEvent(BaseEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        wheelBeingHeld = true;
        centerPoint = RectTransformUtility.WorldToScreenPoint(((PointerEventData)eventData).pressEventCamera, rectT.position);
        wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
    }
    public void DragEvent(BaseEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
        if (Vector2.Distance(pointerPos, centerPoint) > 20f)
        {
            if (pointerPos.x > centerPoint.x)
                wheelAngle += wheelNewAngle - wheelPrevAngle;
            else
                wheelAngle -= wheelNewAngle - wheelPrevAngle;
        }
        wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
        wheelPrevAngle = wheelNewAngle;
    }
    public void ReleaseEvent(BaseEventData eventData)
    {
        DragEvent(eventData);
        wheelBeingHeld = false;
    }
}