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
        public IEnumerator hydrate( Dictionary<int, List<StarNodeModel>> savedModels,Dictionary<int, List<StarNode>> starNodes)
        {
            yield return null;
            foreach(var branchThing in savedModels)
            {
                var branchSaved = branchThing.Value;
                var branchI = branchThing.Key;
                var branch = new List<StarNode>();
                foreach(var starModel in branchSaved){
                    var star = starFactory.createStar(holder.transform,starModel);
                    branch.Add(star);
                    yield return new WaitForSeconds(.1f);
                    Debug.Log("created " + star + " " + star.state.id);
                }
                starNodes[branchI] = branch;
                yield return new WaitForSeconds(.1f);

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
                        var a = connection.nodes[1];
                        var b = connection.nodes[0]; 
                        var otherproto = a == protoStar ? b : a;
                        var otherStarNode = protoToStar[otherproto];
                        starFactory.makeConnection(starNode,otherStarNode );
                        Debug.Log("created " + starNode + " " + starNode.state.id);

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