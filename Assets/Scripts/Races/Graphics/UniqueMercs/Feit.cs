﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Feit : BlankSlate
{
    const float BellyScale = 1.8f;
    public Feit()
    {
        CanBeGender = new List<Gender>() { Gender.Female };
		GentleAnimation = true;
        Body = new SpriteExtraInfo(3, BodySprite, WhiteColored);
		Head = new SpriteExtraInfo(4, HeadSprite, WhiteColored);
        BodyAccent = new SpriteExtraInfo(1, BodyAccentSprite, WhiteColored);
        Belly = new SpriteExtraInfo(2, null, WhiteColored);
    }

    internal override void RandomCustom(Unit unit)
    {
        base.RandomCustom(unit);
        unit.Name = "Feit";
    }


    protected override Sprite HeadSprite(Actor_Unit actor) // Head
    {
        AddOffset(Head, 0, 20 * .85f);
		if ((actor.GetStomachSize(38) <= 11) && (actor.IsOralVoring || actor.IsAttacking)) return State.GameManager.SpriteDictionary.Feit[4];
		else if ((actor.GetStomachSize(38) >= 12) && (actor.IsOralVoring || actor.IsAttacking)) return State.GameManager.SpriteDictionary.Feit[6];
		else if (actor.GetStomachSize(38) >= 12) return State.GameManager.SpriteDictionary.Feit[5];
        else return State.GameManager.SpriteDictionary.Feit[3];
    }

    protected override Sprite BodySprite(Actor_Unit actor) // Body
    {
        AddOffset(Body, 0, 20 * .85f);
		if (actor.GetStomachSize(38) >= 12) return State.GameManager.SpriteDictionary.Feit[2];
		else if (actor.GetStomachSize(38) >= 1) return State.GameManager.SpriteDictionary.Feit[1];
        else return State.GameManager.SpriteDictionary.Feit[0];
    }

    protected override Sprite BodyAccentSprite(Actor_Unit actor) // Left Side
    {
        AddOffset(BodyAccent, 0, 20 * .85f);
		if (actor.GetStomachSize(38) >= 12) return State.GameManager.SpriteDictionary.Feit[8];
        else return State.GameManager.SpriteDictionary.Feit[7];
    }


    internal override Sprite BellySprite(Actor_Unit actor, GameObject belly)
    {
        AddOffset(Belly, 0, 20 * .85f);
        if (!actor.HasBelly)
            return null;

        int size = actor.GetStomachSize(38);

        if ( size >= 40 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, true) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[35];
        }

        if (size >= 37 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[34];
        }

        if (size >= 34 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[33];
        }

        if (size >= 31 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[32];
        }

        if (size >= 28 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[31];
        }

        if (size >= 25 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[30];
        }

        if (size >= 22 && (actor.PredatorComponent?.IsUnitOfSpecificationInPrey(Race.Selicia, false) ?? false))
        {
            return State.GameManager.SpriteDictionary.Feit[29];
        }

        if (size > 19) size = 19;
        return State.GameManager.SpriteDictionary.Feit[9 + size];
    }
}

