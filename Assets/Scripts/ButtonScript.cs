using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : UIScript
{
    [SerializeField]
    private Button MenuButton;
    [SerializeField]
    private Image PointImage;

    public void SetInteractable(bool isInteractable)
    {
        PointImage.enabled = isInteractable;
        MenuButton.interactable = isInteractable;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetInteractable(false);
        SetInteractable(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
