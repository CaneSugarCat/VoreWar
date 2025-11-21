using UnityEngine;

class Bunnies : DefaultRaceData
{
    public Bunnies()
    {
        BasicMeleeWeaponTypes = 2;
        FurCapable = true;
        BaseBody = true;
    }

    protected override Sprite AccessorySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.Bodies[13];
}
