using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
public class StartGame : MonoBehaviour {

    public GalaxyCreators.galaxyCreator creator;
    public GameManager gameManager;

    public void StartGameA()
    {

        gameManager.startgame(creator.starNodes);
    }
}
