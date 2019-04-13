using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
using System;
using UI;
using Newtonsoft.Json;
namespace Objects.Galaxy
{
    public static class PopNames{
        public static string[] names = new string[]{"bob","tim","fisher","ms.disher","constilisher"};
    }
    public class PopState : TerrestrialState{
        [SerializeField]public int money;
    }
    [System.Serializable]
    [JsonObject(MemberSerialization.OptIn)]

    public partial class Pop : IIconable, ISaveable<PopState>
    {
        public object  stateObject{get{return state;}set{state = (PopState)value;}}

        public long getId(){return state.id; }  
        public PopState state{get;set;}     

        // public Pop(Sprite sprite,PopModel model)
        // {
        //     popSprite = sprite;
        //     name = model.name;
        //     money = model.money;
        // }
        public Pop(PopState state)
        {
            this.state = state;
            // popSprite = sprite;
            // name = PopNames.names[UnityEngine.Random.Range(0,PopNames.names.Length-1)];
            // money = UnityEngine.Random.Range(0,100);
        }
    }
    public partial class Pop{
        public IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = state.named.name;
            info.icon = state.sprite;
            return info;
        }
        private GameObject renderIcon(){
            var go =  new GameObject("popIcon");
            var image = go.AddComponent<Image>();
            image.sprite = state.sprite;
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