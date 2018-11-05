using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Objects;
public class StartGame : MonoBehaviour {

    public GalaxyCreators.galaxyCreator creator;
    public GameManager gameManager;

    public void StartGameA()
    {
        Debug.Log("StartGame Button handler called, calling gameManager start");
        gameManager.startgame(creator.starNodes);
    }
}
