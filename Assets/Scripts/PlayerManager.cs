using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        var p1 = PlayerInput.Instantiate(player1, playerIndex: 0, controlScheme: "KeyboardLeft", 0, pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(player1, playerIndex: 0, controlScheme: "KeyboardRight", 0, pairWithDevice: Keyboard.current);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
