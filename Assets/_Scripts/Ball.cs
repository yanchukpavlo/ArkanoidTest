using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    static byte ballAmount = 0;

    private void Start()
    {
        ++ballAmount;
        SetMoveDirection(new Vector2(Random.Range(-0.5f, 0.5f), 1));
    }

    protected override void ZoneInteract()
    {
        base.ZoneInteract();
        
        --ballAmount;
        if(ballAmount == 0) EventsManager.ChangeGameState(GameState.BallLose);
    }
}
