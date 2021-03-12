using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class ValueSetter : MonoBehaviour
{
    public TextMeshProUGUI Donate, Kuadra, Keno, Kina, Acumulado, Count, Id;

    public List<PrizeShowcase> prizes = new List<PrizeShowcase>();

    public void Set(Sorteio sorteio)
    {
        Donate.text    = sorteio.donationValue.ToString(CultureInfo.InvariantCulture);
        Kuadra.text    = sorteio.kuadraPrize.ToString(CultureInfo.InvariantCulture);
        Kina.text      = sorteio.kinaPrize.ToString(CultureInfo.InvariantCulture);
        Keno.text      = sorteio.kenoPrize.ToString(CultureInfo.InvariantCulture);
        Acumulado.text = sorteio.acumuladoPrize.ToString(CultureInfo.InvariantCulture);
        Count.text     = $"{sorteio.acumuladoBallCount} bolas";
        Id.text        = sorteio.sorteioId.ToString();

        prizes[0].Set(true);
    }

    public void SetPrize(int index)
    {
        for (var i = 0; i < prizes.Count; i++)
        {
            prizes[i].Set(i == index);
        }
    }
}
