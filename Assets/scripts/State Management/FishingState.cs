using System.Collections;
using UnityEngine;

public class FishingState : State
{
    AudioManager audioManager;
    // FishManager fishManager;
    public Harbor harbor;
    public FollowPointer redBoat;
    public FollowPointer blueBoat;

    protected override void Awake()
    {
        base.Awake();
        audioManager = FindObjectOfType<AudioManager>();
        // TODO ideally count points/fish/trash here 
        // -> the Net can tell FishingState to count, FishManager can get information from State
        // -> or better to have a point counting script (score) and connect that with the net?
    }

    public override void AfterActivate()
    {
        harbor.OnBoatsInHarbor.AddListener(EndLevel);

        // activate boat movement
        redBoat.GetComponent<FollowPointer>().enabled = true;
        redBoat.GetComponent<Rigidbody>().isKinematic = false;
        blueBoat.GetComponent<FollowPointer>().enabled = true;
        blueBoat.GetComponent<Rigidbody>().isKinematic = false;

        // update fish/trash population variables ?
        // spawn fishes ?

        // AUDIO start game ambient sound
        // AUDIO enable boat movement sound
        audioManager.PlayOnce(AudioManager.SoundName.MainMusic);

    }

    public override void BeforeDeactivate()
    {
        harbor.OnBoatsInHarbor.RemoveListener(EndLevel);

        // deactivate boat movement
        redBoat.GetComponent<FollowPointer>().enabled = false;
        redBoat.GetComponent<Rigidbody>().isKinematic = true;
        blueBoat.GetComponent<FollowPointer>().enabled = false;
        blueBoat.GetComponent<Rigidbody>().isKinematic = true;


        // AUDIO stop boat sounds
        // AUDIO stop game ambient sounds
        audioManager.Stop(AudioManager.SoundName.MainMusic);

    }

    public void EndLevel()
    {
        stateMachine.GoTo<HarborState>();
    }
}
