using System;
using UnityEngine.UI;
using UnityEngine;

public class UnitCustomizerRestrictions
{
    protected Actor_Unit actor;

    internal DefaultRaceData RaceData;

    protected RestrictedList[] listRow;

    protected CustomizerPanel CustomizerUI;

    public struct RestrictedCustomizations
    {

    }

    enum ButtonTypes
    {
        Skintone,
        HairColor,
        HairStyle,
        BeardStyle,
        BodyAccessoryColor,
        BodyAccessoryType,
        HeadType,
        EyeColor,
        EyeType,
        MouthType,
        BreastSize,
        CockSize,
        BodyWeight,
        ClothingType,
        Clothing2Type,
        ClothingExtraType1,
        ClothingExtraType2,
        ClothingExtraType3,
        ClothingExtraType4,
        ClothingExtraType5,
        HatType,
        ClothingAccessoryType,
        ClothingColor,
        ClothingColor2,
        ClothingColor3,
        ExtraColor1,
        ExtraColor2,
        ExtraColor3,
        ExtraColor4,
        Furry,
        TailTypes,
        FurTypes,
        EarTypes,
        BodyAccentTypes1,
        BodyAccentTypes2,
        BodyAccentTypes3,
        BodyAccentTypes4,
        BodyAccentTypes5,
        BallsSizes,
        VulvaTypes,
        AltWeaponTypes,
        LastIndex
    }

    public string HairColorLookup(int colorNumber)
    {
        switch (colorNumber)
        {
            case 0:
                return "Black";
            case 1:
                return "Cream";
            case 2:
                return "Orange";
            case 3:
                return "Blonde";
            case 4:
                return "Pink";
            case 5:
                return "Brown";
            case 6:
                return "Dark Gray";
            case 7:
                return "Yellow";
            case 8:
                return "Red";
            case 9:
                return "Maroon";
            case 10:
                return "Light Gray";
            case 11:
                return "Purple";
            case 12:
                return "Teal";
            case 13:
                return "Grape";
            case 14:
                return "Blue";
            case 15:
                return "Lime";
            case 16:
                return "Light Blue";
            case 17:
                return "Silver";
            case 18:
                return "Fire";
            case 19:
                return "Bubblegum";
            case 20:
                return "Bright Red";
            case 21:
                return "Tangerine";
            default:
                return colorNumber.ToString();
        }
    }

    public UnitCustomizerRestrictions(Unit unit, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetUnit(unit);
    }

    public UnitCustomizerRestrictions(Actor_Unit actor, CustomizerPanel UI)
    {
        CustomizerUI = UI;
        CreateButtons();
        SetActor(actor);
    }

    void SetUpNameChangeButton(Unit unit, CustomizerPanel UI)
    {
        var button = UI.DisplayedSprite.Name.GetComponent<Button>();
        if (button == null)
        {
            button = UI.DisplayedSprite.Name.gameObject.AddComponent<Button>();
            UI.DisplayedSprite.Name.gameObject.GetComponent<Text>().raycastTarget = true;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            var input = State.GameManager.CreateInputBox();
            input.SetData((s) =>
            {
                unit.Name = s;
                RefreshView();
            }, "Change", "Cancel", $"Modify name?", 100);
        });
    }

    public Unit Unit { get; private set; }

    public void SetUnit(Unit unit)
    {
        Vec2i noLoc = new Vec2i(0, 0);
        actor = new Actor_Unit(noLoc, unit);
        RaceData = Races.GetRace(unit);
        Unit = unit;
        Normal(unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }

    public void SetActor(Actor_Unit actor)
    {
        this.actor = actor;
        RaceData = Races.GetRace(actor.Unit);
        Unit = actor.Unit;
        Normal(actor.Unit);
        SetUpNameChangeButton(Unit, CustomizerUI);
        RefreshView();
    }


    protected void Normal(Unit unit)
    {
        for (int i = 0; i < listRow.Length; i++)
        {
            listRow[i].gameObject.SetActive(true);
            listRow[i].Label.text = listRow[i].defaultText;
        }

        listRow[(int)ButtonTypes.BodyAccessoryType].gameObject.SetActive(RaceData.SpecialAccessoryCount > 1);

        listRow[(int)ButtonTypes.BodyAccessoryColor].gameObject.SetActive(RaceData.AccessoryColors > 1);

        listRow[(int)ButtonTypes.ExtraColor1].gameObject.SetActive(false);
        listRow[(int)ButtonTypes.EyeType].gameObject.SetActive(RaceData.EyeTypes > 1);
        listRow[(int)ButtonTypes.Skintone].gameObject.SetActive(RaceData.SkinColors > 1);
        listRow[(int)ButtonTypes.EyeColor].gameObject.SetActive(RaceData.EyeColors > 1);
        listRow[(int)ButtonTypes.HairColor].gameObject.SetActive(RaceData.HairColors > 1);
        listRow[(int)ButtonTypes.HairStyle].gameObject.SetActive(RaceData.HairStyles > 1);
        listRow[(int)ButtonTypes.BeardStyle].gameObject.SetActive(RaceData.BeardStyles > 1);
        listRow[(int)ButtonTypes.BreastSize].gameObject.SetActive(unit.HasBreasts && RaceData.BreastSizes > 1);
        listRow[(int)ButtonTypes.CockSize].gameObject.SetActive(unit.HasDick && RaceData.DickSizes > 1);
        CustomizerUI.Gender.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Nominative.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Accusative.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.PronominalPossessive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.PredicativePossessive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Reflexive.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        CustomizerUI.Quantification.gameObject.SetActive(RaceData.CanBeGender.Count > 1);
        RefreshGenderDropdown(unit);
        RefreshPronouns(unit);
        listRow[(int)ButtonTypes.BodyWeight].gameObject.SetActive(RaceData.BodySizes > 0);
        listRow[(int)ButtonTypes.ClothingColor].gameObject.SetActive(RaceData.clothingColors > 1 && (RaceData.MainClothingTypesCount > 1 || RaceData.WaistClothingTypesCount > 1 || RaceData.ClothingHatTypesCount > 1));
        listRow[(int)ButtonTypes.ClothingType].gameObject.SetActive(RaceData.MainClothingTypesCount > 1);
        listRow[(int)ButtonTypes.Clothing2Type].gameObject.SetActive(RaceData.WaistClothingTypesCount > 1);
        listRow[(int)ButtonTypes.ClothingExtraType1].gameObject.SetActive(RaceData.ExtraMainClothing1Count > 1);
        listRow[(int)ButtonTypes.ClothingExtraType2].gameObject.SetActive(RaceData.ExtraMainClothing2Count > 1);
        listRow[(int)ButtonTypes.ClothingExtraType3].gameObject.SetActive(RaceData.ExtraMainClothing3Count > 1);
        listRow[(int)ButtonTypes.ClothingExtraType4].gameObject.SetActive(RaceData.ExtraMainClothing4Count > 1);
        listRow[(int)ButtonTypes.ClothingExtraType5].gameObject.SetActive(RaceData.ExtraMainClothing5Count > 1);
        listRow[(int)ButtonTypes.HatType].gameObject.SetActive(RaceData.ClothingHatTypesCount > 1);
        listRow[(int)ButtonTypes.ClothingAccessoryType].gameObject.SetActive(RaceData.ClothingAccessoryTypesCount > 1 || (unit.EarnedMask && Unit.Race <= Race.Goblins && Unit.Race != Race.Lizards));
        listRow[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(RaceData == Races.SlimeQueen || RaceData == Races.Panthers || RaceData == Races.Imps || RaceData == Races.Goblins); //The additional clothing colors are slime queen only for the moment
        listRow[(int)ButtonTypes.ClothingColor3].gameObject.SetActive(RaceData == Races.SlimeQueen || RaceData == Races.Panthers);
        listRow[(int)ButtonTypes.MouthType].gameObject.SetActive(RaceData.MouthTypes > 1);

        listRow[(int)ButtonTypes.ExtraColor1].gameObject.SetActive(RaceData.ExtraColors1 > 0);
        listRow[(int)ButtonTypes.ExtraColor2].gameObject.SetActive(RaceData.ExtraColors2 > 0);
        listRow[(int)ButtonTypes.ExtraColor3].gameObject.SetActive(RaceData.ExtraColors3 > 0);
        listRow[(int)ButtonTypes.ExtraColor4].gameObject.SetActive(RaceData.ExtraColors4 > 0);


        listRow[(int)ButtonTypes.Furry].gameObject.SetActive(RaceData.FurCapable);
        listRow[(int)ButtonTypes.HeadType].gameObject.SetActive(RaceData.HeadTypes > 1);
        listRow[(int)ButtonTypes.TailTypes].gameObject.SetActive(RaceData.TailTypes > 1);
        listRow[(int)ButtonTypes.FurTypes].gameObject.SetActive(RaceData.FurTypes > 1);
        listRow[(int)ButtonTypes.EarTypes].gameObject.SetActive(RaceData.EarTypes > 1);
        listRow[(int)ButtonTypes.BodyAccentTypes1].gameObject.SetActive(RaceData.BodyAccentTypes1 > 1);
        listRow[(int)ButtonTypes.BodyAccentTypes2].gameObject.SetActive(RaceData.BodyAccentTypes2 > 1);
        listRow[(int)ButtonTypes.BodyAccentTypes3].gameObject.SetActive(RaceData.BodyAccentTypes3 > 1);
        listRow[(int)ButtonTypes.BodyAccentTypes4].gameObject.SetActive(RaceData.BodyAccentTypes4 > 1);
        listRow[(int)ButtonTypes.BodyAccentTypes5].gameObject.SetActive(RaceData.BodyAccentTypes5 > 1);
        listRow[(int)ButtonTypes.BallsSizes].gameObject.SetActive(RaceData.BallsSizes > 1);
        listRow[(int)ButtonTypes.VulvaTypes].gameObject.SetActive(RaceData.VulvaTypes > 1);
        listRow[(int)ButtonTypes.AltWeaponTypes].gameObject.SetActive(RaceData.BasicMeleeWeaponTypes > 1 || RaceData.BasicRangedWeaponTypes > 1 || RaceData.AdvancedMeleeWeaponTypes > 1 || RaceData.AdvancedRangedWeaponTypes > 1);

        switch (unit.Race)
        {
            case Race.Cats:
            case Race.Dogs:
            case Race.Foxes:
            case Race.Wolves:
            case Race.Bunnies:
            case Race.Tigers:
                listRow[(int)ButtonTypes.HairColor].Label.text = "Hair Color: " + HairColorLookup(Unit.HairColor);
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(Unit.AccessoryColor);
                listRow[(int)ButtonTypes.AltWeaponTypes].Label.text = "Mace Alt";
                break;
            case Race.Imps:
                Imp();
                break;
            case Race.Goblins:
                Goblin();
                break;
            case Race.Lizards:
                Lizard();
                break;
            case Race.Slimes:
                Slime();
                break;
            case Race.Crypters:
                listRow[(int)ButtonTypes.ClothingColor].gameObject.SetActive(false);
                break;
            case Race.Harpies:
                Harpy();
                break;
            case Race.Lamia:
                Lamia();
                break;
            case Race.Kangaroos:
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Ear Type";
                break;
            case Race.Taurus:
                listRow[(int)ButtonTypes.EyeType].Label.text = "Face Expression";
                break;
            case Race.Crux:
                Crux();
                break;
            case Race.Wyvern:
                listRow[(int)ButtonTypes.BodyWeight].Label.text = "Horn Type";
                break;
            case Race.Succubi:
                listRow[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(true);
                break;
            case Race.Collectors:
                listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Mouth / Dick Color";
                break;
            case Race.Asura:
                listRow[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Mask";
                break;
            case Race.Kobolds:
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Preferred Facing";
                break;
            case Race.Fairies:
                listRow[(int)ButtonTypes.HatType].Label.text = "Leg Accessory";
                listRow[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Arm Accessory";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Fairy Season";
                break;
            case Race.Equines:
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Overtop";
                listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Overbottom";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Skin Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Head Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Torso Color";
                break;
            case Race.Zera:
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Default Facing";
                break;
            case Race.Bees:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;
            case Race.Driders:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Spider Half Color";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Spider Accent Color";
                break;
            case Race.Alraune:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Hair Accessory";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Plant Colors";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Inner Petals";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Outer Petals";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Plant Base";
                break;
            case Race.Gryphons:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Style";
                break;
            case Race.Bats:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Collar Fur Type";
                break;
            case Race.Panthers:
                Panther();
                break;
            case Race.Salamanders:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Spine Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Spine Type";
                break;
            case Race.Vipers:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Hood Type";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Accent Color";
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Tail Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Accent Pattern";
                break;
            case Race.Merfolk:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Fin";
                listRow[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Necklace / Hair Ornament";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Scale Color";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Tail Fin";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Arm Fin";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Eyebrow";

                break;
            case Race.Avians:
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Head Type";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Beak Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Pattern";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Core Color";
                listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Feather Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Underwing Palettes";
                break;
            case Race.Hippos:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Accent Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.HatType].Label.text = "Headwear Type";
                listRow[(int)ButtonTypes.ClothingAccessoryType].Label.text = "Necklace Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Left Arm Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Right Arm Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Leg Pattern";

                break;
            case Race.Mantis:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Wing Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Back Spines";

                break;
            case Race.Auri:
                listRow[(int)ButtonTypes.ClothingType].Label.text = "Breast Wrap";
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Kimono";
                listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Socks";
                listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Hair Ornament";
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Tail Quantity";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Beast Mode";

                break;
            case Race.Catfish:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Barbel (Whisker) Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Dorsal Fin Type";

                break;
            case Race.Ants:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Exoskeleton Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;
            case Race.WarriorAnts:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Antennae Type";
                break;

            case Race.Frogs:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Primary Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Secondary Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Extra Colors for Females";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
                break;

            case Race.Gazelle:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Fur Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Horn Type (for males)";
                break;

            case Race.Sharks:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Nose Type";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Secondary Pattern Colors";
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Hats";
                break;

            case Race.Komodos:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Head Shape";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Secondary Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Head Pattern on/off";
                break;

            case Race.FeralLizards:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Visible Teeth (during attacks)";
                break;

            case Race.Cockatrice:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Feather Color";
                break;

            case Race.Monitors:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
                break;

            case Race.Deer:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Antlers Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Leg Type";
                break;

            case Race.Schiwardez:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                break;

            case Race.Terrorbird:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Plumage Type";
                break;

            case Race.Erin:
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Panties";
                listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Stockings";
                listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Shoes";
                break;

            case Race.Vargul:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Body Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Head Pattern Type";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Mask On/Off (for armors)";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Pattern Colors";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Armor Details Color";
                break;

            case Race.FeralLions:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Mane Style";
                listRow[(int)ButtonTypes.HairColor].Label.text = "Mane Color";
                break;
            case Race.FeralHorses:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Fur Color";
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Mane Style";
                listRow[(int)ButtonTypes.HairColor].Label.text = "Mane Color";
                break;
            case Race.Aabayx:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Head Color";
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Face Paint";
                listRow[(int)ButtonTypes.EyeColor].Label.text = "Face Paint Color";
                break;
            case Race.Mice:
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Face Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Chest Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Hands/Feet Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Left Ear Damage";
                listRow[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Right Ear Damage";
                break;
            case Race.FeralOrcas:
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Dorsal Fin";
                break;
            case Race.RwuMercenaries:
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Race Detail";
                listRow[(int)ButtonTypes.ClothingType].Label.text = "Bonus Accessory";
                listRow[(int)ButtonTypes.EyeType].Label.text = "Race";
                listRow[(int)ButtonTypes.MouthType].Label.text = "Helmet";
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Custom livery (for Sharks)";
                break;
            case Race.Olivia:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Top";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Pants";
                break;
            case Race.Firefly:
                listRow[(int)ButtonTypes.Skintone].Label.text = "Secondary Color";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Primary Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Outfit";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Shoulder Pad";
                break;
            case Race.Taraluxia:
                Taraluxia();
                break;
            case Race.ViraeUltimae:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Head Color";
                break;
            case Race.MainlandElves:
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Ear Accessory";
                break;
            case Race.Umbreon:
                listRow[(int)ButtonTypes.Furry].Label.text = "Handedness";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Ring Color";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Body Armor Metal";
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Body Armor(req. item)";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Decal";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Armor Rust";
                break;
            case Race.Equaleon:
            case Race.FeralEqualeon:
                listRow[(int)ButtonTypes.BodyAccentTypes2].gameObject.SetActive(false);
                listRow[(int)ButtonTypes.EyeColor].Label.text = "Right Eye Color";
                listRow[(int)ButtonTypes.EyeType].Label.text = "Left Eye Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Heterochromia On/Off";
                break;
            case Race.Viisels:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Faceplate Color";
                break;
            case Race.Lupine:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Iris Color";
                listRow[(int)ButtonTypes.EyeColor].Label.text = "Sclera Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Chest Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Arm Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Leg Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Head Pattern";
                listRow[(int)ButtonTypes.HairStyle].Label.text = "Cheek Fluff";
                listRow[(int)ButtonTypes.BodyAccessoryType].gameObject.SetActive(false);
                break;
            case Race.Tatltuae:
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Hat";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Glasses";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Outfit";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Shirt On/Off";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Pants On/Off";
                break;
            case Race.Skapa:
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Facing";
                break;
            case Race.Ryan:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Armor";
                break;
            case Race.Jackals:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color";
                listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Leg Ring";
                listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Arm Ring";
                listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Neck Accessory";
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Inner Ear Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Eyebrows";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Ear Piercing";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Navel Piercing";
                break;
            case Race.Smudger:
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Internal Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Head Frills";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Pattern Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Patterns";
                break;
            case Race.Utahraptor:
                listRow[(int)ButtonTypes.TailTypes].Label.text = "Preferred Facing";
                break;
            case Race.WoodDryad:
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Leaf Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Trunk Type";
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Wood Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Horn Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Leaves On/Off";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Eyebrows";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Trunk Addon";
                break;
            case Race.EarthDryad:
                listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Leaf Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Pattern";
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Horn Type";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Leaves On/Off";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Eyebrows";
                break;
            case Race.RiverDryad:
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Pattern";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Eyebrows";
                break;
            case Race.FungalDryad:
                listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Mushroom Color";
                listRow[(int)ButtonTypes.BodyAccessoryType].Label.text = "Hat Type";
                listRow[(int)ButtonTypes.Skintone].Label.text = "Body Color";
                listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Mushroom Damage";
                listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Spots On/Off";
                listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Spots Type";
                listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Lower Mushroom Type";
                break;
        }
    }

    private void RefreshGenderDropdown(Unit unit)
    {
        if (unit.HasBreasts)
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina || Config.HermsCanUB == false)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (unit.HasDick)
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        CustomizerUI.Gender.options[0].text = RaceData.CanBeGender.Contains(Gender.Male) ? "Male" : "--";
        CustomizerUI.Gender.options[1].text = RaceData.CanBeGender.Contains(Gender.Female) ? "Female" : "--";
        CustomizerUI.Gender.options[2].text = RaceData.CanBeGender.Contains(Gender.Hermaphrodite) ? "Hermaphrodite" : "--";
        CustomizerUI.Gender.options[3].text = RaceData.CanBeGender.Contains(Gender.Gynomorph) ? "Gynomorph" : "--";
        CustomizerUI.Gender.options[4].text = RaceData.CanBeGender.Contains(Gender.Maleherm) ? "Maleherm" : "--";
        CustomizerUI.Gender.options[5].text = RaceData.CanBeGender.Contains(Gender.Andromorph) ? "Andromorph" : "--";
        CustomizerUI.Gender.options[6].text = RaceData.CanBeGender.Contains(Gender.Agenic) ? "Agenic" : "--";

    }

    private void RefreshPronouns(Unit unit)
    {
        CustomizerUI.Nominative.text = unit.GetPronoun(0);
        CustomizerUI.Accusative.text = unit.Pronouns[1];
        CustomizerUI.PronominalPossessive.text = unit.Pronouns[2];
        CustomizerUI.PredicativePossessive.text = unit.Pronouns[3];
        CustomizerUI.Reflexive.text = unit.Pronouns[4];
        if (Unit.Pronouns[5] == "singular")
            CustomizerUI.Quantification.value = 0;
        else
            CustomizerUI.Quantification.value = 1;
    }

    void Panther()
    {
        listRow[(int)ButtonTypes.EyeType].Label.text = "Face Type";
        listRow[(int)ButtonTypes.ClothingColor].Label.text = "Innerwear Color";
        listRow[(int)ButtonTypes.ClothingColor2].Label.text = "Outerwear Color";
        listRow[(int)ButtonTypes.ClothingColor3].Label.text = "Clothing Accent Color";
        listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Arm Bodypaint";
        listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Shoulder Bodypaint";
        listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Feet Bodypaint";
        listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Thigh Bodypaint";
        listRow[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Face Bodypaint";
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Bodypaint Color";
        listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Tops";
        listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Bottoms";
        listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Hats";
        listRow[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        listRow[(int)ButtonTypes.ClothingExtraType5].Label.text = "Legs";
    }

    void Imp()
    {
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
        listRow[(int)ButtonTypes.ClothingType].Label.text = "Under Tops";
        listRow[(int)ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
        listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
        listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
        listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Legs";
        listRow[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        listRow[(int)ButtonTypes.ClothingExtraType5].Label.text = "Hats";
        listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Center Pattern";
        listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Outer Pattern";
        listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Horn Type";
        listRow[(int)ButtonTypes.BodyAccentTypes4].Label.text = "Special Type";
    }

    void Goblin()
    {
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Accent Color";
        listRow[(int)ButtonTypes.ClothingType].Label.text = "Under Tops";
        listRow[(int)ButtonTypes.Clothing2Type].Label.text = "Under Bottoms";
        listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Over Bottoms";
        listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Over Tops";
        listRow[(int)ButtonTypes.ClothingExtraType3].Label.text = "Legs";
        listRow[(int)ButtonTypes.ClothingExtraType4].Label.text = "Gloves";
        listRow[(int)ButtonTypes.ClothingExtraType5].Label.text = "Hats";
        listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Ear Type";
        listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Eyebrow Type";
    }

    void Lizard()
    {
        listRow[(int)ButtonTypes.Skintone].gameObject.SetActive(false);
        listRow[(int)ButtonTypes.HairColor].gameObject.SetActive(true);
        listRow[(int)ButtonTypes.HairColor].Label.text = "Horn Color";
        listRow[(int)ButtonTypes.HairStyle].Label.text = "Horn Style";
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
        listRow[(int)ButtonTypes.ClothingExtraType1].Label.text = "Leg Guards";
        listRow[(int)ButtonTypes.ClothingExtraType2].Label.text = "Armlets";
        listRow[(int)ButtonTypes.HatType].Label.text = "Crown";
    }

    void Slime()
    {
        listRow[(int)ButtonTypes.HairColor].gameObject.SetActive(true);
        listRow[(int)ButtonTypes.Skintone].gameObject.SetActive(false);
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Body Color";
        listRow[(int)ButtonTypes.HairColor].Label.text = "Secondary Color";
        if (Unit.Type == UnitType.Leader)
        {
            listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Breast Covering";
            listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Cock Covering";
        }

    }

    void Harpy()
    {
        listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Upper Feathers";
        listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Middle Feathers";
        listRow[(int)ButtonTypes.ExtraColor3].Label.text = "Lower Feathers";
        listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Lower Feather brightness";
    }

    void Crux()
    {
        listRow[(int)ButtonTypes.ClothingColor2].Label.text = "Pack / Boxer Color";
        listRow[(int)ButtonTypes.ClothingColor2].gameObject.SetActive(true);
        listRow[(int)ButtonTypes.BodyWeight].Label.text = "Body Type";
        listRow[(int)ButtonTypes.EyeType].Label.text = "Face Expression";
        listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Primary Color";
        listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Secondary Color";
        listRow[(int)ButtonTypes.ExtraColor3].Label.text = "Flesh Color";
        listRow[(int)ButtonTypes.ExtraColor4].gameObject.SetActive(Config.HideCocks == false && actor.Unit.GetBestMelee().Damage == 4);
        listRow[(int)ButtonTypes.ExtraColor4].Label.text = "Dildo Color";
        listRow[(int)ButtonTypes.FurTypes].Label.text = "Head Fluff";
        listRow[(int)ButtonTypes.BodyAccentTypes1].Label.text = "Visible Areola";
        listRow[(int)ButtonTypes.BodyAccentTypes2].Label.text = "Leg Stripes";
        listRow[(int)ButtonTypes.BodyAccentTypes3].Label.text = "Arm Stripes";
    }

    void Lamia()
    {
        listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Scale Color";
        listRow[(int)ButtonTypes.ExtraColor1].Label.text = "Accent Color";
        listRow[(int)ButtonTypes.ExtraColor2].Label.text = "Tail Pattern Color";
    }

    void Taraluxia()
    {
        listRow[(int)ButtonTypes.BodyAccentTypes5].Label.text = "Glowing Maw";
        listRow[(int)ButtonTypes.TailTypes].Label.text = "Facing Front";
    }


    internal void RefreshGenderSelector()
    {
        if (actor.Unit.HasBreasts)
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 2;
                else
                    CustomizerUI.Gender.value = 3;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 1;
                else
                    CustomizerUI.Gender.value = 6;
            }
        }
        else
        {
            if (actor.Unit.HasDick)
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 4;
                else
                    CustomizerUI.Gender.value = 0;
            }
            else
            {
                if (actor.Unit.HasVagina)
                    CustomizerUI.Gender.value = 5;
                else
                    // What in the hell--
                    CustomizerUI.Gender.value = 0;
            }
        }
        if (RaceData.CanBeGender.Count <= 2)
        {

        }
    }

    internal void RefreshView()
    {
        actor.UpdateBestWeapons();
        CustomizerUI.DisplayedSprite.UpdateSprites(actor);
        CustomizerUI.DisplayedSprite.Name.text = Unit.Name;
    }

    void CreateButtons()
    {
        listRow = new RestrictedList[(int)ButtonTypes.LastIndex];
        listRow[(int)ButtonTypes.Skintone] = CreateNewRow("Skintone", ChangeSkinTone);
        listRow[(int)ButtonTypes.HairColor] = CreateNewRow("Hair Color", ChangeHairColor);
        listRow[(int)ButtonTypes.HairStyle] = CreateNewRow("Hair Style", ChangeHairStyle);
        listRow[(int)ButtonTypes.BeardStyle] = CreateNewRow("Beard Style", ChangeBeardStyle);
        listRow[(int)ButtonTypes.BodyAccessoryColor] = CreateNewRow("Body Accessory Color", ChangeBodyAccessoryColor);
        listRow[(int)ButtonTypes.BodyAccessoryType] = CreateNewRow("Body Accessory Type", ChangeBodyAccessoryType);
        listRow[(int)ButtonTypes.HeadType] = CreateNewRow("Head Type", ChangeHeadType);
        listRow[(int)ButtonTypes.EyeColor] = CreateNewRow("Eye Color", ChangeEyeColor);
        listRow[(int)ButtonTypes.EyeType] = CreateNewRow("Eye Type", ChangeEyeType);
        listRow[(int)ButtonTypes.MouthType] = CreateNewRow("Mouth Type", ChangeMouthType);
        listRow[(int)ButtonTypes.BreastSize] = CreateNewRow("Breast Size", ChangeBreastSize);
        listRow[(int)ButtonTypes.CockSize] = CreateNewRow("Cock Size", ChangeDickSize);
        listRow[(int)ButtonTypes.BodyWeight] = CreateNewRow("Body Weight", ChangeBodyWeight);
        listRow[(int)ButtonTypes.ClothingType] = CreateNewRow("Main Clothing Type", ChangeClothingType);
        listRow[(int)ButtonTypes.Clothing2Type] = CreateNewRow("Waist Clothing Type", ChangeClothing2Type);
        listRow[(int)ButtonTypes.ClothingExtraType1] = CreateNewRow("Extra Clothing Type 1", ChangeExtraClothing1Type);
        listRow[(int)ButtonTypes.ClothingExtraType2] = CreateNewRow("Extra Clothing Type 2", ChangeExtraClothing2Type);
        listRow[(int)ButtonTypes.ClothingExtraType3] = CreateNewRow("Extra Clothing Type 3", ChangeExtraClothing3Type);
        listRow[(int)ButtonTypes.ClothingExtraType4] = CreateNewRow("Extra Clothing Type 4", ChangeExtraClothing4Type);
        listRow[(int)ButtonTypes.ClothingExtraType5] = CreateNewRow("Extra Clothing Type 5", ChangeExtraClothing5Type);
        listRow[(int)ButtonTypes.HatType] = CreateNewRow("Hat Type", ChangeClothingHatType);
        listRow[(int)ButtonTypes.ClothingAccessoryType] = CreateNewRow("Clothing Accessory Type", ChangeClothingAccesoryType);
        listRow[(int)ButtonTypes.ClothingColor] = CreateNewRow("Clothing Color", ChangeClothingColor);
        listRow[(int)ButtonTypes.ClothingColor2] = CreateNewRow("Clothing Color 2", ChangeClothingColor2);
        listRow[(int)ButtonTypes.ClothingColor3] = CreateNewRow("Clothing Color 3", ChangeClothingColor3);
        listRow[(int)ButtonTypes.ExtraColor1] = CreateNewRow("Extra Color 1", ChangeExtraColor1);
        listRow[(int)ButtonTypes.ExtraColor2] = CreateNewRow("Extra Color 2", ChangeExtraColor2);
        listRow[(int)ButtonTypes.ExtraColor3] = CreateNewRow("Extra Color 3", ChangeExtraColor3);
        listRow[(int)ButtonTypes.ExtraColor4] = CreateNewRow("Extra Color 4", ChangeExtraColor4);
        listRow[(int)ButtonTypes.Furry] = CreateNewRow("Furry", ChangeFurriness);
        listRow[(int)ButtonTypes.TailTypes] = CreateNewRow("Tail Types", ChangeTailType);
        listRow[(int)ButtonTypes.FurTypes] = CreateNewRow("Fur Types", ChangeFurType);
        listRow[(int)ButtonTypes.EarTypes] = CreateNewRow("Ear Types", ChangeEarType);
        listRow[(int)ButtonTypes.BodyAccentTypes1] = CreateNewRow("BATypes1", ChangeBodyAccentTypes1Type);
        listRow[(int)ButtonTypes.BodyAccentTypes2] = CreateNewRow("BATypes2", ChangeBodyAccentTypes2Type);
        listRow[(int)ButtonTypes.BodyAccentTypes3] = CreateNewRow("BATypes3", ChangeBodyAccentTypes3Type);
        listRow[(int)ButtonTypes.BodyAccentTypes4] = CreateNewRow("BATypes4", ChangeBodyAccentTypes4Type);
        listRow[(int)ButtonTypes.BodyAccentTypes5] = CreateNewRow("BATypes5", ChangeBodyAccentTypes5Type);
        listRow[(int)ButtonTypes.BallsSizes] = CreateNewRow("Ball Size", ChangeBallsSize);
        listRow[(int)ButtonTypes.VulvaTypes] = CreateNewRow("Vulva Type", ChangeVulvaType);
        listRow[(int)ButtonTypes.AltWeaponTypes] = CreateNewRow("Alt Weapon Sprite", ChangeWeaponSprite);
    }

    RestrictedList CreateNewRow(string text, Action<bool> action)
    {
        RestrictedList button = UnityEngine.Object.Instantiate(CustomizerUI.ButtonPrefab, CustomizerUI.ButtonPanel.transform).GetComponent<RestrictedList>();
        button.Init(text, action);
        return button;
    }


    public void RandomizeUnit()
    {
        RaceData.RandomCustom(Unit);
        RefreshView();
    }


    void ChangeSkinTone(bool change)
    {
        Unit.SkinColor = (RaceData.SkinColors + Unit.SkinColor  ) % RaceData.SkinColors;
        RefreshView();
    }

    void ChangeHairColor(bool change)
    {
        Unit.HairColor = (RaceData.HairColors + Unit.HairColor  ) % RaceData.HairColors;
        if (Unit.Race == Race.Cats | Unit.Race == Race.Dogs | Unit.Race == Race.Bunnies | Unit.Race == Race.Wolves | Unit.Race == Race.Foxes | Unit.Race == Race.Tigers)
        {
            listRow[(int)ButtonTypes.HairColor].Label.text = "Hair Color: " + HairColorLookup(Unit.HairColor);
        }
        RefreshView();
    }

    void ChangeEyeColor(bool change)
    {
        Unit.EyeColor = (RaceData.EyeColors + Unit.EyeColor  ) % RaceData.EyeColors;
        RefreshView();
    }

    void ChangeExtraColor1(bool change)
    {
        Unit.ExtraColor1 = (RaceData.ExtraColors1 + Unit.ExtraColor1  ) % RaceData.ExtraColors1;
        RefreshView();
    }
    void ChangeExtraColor2(bool change)
    {
        Unit.ExtraColor2 = (RaceData.ExtraColors2 + Unit.ExtraColor2  ) % RaceData.ExtraColors2;
        RefreshView();
    }
    void ChangeExtraColor3(bool change)
    {
        Unit.ExtraColor3 = (RaceData.ExtraColors3 + Unit.ExtraColor3  ) % RaceData.ExtraColors3;
        RefreshView();
    }
    void ChangeExtraColor4(bool change)
    {
        Unit.ExtraColor4 = (RaceData.ExtraColors4 + Unit.ExtraColor4  ) % RaceData.ExtraColors4;
        RefreshView();
    }

    void ChangeEyeType(bool change)
    {
        Unit.EyeType = (RaceData.EyeTypes + Unit.EyeType  ) % RaceData.EyeTypes;
        RefreshView();
    }

    void ChangeHairStyle(bool change)
    {
        Unit.HairStyle = (RaceData.HairStyles + Unit.HairStyle  ) % RaceData.HairStyles;

        RefreshView();
    }

    void ChangeBeardStyle(bool change)
    {
        Unit.BeardStyle = (RaceData.BeardStyles + Unit.BeardStyle  ) % RaceData.BeardStyles;

        RefreshView();
    }


    void ChangeBodyAccessoryColor(bool change)
    {
        Unit.AccessoryColor = (RaceData.AccessoryColors + Unit.AccessoryColor  ) % RaceData.AccessoryColors;
        if (Unit.Race == Race.Cats | Unit.Race == Race.Dogs | Unit.Race == Race.Bunnies | Unit.Race == Race.Wolves | Unit.Race == Race.Foxes | Unit.Race == Race.Tigers)
            listRow[(int)ButtonTypes.BodyAccessoryColor].Label.text = "Fur Color: " + HairColorLookup(Unit.AccessoryColor);
        RefreshView();
    }

    void ChangeBodyAccessoryType(bool change)
    {
        Unit.SpecialAccessoryType = (RaceData.SpecialAccessoryCount + Unit.SpecialAccessoryType  ) % RaceData.SpecialAccessoryCount;
        RefreshView();
    }

    internal void ChangeGender()
    {
        bool changedGender = false;
        if (CustomizerUI.Gender.value == 0 && Unit.GetGender() != Gender.Male)
        {
            if (RaceData.CanBeGender.Contains(Gender.Male) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 1 && Unit.GetGender() != Gender.Female)
        {
            if (RaceData.CanBeGender.Contains(Gender.Female) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 2 && Unit.GetGender() != Gender.Hermaphrodite)
        {
            if (RaceData.CanBeGender.Contains(Gender.Hermaphrodite) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = Config.HermsCanUB;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 3 && Unit.GetGender() != Gender.Gynomorph)
        {
            if (RaceData.CanBeGender.Contains(Gender.Gynomorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }
        else if (CustomizerUI.Gender.value == 4 && Unit.GetGender() != Gender.Maleherm)
        {
            if (RaceData.CanBeGender.Contains(Gender.Maleherm) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = State.Rand.Next(RaceData.DickSizes);
        }
        else if (CustomizerUI.Gender.value == 5 && Unit.GetGender() != Gender.Andromorph)
        {
            if (RaceData.CanBeGender.Contains(Gender.Andromorph) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = true;
            Unit.SetDefaultBreastSize(-1);
        }
        else if (CustomizerUI.Gender.value == 6 && Unit.GetGender() != Gender.Agenic)
        {
            if (RaceData.CanBeGender.Contains(Gender.Agenic) == false)
            {
                RefreshGenderDropdown(Unit);
                return;
            }
            changedGender = true;
            Unit.DickSize = -1;
            Unit.HasVagina = false;
            Unit.SetDefaultBreastSize(State.Rand.Next(RaceData.BreastSizes));
        }

        listRow[(int)ButtonTypes.BreastSize].gameObject.SetActive(Unit.HasBreasts && RaceData.BreastSizes > 1);
        listRow[(int)ButtonTypes.CockSize].gameObject.SetActive(Unit.HasDick && RaceData.DickSizes > 1);
        if (changedGender)
        {
            if (CustomizerUI.Gender.value == 0 || CustomizerUI.Gender.value == 5)
            {
                CustomizerUI.Nominative.text = "he";
                CustomizerUI.Accusative.text = "him";
                CustomizerUI.PronominalPossessive.text = "his";
                CustomizerUI.PredicativePossessive.text = "his";
                CustomizerUI.Reflexive.text = "himself";
                CustomizerUI.Quantification.value = 0;
            }
            else if (CustomizerUI.Gender.value == 1)
            {
                CustomizerUI.Nominative.text = "she";
                CustomizerUI.Accusative.text = "her";
                CustomizerUI.PronominalPossessive.text = "her";
                CustomizerUI.PredicativePossessive.text = "hers";
                CustomizerUI.Reflexive.text = "herself";
                CustomizerUI.Quantification.value = 0;
            }
            else
            {
                CustomizerUI.Nominative.text = "they";
                CustomizerUI.Accusative.text = "them";
                CustomizerUI.PronominalPossessive.text = "their";
                CustomizerUI.PredicativePossessive.text = "theirs";
                CustomizerUI.Reflexive.text = "themself";
                CustomizerUI.Quantification.value = 1;
            }
            CheckClothes(Unit);
            RefreshPronouns(Unit);
            Unit.ReloadTraits();
            Unit.InitializeTraits();
            RefreshView();
        }

    }
    internal void CheckClothes(Unit unit)
    {
        if (RaceData.AllowedMainClothingTypes.Count > 0)
        {
            MainClothing current_cloth = RaceData.AllowedMainClothingTypes[unit.ClothingType > 0 ? unit.ClothingType - 1 : 0];
            if (!current_cloth.CanWear(Unit) && current_cloth.ExposeSwapValue() >= 0)
            {
                unit.ClothingType = current_cloth.ExposeSwapValue();
            }
        }

        if (RaceData.AllowedWaistTypes.Count > 0)
        {
            MainClothing current_cloth = RaceData.AllowedWaistTypes[unit.ClothingType2 > 0 ? unit.ClothingType2 - 1 : 0];
            if (!current_cloth.CanWear(Unit) && current_cloth.ExposeSwapValue() >= 0)
            {
                unit.ClothingType2 = current_cloth.ExposeSwapValue();
            }
        }
    }

    internal void ChangePronouns()
    {
        if (Unit.Pronouns == null)
            Unit.GeneratePronouns();
        Unit.Pronouns[0] = CustomizerUI.Nominative.text;
        Unit.Pronouns[1] = CustomizerUI.Accusative.text;
        Unit.Pronouns[2] = CustomizerUI.PronominalPossessive.text;
        Unit.Pronouns[3] = CustomizerUI.PredicativePossessive.text;
        Unit.Pronouns[4] = CustomizerUI.Reflexive.text;
        if (CustomizerUI.Quantification.value == 0)
            Unit.Pronouns[5] = "singular";
        else
            Unit.Pronouns[5] = "plural";
    }

    void ChangeBreastSize(bool change)
    {
        Unit.SetDefaultBreastSize((RaceData.BreastSizes + Unit.BreastSize  ) % RaceData.BreastSizes);
        RefreshView();
    }


    void ChangeDickSize(bool change)
    {
        Unit.DickSize = (RaceData.DickSizes + Unit.DickSize  ) % RaceData.DickSizes;
        RefreshView();
    }

    void ChangeMouthType(bool change)
    {
        Unit.MouthType = (RaceData.MouthTypes + Unit.MouthType  ) % RaceData.MouthTypes;
        RefreshView();
    }

    void ChangeBodyWeight(bool change)
    {
        if (Unit.BodySizeManuallyChanged == false)
            Unit.BodySize = Config.DefaultStartingWeight;
        Unit.BodySizeManuallyChanged = true;
        Unit.BodySize = (RaceData.BodySizes + Unit.BodySize  ) % RaceData.BodySizes;
        RefreshView();
    }

    void ChangeClothingType(bool change)
    {
        int totalClothingTypes = RaceData.MainClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType = 0;
            return;
        }

        if (Unit.ClothingType > RaceData.MainClothingTypesCount)
            Unit.ClothingType = 0;

        Unit.ClothingType = (totalClothingTypes + Unit.ClothingType  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType == 0)
                break;
            if (RaceData.AllowedMainClothingTypes[Unit.ClothingType - 1].CanWear(Unit))
                break;
            Unit.ClothingType = (totalClothingTypes + Unit.ClothingType  ) % totalClothingTypes;
        }
        RefreshView();
    }
    void ChangeClothing2Type(bool change)
    {
        int totalClothingTypes = RaceData.WaistClothingTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingType2 = 0;
            return;
        }

        if (Unit.ClothingType2 > RaceData.WaistClothingTypesCount)
            Unit.ClothingType2 = 0;

        Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingType2 == 0)
                break;
            if (RaceData.AllowedWaistTypes[Unit.ClothingType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingType2 = (totalClothingTypes + Unit.ClothingType2  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing1Type(bool change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing1Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType1 = 0;
            return;
        }

        if (Unit.ClothingExtraType1 > RaceData.ExtraMainClothing1Count)
            Unit.ClothingExtraType1 = 0;

        Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType1 == 0)
                break;
            if (RaceData.ExtraMainClothing1Types[Unit.ClothingExtraType1 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType1 = (totalClothingTypes + Unit.ClothingExtraType1  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing2Type(bool change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing2Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType2 = 0;
            return;
        }

        if (Unit.ClothingExtraType2 > RaceData.ExtraMainClothing2Count)
            Unit.ClothingExtraType2 = 0;

        Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType2 == 0)
                break;
            if (RaceData.ExtraMainClothing2Types[Unit.ClothingExtraType2 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType2 = (totalClothingTypes + Unit.ClothingExtraType2  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing3Type(bool change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing3Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType3 = 0;
            return;
        }

        if (Unit.ClothingExtraType3 > RaceData.ExtraMainClothing3Count)
            Unit.ClothingExtraType3 = 0;

        Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType3 == 0)
                break;
            if (RaceData.ExtraMainClothing3Types[Unit.ClothingExtraType3 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType3 = (totalClothingTypes + Unit.ClothingExtraType3  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing4Type(bool change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing4Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType4 = 0;
            return;
        }

        if (Unit.ClothingExtraType4 > RaceData.ExtraMainClothing4Count)
            Unit.ClothingExtraType4 = 0;

        Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType4 == 0)
                break;
            if (RaceData.ExtraMainClothing4Types[Unit.ClothingExtraType4 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType4 = (totalClothingTypes + Unit.ClothingExtraType4  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeExtraClothing5Type(bool change)
    {
        int totalClothingTypes = RaceData.ExtraMainClothing5Count;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingExtraType5 = 0;
            return;
        }

        if (Unit.ClothingExtraType5 > RaceData.ExtraMainClothing5Count)
            Unit.ClothingExtraType5 = 0;

        Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingExtraType5 == 0)
                break;
            if (RaceData.ExtraMainClothing5Types[Unit.ClothingExtraType5 - 1].CanWear(Unit))
                break;
            Unit.ClothingExtraType5 = (totalClothingTypes + Unit.ClothingExtraType5  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingAccesoryType(bool change)
    {
        int totalClothingTypes = RaceData.ClothingAccessoryTypesCount;
        if (Unit.EarnedMask && Unit.Race <= Race.Goblins && Unit.Race != Race.Lizards)
            totalClothingTypes += 1;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingAccessoryType = 0;
            return;
        }

        if (Unit.ClothingAccessoryType > totalClothingTypes)
            Unit.ClothingAccessoryType = 0;

        Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingAccessoryType == 0)
                break;
            if (Unit.ClothingAccessoryType == RaceData.ClothingAccessoryTypesCount && Unit.EarnedMask)
                break;
            if (RaceData.AllowedClothingAccessoryTypes[Unit.ClothingAccessoryType - 1].CanWear(Unit))
                break;
            Unit.ClothingAccessoryType = (totalClothingTypes + Unit.ClothingAccessoryType  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingHatType(bool change)
    {
        int totalClothingTypes = RaceData.ClothingHatTypesCount;
        if (totalClothingTypes == 0)
        {
            Unit.ClothingHatType = 0;
            return;
        }

        if (Unit.ClothingHatType > RaceData.ClothingHatTypesCount)
            Unit.ClothingHatType = 0;

        Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType  ) % totalClothingTypes;
        for (int i = 0; i < 20; i++)
        {
            if (Unit.ClothingHatType == 0)
                break;
            if (RaceData.AllowedClothingHatTypes[Unit.ClothingHatType - 1].CanWear(Unit))
                break;
            Unit.ClothingHatType = (totalClothingTypes + Unit.ClothingHatType  ) % totalClothingTypes;
        }
        RefreshView();
    }

    void ChangeClothingColor(bool change)
    {
        Unit.ClothingColor = (RaceData.clothingColors + Unit.ClothingColor  ) % RaceData.clothingColors;
        RefreshView();
    }
    void ChangeClothingColor2(bool change)
    {
        Unit.ClothingColor2 = (RaceData.clothingColors + Unit.ClothingColor2  ) % RaceData.clothingColors;
        RefreshView();
    }
    void ChangeClothingColor3(bool change)
    {
        Unit.ClothingColor3 = (RaceData.clothingColors + Unit.ClothingColor3  ) % RaceData.clothingColors;
        RefreshView();
    }

    void ChangeFurriness(bool change)
    {
        Unit.Furry = !Unit.Furry;
        RefreshView();
    }

    //    FurTypes = 11;
    //    EarTypes = 17;
    //    BodyAccentTypes1 = 2; // Used for checking if breasts have a visible areola or not.
    //    BodyAccentTypes2 = 7; // Leg Stripe patterns.
    //    BodyAccentTypes3 = 5; // Arm Stripe patterns.
    //    BallsSizes = 3;
    //    VulvaTypes = 3;
    //    BasicMeleeWeaponTypes = 2;
    //    AdvancedMeleeWeaponTypes = 2;
    //    BasicRangedWeaponTypes = 1;
    //    AdvancedRangedWeaponTypes = 1;

    void ChangeHeadType(bool change)
    {
        Unit.HeadType = (RaceData.HeadTypes + Unit.HeadType  ) % RaceData.HeadTypes;
        RefreshView();
    }

    void ChangeTailType(bool change)
    {
        Unit.TailType = (RaceData.TailTypes + Unit.TailType  ) % RaceData.TailTypes;
        RefreshView();
    }

    void ChangeFurType(bool change)
    {
        Unit.FurType = (RaceData.FurTypes + Unit.FurType  ) % RaceData.FurTypes;
        RefreshView();
    }

    void ChangeEarType(bool change)
    {
        Unit.EarType = (RaceData.EarTypes + Unit.EarType  ) % RaceData.EarTypes;
        RefreshView();
    }

    void ChangeBodyAccentTypes1Type(bool change)
    {
        Unit.BodyAccentType1 = (RaceData.BodyAccentTypes1 + Unit.BodyAccentType1  ) % RaceData.BodyAccentTypes1;
        RefreshView();
    }

    void ChangeBodyAccentTypes2Type(bool change)
    {
        Unit.BodyAccentType2 = (RaceData.BodyAccentTypes2 + Unit.BodyAccentType2  ) % RaceData.BodyAccentTypes2;
        RefreshView();
    }

    void ChangeBodyAccentTypes3Type(bool change)
    {
        Unit.BodyAccentType3 = (RaceData.BodyAccentTypes3 + Unit.BodyAccentType3  ) % RaceData.BodyAccentTypes3;
        RefreshView();
    }

    void ChangeBodyAccentTypes4Type(bool change)
    {
        Unit.BodyAccentType4 = (RaceData.BodyAccentTypes4 + Unit.BodyAccentType4  ) % RaceData.BodyAccentTypes4;
        RefreshView();
    }

    void ChangeBodyAccentTypes5Type(bool change)
    {
        Unit.BodyAccentType5 = (RaceData.BodyAccentTypes5 + Unit.BodyAccentType5  ) % RaceData.BodyAccentTypes5;
        RefreshView();
    }

    void ChangeBallsSize(bool change)
    {
        Unit.BallsSize = (RaceData.BallsSizes + Unit.BallsSize  ) % RaceData.BallsSizes;
        RefreshView();
    }

    void ChangeVulvaType(bool change)
    {
        Unit.VulvaType = (RaceData.VulvaTypes + Unit.VulvaType  ) % RaceData.VulvaTypes;
        RefreshView();
    }

    void ChangeWeaponSprite(bool change)
    {
        int basicMeleeTypes = RaceData.BasicMeleeWeaponTypes;
        if (Config.HideCocks == false && Unit.Race == Race.Crux) // Give Crux the ability to have dildo weapons only if HideCocks is off
            basicMeleeTypes++;
        Unit.BasicMeleeWeaponType = (basicMeleeTypes + Unit.BasicMeleeWeaponType  ) % basicMeleeTypes;

        Unit.AdvancedMeleeWeaponType = (RaceData.AdvancedMeleeWeaponTypes + Unit.AdvancedMeleeWeaponType  ) % RaceData.AdvancedMeleeWeaponTypes;
        Unit.BasicRangedWeaponType = (RaceData.BasicRangedWeaponTypes + Unit.BasicRangedWeaponType  ) % RaceData.BasicRangedWeaponTypes;
        Unit.AdvancedRangedWeaponType = (RaceData.AdvancedRangedWeaponTypes + Unit.AdvancedRangedWeaponType  ) % RaceData.AdvancedRangedWeaponTypes;
        RefreshView();
    }




}

