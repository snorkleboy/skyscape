using Game.Core.Entities;

namespace Game.Application
{
    using System.Collections.Generic;

    public class SaveGameModel
        {
            public SaveGameModel() { }
            public SaveGameModel(GameManager gm)
            {
                //idMaker = GameManager.idMaker.count;
                //starNodes = gm._starNodes.starNodeRef;
                //objectTable = gm.objectTable.objects.toStateTable();
                //factions = gm.factions.factions.Values.referenceAll();
            }
            public long idMaker;
            public Dictionary<long, object> objectTable;
            //public List<Reference<Faction>> factions;
            //public Dictionary<int, List<Reference<StarNode>>> starNodes;
        }


}
