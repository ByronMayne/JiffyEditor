using UnityEngine;

public class Lang
{

public enum LanguageCode
{
	Unassigned,
	//null
	AA,
	//Afar
	AB,
	//Abkhazian
	AF,
	//Afrikaans
	AM,
	//Amharic
	AR,
	//Arabic
	AS,
	//Assamese
	AY,
	//Aymara
	AZ,
	//Azerbaijani
	BA,
	//Bashkir
	BE,
	//Byelorussian
	BG,
	//Bulgarian
	BH,
	//Bihari
	BI,
	//Bislama
	BN,
	//Bengali
	BO,
	//Tibetan
	BR,
	//Breton
	CA,
	//Catalan
	CO,
	//Corsican
	CS,
	//Czech
	CY,
	//Welch
	DA,
	//Danish
	DE,
	//German
	DZ,
	//Bhutani
	EL,
	//Greek
	EN,
	//English
	EO,
	//Esperanto
	ES,
	//Spanish
	ET,
	//Estonian
	EU,
	//Basque
	FA,
	//Persian
	FI,
	//Finnish
	FJ,
	//Fiji
	FO,
	//Faeroese
	FR,
	//French
	FY,
	//Frisian
	GA,
	//Irish
	GD,
	//Scots Gaelic
	GL,
	//Galician
	GN,
	//Guarani
	GU,
	//Gujarati
	HA,
	//Hausa
	HI,
	//Hindi
	HE,
	//Hebrew
	HR,
	//Croatian
	HU,
	//Hungarian
	HY,
	//Armenian
	IA,
	//Interlingua
	ID,
	//Indonesian
	IE,
	//Interlingue
	IK,
	//Inupiak
	IN,
	//former Indonesian
	IS,
	//Icelandic
	IT,
	//Italian
	IU,
	//Inuktitut (Eskimo)
	IW,
	//former Hebrew
	JA,
	//Japanese
	JI,
	//former Yiddish
	JW,
	//Javanese
	KA,
	//Georgian
	KK,
	//Kazakh
	KL,
	//Greenlandic
	KM,
	//Cambodian
	KN,
	//Kannada
	KO,
	//Korean
	KS,
	//Kashmiri
	KU,
	//Kurdish
	KY,
	//Kirghiz
	LA,
	//Latin
	LN,
	//Lingala
	LO,
	//Laothian
	LT,
	//Lithuanian
	LV,
	//Latvian, Lettish
	MG,
	//Malagasy
	MI,
	//Maori
	MK,
	//Macedonian
	ML,
	//Malayalam
	MN,
	//Mongolian
	MO,
	//Moldavian
	MR,
	//Marathi
	MS,
	//Malay
	MT,
	//Maltese
	MY,
	//Burmese
	NA,
	//Nauru
	NE,
	//Nepali
	NL,
	//Dutch
	NO,
	//Norwegian
	OC,
	//Occitan
	OM,
	//(Afan) Oromo
	OR,
	//Oriya
	PA,
	//Punjabi
	PL,
	//Polish
	PS,
	//Pashto, Pushto
	PT,
	//Portuguese
	QU,
	//Quechua
	RM,
	//Rhaeto-Romance
	RN,
	//Kirundi
	RO,
	//Romanian
	RU,
	//Russian
	RW,
	//Kinyarwanda
	SA,
	//Sanskrit
	SD,
	//Sindhi
	SG,
	//Sangro
	SH,
	//Serbo-Croatian
	SI,
	//Singhalese
	SK,
	//Slovak
	SL,
	//Slovenian
	SM,
	//Samoan
	SN,
	//Shona
	SO,
	//Somali
	SQ,
	//Albanian
	SR,
	//Serbian
	SS,
	//Siswati
	ST,
	//Sesotho
	SU,
	//Sudanese
	SV,
	//Swedish
	SW,
	//Swahili
	TA,
	//Tamil
	TE,
	//Tegulu
	TG,
	//Tajik
	TH,
	//Thai
	TI,
	//Tigrinya
	TK,
	//Turkmen
	TL,
	//Tagalog
	TN,
	//Setswana
	TO,
	//Tonga
	TR,
	//Turkish
	TS,
	//Tsonga
	TT,
	//Tatar
	TW,
	//Twi
	UG,
	//Uigur
	UK,
	//Ukrainian
	UR,
	//Urdu
	UZ,
	//Uzbek
	VI,
	//Vietnamese
	VO,
	//Volapuk
	WO,
	//Wolof
	XH,
	//Xhosa
	YI,
	//Yiddish
	YO,
	//Yoruba
	ZA,
	//Zhuang
	ZH,
	//Chinese
	ZU
	//Zulu

}

public static LanguageCode LanguageNameToCode (SystemLanguage name)
{
	if (name == SystemLanguage.Afrikaans)
		return LanguageCode.AF;
	else if (name == SystemLanguage.Arabic)
		return LanguageCode.AR;
	else if (name == SystemLanguage.Basque)
		return LanguageCode.BA;
	else if (name == SystemLanguage.Belarusian)
		return LanguageCode.BE;
	else if (name == SystemLanguage.Bulgarian)
		return LanguageCode.BG;
	else if (name == SystemLanguage.Catalan)
		return LanguageCode.CA;
	else if (name == SystemLanguage.Chinese)
		return LanguageCode.ZH;
	else if (name == SystemLanguage.Czech)
		return LanguageCode.CS;
	else if (name == SystemLanguage.Danish)
		return LanguageCode.DA;
	else if (name == SystemLanguage.Dutch)
		return LanguageCode.NL;
	else if (name == SystemLanguage.English)
		return LanguageCode.EN;
	else if (name == SystemLanguage.Estonian)
		return LanguageCode.ET;
	else if (name == SystemLanguage.Faroese)
		return LanguageCode.FA;
	else if (name == SystemLanguage.Finnish)
		return LanguageCode.FI;
	else if (name == SystemLanguage.French)
		return LanguageCode.FR;
	else if (name == SystemLanguage.German)
		return LanguageCode.DE;
	else if (name == SystemLanguage.Greek)
		return LanguageCode.EL;
	else if (name == SystemLanguage.Hebrew)
		return LanguageCode.HE;
	else if (name == SystemLanguage.Hungarian)
		return LanguageCode.HU;
	else if (name == SystemLanguage.Icelandic)
		return LanguageCode.IS;
	else if (name == SystemLanguage.Indonesian)
		return LanguageCode.ID;
	else if (name == SystemLanguage.Italian)
		return LanguageCode.IT;
	else if (name == SystemLanguage.Japanese)
		return LanguageCode.JA;
	else if (name == SystemLanguage.Korean)
		return LanguageCode.KO;
	else if (name == SystemLanguage.Latvian)
		return LanguageCode.LA;
	else if (name == SystemLanguage.Lithuanian)
		return LanguageCode.LT;
	else if (name == SystemLanguage.Norwegian)
		return LanguageCode.NO;
	else if (name == SystemLanguage.Polish)
		return LanguageCode.PL;
	else if (name == SystemLanguage.Portuguese)
		return LanguageCode.PT;
	else if (name == SystemLanguage.Romanian)
		return LanguageCode.RO;
	else if (name == SystemLanguage.Russian)
		return LanguageCode.RU;
	else if (name == SystemLanguage.SerboCroatian)
		return LanguageCode.SH;
	else if (name == SystemLanguage.Slovak)
		return LanguageCode.SK;
	else if (name == SystemLanguage.Slovenian)
		return LanguageCode.SL;
	else if (name == SystemLanguage.Spanish)
		return LanguageCode.ES;
	else if (name == SystemLanguage.Swedish)
		return LanguageCode.SW;
	else if (name == SystemLanguage.Thai)
		return LanguageCode.TH;
	else if (name == SystemLanguage.Turkish)
		return LanguageCode.TR;
	else if (name == SystemLanguage.Ukrainian)
		return LanguageCode.UK;
	else if (name == SystemLanguage.Vietnamese)
		return LanguageCode.VI;
	else if (name == SystemLanguage.Hungarian)
		return LanguageCode.HU;
	else if (name == SystemLanguage.Unknown)
		return LanguageCode.Unassigned; 
	return LanguageCode.Unassigned;
}
}