using System.Collections;
using UnityEngine;

public class StartmenuState : State
{
    // Start Menu Cancas
    // Buttons to start game
    // AudioManager audioManager;
    public FollowPointer redBoat;
    public FollowPointer blueBoat;

    protected override void Awake()
    {
        base.Awake();
        // audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        // deactivate boat movement
        redBoat.GetComponent<FollowPointer>().enabled = false;
        redBoat.GetComponent<Rigidbody>().isKinematic = true;
        blueBoat.GetComponent<FollowPointer>().enabled = false;
        blueBoat.GetComponent<Rigidbody>().isKinematic = true;

        // show start text canvas (credits, explanation)
        // show start button element

        // AUDIO start ambient sound
        // AUDIO start music?
    }

    public override void BeforeDeactivate()
    {
        // disable start menu canvas
        // disable start menu buttons
    }

    public void StartGame()
    {
        // TODO pass as callback to start buttons
        stateMachine.GoTo<FishingState>();
    }
}
