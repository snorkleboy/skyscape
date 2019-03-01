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
            var state = new ProtostarState(){
                appearableState = new State.AppearableState(
                    appearTransform:holder,
                    position:Vector3.zero,
                    star:null
                )
            };
            var mainAppearer = new SingleSceneAppearer(new sceneAppearInfo(_sceneToPrefab[0]),0,holder,state.appearableState);
            var rep = new LinkedAppearer(mainAppearer,state,state.appearableState);
            var star = new ProtoStar(rep,state);
            star.appearer.appear(0);
            return star;
        }
        public virtual ProtoStarConnection makeConnection(ProtoStar a, ProtoStar b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}


