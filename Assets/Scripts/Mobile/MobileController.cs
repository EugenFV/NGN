using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image _joystickBack;
    [SerializeField] private Image _joystick;

    private Vector2 _inputVector;

    //private MoveController _moveController;

    void OnEnable()
    {
        ResetJoystickPosition();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ResetJoystickPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBack.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _joystickBack.rectTransform.sizeDelta.x);
            pos.y = (pos.y / _joystickBack.rectTransform.sizeDelta.x);

            _inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
            _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

            //joystick anchored Position
            var anchX = _inputVector.x * (_joystickBack.rectTransform.sizeDelta.x / 2);
            var anchY = _inputVector.y * (_joystickBack.rectTransform.sizeDelta.x / 2);

            _joystick.rectTransform.anchoredPosition = new Vector2(anchX, anchY);
        }
    }

    public float Horizontal()
    {
        if (_inputVector.x != 0)
        {
            return _inputVector.x;
        }
        else
        {
            return Input.GetAxis("Horizontal");
        }
    }

    public float Vertical()
    {
        if (_inputVector.y != 0)
        {
            return _inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }
    }

    private void ResetJoystickPosition()
    {
        _inputVector = Vector2.zero;
        _joystick.rectTransform.anchoredPosition = Vector2.zero;
    }

    // Start is called before the first frame update
    private void Start()
    {
        //MoveController
        //MoveController moveController = FindObjectOfType<MoveController>().gameObject.GetComponent<MoveController>();
        //moveController.gameObject.GetComponent<MoveController>();
        //public void SetUnityController(UnityJoystickController _unityController)
        //moveController.SetUnityController(this);
    }

    public void UnlockController()
    {
        _joystickBack.raycastTarget = true;
        _joystick.raycastTarget = true;
    }

    // Update is called once per frame
    private void Update()
    {

    }
}