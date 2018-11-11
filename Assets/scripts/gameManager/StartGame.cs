using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using Objects.Galaxy;
using GalaxyCreators;
public class StartGame : MonoBehaviour {

    public ProtoGalaxyCreator creator;
    public GameManager gameManager;

    public async void startGame()
    {
        Debug.Log("StartGame Button handler called, calling gameManager start");
        await gameManager.startgame(creator.starNodes);
    }
}
