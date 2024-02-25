using UnityEngine;

public class PlayerStateContext
{
    public PlayerStateContext(PlayerController controller)
    {
        playerController = controller;
    }
    public IPlayerState CurrentState { get;set;}
    private PlayerController playerController;


    public void Transition(IPlayerState state)
    {
        CurrentState = state;
        CurrentState.Handle(playerController);
    }
}
