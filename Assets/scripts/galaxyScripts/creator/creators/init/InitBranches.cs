using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using UnityEngine.UI;

namespace GalaxyCreators
{
    public class InitBranches : StarMaker
    {
        [Range(1, 200)]
        [SerializeField] public int centralNoHabZoneRadius = 30;
        public void setCentralNoHabZoneRadius(Slider slider) { centralNoHabZoneRadius = (int)slider.value; }
        [Range(1, 10)]
        [SerializeField] public int numBranches = 4;
        public void setNumBranchess(Slider slider) { numBranches = (int)slider.value; }



        public override Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> starNodes)
        {
            return initBranches(starNodes);
        }
        private Dictionary<int, List<StarNode>> initBranches(Dictionary<int, List<StarNode>> starNodes)
        {

            for (int i = 0; i < numBranches; i++)
            {
                GameObject branch = new GameObject();
                branch.name = "branch " + i;
                branch.transform.SetParent(holder.transform);

                var star = starFactory.newStar(branch.transform);
                star.transform.Translate(star.transform.forward * centralNoHabZoneRadius);
                star.transform.RotateAround(Vector3.zero, Vector3.up, i * (360 / numBranches));
                star.position = star.transform.position;
                var branchArr = new List<StarNode>();
                branchArr.Add(star);
                starNodes[i] = branchArr;
            }
            for (int i = 0; i < numBranches - 1; i++)
            {
                var a = starNodes[i][0];
                var b = starNodes[i + 1][0];
                var connection = starFactory.makeConnection(a, b);
            }
            starFactory.makeConnection(starNodes[0][0], starNodes[starNodes.Count-1][0]);
            return starNodes;
        }
    }
}