using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Loaders;
using Objects.Conceptuals;
using Objects.Galaxy.State;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.Serialization;
using Newtonsoft.Json;
namespace Objects.Galaxy {
    [System.Serializable]
    public class StarNodeState : GalaxyGameObjectState {
        public StarNodeState(){}
        [DataMember]public StarAsContainerState asContainerState;
        public StarNodeState(StarAsContainerState asContainerState,Sprite icon, long id, FactoryStamp stamp, NamedState namedState, AppearableState positionState,FactionOwnedState factionOwned, StateActionState actionState) : base(icon, id, stamp, namedState, positionState,factionOwned, actionState)
        {
            this.asContainerState = asContainerState;
        }
    }

    public partial class StarNode : GalaxyGameObject<StarNodeState> {

        public StarNodeState stateForDebug;

        public void Init (LinkedAppearer renderer, StarNodeState state) {
            this.state = state;
            stateForDebug = state;
            this.appearer = renderer;
            this.enterable = new EnterableStar (state.asContainerState);
            this.gameObject.name = state.namedState.name;
        }
        public override IAppearer appearer { get; set; }
        public EnterableStar enterable;
    }
    public partial class StarNode {
        public GameObject renderIcon () {
            var star = new GameObject ("StarIcon");
            var image = star.AddComponent<Image> ();
            image.sprite = state.icon;
            return star;
        }
        public override IconInfo getIconableInfo () {
            var info = new IconInfo ();
            info.source = this;
            info.name = name;
            info.icon = state.icon;
            var details = new IconInfo[2];
            var detail = new IconInfo ();
            detail.name = state.asContainerState.planets.Count.ToString ();
            var bundle = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites];
            var asset = bundle.LoadAsset<Sprite> ("43");
            detail.icon = asset;
            details[0] = detail;

            var otherDetail = new IconInfo ();
            var popNum = 0;
            foreach (var planet in state.asContainerState.planets) {
                foreach (var tileR in planet.value.tileable.state.tiles) {
                    var tile = tileR;
                    if (tile.value.state.building != null) {
                        if (tile.value.state.building.value.state.pops != null) {
                            popNum += tile.value.state.building.value.state.pops.Count;
                        }
                    }
                }
            }
            otherDetail.name = popNum.ToString ();
            otherDetail.icon = AssetSingleton.bundles[AssetSingleton.bundleNames.sprites].LoadAsset<Sprite> ("69");
            details[1] = otherDetail;

            info.details = details;
            return info;
        }
    }
}