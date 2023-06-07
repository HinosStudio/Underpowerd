# Base Station Mechanics

Base stations work as save points for the player.
Base stations recharge the battery of the player.

Interacting with the base station will recharge the battery of the player and reset the dungeon, a menu will open allowing the player to save the game or travel to other unlocked base stations.

```C#

public class BaseStation : MonoBehaviour, IInteractionHandler
{
    
    public void HandleInteraction() {
        
    }
}

```