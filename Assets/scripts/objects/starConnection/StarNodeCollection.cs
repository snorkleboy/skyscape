using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using util;
using UnityEngine;
namespace Objects.Galaxy
{
    [System.Serializable]

    public class StarNodeCollection
    {
        public Dictionary<int, List<Reference<StarNode>>> _starNodes = new Dictionary<int, List<Reference<StarNode>>>();

        public StarNodeCollection(Dictionary<int, List<StarNode>> starNodes){
            foreach (var item in starNodes)
            {
                _starNodes[item.Key] = item.Value.Select(i=>new Reference<StarNode>(i)).ToList();
            }
        }
        public void render(int scene)
        {
            foreach (var arr in _starNodes.Values)
            {
                foreach (var star in arr)
                {
                    star.value.appearer.appear(scene);
                }
            }
        }
        public void destroy()
        {

            foreach (var keyVal in _starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.value.appearer.destroy();
                }
            }
        }

        // public void deactive()
        // {
        //     foreach (var keyVal in _starNodes)
        //     {
        //         foreach (var star in keyVal.Value)
        //         {
        //             star.appearable.disable();
        //         }
        //     }
        // }
        // public void active()
        // {
        //     foreach (var keyVal in _starNodes)
        //     {
        //         foreach (var star in keyVal.Value)
        //         {
        //             star.appearable.enable();
        //         }
        //     }
        // }
    }
}
