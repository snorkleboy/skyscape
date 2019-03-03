using System.Collections.Generic;
using UnityEngine;
using System;
using Objects.Galaxy.State;
namespace Objects.Galaxy
{
    public class ProtoStarConnectionState{
        public ProtoStar[] nodes = new ProtoStar[2];
    }
    public class ProtoStarConnectionRenderer : IAppearer
    {
        public ProtoStarConnectionState connectionState;
        public AppearableState state{get;set;}
        private sceneAppearInfo[] sceneToPrefab;
        public ProtoStarConnectionRenderer(sceneAppearInfo[] sceneToPrefab,ProtoStarConnectionState connectionState)
        {
            this.state = new AppearableState(
                appearTransform:connectionState.nodes[0].appearer.state.appearTransform,
                position:connectionState.nodes[0].appearer.state.position,
                star: null
            );
            this.connectionState = connectionState;
            this.sceneToPrefab = sceneToPrefab;
        }
        public bool appear(int scene)
        {
            if(scene == 0){
                var _prefab = sceneToPrefab[0].prefab;
                if(state.appearTransform != null){
                    state.activeTransform = GameObject.Instantiate(_prefab, state.appearTransform).transform;
                }else{
                    util.Log.warnLog(this,"appearing object without an attachement point",_prefab,0);
                    state.activeTransform = GameObject.Instantiate(_prefab).transform;
                }
                var line =  state.activeTransform.GetComponent<DrawLineBetweenPoints>();
                line.setTarget(connectionState.nodes[0].state.appearableState.position, 0);
                line.setTarget(connectionState.nodes[1].state.appearableState.position, 1);
                line.draw();
                return true;
            }

            return false;
        }
        public void destroy()
        {
            if (state.activeTransform.gameObject != null)
            {
#if UNITY_EDITOR
    GameObject.DestroyImmediate(state.activeTransform.gameObject);
#else
    GameObject.Destroy(state.activeTransform.gameObject);
#endif
            }

        }
    }
}
