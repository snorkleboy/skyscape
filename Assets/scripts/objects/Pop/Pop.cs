using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
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
        public int updateId{get;}
        public string title{get{return name;}}
        public Sprite popSprite;
        public Pop(Sprite sprite)
        {
            popSprite = sprite;
            name = PopNames.names[UnityEngine.Random.Range(0,PopNames.names.Length-1)];
            money = UnityEngine.Random.Range(0,100);
        }
        public iconInfo getIconableInfo(){
            var info = new iconInfo();
            info.source = this;
            info.name = name;
            info.icon = popSprite;
            return info;
        }
        private GameObject renderIcon(){
            var go =  new GameObject("popIcon");
            var image = go.AddComponent<Image>();
            image.sprite = popSprite;
            image.rectTransform.sizeDelta = new Vector2(20,20);
            return go;
        }
        public GameObject renderIcon(clickViews viewCallBacks){
            var go = renderIcon();
            var button = go.AddComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(()=>{Debug.Log("clicked" + this);});
            return go;
        }
        public List<GameObject> renderInfo(clickViews callBacks){
            return new List<GameObject>();
        }
    }
}