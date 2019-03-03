using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    public class MakeBranches : ProtoStarMaker
    {
        [Range(1, 50)]
        [SerializeField] public int perStarAngle = 10;
        public void setStarAngle(Slider slider) { perStarAngle = (int)slider.value; }
        [Range(1, 200)]
        [SerializeField] public int starToStarDistance = 50;
        public void setStarToStarDistance(Slider slider) { starToStarDistance = (int)slider.value; }
        [Range(1, 40)]
        [SerializeField] public int branchSize = 10;
        public void setBanchSize(Slider slider) { branchSize = (int)slider.value; }
        public override Dictionary<int, List<ProtoStar>> actOn(Dictionary<int, List<ProtoStar>> starNodes)
        {
            foreach (var ketVal in starNodes)
            {
                createBranch(ketVal.Value, ketVal.Key);
            }
            return starNodes;
        }

        private List<ProtoStar> createBranch(List<ProtoStar> starArr, int branchI)
        {
            var numStars = (int)(branchSize * Random.Range(.8f, 1.2f));
            for (int i = 1; i < numStars; i++)
            {
                var newstar = createStarNode(starArr, branchI, i);
                starArr.Add(newstar);
                var connection = starFactory.makeConnection(starArr[i - 1], newstar);
            }
            return starArr;
        }
        private ProtoStar createStarNode(List<ProtoStar> starArr, int branchNum, int starI)
        {
            var branchLeader = starArr[0];
            var ran = (int)Random.Range(-1, 1);
            var ranMult = 1 + (.2 * ran);
            var star = starFactory.newStar(branchLeader.state.appearableState.appearTransform.parent);
            star.state.appearableState.appearTransform.position = branchLeader.state.appearableState.appearTransform.position;
            star.state.appearableState.appearTransform.Translate(branchLeader.state.appearableState.appearTransform.forward * (int)(ranMult*starI * starToStarDistance + starI));
            star.state.appearableState.appearTransform.Translate(Vector3.up * ((int)(ranMult * starToStarDistance * .3)));
            star.state.appearableState.appearTransform.RotateAround(Vector3.zero, Vector3.up, ((int)(ranMult * starI * perStarAngle)+ starI));
            star.state.appearableState.position = star.state.appearableState.appearTransform.position;

            if (starI > 0)
            {
                star.state.appearableState.appearTransform.RotateAround(starArr[starI-1].state.appearableState.appearTransform.position, Vector3.up, ((int)(ranMult * perStarAngle)));
            }
            star.appearer.state.position = star.state.appearableState.appearTransform.position;
            return star;
        }
    }
}
