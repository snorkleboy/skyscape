using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace GalaxyCreators
{

    public class GameGalaxyCreator:galaxyCreator<StarMaker>{
        
        [SerializeField] public new List<StarMaker> creatorStack = new List<StarMaker>();
        public StarFactory starFactory;
        public Dictionary<int, List<StarNode>> hydrate( SavedGame savedGame)
        {
            var starNodes = new Dictionary<int, List<StarNode>>();

            var objectArr = new Dictionary<int, Object>();
            var json = JObject.Parse(savedGame.data);
            var gmIdStartCount = (int)json["idMaker"];
            Objects.GameManager.idMaker.count = gmIdStartCount;

            JArray starArr = (JArray)json["_starNodes"]["starNodes"];

            for(var starNum = 0; starNum < starArr.Count; starNum++){
                var starJson = starArr[starNum];
                objectArr[(int)starJson["id"]] =  starFactory.createBaseStar();
            }
            for(var starNum = 0; starNum < starArr.Count; starNum++){
                var starJson = starArr[starNum];
                // ((StarNode)objectArr[(int)starJson["id"]]).planetable
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