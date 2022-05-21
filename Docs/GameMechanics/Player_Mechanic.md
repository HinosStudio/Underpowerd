# Player mechanic

- The player has lives.
- If the player dies, he will respawn and lose a life.
- If the player dies, and he has no lives remaining, it is game over.

- The player has a character

```C#

class Player {
    private int lives;

    private Character character;

    event outOfLives;

    public Player() { }

    public void AddLive(int amount = 1) { }

    public void RemoveLive(int amount = 1) { }
}

```