using Game.Core.Entities;
using Game.Core.Util;
using System.Collections.Generic;
namespace Game.Core.State
{
    public static class buildingNames
    {
        public static string[] names = new string[] { "buildinger", "anotherBuil", "Foo", "Bar" };
    }

    public class BuildingState : TerrestrialState
    {
        public BuildingState() { }
        private List<Reference<Pop>> _pops = new List<Reference<Pop>>();
        public List<Reference<Pop>> pops { get { return _pops; } set { _pops = value; } }
    }

}