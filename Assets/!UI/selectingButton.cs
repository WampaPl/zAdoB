using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectingButton : MonoBehaviour
{
    [SerializeField] Color inactiveColor;
    [SerializeField] Color activeColor;
    [SerializeField] ControllableNavAgent agent;
    [SerializeField] bool state;
    Image myImage;
    MyPlayerController myPlayer;

    void Start()
    {
        myImage = GetComponent<Image>();
        myPlayer = this.GetComponentInParent<MyPlayerController>();

        ApplyStateChange();
    }

    public void buttonClick()
    {
        state = !state;
        ApplyStateChange();
    }

    void ApplyStateChange()
    {
        if (state)
        {
            myImage.color = activeColor;
            myPlayer.AddLeader(agent);
        }
        else
        {
            myImage.color = inactiveColor;
            myPlayer.RemoveLeader(agent);
        }
    }
}
