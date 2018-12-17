using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
using Objects.Galaxy;
using GalaxyCreators;
public class StartGame : MonoBehaviour {

    public ProtoGalaxyCreator creator;
    public GameManager gameManager;

    public void startGame()
    {
        gameManager.startgame(creator.starNodes);
    }
    public void startGame(SavedGame savedGame){
        gameManager.startgame(savedGame);
    }
}
