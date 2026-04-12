using OdinSerializer;

public class EmpireModifiers
{
    /// <summary>
    /// General
    /// </summary>
    // Controlls empire type 
    [OdinSerialize] public EmpireType EmpireType;
    // Adjust how empire operates internally
    [OdinSerialize] public EmpireInnerPersona InnerPersona;
    // Adjust how empire interacts with others
    [OdinSerialize] public EmpireOuterPersona OuterPersona;
    // Adds value to current income
    [OdinSerialize] internal int IncomeShift = 0;
    // Multiplies this value by current income
    [OdinSerialize] internal float IncomeMult = 1;
    // Multiplies population growth by this value
    [OdinSerialize] internal float PopGrowthMult = 1;
    // Sets this empire's default opinion of others to this value.
    [OdinSerialize] internal float OutgoingOpinionShift = 0;
    // All changes in this empire's opinion of others is multiplied this value.
    [OdinSerialize] internal float OutgoingOpinionMult= 1;
    // Sets others's default opinion of this empire to this value.
    [OdinSerialize] internal float IncomingOpinionShift = 0;
    // All changes in other's opinion of this empuire is multiplied this value.
    [OdinSerialize] internal float IncomingOpinionMult = 1;
    // Adds this value to all strategic army MP.
    [OdinSerialize] internal float ArmyMPShift = 0;

    [OdinSerialize]
    public EmpireEthics Ethics;

    public void ResetValues()
    {
        IncomeShift = 0;
        IncomeMult = 1;
        PopGrowthMult = 1;
        OutgoingOpinionShift = 0;
        OutgoingOpinionMult = 1;
        IncomingOpinionShift = 0;
        IncomingOpinionMult = 1;
        ArmyMPShift = 0;
    }
}
