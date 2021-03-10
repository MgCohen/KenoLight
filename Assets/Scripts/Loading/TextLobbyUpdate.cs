using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLobbyUpdate : MonoBehaviour
{
     [Header("Sorteio Especial")]
    [SerializeField] private TextMeshProUGUI _horaEspecial;
    [SerializeField] private TextMeshProUGUI _dataEspecial;
    [SerializeField] private TextMeshProUGUI _valorKenoEspecial;
    [SerializeField] private TextMeshProUGUI _valorKinaEspecial;
    [SerializeField] private TextMeshProUGUI _valorKuadraEspecial;
    [Header("Sorteio Super Especial")]
    [SerializeField] private TextMeshProUGUI _horaSuperEspecial;
    [SerializeField] private TextMeshProUGUI _dataSuperEspecial;
    [SerializeField] private TextMeshProUGUI _valorKenoSuperEspecial;
    [SerializeField] private TextMeshProUGUI _valorKinaSuperEspecial;
    [SerializeField] private TextMeshProUGUI _valorKuadraSuperEspecial;

    [Header("Sorteio Normal")]
    [SerializeField] private TextMeshProUGUI _hora;
    [SerializeField] private TextMeshProUGUI _data;
    [SerializeField] private TextMeshProUGUI _valorKeno;
    [SerializeField] private TextMeshProUGUI _valorKina;
    [SerializeField] private TextMeshProUGUI _valorKuadra;

    [Header("Historico Próximos")]
    [SerializeField] private List<TextMeshProUGUI> linhasAnterior;
    [SerializeField] private List<TextMeshProUGUI> linhasProximo;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
