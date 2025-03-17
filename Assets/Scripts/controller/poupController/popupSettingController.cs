using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupSettingController : MonoBehaviour
{
    public Button btnNotification;
    public Button btnVibrate;
    public Button btnExit;
    public Sprite spVibrateOn;
    public Sprite spVibrateOff;
    public Sprite spNotificationOn;
    public Sprite spNotificationOff;
    private bool isVibrate = true;
    private bool isNotification = true;
    private void Awake()
    {
        btnNotification.onClick.AddListener(OnClickBtnNotification);
        btnVibrate.onClick.AddListener(OnClickBtnVibrate);
        btnExit.onClick.AddListener(OnClickBtnExit);
    }
    void OnClickBtnVibrate()
    {
        if(isVibrate)
        {
            isVibrate = false;
            btnVibrate.image.sprite = spVibrateOff;
        }
        else
        {
            isVibrate = true;
            btnVibrate.image.sprite = spVibrateOn;
        }
    }
    void OnClickBtnNotification()
    {
        if (isNotification)
        {
            isNotification = false;
            btnNotification.image.sprite = spNotificationOff;
        }
        else
        {
            isNotification = true;
            btnNotification.image.sprite = spNotificationOn;
        }
    }
    void OnClickBtnExit()
    {
        gameObject.SetActive(false);
    }
}
