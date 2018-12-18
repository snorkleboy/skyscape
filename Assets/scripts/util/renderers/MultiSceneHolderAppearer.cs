// using System.Collections.Generic;
// using UnityEngine;
// using System;
// namespace Objects.Galaxy
// {
//     [System.Serializable]
//     public class MultiSceneHolderAppearer : MultiSceneAppearer
//     {
//         public MultiSceneHolderAppearer(sceneAppearInfo[] appearInfo, Transform parent) : base(appearInfo, parent){}
//         public List<IAppearable> appearables = new List<IAppearable>();
//         public void setAppearables(IAppearable[] renderables){
//             this.appearables = new List<IAppearable>();
//             addAppearables(renderables);
//         }
//         public void addAppearables(IAppearable[] renderablesIn)
//         {
//             this.appearables.AddRange(renderablesIn);
//         }
//         public void addAppearables(IAppearable renderable)
//         {
//             this.appearables.Add(renderable);
//         }
//         public override bool appear(int scene)
//         {
//             if(base.appear(scene))
//             {
//                 // activeGO.transform.position = position;
//                 foreach (var appearable in appearables)
//                 {
//                     if (appearable.appearer.attachementPoint == null){
//                         appearable.appearer.attachementPoint = activeGO.transform;
//                     }
//                     appearable.appear(scene);
//                 }
//             }
//             return true;
//         }
//         public override void destroy(){
//             base.destroy();
//             foreach (var item in appearables)
//             {
//                 item.appearer.destroy();
//             }
//         }
//     }
// }
