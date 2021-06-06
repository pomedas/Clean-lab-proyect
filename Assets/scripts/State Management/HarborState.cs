﻿using System.Collections;
using UnityEngine;

public class HarborState : State
{
    // Canvas to show the points
    // Power up menu
    // AudioManager audioManager;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void AfterActivate()
    {
        // AUDIO start harbor ambient sound
        // AUDIO start lounge music?

        // enable canvas
        // show "selling" of fish, counting of points
        // enable power up menu buttons
    }

    public override void BeforeDeactivate()
    {
        // disable power up buttons
        // disable canvas

        // AUDIO stop loung music
        // AUDIO stop harbor ambient sound
    }

    public void StartNextLevel()
    {
        // TODO call when powerup was selected
        stateMachine.GoTo<FishingState>();
    }
}
