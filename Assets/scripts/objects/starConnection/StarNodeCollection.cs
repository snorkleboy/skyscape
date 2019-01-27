using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using util;
using UnityEngine;
namespace Objects.Galaxy
{
    [System.Serializable]
    public struct StarNodeCollectionModel{
        [SerializeField]public Dictionary<int, List<StarNodeModel>> starNodes;
        public StarNodeCollectionModel(StarNodeCollection collection){
            starNodes = new Dictionary<int, List<StarNodeModel>>();
            foreach (var keyVal in collection._starNodes)
            {
                var branchI = keyVal.Key;
                var starBranch = keyVal.Value;
                var length = starBranch.Count;
                var branch = new List<StarNodeModel>();
                for(var i=0;i< length;i++){
                    branch.Add(starBranch[i].model);
                }
                starNodes.Add(branchI,branch);
            }
        }
    }
    public class StarNodeCollection : ISaveAble<StarNodeCollectionModel>
    {
        public StarNodeCollectionModel model{get{return new StarNodeCollectionModel(this);}}
        public Dictionary<int, List<StarNode>> _starNodes = new Dictionary<int, List<StarNode>>();

        public StarNodeCollection(Dictionary<int, List<StarNode>> starNodes){
            foreach (var item in starNodes)
            {
                _starNodes[item.Key] = item.Value;
            }
        }
        public void render(int scene)
        {
            foreach (var arr in _starNodes.Values)
            {
                foreach (var star in arr)
                {
                    star.appear(scene);
                }
            }
        }
        public void destroy()
        {

            foreach (var keyVal in _starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.appearer.destroy();
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
