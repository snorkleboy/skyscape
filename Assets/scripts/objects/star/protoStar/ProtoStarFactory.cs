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
            var rep = new ProtoStarRenderer(_sceneToPrefab, holder);
            var star = new ProtoStar(rep);
            star.render(0);
            return star;
        }
        public virtual ProtoStarConnection makeConnection(ProtoStar a, ProtoStar b)
        {
            return starConnectionFactory.makeConnection(a, b);
        }
    }
}


