﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CBR_Parser
{

    public class ReuterCurrency
    {
        public string Code { get; set; }
        public string Price { get; set; }
        public string Dir { get; set; }
        public string ISOCode
        {
            get
            {
                if (CurrencyCodes.ContainsKey(Code))
                {
                    return CurrencyCodes[Code];
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public string Name
        {
            get
            {
                if (Names.ContainsKey(ISOCode))
                {
                    return Names[ISOCode];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static Dictionary<string, string> CurrencyCodes = new Dictionary<string, string>
        {
            {"008", "ALL"},
            {"012", "DZD"},
            {"032", "ARS"},
            {"044", "BSD"},
            {"048", "BHD"},
            {"050", "BDT"},
            {"052", "BBD"},
            {"060", "BMD"},
            {"064", "BTN"},
            {"068", "BOB"},
            {"072", "BWP"},
            {"084", "BZD"},
            {"090", "SBD"},
            {"096", "BND"},
            {"108", "BIF"},
            {"116", "KHR"},
            {"132", "CVE"},
            {"144", "LKR"},
            {"152", "CLP"},
            {"170", "COP"},
            {"174", "KMF"},
            {"188", "CRC"},
            {"191", "HRK"},
            {"192", "CUP"},
            {"214", "DOP"},
            {"222", "SVC"},
            {"230", "ETB"},
            {"232", "ERN"},
            {"238", "FKP"},
            {"242", "FJD"},
            {"262", "DJF"},
            {"270", "GMD"},
            {"292", "GIP"},
            {"320", "GTQ"},
            {"324", "GNF"},
            {"328", "GYD"},
            {"332", "HTG"},
            {"340", "HNL"},
            {"352", "ISK"},
            {"360", "IDR"},
            {"364", "IRR"},
            {"368", "IQD"},
            {"376", "ILS"},
            {"388", "JMD"},
            {"400", "JOD"},
            {"404", "KES"},
            {"408", "KPW"},
            {"414", "KWD"},
            {"418", "LAK"},
            {"422", "LBP"},
            {"430", "LRD"},
            {"434", "LYD"},
            {"446", "MOP"},
            {"454", "MWK"},
            {"458", "MYR"},
            {"462", "MVR"},
            {"480", "MUR"},
            {"484", "MXN"},
            {"496", "MNT"},
            {"504", "MAD"},
            {"512", "OMR"},
            {"516", "NAD"},
            {"524", "NPR"},
            {"533", "AWG"},
            {"548", "VUV"},
            {"554", "NZD"},
            {"558", "NIO"},
            {"566", "NGN"},
            {"586", "PKR"},
            {"598", "PGK"},
            {"600", "PYG"},
            {"604", "PEN"},
            {"608", "PHP"},
            {"634", "QAR"},
            {"646", "RWF"},
            {"654", "SHP"},
            {"682", "SAR"},
            {"690", "SCR"},
            {"694", "SLL"},
            {"704", "VND"},
            {"706", "SOS"},
            {"748", "SZL"},
            {"760", "SYP"},
            {"764", "THB"},
            {"776", "TOP"},
            {"780", "TTD"},
            {"784", "AED"},
            {"788", "TND"},
            {"800", "UGX"},
            {"807", "MKD"},
            {"818", "EGP"},
            {"834", "TZS"},
            {"840", "USD"},
            {"858", "UYU"},
            {"886", "YER"},
            {"901", "TWD"},
            {"928", "VES"},
            {"929", "MRU"},
            {"930", "STN"},
            {"936", "GHS"},
            {"938", "SDG"},
            {"941", "RSD"},
            {"943", "MZN"},
            {"950", "XAF"},
            {"951", "XCD"},
            {"952", "XOF"},
            {"967", "ZMW"},
            {"968", "SRD"},
            {"969", "MGA"},
            {"971", "AFN"},
            {"973", "AOA"},
            {"976", "CDF"},
            {"977", "BAM"},
            {"978", "EUR"},
            {"981", "GEL"}
        };
        public static Dictionary<string, string> Names = new Dictionary<string, string>
        {
            {"EUR", "EURO"},
            {"ALL", "Albanian Lek"},
            {"DZD", "Algerian Dinar"},
            {"ARS", "Argentine Peso"},
            {"AMD", "US$/Armenian Dram"},
            {"BSD", "Bahamian Dollar"},
            {"BHD", "Bahrani Dinar"},
            {"BDT", "Bangladesh Taka"},
            {"BBD", "Barbados Dollar"},
            {"BMD", "Bermuda Dollar"},
            {"BTN", "Bhutan Ngultrum"},
            {"BOB", "Bolivian Boliviano"},
            {"BWP", "Botswana Pula"},
            {"BZD", "Belize Dollar"},
            {"SBD", "Solomon Is. Dollar"},
            {"BND", "Brunei Dollar"},
            {"MMK", "Myanmar Kyat"},
            {"BIF", "Burundi Franc"},
            {"KHR", "Cambodia Riel"},
            {"CVE", "Cape Verde Escudo"},
            {"KYD", "Cayman Is. Dollar"},
            {"LKR", "Sri Lanka Rupee"},
            {"CLP", "Chilean Peso"},
            {"COP", "Colombian Peso"},
            {"KMF", "Comoro Franc"},
            {"CRC", "Costa Rica Colon"},
            {"HRK", "Croatian Kuna"},
            {"DOP", "Dominican Peso"},
            {"ECS", "Ecuador Sucre"},
            {"SVC", "El Salvador Colon"},
            {"ETB", "Ethiopian Birr"},
            {"FKP", "Falklands Pound"},
            {"FJD", "Fiji Dollar"},
            {"DJF", "Dijibouti Franc"},
            {"GMD", "Gambian Dalasi"},
            {"USD", "United States Dollar"},
            {"GTQ", "Guatemala Quetzal"},
            {"GNF", "Guinea Franc"},
            {"GYD", "Guyana Dollar"},
            {"HTG", "Haiti Gourde"},
            {"HNL", "Honduras Lempira"},
            {"ISK", "Iceland Krona"},
            {"IDR", "Indonesia Rupiah"},
            {"IRR", "Iran Rial"},
            {"IQD", "Iraqi Dinar"},
            {"ILS", "Israel Shekel"},
            {"JMD", "Jamaican Dollar"},
            {"JOD", "Jordan Dinar"},
            {"KES", "Kenya Shilling"},
            {"KPW", "Nth Korea Won"},
            {"KWD", "Kuwaiti Dinar"},
            {"LAK", "Lao Kip"},
            {"LBP", "Lebanon Pound"},
            {"LSL", "Lesotho Loti"},
            {"LRD", "Liberian Dollar"},
            {"LYD", "LIBYAN DINAR  "},
            {"MOP", "Macau Pataca"},
            {"MGF", "MalagasyFranc"},
            {"MWK", "Malawi Kwacha"},
            {"MYR", "Malaysia Ringgit"},
            {"MVR", "Maldives Rufiyaa"},
            {"MRO", "Mauritania Ouguiya"},
            {"MUR", "Mauritius Rupee"},
            {"MXN", "Mexican Peso"},
            {"MNT", "Mongolia Tugrik"},
            {"MAD", "Moroccan Dirham"},
            {"MZM", "Mozambique Metical"},
            {"OMR", "Omani Rial"},
            {"NAD", "Namibian Dollar"},
            {"NPR", "Nepalese Rupee"},
            {"ANG", "Nth Antil Guilder"},
            {"AWG", "Nth Antil Guilder"},
            {"VUV", "Vanuatu Vatu"},
            {"NZD", "New Zealand $"},
            {"NIO", "Nicaragua Cordoba"},
            {"NGN", "Nigerian Naira"},
            {"PKR", "Pakistan Rupee"},
            {"PAB", "Panama Balboa"},
            {"PGK", "Papua New Guinea"},
            {"PYG", "Paraguay Guarani"},
            {"PEN", "Peru Nuevo Sol"},
            {"PHP", "Philippine Peso"},
            {"QAR", "Qatari Rial"},
            {"RWF", "US$/Rwanda Franc"},
            {"SHP", "St Helena Pound"},
            {"STD", "Sao Tome Dobra"},
            {"SAR", "Saudi Riyal"},
            {"SCR", "SC Rupee"},
            {"SLL", "SierraLeonLeon"},
            {"SKK", "Slovak Koruna"},
            {"VND", "Vietnam Dong"},
            {"SIT", "Slovenian Tolar"},
            {"SOS", "Somali Shilling"},
            {"ZWD", "Zimbabwe Dollar"},
            {"SDD", "Sudanese Dinar"},
            {"SRG", "Surinam Guilder"},
            {"SZL", "Swaziland Lilageni"},
            {"SYP", "Syrian Pound"},
            {"THB", "Thai Baht"},
            {"TOP", "Tonga Pa'anga"},
            {"TTD", "Trin Tob Dollar"},
            {"AED", "UAE Dirham"},
            {"TND", "Tunisian Dinar"},
            {"UGX", "Uganda Shilling"},
            {"MKD", "Macedonia Denar"},
            {"EGP", "Egypt Pound"},
            {"TZS", "Tanzania Shilling"},
            {"UYU", "Uruguayan Peso"},
            {"VEB", "Venezuela Bolivar"},
            {"WST", "Samoa Tala"},
            {"YER", "Yemen Rial"},
            {"ZMK", "Zambian Kwacha"},
            {"TWD", "Taiwan Dollar"},
            {"XAF", "CFA Franc"},
            {"XCD", "E Carribean Dollar"},
            {"XOF", "CFA Franc"},
            {"XPF", "Pacific Franc"},
            {"AOA", "Angolan Kwanza"},
            {"CDF", "Congo Franc"},
            {"GEL", "US$/Georgian Lari"}
        };
    }
}
