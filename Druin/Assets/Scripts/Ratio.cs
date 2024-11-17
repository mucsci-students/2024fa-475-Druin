using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ratio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
{
    SetRatio(5, 4); // Set aspect ratio to 3:4
}

void SetRatio(float w, float h)
{
    if (((float)Screen.width) / ((float)Screen.height) > w / h)
    {
        Screen.SetResolution((int)(((float)Screen.height) * (w / h)), Screen.height, true);
    }
    else
    {
        Screen.SetResolution(Screen.width, (int)(((float)Screen.width) * (h / w)), true);
    }
}
}
