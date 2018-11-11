using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public interface ICreator<T>
    {
        Dictionary<int, List<T>> actOn(Dictionary<int, List<T>> nodes);
        
    }
    public class ProtoStarMaker : MonoBehaviour,ICreator<ProtoStar>
    {
        public ProtoStarFactory starFactory;
        public GameObject holder;
        public virtual Dictionary<int, List<ProtoStar>> actOn(Dictionary<int, List<ProtoStar>> nodes){
            return nodes;
        }


    }
        public class StarMaker : MonoBehaviour,ICreator<StarNode>
    {
        public ProtoStarFactory starFactory;
        public GameObject holder;
        public virtual Dictionary<int, List<StarNode>> actOn(Dictionary<int, List<StarNode>> nodes){
            return nodes;
        }
    }

}

