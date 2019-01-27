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
        public Dictionary<int, List<StarNode>> hydrate( Dictionary<int, List<StarNodeModel>> savedModels)
        {
            var starNodes = new Dictionary<int, List<StarNode>>();
            foreach(var branchThing in savedModels)
            {
                var branchSaved = branchThing.Value;
                var branchI = branchThing.Key;

                var branch = new List<StarNode>();

                foreach(var starModel in branchSaved){
                    var star = starFactory.createStar(holder.transform,starModel);


                    branch.Add(star);
                }
                starNodes[branchI] = branch;
            }
            return starNodes;
        }
        public Dictionary<int, List<StarNode>> hydrate( Dictionary<int, List<ProtoStar>> protoNodes)
        {
            var protoToStar = new Dictionary<ProtoStar,StarNode>();
            var starNodes = new Dictionary<int, List<StarNode>>();
            foreach(var branchI in protoNodes.Keys)
            {
                starNodes[branchI] = new List<StarNode>();
                for(var i = 0; i<protoNodes[branchI].Count; i++)
                {
                    var protoStar = protoNodes[branchI][i];
                    var starNode = starFactory.createStar(holder.transform, protoStar.appearer.getAppearPosition(0));
                    starNodes[branchI].Add(starNode);
                    protoToStar[protoStar] = starNode;
                }
            }

            foreach(var branchI in protoNodes.Keys)
            {
                for(var i = 0; i<protoNodes[branchI].Count; i++)
                {
                    var protoStar = protoNodes[branchI][i];
                    var starNode = protoToStar[protoStar];
                    foreach(var connection in protoStar.connections){
                        var a = connection.nodes[1];
                        var b = connection.nodes[0]; 
                        var otherproto = a == protoStar ? b : a;
                        var otherStarNode = protoToStar[otherproto];
                        starFactory.makeConnection(starNode,otherStarNode );
                    };
                }
            }

            buildUpGalaxy(starNodes);
            return starNodes;
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