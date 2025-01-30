using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBar : MonoBehaviour
{
    [SerializeField] private List<Image> itemList = new List<Image>();
    [SerializeField] private int arrayIndex;
    private Color originalColor;

    private void Start()
    {
        arrayIndex = 0;
        originalColor = itemList[0].color;
    }

    private void UpdateColor()
    {
        foreach (var VARIABLE in itemList)
        {
            VARIABLE.color = originalColor;
        }
        itemList[arrayIndex].color = Color.gray;
    }
    
    private void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            if (scroll > 0)
            {
                if (arrayIndex == 0)
                {
                    arrayIndex = 3;
                    UpdateColor();
                    return;
                }
                arrayIndex--;
            }
            else
            {
                if (arrayIndex == 3)
                {
                    arrayIndex = 0;
                    UpdateColor();
                    return;
                }
                arrayIndex++;
            }

            UpdateColor();
        }
    }
}


