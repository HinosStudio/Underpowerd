# Condition Reaction Mechanics

- Condition
- Condition Collection
- Reaction
- Reaction Collection

```C#

public class Condition : ScriptableObject {

}

public class ConditionCollection : ScriptableObject {
    public string description;
    public Condition[] requiredConditions;
    public ReactionCollection reactionCollection;

    public bool CheckAndReact() {
        for(int i = 0; i < requiredConditions.Length; ++i){
            if(!AllConditions.CheckCondition(requiredConditions[i]))
                return false;
        }

        if(reactionCollection) {
            reactionCollection.React();
        }

        return true;
    }
}

public class Reaction {

}

public class ReactionCollection {

}

public class AllConditions : ScriptableObject {
    public Condition[] conditions;

    private static AllConditions instance;

    private const string loadPath = "AllConditions";

    public static AllConditions Instance {
        get {
            if(!instance)
                instance = FindObjectOfType<AllConditions>();
            if(!instace)
                instance = Resources.load<AllConditions>(loadPath);
            if(!instance)
                Debug.LogError();
            return instance;
        }
        set { instance = value; }
    }

    public void Reset() {
        if(conditions == null)
            return;
        
        for(int i = 0; i < conditions.Length; ++i){
            conditions[i].satisfied = false;
        }
    }

    public static bool CheckCondition(Condition requiredCondition) {
        Condition[] allCondition = Instance.conditions;
        Condition globalCondition = null;

        if(allConditions != null && allConditions[0] != null) {
            for(int i = 0; i < allConditions.Length; ++i) {
                if(allConditions[i].hash == requiredCondition.hash){
                    globalCondition = allConditions[i];
                }
            }
        }

        if(!globalCondition) {
            return false;
        }

        return globalCondition.satisfied == requiredCondition.satisfied;
    }
}

```