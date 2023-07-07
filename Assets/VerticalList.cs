using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalList : VerticalLayoutGroup
{
    // https://stackoverflow.com/questions/39114670/how-to-create-dynamic-table-in-unity
    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, minHeight);
    }

}

