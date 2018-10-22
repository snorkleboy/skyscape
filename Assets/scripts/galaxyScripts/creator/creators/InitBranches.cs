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
        [SerializeField] public int numBranches = 5;
        public void setNumBranchess(Slider slider) { numBranches = (int)slider.value; }
        [Range(1, 40)]
        [SerializeField] public int branchSize = 10;
        public void setBanchSize(Slider slider) { branchSize = (int)slider.value; }


        public override Dictionary<int, StarNode[]> actOn(Dictionary<int, StarNode[]> starNodes)
        {
            return initBranches(starNodes);
        }
        private Dictionary<int, StarNode[]> initBranches(Dictionary<int, StarNode[]> starNodes)
        {

            for (int i = 0; i < numBranches; i++)
            {
                GameObject branch = new GameObject();
                branch.transform.SetParent(holder.transform);

                var star = new StarNode(newStarRepresention(branch.transform));
                star.transform.Translate(star.transform.forward * centralNoHabZoneRadius);
                star.transform.RotateAround(Vector3.zero, Vector3.up, i * (360 / numBranches));

                var numStars = (int)(branchSize * Random.Range(.8f, 1.2f));
                var branchArr = new StarNode[numStars];
                branchArr[0] = star;
                starNodes[i] = branchArr;
            }
            return starNodes;
        }
    }
}