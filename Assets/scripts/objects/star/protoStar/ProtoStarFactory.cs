using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Loaders;
namespace Objects.Galaxy
{
    public class ProtoStarFactory : MonoBehaviour
    {
        [SerializeField] public GameObject[] _sceneToPrefab;
        [SerializeField] public ProtoStarConnectionFactory starConnectionFactory;

        public virtual ProtoStar newStar(Transform holder)
        {
            return createStar(holder);
        }
        public ProtoStar createStar(Transform holder)
        {
            var go = new GameObject("protoNode");
            go.transform.SetParent(holder);
            var state = new ProtostarState(){
                appearableState = new State.AppearableState(
                    appearTransform: go.transform,
                    position:new Vector3(1,1,1),
                    star:null
                )
            };
            var mainAppearer = new SingleSceneAppearer(new sceneAppearInfo(_sceneToPrefab[0]),0,state.appearableState);
            var rep = new LinkedAppearer(mainAppearer,state);


            var star = new ProtoStar();
            star.init(rep, state);
            star.appearer.appear(0);
            return star;
        }
        public virtual ProtoStarConnection makeConnection(ProtoStar a, ProtoStar b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}


