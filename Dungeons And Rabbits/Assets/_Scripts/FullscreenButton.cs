using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FullscreenButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    [SerializeField] Sprite fullscreen_w_unhoveredImage, fullscreen_w_hoveredImage, fullscreen_w_clickedImage;
    [SerializeField] Sprite fullscreen_f_unhoveredImage, fullscreen_f_hoveredImage, fullscreen_f_clickedImage;

    private Sprite currentUnhoveredImage, currentHoveredImage, currentClickedImage;

    private Image buttonImage;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        ButtonSpriteHandler();


    }



    public void OnPress()
    {
        ButtonSpriteHandler();
    }

    void ButtonSpriteHandler()
    {
        if (MiscellaneousEvents.isWindowed)
        {
            currentUnhoveredImage = fullscreen_w_unhoveredImage;
            currentHoveredImage = fullscreen_w_hoveredImage;
            currentClickedImage = fullscreen_w_clickedImage;
        }
        else
        {
            currentUnhoveredImage = fullscreen_f_unhoveredImage;
            currentHoveredImage = fullscreen_f_hoveredImage;
            currentClickedImage = fullscreen_f_clickedImage;
        }
    }






    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.sprite = currentHoveredImage;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.sprite = currentUnhoveredImage;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.sprite = currentClickedImage;
    }
}
