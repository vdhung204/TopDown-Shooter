using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popupShopController : MonoBehaviour
{
    public Button btnClose;

    private void Awake()
    {
        btnClose.onClick.AddListener(OnCloseClick);
    }
    void OnCloseClick()
    {
        gameObject.SetActive(false);
    }
}
