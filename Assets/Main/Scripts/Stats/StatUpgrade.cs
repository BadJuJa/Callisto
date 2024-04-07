using BadJuja.CharacterStats;
using UnityEngine;

public class StatUpgrade : MonoBehaviour
{
    public AllCharacterStats Stat;
    public StatModType ModType;
    public float Strenght;

    public virtual void Apply(CharacterCore c)
    {
        c.Stats[Stat].AddModifier(new StatModifier(Strenght, ModType, this));
        var t = c.Stats[Stat].Value;
    }
    public virtual void Remove(CharacterCore c)
    {
        c.Stats[Stat].RemoveAllModifiersFromSource(this);
        var t = c.Stats[Stat].Value;
    }

}
