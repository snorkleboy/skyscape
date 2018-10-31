using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
namespace Objects.Galaxy
{
    public static class PopNames{
        public static string[] names = new string[]{"bob","tim","fisher","ms.disher","constilisher"};
    }
    [System.Serializable]
    public class Pop : IIconable
    {
       [SerializeField]public string name;
        [SerializeField]public int money;
        public Pop()
        {
            name = PopNames.names[UnityEngine.Random.Range(0,PopNames.names.Length-1)];
            money = UnityEngine.Random.Range(0,100);
        }
        public GameObject renderIcon(Transform transform,clickViews viewCallBacks){
            Debug.Log("POP RENDER ICON");
            Debug.Log(name + " " + money);
            return new GameObject();
        }
    }
}