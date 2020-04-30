using System.Collections.Generic;
using Newtonsoft.Json;
using Game.Core.Util;
using Game.Core.Entities;
namespace Game.Application
{
        [System.Serializable]
        [JsonObject(MemberSerialization.OptIn)]
        public class StarNodeCollection
        {
            public Dictionary<int, List<StarNode>> _starNodes = new Dictionary<int, List<StarNode>>();
            [JsonProperty]
            public Dictionary<int, List<Reference<StarNode>>> starNodeRef
            {
                get
                {
                    var ret = new Dictionary<int, List<Reference<StarNode>>>();
                    foreach (var branch in _starNodes)
                    {
                        var branchR = new List<Reference<StarNode>>();
                        foreach (var item in branch.Value)
                        {
                            branchR.Add((Reference<StarNode>)item);
                        }
                        ret[branch.Key] = branchR;
                    }
                    return ret;
                }
            }

            public StarNodeCollection(Dictionary<int, List<StarNode>> starNodes)
            {
                _starNodes = starNodes;
            }
            //public void render(int scene)
            //{
            //    foreach (var arr in _starNodes.Values)
            //    {
            //        foreach (var star in arr)
            //        {
            //            star.appearer.appear(scene);
            //        }
            //    }
            //}
            //public void destroy()
            //{

            //    foreach (var keyVal in _starNodes)
            //    {
            //        foreach (var star in keyVal.Value)
            //        {
            //            star.appearer.destroy();
            //        }
            //    }
            //}

        }

}
