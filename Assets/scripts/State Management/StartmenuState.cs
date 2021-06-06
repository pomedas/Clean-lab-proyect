using System.Collections;
using UnityEngine;

public class StartmenuState : State
{
    // public Canvas startMenuCanvas
    // public Button startButton
    AudioManager audioManager;
    public FollowPointer redBoat;
    public FollowPointer blueBoat;

    protected override void Awake()
    {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        // startButton.OnSelect.AddListener(StartGame);
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

        // AUDIO start ambient sounds
        audioManager.Play(AudioManager.SoundName.MainMusic);
    }

    public override void BeforeDeactivate()
    {
        // hide/disable start menu canvas
        // hide/disable start menu buttons
    }

    public void StartGame()
    {
        // TODO pass as callback to start buttons
        stateMachine.GoTo<FishingState>();
    }
}
