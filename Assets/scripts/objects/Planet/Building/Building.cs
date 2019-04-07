using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Newtonsoft.Json;
namespace Objects.Galaxy
{
    public static class buildingNames{
        public static string[] names = new string[]{"buildinger","anotherBuil","Foo","Bar"};
    }
    public class BuildingModel{
        public string name;
        public PopModel[] pops;
        public long id;
        public BuildingModel(){}
        public BuildingModel(Building building){
            id = building.id;
            name = building.name;
            pops = new PopModel[building.pops.Count];
            for(var i=0;i<pops.Length;i++){
                pops[i] = building.pops[i].model;
            }
        }
    }
    [System.Serializable]
	[JsonObject(MemberSerialization.OptIn)]

    public class Building : IIconable, IContextable, ISaveAble<BuildingModel>
    {
        public long id;
        public BuildingModel model{get{return new BuildingModel(this);}}
        [SerializeField]public string name;
        public Sprite buildingSprite;
        public string title{get;}
        private List<Pop> _pops = new List<Pop>();
        public List<Pop> pops{get{return _pops;}}
        public int updateId{get;}
        public Building(Sprite icon,Pop[] startPops, BuildingModel model){
            title = "building";
            buildingSprite = icon;
            name = model.name;
            pops.AddRange(startPops);

        }
        public Building(Sprite icon)
        {
            title = "building";
            name = buildingNames.names[Random.Range(0,buildingNames.names.Length-1)];
            buildingSprite = icon;
        }
        public Building(Sprite icon,Pop[] startPops):this(icon)
        {
            pops.AddRange(startPops);
        }
        public GameObject renderContext(Transform parent, clickViews callbacks){
            var holder =  new GameObject("BUILDING Context");
            holder.transform.SetParent(parent, false);
            holder.AddComponent<HorizontalLayoutGroup>();
            holder.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;
            renderIcon(callbacks).transform.SetParent(holder.transform, false);
            var children = renderInfo(callbacks);
            if (children != null){
                var right = new GameObject("info-right");
                right.transform.SetParent(holder.transform, false);
                right.AddComponent<VerticalLayoutGroup>();
                foreach( var infoObj in children){
                    infoObj.transform.SetParent(right.transform,false);
                }
            }
            return holder;

        }
        public IconInfo getIconableInfo(){
            var info = new IconInfo();
            info.source = this;
            info.name = name;
            info.icon = buildingSprite;
            return info;
        }
        public GameObject renderIcon(){
            var go =  new GameObject("BuildingIcon");
            var image = go.AddComponent<Image>();
            image.sprite = buildingSprite;
            image.rectTransform.sizeDelta = new Vector2(20,20);
            return go;
        }
        public GameObject renderIcon(clickViews viewCallBacks){
            var go = renderIcon();
            var button = go.AddComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(()=>{viewCallBacks.contextViewCallback(this);});
            button.image = go.GetComponent<Image>();
            return go;
        }
        public List<GameObject> renderInfo(clickViews viewCallBacks){
            List<GameObject> gos = new List<GameObject>();
            if (pops != null)
            {
                foreach (var item in pops)
                {
                    gos.Add(item.renderIcon(viewCallBacks));
                }
            }
            return gos;        
        }

    }
}