using OdinSerializer;
using System.Collections.Generic;

public class EmpireEthics
{
    [OdinSerialize]
    private Dictionary<DichotomyEmpireEthicTypes, int> EthicValues;

    internal void Init()
    {
        EthicValues = new Dictionary<DichotomyEmpireEthicTypes, int>();
    }

    internal void ShiftEthicLeft(DichotomyEmpireEthicTypes ethic)
    {
        if (EthicValues.ContainsKey(ethic))
        {
            if (EthicValues[ethic] == 1)
            {
                EthicValues.Remove(ethic);
            }
            else
            {
                EthicValues[ethic] = EthicValues[ethic] - 1;
            }
        }
        else 
        {
            EthicValues.Add(ethic, -1);
        }
    }

    internal void ShiftEthicRight(DichotomyEmpireEthicTypes ethic)
    {
        if (EthicValues.ContainsKey(ethic))
        {
            if (EthicValues[ethic] == -1)
            {
                EthicValues.Remove(ethic);
            }
            else
            {
                EthicValues[ethic] = EthicValues[ethic] + 1;
            }
        }
        else
        {
            EthicValues.Add(ethic, 1);
        }
    }
    internal int EthicsValue(DichotomyEmpireEthicTypes ethic) => EthicValues[ethic];

    internal void ResetEthics()
    {
        Init();
    }

}
