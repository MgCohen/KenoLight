using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueSetter : MonoBehaviour
{
    public TextMeshProUGUI Donate, Kuadra, Keno, Kina, Acumulado, Count, Id;

    public List<PrizeShowcase> prizes = new List<PrizeShowcase>();

    public void Set(Sorteio sorteio)
    {
        Donate.text = sorteio.donationValue.ToString();
        Kuadra.text = sorteio.kuadraPrize.ToString();
        Kina.text = sorteio.kinaPrize.ToString();
        Keno.text = sorteio.kenoPrize.ToString();
        Acumulado.text = sorteio.acumuladoPrize.ToString();
        Count.text = sorteio.acumuladoBallCount + "bolas";
        Id.text = sorteio.sorteioId.ToString();

        prizes[0].Set(true);
    }

    public void SetPrize(int index)
    {
        for (int i = 0; i < prizes.Count; i++)
        {
            prizes[i].Set((i == index) ? true : false);
        }
    }
}
