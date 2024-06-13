using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorchUICircle : MonoBehaviour
{

    public Image radialImage;

    private void Start()
    {
        radialImage = GetComponent<Image>();
    }

    void Update()
    {

        radialImage.fillAmount = Player.torchAmount / Torch.torchDurationLimit;

        Debug.Log($"{Player.torchAmount}, {Torch.torchDurationLimit}, {Player.torchAmount / Torch.torchDurationLimit}");


        radialImage.color = Color.Lerp(Color.red, Color.green, Player.torchAmount / Torch.torchDurationLimit);
    }

}
