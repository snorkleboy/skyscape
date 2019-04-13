using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Objects;

namespace GalaxyCreators
{

    public class GameGalaxyCreator:galaxyCreator<StarMaker>{
        
        [SerializeField] public new List<StarMaker> creatorStack = new List<StarMaker>();
        public StarFactory starFactory;
        public IEnumerator hydrate(SavedGame saved, Dictionary<int, List<StarNode>> toFill)
        {
            yield return null;
            var stateTable = saved.loadedModel.objectTable;
            foreach(var referenceBranch in saved.loadedModel.starNodes)
            {
                var newBranch = new List<StarNode>();
                foreach(var starReference in referenceBranch.Value){
                    yield return starFactory.createStar(holder.transform,starReference,stateTable,(star)=>newBranch.Add(star));
                }
                toFill[referenceBranch.Key] = newBranch;
            }
        }
        public IEnumerator hydrate( Dictionary<int, List<ProtoStar>> protoNodes,Dictionary<int, List<StarNode>> starNodes)
        {
            yield return null;
            var protoToStar = new Dictionary<ProtoStar,StarNode>();
            foreach(var branchI in protoNodes.Keys)
            {
                starNodes[branchI] = new List<StarNode>();
                for(var i = 0; i<protoNodes[branchI].Count; i++)
                {
                    var protoStar = protoNodes[branchI][i];
                    var starNode = starFactory.createStar(holder.transform, protoStar.appearer.state.position);
                    starNodes[branchI].Add(starNode);
                    protoToStar[protoStar] = starNode;
                }
                yield return null;
            }

            foreach(var branchI in protoNodes.Keys)
            {
                for(var i = 0; i<protoNodes[branchI].Count; i++)
                {
                    var protoStar = protoNodes[branchI][i];
                    var starNode = protoToStar[protoStar];
                    
                    foreach(var connection in protoStar.state.connections){
                        var a = connection.state.nodes[1];
                        var b = connection.state.nodes[0]; 
                        var otherproto = a == protoStar ? b : a;
                        var otherStarNode = protoToStar[otherproto];
                        var conn = starFactory.makeConnection(starNode,otherStarNode );
                    };
                    yield return null;
                }
            }

            buildUpGalaxy(starNodes);
            yield return null;
        }
        public void buildUpGalaxy(Dictionary<int, List<StarNode>> starNodes){
            foreach (ICreator<StarNode> creator in creatorStack)
            {
                creator.actOn(starNodes);
            }
        }
        public override void create()
        {
            Debug.Log("CREATE GALAXY");
            if (created)
            {
                destroy();
                created = false;
            }
            starNodes = new Dictionary<int, List<StarMaker>>();
            foreach (ICreator<StarMaker> creator in creatorStack)
            {
                creator.actOn(starNodes);
            }
            created = true;
        }
    }
}