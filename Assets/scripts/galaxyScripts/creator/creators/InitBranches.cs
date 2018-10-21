using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public class InitBranches : StarMaker
    {
        [Range(1, 200)]
        [SerializeField] private int centralNoHabZoneRadius = 30;
        [Range(1, 10)]
        [SerializeField] public int numBranches = 5;
        [Range(1, 40)]
        [SerializeField] private int branchSize = 10;

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