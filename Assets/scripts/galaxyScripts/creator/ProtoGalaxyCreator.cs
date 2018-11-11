using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects.Galaxy;
using Util;
using UnityEngine.SceneManagement;
namespace GalaxyCreators
{
    [System.Serializable]
    public class ProtoGalaxyCreator:galaxyCreator<ProtoStar>{
        [SerializeField] public new List<ProtoStarMaker> creatorStack = new List<ProtoStarMaker>();
        public GameObject GameManagerPrefab;
        public void startGame(){
            SceneManager.LoadScene(1);
            Instantiate(GameManagerPrefab,transform);
        }
        public override void create()
        {
            if (created)
            {
                destroy();
                created = false;
            }
            starNodes = new Dictionary<int, List<ProtoStar>>();
            foreach (ICreator<ProtoStar> creator in creatorStack)
            {
                creator.actOn(starNodes);
            }
            created = true;
        }
        public void destroy()
        {
            Util.Util.destroyRecursive(holder.transform);
            starNodes = new Dictionary<int, List<ProtoStar>>();
        }
    }
}