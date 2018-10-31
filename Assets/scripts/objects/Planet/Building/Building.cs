using System.Collections.Generic;
using UnityEngine;
using UI;
namespace Objects.Galaxy
{
    public static class buildingNames{
        public static string[] names = new string[]{"buildinger","anotherBuil","Foo","Bar"};
    }
    [System.Serializable]
    public class Building : IIconable
    {
        [SerializeField]public string name;
        [SerializeField]private Pop[] _pops;
        public Pop[] pops{get;}
        public Building()
        {
            name = buildingNames.names[Random.Range(0,buildingNames.names.Length-1)];
            _pops = new Pop[1];
        }
        public Building(Pop[] startPops):this()
        {
            pops = startPops;
        }
        public GameObject renderIcon(Transform transform,clickViews viewCallBacks){
            Debug.Log("building" + name + "   pop names:");
            foreach (var item in pops)
            {
                item.renderIcon(transform,viewCallBacks);
            }            
            return new GameObject();
        }
    }
}