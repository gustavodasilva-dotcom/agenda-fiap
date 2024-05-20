using System.ComponentModel.DataAnnotations;

namespace Agenda.Common.Enums;

public enum DDDs
{
    [Display(Name = "11 – São Paulo – SP")]
    SP = 11,

    [Display(Name = "12 – São José dos Campos – SP")]
    SP_ValeParaibaLitoralNorte = 12,

    [Display(Name = "13 – Santos – SP")]
    SP_BaixadaSantistaValeRibeira = 13,

    [Display(Name = "14 – Bauru – SP")]
    SP_AvareBauruBotucatuJauLinsMariliaOurinhos = 14,

    [Display(Name = "15 – Sorocaba – SP")]
    SP_ItapetiningaItapevaSorocabaTatui = 15,

    [Display(Name = "16 – Ribeirão Preto – SP")]
    SP_AraraquaraFrancaJaboticabalRibeiraoPretoSaoCarlosSertaozinho = 16,

    [Display(Name = "17 – São José do Rio Preto – SP")]
    SP_BarretosCatanduvaFernandopolisJalesSaoJosedoRioPretoVotuporanga = 17,

    [Display(Name = "18 – Presidente Prudente – SP")]
    SP_AndradinaAracatubaAssisBiriguiDracenaPresidentePrudente = 18,

    [Display(Name = "19 – Campinas – SP")]
    SP_AmericanaCampinasLimeiraPiracicabaRioClaroSaoJoaodaBoaVista = 19,

    [Display(Name = "21 – Rio de Janeiro – RJ")]
    RJ = 21,

    [Display(Name = "22 – Campos dos Goytacazes – RJ")]
    RJ_CaboFrioCamposdosGoytacazesItaperunaMacaeNovaFriburgo = 22,

    [Display(Name = "24 – Volta Redonda – RJ")]
    RJ_AngradosReisPetropolisVoltaRedondaPirai = 24,

    [Display(Name = "27 – Vila Velha/Vitória – ES")]
    ES = 27,

    [Display(Name = "28 – Cachoeiro de Itapemirim – ES")]
    ES_CachoeirodeItapemirimCasteloItapemirimMarataizes = 28,

    [Display(Name = "31 – Belo Horizonte – MG")]
    MG = 31,

    [Display(Name = "32 – Juiz de Fora – MG")]
    MG_BarbacenaJuizdeForaMuriaeSaoJoaodelReiUba = 32,

    [Display(Name = "33 – Governador Valadares – MG")]
    MG_AlmenaraCaratingaGovernadorValadaresManhuacuTeofiloOtoni = 33,

    [Display(Name = "34 – Uberlândia – MG")]
    MG_AraguariAraxaPatosdeMinasUberlandiaUberaba = 34,

    [Display(Name = "35 – Poços de Caldas – MG")]
    MG_AlfenasGuaxupeLavrasPocosdeCaldasPousoAlegreVarginha = 35,

    [Display(Name = "37 – Divinópolis – MG")]
    MG_BomDespachoDivinopolisFormigaItaunaParadeMinas = 37,

    [Display(Name = "38 – Montes Claros – MG")]
    MG_CurveloDiamantinaMontesClarosPiraporaUnai = 38,

    [Display(Name = "41 – Curitiba – PR")]
    PR = 41,

    [Display(Name = "42 – Ponta Grossa – PR")]
    PR_PontaGrossaGuarapuava = 42,

    [Display(Name = "43 – Londrina – PR")]
    PR_ApucaranaLondrina = 43,

    [Display(Name = "44 – Maringá – PR")]
    PR_MaringaCampoMouraoUmuarama = 44,

    [Display(Name = "45 – Foz do Iguaçú – PR")]
    PR_CascavelFozdoIguacu = 45,

    [Display(Name = "46 – Francisco Beltrão/Pato Branco – PR")]
    PR_FranciscoBeltraoPatoBranco = 46,

    [Display(Name = "47 – Joinville – SC")]
    SC_BalnearioCamboriuBlumenauItajaiJoinville = 47,

    [Display(Name = "48 – Florianópolis – SC")]
    SC = 48,

    [Display(Name = "49 – Chapecó – SC")]
    SC_CacadorChapecoLages = 49,

    [Display(Name = "51 – Porto Alegre – RS")]
    RS = 51,

    [Display(Name = "53 – Pelotas – RS")]
    RS_PelotasRioGrande = 53,

    [Display(Name = "54 – Caxias do Sul – RS")]
    RS_CaxiasdoSulPassoFundo = 54,

    [Display(Name = "55 – Santa Maria – RS")]
    RS_SantaMariaSantanadoLivramentoSantoAngeloUruguaiana = 55,

    [Display(Name = "61 – Brasília – DF")]
    DF = 61,

    [Display(Name = "62 – Goiânia – GO")]
    GO = 62,

    [Display(Name = "63 – Palmas – TO")]
    TO = 63,

    [Display(Name = "64 – Rio Verde – GO")]
    GO_CaldasNovasCatalaoItumbiaraRioVerde = 64,

    [Display(Name = "65 – Cuiabá – MT")]
    MT = 65,

    [Display(Name = "66 – Rondonópolis – MT")]
    MT_RondonopolisSinop = 66,

    [Display(Name = "67 – Campo Grande – MS")]
    MS = 67,

    [Display(Name = "68 – Rio Branco – AC")]
    AC = 68,

    [Display(Name = "69 – Porto Velho – RO")]
    RO = 69,

    [Display(Name = "71 – Salvador – BA")]
    BA = 71,

    [Display(Name = "73 – Ilhéus – BA")]
    BA_EunápolisIlheusPortoSeguroTeixeiradeFreitas = 73,

    [Display(Name = "74 – Juazeiro – BA")]
    BA_IrecêJacobinaJuazeiroXiqueXique = 74,

    [Display(Name = "75 – Feira de Santana – BA")]
    BA_AlagoinhasFeiradeSantanaPauloAfonsoValenca = 75,

    [Display(Name = "77 – Barreiras – BA")]
    BA_BarreirasBomJesusdaLapaGuanambiVitoriadaConquista = 77,

    [Display(Name = "79 – Aracaju – SE")]
    SE = 79,

    [Display(Name = "81 – Recife – PE")]
    PE = 81,

    [Display(Name = "82 – Maceió – AL")]
    AL = 82,

    [Display(Name = "83 – João Pessoa – PB")]
    PB = 83,

    [Display(Name = "84 – Natal – RN")]
    RN = 84,

    [Display(Name = "85 – Fortaleza – CE")]
    CE = 85,

    [Display(Name = "86 – Teresina – PI")]
    PI = 86,

    [Display(Name = "87 – Petrolina – PE")]
    PE_GaranhunsPetrolinaSalgueiroSerraTalhada = 87,

    [Display(Name = "88 – Juazeiro do Norte – CE")]
    CE_JuazeirodoNorteSobral = 88,

    [Display(Name = "89 – Picos – PI")]
    PI_PicosFloriano = 89,

    [Display(Name = "91 – Belém – PA")]
    PA = 91,

    [Display(Name = "92 – Manaus – AM")]
    AM = 92,

    [Display(Name = "93 – Santarém – PA")]
    PA_SantaremAltamira = 93,

    [Display(Name = "94 – Marabá – PA")]
    PA_Maraba = 94,

    [Display(Name = "95 – Boa Vista – RR")]
    RR = 95,

    [Display(Name = "96 – Macapá – AP")]
    AP = 96,

    [Display(Name = "97 – Coari – AM")]
    AM_interior = 97,

    [Display(Name = "98 – São Luís – MA")]
    MA = 98,

    [Display(Name = "99 – Imperatriz – MA")]
    MA_CaxiasCodoImperatriz = 99
}
