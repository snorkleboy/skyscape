using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    public class MakeBranches : StarMaker
    {
        [Range(1, 50)]
        [SerializeField] public int perStarAngle = 5;
        public void setStarAngle(Slider slider) { perStarAngle = (int)slider.value; }
        [Range(1, 200)]
        [SerializeField] public int starToStarDistance = 10;
        public void setStarToStarDistance(Slider slider) { starToStarDistance = (int)slider.value; }
        public override Dictionary<int, StarNode[]> actOn(Dictionary<int, StarNode[]> starNodes)
        {
            foreach (var ketVal in starNodes)
            {
                createBranch(ketVal.Value, ketVal.Key);
            }
            return starNodes;
        }

        private StarNode[] createBranch(StarNode[] starArr, int branchI)
        {
            int numStars = starArr.Length;
            for (int i = 1; i < numStars; i++)
            {
                var newstar = createStarNode(starArr[0], branchI, i);
                starArr[i] = (newstar);
            }
            return starArr;
        }
        private StarNode createStarNode(StarNode branchLeader, int branchNum, int starI)
        {
            var starRep = newStarRepresention(branchLeader.transform.parent);
            starRep.transform.position = branchLeader.transform.position;
            starRep.transform.Translate(branchLeader.transform.forward * starI * starToStarDistance);
            starRep.transform.RotateAround(Vector3.zero, Vector3.up, perStarAngle * starI);
            return new StarNode(starRep);
        }
    }
}
