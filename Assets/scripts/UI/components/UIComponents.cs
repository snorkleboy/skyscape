using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
namespace UI
{
    public static class UIComponents
    {
        public static GameObject renderStrSprPair(string name, Sprite sprite, bool vertical){
            var holder = new GameObject("iconedMsg");

            if (vertical){
                var layout = holder.AddComponent<VerticalLayoutGroup>();
                layout.childControlHeight = false;
                layout.childControlWidth = false;
                layout.childForceExpandHeight = false;
                layout.childForceExpandWidth = false;
            }else{
                 var layout = holder.AddComponent<HorizontalLayoutGroup>();
                layout.childControlHeight = false;
                layout.childControlWidth = false;
                layout.childForceExpandHeight = false;
                layout.childForceExpandWidth = false;
            }

            var img = new GameObject("image");
            var imgComp = img.AddComponent<Image>();
            imgComp.sprite = sprite;
            imgComp.rectTransform.sizeDelta = new Vector2(20,20);
            img.transform.SetParent(holder.transform, true);

            var text = new GameObject("txt");
            var txt = text.AddComponent<Text>();
            txt.text = name;
            txt.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            txt.color = Color.magenta;
            txt.rectTransform.sizeDelta = new Vector2(name.Length*8,20);
            text.transform.SetParent(holder.transform,false);

            var fitter = holder.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            return holder;
        }

        public static GameObject renderIconLabel(IconInfo info){
            var holder = new GameObject("galaxyViewIcon" + info.name);

            var layout = holder.AddComponent<HorizontalLayoutGroup>();
            layout.childControlHeight = false;
            layout.childControlWidth = false;
            layout.childForceExpandHeight = false;
            layout.childForceExpandWidth = false;
            renderStrSprPair(info.name, info.icon,false).transform.SetParent(holder.transform,false);
            if (info.details != null){
                for(var i = 0;i<info.details.Length;i++){
                    renderStrSprPair(info.details[i].name,info.details[i].icon,false)
                        .transform.SetParent(holder.transform,false);
                }
            }
            var fitter = holder.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            return holder;
        }
        public static GameObject renderIconUI(IconInfo info)
        {
            var holder = new GameObject("icon " + info.name);
            holder.AddComponent<HorizontalLayoutGroup>();
            var left = new GameObject("icon");
            left.transform.SetParent(holder.transform,false);
            left.AddComponent<Image>().sprite = info.icon;
            var title = new GameObject("title");
            title.transform.SetParent(left.transform,false);
            title.AddComponent<Text>().text = info.name;

            var right = new GameObject("details");
            right.transform.SetParent(holder.transform,false);
            right.AddComponent<VerticalLayoutGroup>();
            if (info.details != null){
                foreach(var subInfo in info.details){
                    var subIcon = subInfo.icon;
                    var subName = subInfo.name;
                    var subGo = new GameObject("subIcon " + subName);
                    subGo.transform.SetParent(right.transform);
                    subGo.AddComponent<Image>().sprite = subIcon;
                    var subTitle = new GameObject("subTitle");
                    subTitle.transform.SetParent(subGo.transform,false);
                    subTitle.AddComponent<Text>().text = subName;
                }
            }

            var fitter = holder.AddComponent<ContentSizeFitter>();
            fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            return holder;
        }
        
    }
}