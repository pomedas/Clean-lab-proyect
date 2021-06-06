using System.Collections;
using UnityEngine;

public class FishingState : State
{
    // AudioManager audioManager;
    // FishManager fishManager;
    public Harbor harbor;

    protected override void Awake()
    {
        base.Awake();
        // TODO ideally count points/fish/trash here 
        // -> net can tell FishingState to count, FishManager can get infomraiton from State
        // or better to have a point counting script (score) and connect that with the net?
    }

    public override void AfterActivate()
    {
        harbor.OnBoatsInHarbor.AddListener(EndLevel);
        // activate boat follow player
        // update fish/trash population variables ?
        // spawn fishes ?

        // AUDIO start game ambient sound
        // AUDIO enable boat movement sound
        // AUDIO start music?
    }

    public override void BeforeDeactivate()
    {
        harbor.OnBoatsInHarbor.RemoveListener(EndLevel);
        // deactivate boat follow player

        // AUDIO stop boat sounds
        // AUDIO stop game ambient sounds
        // AUDIO stop game music ?
    }

    public void EndLevel()
    {
        stateMachine.GoTo<HarborState>();
    }
}
