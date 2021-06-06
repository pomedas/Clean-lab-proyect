using System.Collections;
using UnityEngine;

public class StartmenuState : State
{
    // Start Menu Cancas
    // Buttons to start game
    // AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
        // audioManager = FindObjectOfType<AudioManager>();
    }

    public override void AfterActivate()
    {
        // AUDIO start ambient sound
        // AUDIO start music?

        // deactivate boat follow player
        // show start text canvas (credits, explanation)
        // show start button element
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
