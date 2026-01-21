using System.Collections.Generic;
using UnityEngine;

class Pudding : BlankSlate
{
    internal Pudding()
    {
        SpecialAccessoryCount = 3; // Top Type
        BodyAccentTypes1 = 4; // Cream
        BodyAccentTypes2 = 6; // Ears
        BodyAccentTypes3 = 8; // Topping
        MouthTypes = 6;
        EyeTypes = 8;

        CanBeGender = new List<Gender>() { Gender.None };
        SkinColors = ColorPaletteMap.GetPaletteCount(ColorPaletteMap.SwapType.SlimeMain);
        GentleAnimation = true;

        BodyAccessory = new SpriteExtraInfo(5, AccessorySprite, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor)); // Ears
        BodyAccent = new SpriteExtraInfo(5, BodyAccentSprite, null, null); // Top
        BodyAccent2 = new SpriteExtraInfo(9, BodyAccentSprite2, WhiteColored); // Topping
        BodyAccent3 = new SpriteExtraInfo(6, BodyAccentSprite3, WhiteColored); // Cream
        BodyAccent4 = new SpriteExtraInfo(10, BodyAccentSprite4, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor)); // Mouth2
        Belly = new SpriteExtraInfo(2, null, null, (s) => ColorPaletteMap.GetPalette(ColorPaletteMap.SwapType.SlimeMain, s.Unit.SkinColor));
        Mouth = new SpriteExtraInfo(10, MouthSprite, WhiteColored);
        Eyes = new SpriteExtraInfo(21, EyesSprite, WhiteColored);

    }

    internal override void SetBaseOffsets(Actor_Unit actor)
    {
 
    }

    internal override void RandomCustom(Unit unit)
    {
        unit.SkinColor = State.Rand.Next(SkinColors);
    }
    //protected override Sprite BodySprite(Actor_Unit actor) => State.GameManager.SpriteDictionary.FeralSlime[0];
    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        if (actor.HasBelly == false)
            return State.GameManager.SpriteDictionary.Pudding[0];

        return State.GameManager.SpriteDictionary.Pudding[0 + actor.GetStomachSize(25)];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor)
    {
        if (actor.Unit.SpecialAccessoryType >= 2)
        {
            return State.GameManager.SpriteDictionary.Pudding[27];
        }
        return null;
    }
    protected override Sprite AccessorySprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Pudding[0 + actor.Unit.BodyAccentType2];
    }
    protected override Sprite BodyAccentSprite2(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 4 && actor.Unit.BodyAccentType3 >= 4)
        {
            return State.GameManager.SpriteDictionary.Pudding[52 + actor.Unit.BodyAccentType3 - 4];
        }
        return State.GameManager.SpriteDictionary.Pudding[56 + actor.Unit.BodyAccentType3];
    }
    protected override Sprite BodyAccentSprite3(Actor_Unit actor)
    {
        if (actor.Unit.BodyAccentType1 == 4 || actor.Unit.SpecialAccessoryType >= 2)
        {
            return null;
        }
        return State.GameManager.SpriteDictionary.Pudding[48 + actor.Unit.BodyAccentType1];

    }
    protected override Sprite BodyAccentSprite4(Actor_Unit actor)
    {
        if (actor.IsEating)
        {
            return State.GameManager.SpriteDictionary.Pudding[26];
        }
        return null;

    }
    protected override Sprite EyesSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Pudding[28 + actor.Unit.EyeType];
    }
    protected override Sprite MouthSprite(Actor_Unit actor)
    {
        return State.GameManager.SpriteDictionary.Pudding[36 + actor.Unit.MouthType];
    }
}

