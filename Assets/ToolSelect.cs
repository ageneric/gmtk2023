using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanHammer : MonoBehaviour
{
    public enum Tool { None, BanHammer, Flagger }

    public static Tool userTool = Tool.BanHammer;

    // Update is called once per frame
    public void SetTool(Tool tool)
    {
        userTool = tool;
    }
}
