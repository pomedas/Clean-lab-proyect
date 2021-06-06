using System.Collections;
using UnityEngine;

public class FishingState : State
{
    // AudioManager audioManager;
    // FishManager fishManager;

    protected override void Awake()
    {
        base.Awake();
        // TODO ideally count points/fish/trash here 
        // -> net can tell FishingState to count, FishManager can get infomraiton from State
        // or better to have a point counting script (score) and connect that with the net?
    }

    public override void AfterActivate()
    {
        // AUDIO start game ambient sound
        // AUDIO enable boat movement sound
        // AUDIO start music?

        // activate boat follow player
        // update fish/trash population variables ?
        // spawn fishes ?
    }

    public override void BeforeDeactivate()
    {
        // deactivate boat follow player
        // AUDIO stop boat sounds
        // AUDIO stop game ambient sounds
        // AUDIO stop game music ?
    }

    public void EndLevel()
    {
        // TODO call when both ships arrive at harbor
        stateMachine.GoTo<HarborState>();
    }
}
