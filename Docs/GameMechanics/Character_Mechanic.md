# Character

A character is an entity in the game. That has stats, 

## Attributes

- capacity, the max charge limit of the battery
- conductivity, charge transfer speed
- consumption,

---

## Flags

- A character can die.
- A character can be invincible.

---

- Characters consume power
-

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

enum RarityType {
    NONE, COMMON, UNCOMMON, RARE, LEGENDARY, UNIQUE
}

class ModChip {
    private RarityType rarity;
}

class Loadout : ScriptableObject {
    private string name;
    private string description;

    // Battery
    private float batteryCapacity;
    private float powerConsumption;
    private float maxOvercharge;
    private bool overloadImmunity;
    
    // Mobility
    private float agility;

    // Status Conditions
    private float burnThreshold;
    private float burnResistance;
    private float burnRecovery;
    private bool burnImmunity;

    private float freezeThreshold;
    private float freezeResistance;
    private float freezeRecovery;
    private bool freezeImmunity;

    private float stunThreshold;
    private float stunResistance;
    private float stunRecovery;
    private bool stunImmunity;

    // Equipment
    private Weapon weapon;
    private object[] traits;
}

class Character : Monobehaviour, IFlammable, IFreezable, IStunnable, IDestroyable {
    [SerializeField] private Battery battery;

    private Stat batteryCapacity;   // Health
    private Stat powerConsumption;  // Decay
    private Stat agility;           // Speed

    private bool dead;
    private bool invincible;
    
    event onDeath;

    private void Awake(){
        Initialize();
    }

    private void Initialize(){
        _battery.capacity = capacity.value;
        _battery.charge = capacity.value * Settings.initialChargePercent;
        dead = false;
    }

    public void Destroy(string message) {
        if(!dead){
            dead = true;
            onDeath.invoke(message);
        }
    }
}

[RequireComponent(typeof(Character), typeof(battery))]
class PowerConsumingCharacter : Monobehaviour {
    private Character _character;
    private Battery _battery;

    private void Awake(){
        _character = GetComponent<Character>();
        _battery = GetComponent<Battery>();
    }

    private void Update(){
        _battery.RemoveCharge(_character.powerConsumption);
        if(_battery.IsDepleted) {
            _character.Destroy("Battery depleted");
        }
    }
}



```