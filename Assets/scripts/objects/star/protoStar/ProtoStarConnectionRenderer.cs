using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    public class ProtoStarConnectionState{
        public ProtoStar[] nodes = new ProtoStar[2];
    }
    public class ProtoStarConnectionRenderer : MultiSceneAppearer
    {
        ProtoStarConnectionState connectionState;
        public ProtoStarConnectionRenderer(sceneAppearInfo[] sceneToPrefab,ProtoStarConnectionState connectionState ,AppearableState appearableState) : base(sceneToPrefab,appearableState)
        {
            this.connectionState = connectionState;
        }
        protected override bool _appearImplimentation(int scene)
        {
            if (base._appearImplimentation(scene))
            {
                if ( scene == 0)
                {
                    if ((connectionState.nodes[0].transform != null && connectionState.nodes[1].transform != null))
                    {
                        var line = state.appearTransform.GetComponent<DrawLineBetweenPoints>();
                        line.setTarget(connectionState.nodes[0].transform.gameObject, 0);
                        line.setTarget(connectionState.nodes[1].transform.gameObject, 1);
                        line.draw();
                    }

                }
                return true;
            }
            return false;
        }
    }
}
