using UnityEngine;
public static class GOExtensions
{
    public static GameObject SetParent(this GameObject go, Transform trans, bool worldPos = false){
        go.transform.SetParent(trans,worldPos);
        return go;
    }
    public static GameObject SetParent(this GameObject go, GameObject otherGo, bool worldPos = false){
        go.transform.SetParent(otherGo.transform,worldPos);
        return go;
    }
}