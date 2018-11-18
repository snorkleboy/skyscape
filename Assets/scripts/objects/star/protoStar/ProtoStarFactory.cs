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
            var star = new ProtoStar(null);
            var rep = new HolderRenderer<ProtoStar>(_sceneToPrefab, holder,star);
            star.renderHelper = rep;
            star.render(0);
            return star;
        }
        public virtual ProtoStarConnection makeConnection(ProtoStar a, ProtoStar b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}


