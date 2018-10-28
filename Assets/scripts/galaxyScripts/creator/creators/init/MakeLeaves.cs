using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    class MakeLeaves : StarMaker
    {
        public override Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> starNodes)
        {
            var count = 0;

            List<List<StarNode>> listLists = new List<List<StarNode>>();
            foreach (var branchStars in starNodes)
            {
                count = 0;
                var starArr = branchStars.Value;
                var list = new List<StarNode>();
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
        private List<StarNode> makeLeafVein(StarNode star, int branchCount)
        {
            var originalstar = star;
            var list = new List<StarNode>();
            for (var i =0; i < Random.Range(1, branchCount); i++)
            {
                var newStar = starFactory.newStar(originalstar.transform.parent);
                list.Add(newStar);
                newStar.render(0);
                var ranMult = Random.Range(-.4f, .4f) + 1;
                Transform tran = newStar.transform;
                tran.position = star.transform.position;
                tran.Translate(star.transform.forward *(int)( ranMult * 50+ branchCount));
                tran.Translate(Vector3.up * (int)(ranMult*10));
                tran.RotateAround(originalstar.transform.position, Vector3.up, (int)(ranMult*30/ branchCount * i ));
                newStar.position = tran.position;
                starFactory.makeConnection(star, newStar);
                star = newStar;
            }
            return list;

        }
    }
}
