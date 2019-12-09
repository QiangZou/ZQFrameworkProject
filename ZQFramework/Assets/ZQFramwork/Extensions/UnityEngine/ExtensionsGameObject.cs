using UnityEngine;
using System.Collections;

public static class ExtensionsGameObject
{
    public static GameObject Clone(this GameObject self, string name = "", bool active = true)
    {
        GameObject gameObject = GameObject.Instantiate(self) as GameObject;
        gameObject.transform.parent = self.transform.parent;
        gameObject.transform.localScale = self.transform.localScale;

        if (name != "")
        {
            gameObject.name = name;
        }

        gameObject.SetActive(active);

        return gameObject;
    }

    public static void SetActive(this GameObject self, bool active, string path)
    {
        Transform transform = self.transform.Find(path);
        if (transform == null)
        {
            return;
        }

        transform.gameObject.SetActive(active);
    }

}
