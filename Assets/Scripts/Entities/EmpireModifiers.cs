public class EmpireModifiers
{
    // Adds value to current income
    internal int IncomeShift = 0;
    // Multiplies this value by current income
    internal float IncomeMult = 1;
    // Multiplies population growth by this value
    internal float PopGrowthMult = 1;
    // Sets this empire's default opinion of others to this value.
    internal float OutgoingOpinionShift = 0;
    // All changes in this empire's opinion of others is multiplied this value.
    internal float OutgoingOpinionMult= 1;
    // Sets others's default opinion of this empire to this value.
    internal float IncomingOpinionShift = 0;
    // All changes in other's opinion of this empuire is multiplied this value.
    internal float IncomingOpinionMult = 1;
    // Adds this value to all strategic army MP.
    internal float ArmyMPShift = 0;
    

    public void ResetValues()
    {
        IncomeShift = 0;
        IncomeMult = 0;
        PopGrowthMult = 0;
        OutgoingOpinionShift = 0;
        OutgoingOpinionMult = 0;
        IncomingOpinionShift = 0;
        IncomingOpinionMult = 0;
        ArmyMPShift = 0;
    }
}
