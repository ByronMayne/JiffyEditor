using System;
using UnityEngine;
using System.Collections.Generic;

public class LangSettings : ScriptableObject
{
	[Serializable]
	public class LangItem
	{
		public Lang.LanguageCode langCode = Lang.LanguageCode.Unassigned;
		public bool included = true;
	}

	[Serializable]
	public class LangDictionary
	{
		public string aliasName = "";
		public string resourcePath = "";
	}

	[Serializable]
	public class GoogleDoc
	{
		public string fileName = "";
		public string publicKey = "";

		public string googleURL {
			get {
				return "https://docs.google.com/spreadsheet/ccc?key=" + publicKey;
			}
		}

		public string odsURL {
			get {
				return "https://docs.google.com/spreadsheet/pub?key=" + publicKey + "&output=ods";
			}
		}

		public string odsUnityPath {
			get {
				return Application.dataPath + "/Resources Localization/" + fileName + ".ods";
			}
		}
	}

	public Lang.LanguageCode defaultLanguage;
	public bool forceLanguage = false;
	public Lang.LanguageCode forcedLanguage = Lang.LanguageCode.EN;

	public List<LangItem> supportedLanguages = new List<LangItem> ();
	public List<GoogleDoc> googleDocs = new List<GoogleDoc> ();
	public List<LangDictionary> dictionaries = new List<LangDictionary> ();
	public Dictionary<string,string> dontserialzie = new Dictionary<string, string>();

	public bool IsSupportedLanguage (Lang.LanguageCode langCode)
	{
		foreach (LangItem item in supportedLanguages) {
			if (item.included && item.langCode == langCode)
				return true;
		}
		return false;
	}
}