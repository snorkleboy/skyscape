using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    class MakeLeaves : ProtoStarMaker
    {
        public override Dictionary<int, List<ProtoStar>> actOn(Dictionary<int, List<ProtoStar>> starNodes)
        {
            var count = 0;

            List<List<ProtoStar>> listLists = new List<List<ProtoStar>>();
            foreach (var branchStars in starNodes)
            {
                count = 0;
                var starArr = branchStars.Value;
                var list = new List<ProtoStar>();
                listLists.Add(list);
                foreach (var star in starArr)
                {
                    if (count > 0 && Random.Range(0,10)>6)
                    {
                        list.AddRange(makeLeafVein(star, count));
                    }
                    count++;
                }
                var branchI = branchStars.Key;

            }
            count = 0;
            listLists.ForEach(list =>
            {
                list.AddRange(starNodes[count]);
                starNodes[count] = list;
                count++;
            });
            return starNodes;
        }
        private List<ProtoStar> makeLeafVein(ProtoStar star, int branchCount)
        {

            var originalstar = star;
            var list = new List<ProtoStar>();
            for (var i =0; i < Random.Range(1, branchCount); i++)
            {
                var newStar = starFactory.newStar(originalstar.state.appearableState.appearTransform.parent);
                list.Add(newStar);
                newStar.appearer.appear(0);
                var ranMult = Random.Range(-.4f, .4f) + 1;
                Transform tran = newStar.state.appearableState.appearTransform;
                tran.position = star.state.appearableState.appearTransform.position;
                tran.Translate(star.state.appearableState.appearTransform.forward *(int)( ranMult * 50+ branchCount));
                tran.Translate(Vector3.up * (int)(ranMult*10));
                tran.RotateAround(originalstar.state.appearableState.appearTransform.position, Vector3.up, (int)(ranMult*30/ branchCount * i ));
                newStar.state.appearableState.position = tran.position;
                starFactory.makeConnection(star, newStar);
                star = newStar;
            }
            return list;
        }
    }
}
