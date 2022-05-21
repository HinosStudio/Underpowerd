# Character

## Attributes

- capacity, the max charge limit of the battery
- conductivity, charge transfer speed
- consumption,

---

## Flags

- A character can die.
- A character can be invincible.

---

- A character has a battery.
- The max-charge of the battery is influenced by the capacity stat.
- If the battery is depleted the character will die.

- A character continuously consumes power.

- A character can move.
- The movement-speed of the character is influenced by the agility stat.
- The character will move into the direction of the input.
- Moving will increase power consumption
- A character cannot move, when he is dead.

- A character has a weapon.
- A character can attack with a weapon.
- The character will attack into the direction of the input.
- Attacking with a weapon will increase power consumption.

- A character can take damage.
- When a character takes damage, he will lose an amount of charge equal to the damage.

- A character can be hit by an attack.
- When a character is hit, he will take damage.
- When a character is hit, he will become invincible for a short period of time.
- When a character is hit, it stops power consumption for a short period of time.
- A character cannot be hit, when he is invincible.

```C#

class Character {
    private Stat capacity;
    private Stat consumption;
    private Stat agility;

    private bool dead;
    private bool invincible;

    private Battery battery;
    private Weapon weapon;
    
    event onDeath;

    public void Move(Vector2 input) { }

    public void Die() { }

    public void Attack(Vector2 input) {}

    public void TakeHit(float damage) {}

    public void TakeDamage(float amount) {}
}

```