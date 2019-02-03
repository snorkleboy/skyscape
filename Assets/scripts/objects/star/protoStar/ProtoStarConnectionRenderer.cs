using System.Collections.Generic;
using UnityEngine;
using System;
namespace Objects.Galaxy
{
    public class ProtoStarConnectionRenderer : MultiSceneAppearer
    {
        ProtoStar[] nodes;
        public override Transform appearTransform { get; set; }
        public ProtoStarConnectionRenderer(sceneAppearInfo[] sceneToPrefab, ProtoStar[] nodes) : base(sceneToPrefab,nodes[0].transform)
        {
            this.nodes = nodes;
        }
        protected override bool _appearImplimentation(int scene)
        {
            if (base._appearImplimentation(scene))
            {
                if ( scene == 0)
                {
                    if ((nodes[0].transform != null && nodes[1].transform != null))
                    {
                        var line = activeGO.GetComponent<DrawLineBetweenPoints>();
                        line.setTarget(nodes[0].transform.gameObject, 0);
                        line.setTarget(nodes[1].transform.gameObject, 1);
                        line.draw();
                    }

                }
                return true;
            }
            return false;
        }
    }
}
