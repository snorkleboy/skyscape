using UnityEngine;
using UnityEngine.UI;
using Objects.Galaxy;
namespace GalaxyCreators
{
    public class GalaxyMenuListener : MonoBehaviour
    {
        public Slider branchNumslider;
        public Slider branchSizeSlider;
        public Slider centralNoZoneSlider;
        public Slider perStarAngleSlider;
        public Slider starDistenceSlider;
        public galaxyCreator<ProtoStar> creator;
    }
}

