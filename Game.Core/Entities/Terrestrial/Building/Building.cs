using Game.Core.Entities.Interfaces;
using Game.Core.State;
using Newtonsoft.Json;
namespace Game.Core.Entities
{
    public static class buildingNames{
        public static string[] names = new string[]{"buildinger","anotherBuil","Foo","Bar"};
    }

    [System.Serializable]
	[JsonObject(MemberSerialization.OptIn)]

    public class Building : IHasStateObject
    {
        public long getId(){return state.id;}
        public IHasID  stateObject{get{return state;}set{state = (BuildingState)value;}}

        public BuildingState state{get;set;}

        // public Building(Sprite icon,Pop[] startPops, BuildingModel model){
        //     title = "building";
        //     buildingSprite = icon;
        //     name = model.name;
        //     pops.AddRange(startPops);

        // }
        public Building(BuildingState state){
            if(state == null){
                //Debug.LogError("state null");
            }
            this.state = state;
        }
        //public GameObject renderContext(Transform parent, clickViews callbacks){
        //    var holder =  new GameObject("BUILDING Context");
        //    holder.transform.SetParent(parent, false);
        //    holder.AddComponent<HorizontalLayoutGroup>();
        //    holder.AddComponent<AspectRatioFitter>().aspectMode = UnityEngine.UI.AspectRatioFitter.AspectMode.FitInParent;
        //    renderIcon(callbacks).transform.SetParent(holder.transform, false);
        //    var children = renderInfo(callbacks);
        //    if (children != null){
        //        var right = new GameObject("info-right");
        //        right.transform.SetParent(holder.transform, false);
        //        right.AddComponent<VerticalLayoutGroup>();
        //        foreach( var infoObj in children){
        //            infoObj.transform.SetParent(right.transform,false);
        //        }
        //    }
        //    return holder;

        //}
        //public IconInfo getIconableInfo(){
        //    var info = new IconInfo();
        //    info.source = this;
        //    info.name = state.named.name;
        //    info.icon = state.sprite;
        //    return info;
        //}
        //public GameObject renderIcon(){
        //    var go =  new GameObject("BuildingIcon");
        //    var image = go.AddComponent<Image>();
        //    image.sprite = state.sprite;
        //    image.rectTransform.sizeDelta = new Vector2(20,20);
        //    return go;
        //}
        //public GameObject renderIcon(clickViews viewCallBacks){
        //    var go = renderIcon();
        //    var button = go.AddComponent<UnityEngine.UI.Button>();
        //    button.onClick.AddListener(()=>{viewCallBacks.contextViewCallback(this);});
        //    button.image = go.GetComponent<Image>();
        //    return go;
        //}
        //public List<GameObject> renderInfo(clickViews viewCallBacks){
        //    List<GameObject> gos = new List<GameObject>();
        //    if (state.pops != null)
        //    {
        //        foreach (var item in state.pops)
        //        {
        //            gos.Add(item.value.renderIcon(viewCallBacks));
        //        }
        //    }
        //    return gos;        
        //}

    }
}