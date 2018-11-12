using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using util;
namespace Objects.Galaxy
{
    public class StarNodeCollection
    {
        public Dictionary<int, List<StarNode>> _starNodes;

        public StarNodeCollection(Dictionary<int, List<StarNode>> starNodes){
            _starNodes = starNodes;
        }
        public void render(int scene)
        {
            foreach (var arr in _starNodes.Values)
            {
                foreach (var star in arr)
                {
                    star.render(scene);
                }
            }
        }
        public void destroy()
        {

            foreach (var keyVal in _starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.renderHelper.destroy();
                }
            }
        }

        public void deactive()
        {
            foreach (var keyVal in _starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.renderHelper.disable();
                }
            }
        }
        public void active()
        {
            foreach (var keyVal in _starNodes)
            {
                foreach (var star in keyVal.Value)
                {
                    star.renderHelper.enable();
                }
            }
        }
    }
}
