using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private PlayerLogic _pm = null;
    [SerializeField]
    private bool _rightSide = false;


    void Start()
    {
        if (_pm == null)
        {
            Debug.LogError("You forgot to set the _pm variable");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_rightSide)
        {
            _pm.MoveToSide(1f);
            return;
        }

        _pm.MoveToSide(-1f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pm.MoveToSide(0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_rightSide)
        {
            _pm.MoveToSide(1f);
            return;
        }

        _pm.MoveToSide(-1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _pm.MoveToSide(0f);
    }
}
