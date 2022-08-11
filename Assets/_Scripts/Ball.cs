using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Projectile
{
    static byte ballAmount = 0;
    
    public static void ResetBallAmount() => ballAmount = 0;

    private IEnumerator Start()
    {
        ++ballAmount;
        yield return new WaitForSeconds(1f);
        SetMoveDirection(new Vector2(Random.Range(-0.5f, 0.5f), 1));
    }

    protected override void ZoneInteract()
    {
        --ballAmount;
        if (ballAmount == 0) EventsManager.ChangeGameState(GameState.BallLose);
        base.ZoneInteract();
    }
}
