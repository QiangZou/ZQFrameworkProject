using UnityEngine;
using System.Collections;

public static class ExtensionsUILabel
{
    public static void SetText(this UILabel self, string path, string text)
    {
        self.text = text;
    }

}
