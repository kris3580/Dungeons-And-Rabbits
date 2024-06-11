using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonVisual : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{


    [SerializeField] Sprite unhoveredButtonImage, hoveredButtonImage, clickedButtonImage;
    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = hoveredButtonImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = unhoveredButtonImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = clickedButtonImage;
        Invoke("ReturnToDefaultState", 0.1f);
    }

    void ReturnToDefaultState()
    {
        buttonImage.sprite = unhoveredButtonImage;
    }

}
