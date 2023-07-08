using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSelect : MonoBehaviour
{
    public enum Tool { None, BanHammer, Flagger }
    [SerializeField]
    public static Tool userTool = Tool.BanHammer;

    // Update is called once per frame
    public void SetTool(Tool tool)
    {
        userTool = tool;
    }
}
