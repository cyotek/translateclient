#region License block : MPL 1.1/GPL 2.0/LGPL 2.1
/* ***** BEGIN LICENSE BLOCK *****
 * Version: MPL 1.1/GPL 2.0/LGPL 2.1
 *
 * The contents of this file are subject to the Mozilla Public License Version
 * 1.1 (the "License"); you may not use this file except in compliance with
 * the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS" basis,
 * WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
 * for the specific language governing rights and limitations under the
 * License.
 *
 * The Original Code is the FreeCL.Net library.
 *
 * The Initial Developer of the Original Code is 
 *  Oleksii Prudkyi (Oleksii.Prudkyi@gmail.com).
 * Portions created by the Initial Developer are Copyright (C) 2008
 * the Initial Developer. All Rights Reserved.
 *
 * Contributor(s):
 *
 * Alternatively, the contents of this file may be used under the terms of
 * either the GNU General Public License Version 2 or later (the "GPL"), or
 * the GNU Lesser General Public License Version 2.1 or later (the "LGPL"),
 * in which case the provisions of the GPL or the LGPL are applicable instead
 * of those above. If you wish to allow use of your version of this file only
 * under the terms of either the GPL or the LGPL, and not to allow others to
 * use your version of this file under the terms of the MPL, indicate your
 * decision by deleting the provisions above and replace them with the notice
 * and other provisions required by the GPL or the LGPL. If you do not delete
 * the provisions above, a recipient may use your version of this file under
 * the terms of any one of the MPL, the GPL or the LGPL.
 *
 * ***** END LICENSE BLOCK ***** */
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Translate.DictD;
using System.Text;
using Translate;

namespace EnumDictd
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		class DictionaryInfo
		{
			public DictionaryInfo(string name, string description)
			{
				this.name = name;
				this.description = description;
			}
			
			List<Uri> urls = new List<Uri>();
			public List<Uri> Urls
			{
				get { return urls; }
			}
			
			string name;
			public string Name
			{
				get { return name; }
				set { name = value; }
			}
			
			string description;
			public string Description
			{
				get { return description; }
				set { description = value; }
			}
			
			string descriptionRu;
			public string DescriptionRu
			{
				get { return descriptionRu; }
				set { descriptionRu = value; }
			}
			
			string descriptionUa;
			public string DescriptionUa
			{
				get { return descriptionUa; }
				set { descriptionUa = value; }
			}

			
			LanguagePairCollection supportedTranslations = new LanguagePairCollection();
			
			public ReadOnlyLanguagePairCollection SupportedTranslations
			{
				get{return new ReadOnlyLanguagePairCollection(supportedTranslations);}
			}
			
			protected internal void AddSupportedTranslation(LanguagePair languagePair)
			{
				supportedTranslations.Add(languagePair);
				
				if(baseType == "DictDMonolingualDictionary" && languagePair.From != languagePair.To)
					baseType = "DictDBilingualDictionary";
			}
	
			protected internal void AddSupportedTranslation(Language from, Language to)
			{
				AddSupportedTranslation(new LanguagePair(from ,to));
			}
			
			protected internal void AddSupportedTranslationToEnglish(Language from)
			{
				AddSupportedTranslation(from, Language.English);
				AddSupportedTranslation(from, Language.English_GB);
				AddSupportedTranslation(from, Language.English_US);
			}
	
			protected internal void AddSupportedTranslationFromEnglish(Language to)
			{
				AddSupportedTranslation(Language.English, to);
				AddSupportedTranslation(Language.English_GB, to);
				AddSupportedTranslation(Language.English_US, to);
			}
			
			SubjectCollection supportedSubjects = new SubjectCollection();
			public ReadOnlySubjectCollection SupportedSubjects {
				get { return new ReadOnlySubjectCollection(supportedSubjects); }
			}
			
			protected internal void AddSupportedSubject(string subject)
			{
				supportedSubjects.Add(subject);
			}
			
			string baseType = "DictDMonolingualDictionary";
			public string BaseType
			{
				get { return baseType; }
				set { baseType = value; }
			}
			
			bool ignore;
			public bool Ignore
			{
				get { return ignore; }
				set { ignore = value; }
			}
			
		}
		
		class DictionaryClientComparerByConnectTicks : IComparer<DictionaryClient>
		{
			public int Compare(DictionaryClient x, DictionaryClient y)
			{
				return (int)(x.ConnectTicks - y.ConnectTicks);
			}
		}

		class DictionaryInfoComparerByUrlsCount : IComparer<DictionaryInfo>
		{
			public int Compare(DictionaryInfo x, DictionaryInfo y)
			{
				//if(y.Urls.Count != x.Urls.Count)
				//	return (int)(y.Urls.Count - x.Urls.Count);
				//else 	
					return string.Compare(x.Name, y.Name);
			}
		}
		
		void BGenClick(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			
			//http://luetzschena-stahmeln.de/dictd/index.php
			List<string> uris = new List<string>();
			uris.Add("dict://dict.org");
			uris.Add("dict://dict1.us.dict.org");
			uris.Add("dict://alt0.dict.org");
			//uris.Add("dict://vocabulary.aioe.org");
			//uris.Add("dict://dict.aioe.org");
			uris.Add("dict://dict.tugraz.at");
			
			uris.Add("dict://dict.tu-chemnitz.de");
			//uris.Add("dict://dict.die.net"); ??? http works
			// don't needed ??? uris.Add("dict://dict.lojban.org");
			uris.Add("dict://dict.arabeyes.org");
			uris.Add("dict://dict.saugus.net");
			uris.Add("dict://dictionary.bishopston.net");
			uris.Add("dict://la-sorciere.de");
			//uris.Add("dict://lividict.org"); //no url
			uris.Add("dict://dictd.xdsl.by");
			//alternate for xdsl.by 
			uris.Add("dict://mova.org");
			//uris.Add("dict://dict.uni-leipzig.de");??? http works
			//uris.Add("dict://dict.poboxes.info");??? http works
			uris.Add("dict://dict.dvo.ru");
			//uris.Add("dict://indica-et-buddhica.org");??? http works but not what needed
			uris.Add("dict://linicks.net");
			uris.Add("dict://nihongobenkyo.org");
			//wikipedia2dict uris.Add("dict://dict.hewgill.com");
			uris.Add("dict://dict.bober757.org.ru");
			//uris.Add("dict://dict.tw");
			
			
			List<Uri> urls = new List<Uri>();
			foreach(string str in uris)
				urls.Add(new Uri(str)); 
			

			sb.AppendLine("/*");
			sb.AppendLine("Code automatically generated by EnumDictd tool at " + DateTime.Now.ToString());
			sb.AppendLine("");
			sb.AppendLine("Errors : ");		
			string mess;

			List<DictionaryClient> clients = new List<DictionaryClient>();
			foreach(Uri url in urls)
			{
				DictionaryClient dc = new DictionaryClient();
				dc.Url = url;
				dc.AutoConnect = false;
				try
				{
					dc.Connected = true;
					clients.Add(dc);
				}
				catch(Exception ex)
				{
					Application.DoEvents();
					mess = dc.Url + " : " + ex.Message;
					if(ex.InnerException != null)
						mess += " : " + ex.InnerException.Message;
					
					sb.AppendLine(mess);
					Application.DoEvents();
				}
				Application.DoEvents();	
			}
			Application.DoEvents();
			clients.Sort(new DictionaryClientComparerByConnectTicks());
			
			DateTime tmpTime;
			sb.AppendLine("Connected : ");
			foreach(DictionaryClient dc in clients)
			{
				tmpTime = new DateTime(dc.ConnectTicks);
				sb.AppendLine(dc.Url + " : " + tmpTime.ToString("ss.fffffff"));;
			}
			
			Dictionary<string, DictionaryInfo> infos = new Dictionary<string, DictionaryInfo>();
			sb.AppendLine("Databases : ");;
			foreach(DictionaryClient dc in clients)
			{
				Application.DoEvents();
				//sb.AppendLine(dc.Url + " : ");
				
				DatabaseCollection dbs = dc.GetDatabases();
				foreach(Database db in dbs)
				{
					//sb.AppendLine("\t"  + db.Name + " : " + db.Description);
					Application.DoEvents();
					
					DictionaryInfo di;
					if(!infos.TryGetValue(db.Name, out di))
					{
						di = new DictionaryInfo(db.Name, db.Description);
						infos.Add(db.Name, di);
					}	
					di.Urls.Add(dc.Url);
				}
			}
			
			//update datas 
			
			//DictdGcideDictionary
			DictionaryInfo d = infos["gcide"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			//DictdElementsDictionary
			d = infos["elements"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Chemistry);
			
			//DictdJargonDictionary
			d = infos["jargon"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			
			//DictdFoldocDictionary
			d = infos["foldoc"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			
			
			//DictdVeraDictionary()
			d = infos["vera"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			

			//DictdWnDictionary
			infos["wn"].Ignore = true;
			
			//DictdEastonDictionary
			d = infos["easton"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Bible);

			//DictdHitchcockDictionary			
			d = infos["hitchcock"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Bible);

			//DictdWorld02Dictionary
			d = infos["world02"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Economy);
			d.AddSupportedSubject(SubjectConstants.Places);
			
			
			//DictdWorld95Dictionary
			infos["world95"].Ignore = true;

			
			//DictdGazetteerDictionary
			d = infos["gazetteer"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Places);

			//DictdGazZipDictionary
			d = infos["gaz-zip"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Places);

			//DictdGazPlaceDictionary
			d = infos["gaz-place"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Places);
			
			//DictdGazCountyDictionary
			d = infos["gaz-county"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Places);

			//DictdDevilsDictionary
			d = infos["devils"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Humour);

			//DictdBouvierDictionary
			d = infos["bouvier"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Law);
			
			//Moby Thesaurus II by Grady Ward, 1.0 : 
			d = infos["moby-thes"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.BaseType = "DictDSynonymsDictionary";
			
			infos["web1913"].Ignore = true; //old webster repleced by gcide
			infos["devil"].Ignore = true; //already exists
			
			//virtuals - dict-org
			infos["english"].Ignore = true;
			infos["trans"].Ignore = true;
			infos["all"].Ignore = true;
			infos["--exit--"].Ignore = true;
			
			//gazetter
			infos["gaz"].Ignore = true;
			infos["gaz2k-counties"].Ignore = true;
			infos["gaz2k-places"].Ignore = true;
			infos["gaz2k-zips"].Ignore = true;
			infos["gazetteer2k-counties"].Ignore = true;
			infos["gazetteer2k-places"].Ignore = true;
			infos["gazetteer2k-zips"].Ignore = true;
			
			
			
			
			//ignore all freedicts
			List<string> fdnames = new List<string>();  
			fdnames.Add("afr");
			fdnames.Add("ara");
			fdnames.Add("cro");
			fdnames.Add("cze");
			fdnames.Add("dan");
			fdnames.Add("deu");
			fdnames.Add("eng");
			fdnames.Add("fra");
			fdnames.Add("gla");
			fdnames.Add("hin");
			fdnames.Add("hun");
			fdnames.Add("iri");
			fdnames.Add("ita");
			fdnames.Add("jpn");
			fdnames.Add("kha");
			fdnames.Add("lat");
			fdnames.Add("nld");
			fdnames.Add("por");
			fdnames.Add("rom");
			fdnames.Add("rus");
			fdnames.Add("sco");
			fdnames.Add("scr");
			fdnames.Add("slo");
			fdnames.Add("spa");
			fdnames.Add("swa");
			fdnames.Add("swe");
			fdnames.Add("tur");
			fdnames.Add("wel");
			
			List<string> fdnames2 = new List<string>();
			fdnames2.AddRange(fdnames);
			
			//ignore all freedicts
			foreach(string fd_from in fdnames)
				foreach(string fd_to in fdnames2)
				{
					if(infos.TryGetValue(fd_from + "-" + fd_to, out d))
						d.Ignore = true;
					
					if(infos.TryGetValue("fd-" + fd_from + "-" + fd_to, out d))
						d.Ignore = true;
						
					if(infos.TryGetValue("freedict-" + fd_from + "-" + fd_to, out d))
						d.Ignore = true;
						
				}
				
			//enable some freedicts
			
			d = infos["ara-eng"];
			d.AddSupportedTranslationToEnglish(Language.Arabic);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["cro-eng"];
			d.AddSupportedTranslationToEnglish(Language.Croatian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["deu-eng"];
			d.AddSupportedTranslationToEnglish(Language.German);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["deu-nld"];
			d.AddSupportedTranslationToEnglish(Language.Dutch);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["eng-ara"];
			d.AddSupportedTranslationFromEnglish(Language.Arabic);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-cro"];
			d.AddSupportedTranslationFromEnglish(Language.Croatian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-cze"];
			d.AddSupportedTranslationFromEnglish(Language.Czech);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-deu"];
			d.AddSupportedTranslationFromEnglish(Language.German);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-hin"];
			d.AddSupportedTranslationFromEnglish(Language.Hindi);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-hun"];
			d.AddSupportedTranslationFromEnglish(Language.Hungarian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-por"];
			d.AddSupportedTranslationFromEnglish(Language.Portuguese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["eng-tur"];
			d.AddSupportedTranslationFromEnglish(Language.Turkish);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["hin-eng"];
			d.AddSupportedTranslationToEnglish(Language.Hindi);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;

			d = infos["hun-eng"];
			d.AddSupportedTranslationToEnglish(Language.Hungarian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["nld-deu"];
			d.AddSupportedTranslation(Language.Dutch, Language.German);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["nld-eng"];
			d.AddSupportedTranslationToEnglish(Language.Dutch);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["nld-fra"];
			d.AddSupportedTranslation(Language.Dutch, Language.French);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			d = infos["por-eng"];
			d.AddSupportedTranslationToEnglish(Language.Portuguese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Ignore = false;
			
			//http://dictionary.bishopston.net/?Form=/4
			infos["english-welsh"].Ignore = true;
			infos["welsh-english"].Ignore = true;
			infos["moby-thesaurus"].Ignore = true;
			
			
			d = infos["stardic"];
			d.AddSupportedTranslationFromEnglish(Language.Chinese);
			d.AddSupportedTranslationFromEnglish(Language.Chinese_CN);
			d.AddSupportedSubject(SubjectConstants.Common);

			d = infos["xdict"];
			d.AddSupportedTranslationFromEnglish(Language.Chinese);
			d.AddSupportedTranslationFromEnglish(Language.Chinese_CN);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			
			
			//dictd.xdsl.by
			infos["mech"].Ignore = true;
			infos["en-ru"].Ignore = true;
			infos["ru-en"].Ignore = true;
			infos["ru-ru"].Ignore = true;
			infos["de-ru"].Ignore = true;
			infos["ru-de"].Ignore = true;
			infos["uk-ru"].Ignore = true;
			infos["ru-uk"].Ignore = true;
			infos["et-ru"].Ignore = true;
			infos["fi-ru"].Ignore = true;
			
			d = infos["1000pbio"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);

			d = infos["abr1w"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.BaseType = "DictDSynonymsDictionary";
			
			d = infos["ahiezer"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);

			d = infos["abr1w"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);

			d = infos["aviation"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Aviation);
			
			d = infos["beslov"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["biology"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Biology);

			d = infos["brok_and_efr"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);

			d = infos["church"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Bible);
			
			infos["compbe"].Ignore = true;
			
			d = infos["dalf"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["deutsch_de-ru"];
			d.AddSupportedTranslation(Language.German, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);

			d = infos["deutsch_ru-de"];
			d.AddSupportedTranslation(Language.Russian, Language.German);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["engcom"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			d.Ignore = false;

			d = infos["estonian_et-ru"];
			d.AddSupportedTranslation(Language.Estonian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["ethnographic"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);
			
			d = infos["findict"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Bank);
			d.AddSupportedSubject(SubjectConstants.Business);
			d.AddSupportedSubject(SubjectConstants.Economy);

			d = infos["geology_en-ru"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Geology);
			
			d = infos["geology_ru-en"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Geology);

			d = infos["hi127"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);
			d.AddSupportedSubject(SubjectConstants.Bible);

			d = infos["idioms"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);


			infos["komi-rus"].Ignore = true; //Komi-Russian Dictionary
	
			d = infos["korolew_en-ru"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary - N.Korolew";
			
			d = infos["korolew_ru-en"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Russian-English dictionary - N.Korolew";
	
			d = infos["latrus"];
			d.AddSupportedTranslation(Language.Latin, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["magus"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			infos["mech_mime"].Ignore = true; 
			
			d = infos["mech_nomime"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary of science and mech";
			
			d = infos["meddict"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Medicine);
			
			
			d = infos["mueller24"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			infos["mueller7"].Ignore = true; 
			
			d = infos["muiswerk"];
			d.AddSupportedTranslation(Language.Dutch, Language.Dutch);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Dutch monolingual dictionary, Copyright (C) 1995-1999 Muiswerk Educatief (www.muiswerk.nl)";

			d = infos["ozhegov"]; //Толковый словарь Ожегова
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["ozhshv"]; //Толковый словарь русского языка Ожегова и Шведовой
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);

			d = infos["religion"]; //История религии
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);

			d = infos["teo"]; //Теософский словарь
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.History);
			//d.Description = "Theosophical dictionary";
			
			d = infos["sc-abbr"]; //Computer Science Abbreviations
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);

			d = infos["sdict_fi-ru"]; //Finnish-Russian dictionary
			d.AddSupportedTranslation(Language.Finnish, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["sdict_ru-en"]; //Russian-English full dictionary
			d.AddSupportedTranslation(Language.Russian, Language.Finnish);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			
			d = infos["ses"]; //Современный энциклопедический словарь
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			infos["sinyagin_abbrev"].Ignore = true;
			
			/*d = infos["sinyagin_abbrev"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary of abbreviations by Sinyagin";
			*/

			d = infos["sinyagin_alexeymavrin"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary by Sinyagin/Alexey Mavrin";
			
			d = infos["sinyagin_business"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Business);
			d.AddSupportedSubject(SubjectConstants.Bank);
			d.AddSupportedSubject(SubjectConstants.Economy);
			d.Description = "English-Russian dictionary of business by Sinyagin";
			
			d = infos["sinyagin_computer"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			d.Description = "English-Russian dictionary of computer science by Sinyagin";
			
			d = infos["sinyagin_general_er"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary by Sinyagin";
			
			d = infos["sinyagin_general_re"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Russian-English dictionary by Sinyagin";
			

			d = infos["smiley"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			d.Description = "Dictionary of smileys";
			
			d = infos["ushakov"]; //Толковый словарь Ушакова
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			//d.Description = "Explanatory Dictionary of Ushakov";
			 
			infos["zhelezyaki_abbr"].Ignore = true; 
			infos["zhelezyaki_analogs"].Ignore = true; 
			 
			 //dict://dict.saugus.net/
			d = infos["SN-CT"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);

			d = infos["SN-FE"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);
			 
			d = infos["SN-FS"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);

			infos["SN-Sa"].Ignore = true;
			/*d = infos["SN-Sa"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.Informatics);
			d.AddSupportedSubject(SubjectConstants.Internet);*/
			 
			 
			d = infos["sokrat_en-ru"];
			d.AddSupportedTranslationFromEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "English-Russian dictionary (Sokrat)";
			
			d = infos["sokrat_ru-en"];
			d.AddSupportedTranslationToEnglish(Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Russian-English dictionary (Sokrat)";


			d = infos["swedish_ru-sv"];
			d.AddSupportedTranslation(Language.Russian, Language.Swedish);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			d = infos["swedish_sv-ru"];
			d.AddSupportedTranslation(Language.Swedish, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Common);

			//dict://linicks.net/
			
			//Shakespearean Words and Meanings
			d = infos["Shakespeare"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.History);
			
			
			 
			//dict://dict.arabeyes.org/			
			d = infos["arabic"];
			d.AddSupportedTranslationFromEnglish(Language.Arabic);
			d.AddSupportedSubject(SubjectConstants.Common);
			
			//dict://nihongobenkyo.org/ japanese
			d = infos["edict-fr"];
			d.AddSupportedTranslation(Language.Japanese, Language.French);
			d.AddSupportedTranslation(Language.French, Language.Japanese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Japanese to French dictionary";
			
			d = infos["jmdict"];
			d.AddSupportedTranslationFromEnglish(Language.Japanese);
			d.AddSupportedTranslationToEnglish(Language.Japanese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Japanese to English dictionary";
			
			d = infos["kanjidic2"];
			d.AddSupportedTranslationFromEnglish(Language.Japanese);
			d.AddSupportedTranslationToEnglish(Language.Japanese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Kanji dictionary";

			d = infos["jmnedict"];
			d.AddSupportedTranslation(Language.Japanese, Language.Japanese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Japanese proper names dictionary";
			
			d = infos["tanaka_corpus"];
			d.AddSupportedTranslationToEnglish(Language.Japanese);
			d.AddSupportedSubject(SubjectConstants.Common);
			d.Description = "Japanese/English sentence examples";
			//d.BaseType = "";
			
			infos["tanaka_corpus_subset"].Ignore = true;
			
			//dict://dict.dvo.ru/
			infos["en-en--ru-ru"].Ignore = true;
			infos["ru-en--en-ru"].Ignore = true;
			infos["slovniks"].Ignore = true;
			
			
			d = infos["obi-bio"];
			d.AddSupportedTranslation(Language.English, Language.English);
			d.AddSupportedTranslation(Language.English_GB, Language.English_GB);
			d.AddSupportedTranslation(Language.English_US, Language.English_US);
			d.AddSupportedSubject(SubjectConstants.History);
			d.Description = "Biographic reference book";
			
			infos["mueller-abbrev"].Ignore = true; 
			infos["mueller-base"].Ignore = true; 
			infos["mueller-dict"].Ignore = true; 
			infos["mueller-geo"].Ignore = true; 
			infos["mueller-names"].Ignore = true; 
			
 
 			d = infos["bus"];
			d.AddSupportedTranslation(Language.Russian, Language.Russian);
			d.AddSupportedSubject(SubjectConstants.Law);

			//dict://dict.tugraz.at
			// already supported http://dict.tu-chemnitz.de/
			infos["english-german"].Ignore = true;
			infos["german-english"].Ignore = true;

			
			foreach(string debname in infos.Keys)
			{
				//disable any debian dicts 
				if(debname.StartsWith("debian") || debname.StartsWith("dward") )
					infos[debname].Ignore = true;
					
				//slovnyk 	
				if(debname.StartsWith("slovnyk_"))	
					infos[debname].Ignore = true;
			}

			//translations
			d = infos["1000pbio"];
			d.Description = "1000+ brief biographical data";
			d.DescriptionRu = "1000+ кратких биографических данных";
			d.DescriptionUa = "1000+ коротких біографічних даних";

			d = infos["abr1w"];
			d.Description = "Dictionary of synonyms of N.Abramov";
			d.DescriptionRu = "Словарь синонимов Н.Абрамова";
			d.DescriptionUa = "Словник синонімів Н.Абрамова";

			d = infos["ahiezer"];
			d.Description = "Terms of book «Criticism of historical experience» of A. Akhiezer";
			d.DescriptionRu = "Термины книги «Критика исторического опыта» А.С.Ахиезера";
			d.DescriptionUa = "Терміни книги «Критика історичного досвіду» А.С.Ахиезера";

			d = infos["arabic"];
			d.Description = "Arabeyes English/Arabic dictionary";
			d.DescriptionRu = "Английско-арабский словарь Arabeyes";
			d.DescriptionUa = "Англійсько-Арабський словник Arabeyes";

			d = infos["ara-eng"];
			d.Description = "Arabic-English Freedict Dictionary";
			d.DescriptionRu = "Арабско-английский словарь Freedict";
			d.DescriptionUa = "Арабсько-англійський словник Freedict";

			d = infos["aviation"];
			d.Description = "Russian abbreviations for aviation";
			d.DescriptionRu = "Словарь русских авиационных аббревиатур";
			d.DescriptionUa = "Словник російських авіаційних скороченнь";

			d = infos["beslov"];
			d.Description = "Large Encyclopedic Dictionary";
			d.DescriptionRu = "Большой Энциклопедический Словарь";
			d.DescriptionUa = "Великий Енциклопедичний Словник";

			d = infos["biology"];
			d.Description = "Russian-English Dictionary of Biology";
			d.DescriptionRu = "Русско-английский биологический словарь";
			d.DescriptionUa = "Російсько-англійський біологічний словник";

			d = infos["bouvier"];
			d.Description = "Bouvier's Law Dictionary, Revised 6th Ed (1856)";
			d.DescriptionRu = "Юридический словарь Боувера, 6-я редакция (Bouvier's Law Dictionary, 1856)";
			d.DescriptionUa = "Юридичний словник Боувера, 6-а редакція (Bouvier's Law Dictionary, 1856)";

			d = infos["brok_and_efr"];
			d.Description = "Brockhaus and Efron's encyclopaedic dictionary";
			d.DescriptionRu = "Энциклопедический словарь Брокгауза и Ефрона";
			d.DescriptionUa = "Енциклопедичний словник Брокгауза і Ефрона";

			d = infos["bus"];
			d.Description = "Big dictionary of law";
			d.DescriptionRu = "Большой юридический словарь";
			d.DescriptionUa = "Великий юридичний словник";

			d = infos["church"];
			d.Description = "Dictionary of the church terms";
			d.DescriptionRu = "Словарь церковных терминов";
			d.DescriptionUa = "Словник церковних термінів";

			d = infos["cro-eng"];
			d.Description = "Croatian-English Freedict Dictionary";
			d.DescriptionRu = "Хорватско-английский словарь Freedict";
			d.DescriptionUa = "Хорватсько-англійський словник Freedict";

			d = infos["dalf"];
			d.Description = "Explanatory dictionary of live Russian language of V.I.Dalja";
			d.DescriptionRu = "Толковый словарь живого великорусского языка В.И. Даля";
			d.DescriptionUa = "Тлумачний словник живої великоросійської мови В.І. Даля";

			d = infos["deu-eng"];
			d.Description = "German-English Freedict dictionary";
			d.DescriptionRu = "Немецко-английский словарь Freedict";
			d.DescriptionUa = "Германсько-англійський словник Freedict";

			d = infos["deu-nld"];
			d.Description = "German-Nederland Freedict dictionary";
			d.DescriptionRu = "Немецко-голландский словарь Freedict";
			d.DescriptionUa = "Германсько-голандський словник Freedict";

			d = infos["deutsch_de-ru"];
			d.Description = "Deutsch-Russian dictionary";
			d.DescriptionRu = "Немецко-русский словарь";
			d.DescriptionUa = "Германсько-російський словник";

			d = infos["deutsch_ru-de"];
			d.Description = "Russian-Deutsch dictionary";
			d.DescriptionRu = "Русско-немецкий словарь";
			d.DescriptionUa = "Російсько-германський словник";

			d = infos["devils"];
			d.Description = "THE DEVIL'S DICTIONARY ((C)1911 Released April 15 1993)";
			d.DescriptionRu = "Шутливый английский толковый словарь Девила (THE DEVIL'S DICTIONARY (C)1911 Released April 15 1993)";
			d.DescriptionUa = "Жартівливий англійський тлумачний словник Девіла (THE DEVIL'S DICTIONARY (C)1911 Released April 15 1993)";

			d = infos["easton"];
			d.Description = "Easton's 1897 Bible Dictionary";
			d.DescriptionRu = "Справочник по Библии Истона (Easton's 1897)";
			d.DescriptionUa = "Довідник по Біблії Істона (Easton's 1897)";

			d = infos["edict-fr"];
			d.Description = "Japanese to French dictionary";
			d.DescriptionRu = "Японско-французский словарь";
			d.DescriptionUa = "Японсько-французький словник";

			d = infos["elements"];
			d.Description = "The Elements (22Oct97)";
			d.DescriptionRu = "Английский справочник по таблице Менделеева";
			d.DescriptionUa = "Англійський довідник по таблиці Менделєєва";

			d = infos["eng-ara"];
			d.Description = "English-Arabic FreeDict Dictionary";
			d.DescriptionRu = "Английско-арабский словарь Freedict";
			d.DescriptionUa = "Англійсько-арабський словник Freedict";

			d = infos["engcom"];
			d.Description = "The Open English-Russian Computer Dictionary";
			d.DescriptionRu = "Открытый английско-русский компьютерный словарь";
			d.DescriptionUa = "Відкритий англійсько-російський комп'ютерний словник";

			d = infos["eng-cro"];
			d.Description = "English-Croatian Freedict Dictionary";
			d.DescriptionRu = "Английско-хорватский словарь Freedict";
			d.DescriptionUa = "Англійсько-хорватський словник Freedict";

			d = infos["eng-cze"];
			d.Description = "English-Czech Freedict Dictionary";
			d.DescriptionRu = "Английско-чешский словарь Freedict";
			d.DescriptionUa = "Англійсько-чеський словник Freedict";

			d = infos["eng-deu"];
			d.Description = "English-German Freedict dictionary";
			d.DescriptionRu = "Английско-немецкий словарь Freedict";
			d.DescriptionUa = "Англійсько-германський словник Freedict";

			d = infos["eng-hin"];
			d.Description = "English-Hindi Freedict Dictionary";
			d.DescriptionRu = "Словарь Freedict с английского на хинди";
			d.DescriptionUa = "Словник Freedict з англійської на хінді";

			d = infos["eng-hun"];
			d.Description = "English-Hungarian Freedict Dictionary";
			d.DescriptionRu = "Английско-венгерский словарь Freedict";
			d.DescriptionUa = "Англійсько-угорський словник Freedict";

			d = infos["eng-por"];
			d.Description = "English-Portugese Freedict dictionary";
			d.DescriptionRu = "Английско-португальский словарь Freedict";
			d.DescriptionUa = "Англійсько-португальський словник Freedict";

			d = infos["eng-tur"];
			d.Description = "English-Turkish FreeDict Dictionary";
			d.DescriptionRu = "Английско-турецкий словарь Freedict";
			d.DescriptionUa = "Англійсько-турецький словник Freedict";

			d = infos["estonian_et-ru"];
			d.Description = "Estonian-Russian dictionary";
			d.DescriptionRu = "Эстонско-русский словарь";
			d.DescriptionUa = "Естонсько-російський словник";

			d = infos["ethnographic"];
			d.Description = "Ethnographic dictionary";
			d.DescriptionRu = "Этнографический словарь";
			d.DescriptionUa = "Етнографічний словник";

			d = infos["findict"];
			d.Description = "Dictionary of the financial terms";
			d.DescriptionRu = "Словарь финансовых терминов";
			d.DescriptionUa = "Словник фінансових термінів";

			d = infos["foldoc"];
			d.Description = "The Free On-line Dictionary of Computing (15Feb98)";
			d.DescriptionRu = "Свободный словарь вычислительной техники";
			d.DescriptionUa = "Вільний словник обчислювальної техніки";

			d = infos["gaz-county"];
			d.Description = "U.S. Gazetteer Counties (2000)";
			d.DescriptionRu = "U.S. Gazetteer Counties (2000)";
			d.DescriptionUa = "U.S. Gazetteer Counties (2000)";

			d = infos["gazetteer"];
			d.Description = "U.S. Gazetteer (1990)";
			d.DescriptionRu = "U.S. Gazetteer (1990)";
			d.DescriptionUa = "U.S. Gazetteer (1990)";

			d = infos["gaz-place"];
			d.Description = "U.S. Gazetteer Places (2000)";
			d.DescriptionRu = "U.S. Gazetteer Places (2000)";
			d.DescriptionUa = "U.S. Gazetteer Places (2000)";

			d = infos["gaz-zip"];
			d.Description = "U.S. Gazetteer Zip Code Tabulation Areas (2000)";
			d.DescriptionRu = "U.S. Gazetteer Zip Code Tabulation Areas (2000)";
			d.DescriptionUa = "U.S. Gazetteer Zip Code Tabulation Areas (2000)";

			d = infos["gcide"];
			d.Description = "The Collaborative International Dictionary of English v.0.48";
			d.DescriptionRu = "Совместный международный словарь английского языка v.0.48";
			d.DescriptionUa = "Спільний міжнародний словник англійської мови v.0.48";

			d = infos["geology_en-ru"];
			d.Description = "Geological English-Russian dictionary";
			d.DescriptionRu = "Геологохимический англорусский словарь";
			d.DescriptionUa = "Геологічний англо-російський словник";

			d = infos["geology_ru-en"];
			d.Description = "Geological Russian-English dictionary";
			d.DescriptionRu = "Геологохимический русско-английський словарь";
			d.DescriptionUa = "Геологічний російсько-англійський словник";

			d = infos["hi127"];
			d.Description = "The dictionary-pointer on Old Russian art";
			d.DescriptionRu = "Словарь-указатель по древнерусскому искусству";
			d.DescriptionUa = "Словник-покажчик по староруському мистецтву";

			d = infos["hin-eng"];
			d.Description = "English-Hindi Freedict Dictionary [reverse index]";
			d.DescriptionRu = "Словарь Freedict с хинди на английский";
			d.DescriptionUa = "Словник Freedict з хінді на англійську";

			d = infos["hitchcock"];
			d.Description = "Hitchcock's Bible Names Dictionary (late 1800's)";
			d.DescriptionRu = "Справочник по библейским именам Хичкока (Hitchcock's Bible Names Dictionary (late 1800's))";
			d.DescriptionUa = "Довідник по біблійним іменам Хічкока (Hitchcock's Bible Names Dictionary (late 1800's))";

			d = infos["hun-eng"];
			d.Description = "Hungarian-English FreeDict Dictionary";
			d.DescriptionRu = "Венгерско-английский словарь Freedict";
			d.DescriptionUa = "Угорсько-англійський словник Freedict";

			d = infos["idioms"];
			d.Description = "Russian-English dictionary of idioms";
			d.DescriptionRu = "Русско-английский словарь идиом";
			d.DescriptionUa = "Російсько-англійський словник ідіом";

			d = infos["jargon"];
			d.Description = "The on-line hacker Jargon File, version 4.3.1, 29 JUN 2001";
			d.DescriptionRu = "Словарь хакерского жаргона (The on-line hacker Jargon File)";
			d.DescriptionUa = "Словник хакерського жаргону (The on-line hacker Jargon File)";

			d = infos["jmdict"];
			d.Description = "Japanese to English dictionary";
			d.DescriptionRu = "Японско-английский словарь";
			d.DescriptionUa = "Японсько-англійський словник";

			d = infos["jmnedict"];
			d.Description = "Japanese proper names dictionary";
			d.DescriptionRu = "Японский словарь имен собственных";
			d.DescriptionUa = "Словник японських власних імен";

			d = infos["kanjidic2"];
			d.Description = "Kanji dictionary";
			d.DescriptionRu = "Словарь кандзи";
			d.DescriptionUa = "Словник кандзі";

			d = infos["korolew_en-ru"];
			d.Description = "English-Russian dictionary - N.Korolew";
			d.DescriptionRu = "Английско-русский словарь Н.Королева";
			d.DescriptionUa = "Англійсько-російський словник Н.Корольова";

			d = infos["korolew_ru-en"];
			d.Description = "Russian-English dictionary - N.Korolew";
			d.DescriptionRu = "Русско-английский словарь Н.Королева";
			d.DescriptionUa = "Російсько-англійський словник Н.Корольова";

			d = infos["latrus"];
			d.Description = "Latin-Russian Dictionary";
			d.DescriptionRu = "Латинско-русский словарь";
			d.DescriptionUa = "Латинсько-російський словник";

			d = infos["magus"];
			d.Description = "New large English-Russian dictionary";
			d.DescriptionRu = "Новый Большой англо-русский словарь";
			d.DescriptionUa = "Новий Великий англо-російський словник";

			d = infos["mech_nomime"];
			d.Description = "English-Russian dictionary of science and mechanics";
			d.DescriptionRu = "Английско-русский словарь по науке и механике";
			d.DescriptionUa = "Англо-російський словник науки і механіки";

			d = infos["meddict"];
			d.Description = "Russian medical dictionary";
			d.DescriptionRu = "Русский медицинский словарь";
			d.DescriptionUa = "Російський медичний словник";

			d = infos["moby-thes"];
			d.Description = "Moby Thesaurus II by Grady Ward, 1.0";
			d.DescriptionRu = "Английский словарь синонимов (Moby Thesaurus II by Grady Ward, 1.0)";
			d.DescriptionUa = "Англійський словник синонімів (Moby Thesaurus II by Grady Ward, 1.0)";

			d = infos["mueller24"];
			d.Description = "English-Russian Dictionary by Professor V.K.Mueller, 24-th Revised Edition";
			d.DescriptionRu = "Англо-русский словарь В.К. Мюллера, 24-е издание";
			d.DescriptionUa = "Англо-російський словник В.К. Мюллера, 24-е видання";

			d = infos["muiswerk"];
			d.Description = "Dutch monolingual dictionary, Copyright (C) 1995-1999 Muiswerk Educatief (www.muiswerk.nl)";
			d.DescriptionRu = "Словарь голландского языка, Copyright (C) 1995-1999 Muiswerk Educatief (www.muiswerk.nl)";
			d.DescriptionUa = "Словник голландської мови, Copyright (C) 1995-1999 Muiswerk Educatief (www.muiswerk.nl)";

			d = infos["nld-deu"];
			d.Description = "Nederlands-German Freedict dictionary";
			d.DescriptionRu = "Голландско-немецкий словарь Freedict";
			d.DescriptionUa = "Голландсько-германський словник Freedict";

			d = infos["nld-eng"];
			d.Description = "Nederlands-English Freedict dictionary";
			d.DescriptionRu = "Голландско-английский словарь Freedict";
			d.DescriptionUa = "Голландсько-англійський словник Freedict";

			d = infos["nld-fra"];
			d.Description = "Nederlands-French Freedict dictionary";
			d.DescriptionRu = "Голландско-французский словарь Freedict";
			d.DescriptionUa = "Голландсько-французський словник Freedict";

			d = infos["obi-bio"];
			d.Description = "Biographic reference book";
			d.DescriptionRu = "Биографический справочник";
			d.DescriptionUa = "Біографічний довідник";

			d = infos["ozhegov"];
			d.Description = "Explanatory dictionary of Russian language by Ozhegov";
			d.DescriptionRu = "Толковый словарь Ожегова";
			d.DescriptionUa = "Тлумачний словник Ожегова";

			d = infos["ozhshv"];
			d.Description = "Explanatory Dictionary of Russian language by Ozhegov and Shvedova";
			d.DescriptionRu = "Толковый словарь русского языка Ожегова и Шведовой";
			d.DescriptionUa = "Тлумачний словник російської мови Ожегова і Шведової";

			d = infos["por-eng"];
			d.Description = "Portugese-English Freedict dictionary";
			d.DescriptionRu = "Португальско-английский словарь Freedict";
			d.DescriptionUa = "Португальсько-англійський словник Freedict";

			d = infos["religion"];
			d.Description = "History of religion";
			d.DescriptionRu = "История религии";
			d.DescriptionUa = "Історія релігії";

			d = infos["sc-abbr"];
			d.Description = "English-Russian Computer Science Abbreviations";
			d.DescriptionRu = "Англо-Русский Словарь Сокращений в Области Информационных Технологий";
			d.DescriptionUa = "Англо-російський cловник cкорочень в області інформаційних технологій";

			d = infos["sdict_fi-ru"];
			d.Description = "Finnish-Russian dictionary";
			d.DescriptionRu = "Финско-русский словарь";
			d.DescriptionUa = "Фінсько-російський словник";

			d = infos["sdict_ru-en"];
			d.Description = "Russian-English full dictionary";
			d.DescriptionRu = "Большой русско-английский словарь";
			d.DescriptionUa = "Великий російсько-англійський словник";

			d = infos["ses"];
			d.Description = "The modern encyclopaedic dictionary (Russian)";
			d.DescriptionRu = "Современный энциклопедический словарь";
			d.DescriptionUa = "Сучасний енциклопедичний словник";

			d = infos["Shakespeare"];
			d.Description = "Shakespearean Words and Meanings";
			d.DescriptionRu = "Словарь терминов из сочинений Шекспира (Shakespearean Words and Meanings)";
			d.DescriptionUa = "Словник термінів із творів Шекспіра (Shakespearean Words and Meanings)";

			d = infos["sinyagin_abbrev"];
			d.Description = "English-Russian dictionary of abbreviations by Sinyagin";
			d.DescriptionRu = "Английско-русский словарь сокращений Синягина";
			d.DescriptionUa = "Англо-російський словник скорочень Сінягіна";

			d = infos["sinyagin_alexeymavrin"];
			d.Description = "English-Russian dictionary by Sinyagin/Alexey Mavrin";
			d.DescriptionRu = "Английско-русский словарь Синягина/Алексея Маврина";
			d.DescriptionUa = "Англо-російський словник Сінягіна/Мавріна";

			d = infos["sinyagin_business"];
			d.Description = "English-Russian dictionary of business by Sinyagin";
			d.DescriptionRu = "Английско-русский бизнес-словарь Синягина";
			d.DescriptionUa = "Англо-російський бізнес-словник Сінягіна";

			d = infos["sinyagin_computer"];
			d.Description = "English-Russian dictionary of computer science by Sinyagin";
			d.DescriptionRu = "Английско-русский словарь по информатике Синягина";
			d.DescriptionUa = "Англо-російський словник по інформатиці Сінягіна";

			d = infos["sinyagin_general_er"];
			d.Description = "English-Russian dictionary by Sinyagin";
			d.DescriptionRu = "Английско-русский словарь Синягина";
			d.DescriptionUa = "Англо-російський словник Сінягіна";

			d = infos["sinyagin_general_re"];
			d.Description = "Russian-English dictionary by Sinyagin";
			d.DescriptionRu = "Русско-английский словарь Синягина";
			d.DescriptionUa = "Російсько-англійський словник Сінягіна";

			d = infos["smiley"];
			d.Description = "Dictionary of smileys";
			d.DescriptionRu = "Словарь смайлов (английский)";
			d.DescriptionUa = "Словник смайлів (англійська мова)";

			d = infos["SN-CT"];
			d.Description = "Computer Terms Glossary, 2008-08-25 (Copyright 1998-2008 Saugus.net)";
			d.DescriptionRu = "Словарь компьютерных терминов, (Copyright 1998-2008 Saugus.net)";
			d.DescriptionUa = "Словник комп'ютерних термінів, (Copyright 1998-2008 Saugus.net)";

			d = infos["SN-FE"];
			d.Description = "Filename Extensions List, 2008-08-25 (Copyright 1998-2008 Saugus.net)";
			d.DescriptionRu = "Каталог расширений файлов, (Copyright 1998-2008 Saugus.net)";
			d.DescriptionUa = "Каталог розширень файлів, (Copyright 1998-2008 Saugus.net)";

			d = infos["SN-FS"];
			d.Description = "Guide to Free Software, 2008-08-25 (Copyright 1998-2008 Saugus.net)";
			d.DescriptionRu = "Руководство по свободному программному обеспечению, (Copyright 1998-2008 Saugus.net)";
			d.DescriptionUa = "Керівництво по вільному програмному забезпеченню, (Copyright 1998-2008 Saugus.net)";

			/*d = infos["SN-Sa"];
			d.Description = "Saugus Related Terms, 2008-08-25 (Copyright 1998-2008 Saugus.net)";
			d.DescriptionRu = "";
			d.DescriptionUa = "";*/

			d = infos["sokrat_en-ru"];
			d.Description = "English-Russian dictionary (Sokrat)";
			d.DescriptionRu = "Английско-русский словарь - Sokrat";
			d.DescriptionUa = "Англо-російський словник - Sokrat";

			d = infos["sokrat_ru-en"];
			d.Description = "Russian-English dictionary (Sokrat)";
			d.DescriptionRu = "Русско-английский словарь - Sokrat";
			d.DescriptionUa = "Російсько-англійський словник - Sokrat";

			d = infos["stardic"];
			d.Description = "Stardic English-Chinese Dictionary";
			d.DescriptionRu = "Английско-китайский словарь Stardic";
			d.DescriptionUa = "Англійсько-китайський cловник Stardic";

			d = infos["swedish_ru-sv"];
			d.Description = "Russian-Swedish dictionary";
			d.DescriptionRu = "Русско-шведский словарь";
			d.DescriptionUa = "Російсько-шведський словник";

			d = infos["swedish_sv-ru"];
			d.Description = "Swedish-Russian dictionary";
			d.DescriptionRu = "Шведско-русский словарь";
			d.DescriptionUa = "Шведсько-російський словник";

			d = infos["tanaka_corpus"];
			d.Description = "Japanese-English sentence examples";
			d.DescriptionRu = "Японско-английский фразеологический словарь";
			d.DescriptionUa = "Японсько-англійський фразеологічний словник";

			d = infos["teo"];
			d.Description = "Theosophic dictionary (Russian)";
			d.DescriptionRu = "Теософский словарь";
			d.DescriptionUa = "Теософський словник";

			d = infos["ushakov"];
			d.Description = "Explanatory Dictionary of Ushakov";
			d.DescriptionRu = "Толковый словарь Ушакова";
			d.DescriptionUa = "Тлумачний словник Ушакова";

			d = infos["vera"];
			d.Description = "V.E.R.A. -- Virtual Entity of Relevant Acronyms (December 2003)";
			d.DescriptionRu = "Словарь компьютерных сокращений (Virtual Entity of Relevant Acronyms)";
			d.DescriptionUa = "Словник комп'ютерних скорочень (Virtual Entity of Relevant Acronyms)";

			d = infos["world02"];
			d.Description = "CIA World Factbook 2002";
			d.DescriptionRu = "Книга фактов ЦРУ 2002 (CIA World Factbook)";
			d.DescriptionUa = "Книга фактів ЦРУ 2002 (CIA World Factbook)";

			d = infos["xdict"];
			d.Description = "XDICT the English-Chinese dictionary";
			d.DescriptionRu = "Английско-китайский словарь XDICT";
			d.DescriptionUa = "Англійсько-китайський словник XDICT";
			
			List<DictionaryInfo> difs = new List<DictionaryInfo>();
			difs.AddRange(infos.Values);
			difs.Sort(new DictionaryInfoComparerByUrlsCount());
			
			string className;
			
			StringBuilder createSb = new StringBuilder();
			int unsupported_count = 0;
			int ignored_count = 0;
			
			foreach(DictionaryInfo di in difs)
			{
				if(!di.Ignore && di.SupportedTranslations.Count == 0)
				{
					mess = "Not supported : " + di.Name + " : " + di.Description + " : ";
					foreach(Uri url in di.Urls)
						mess += url + ", ";

					sb.AppendLine(mess);
					unsupported_count++;
					continue;
				}
			}			

			foreach(DictionaryInfo di in difs)
			{
				if(di.Ignore)
				{
					mess = "Ignored : " + di.Name + " : " + di.Description + " : ";
					foreach(Uri url in di.Urls)
						mess += url + ", ";

					sb.AppendLine(mess);
					ignored_count++;
					continue;
				}
			}		
			
			sb.AppendLine("*/");
			sb.AppendLine();
			sb.AppendLine();

			//d = infos["vera"];
			//d.Description = "";
			/*foreach(DictionaryInfo di in difs)
			{
				if(di.SupportedTranslations.Count == 0 || di.Ignore)
				{
					continue;
				}
				sb.AppendLine(string.Format("\t\t\td = infos[\"{0}\"];", di.Name));
				sb.AppendLine(string.Format("\t\t\td.Description = \"{0}\";", di.Description));
				sb.AppendLine("\t\t\td.DescriptionRu = \"\";");
				sb.AppendLine("\t\t\td.DescriptionUa = \"\";");
				sb.AppendLine();
			}
			*/
			
			sb.AppendLine();
			sb.AppendLine();
			
			foreach(DictionaryInfo di in difs)
			{
				if(di.SupportedTranslations.Count == 0 || di.Ignore)
				{
					continue;
				}
				className = di.Name.ToUpper().Substring(0, 1) + di.Name.Substring(1);
				className = className.Replace(">", "");
				int idx = className.IndexOf("-");
				if(idx >= 0)
				{
					className = className.Substring(0, idx) + className.ToUpper().Substring(idx + 1, 1) + className.Substring(idx + 2);
				}
				
				idx = className.IndexOf("-");
				if(idx >= 0)
				{
					className = className.Substring(0, idx) + className.ToUpper().Substring(idx + 1, 1) + className.Substring(idx + 2);
				}				

				idx = className.IndexOf(".");
				if(idx >= 0)
				{
					className = className.Substring(0, idx) + className.ToUpper().Substring(idx + 1, 1) + className.Substring(idx + 2);
				}				
				
				
				sb.AppendLine("public class Dictd" + className + "Dictionary : " + di.BaseType);
				
				if(di.BaseType == "DictDMonolingualDictionary" || di.BaseType == "DictDSynonymsDictionary")
				{
					createSb.AppendLine(string.Format("\t\t\tAddMonolingualDictionary(new Dictd{0}Dictionary());", className));
				}	
				else
				{
					createSb.AppendLine(string.Format("\t\t\tAddBilingualDictionary(new Dictd{0}Dictionary());", className));
				}	
				
				sb.AppendLine("{");
				sb.AppendLine("\tpublic Dictd" + className + "Dictionary()");
				sb.AppendLine("\t{");
				
				sb.AppendLine(string.Format("\t\tName = \"{0}\";", di.Name));
				sb.AppendLine(string.Format("\t\tDescription = \"{0}\";", di.Description));
				
				foreach(LanguagePair languagePair in di.SupportedTranslations)
					sb.AppendLine(string.Format("\t\tAddSupportedTranslation(Language.{0}, Language.{1});", languagePair.From.ToString(), languagePair.To.ToString()));

				sb.AppendLine();

				foreach(string subject in di.SupportedSubjects)
					sb.AppendLine(string.Format("\t\tAddSupportedSubject(SubjectConstants.{0});", subject ));
					
				sb.AppendLine();
				
				foreach(Uri url in di.Urls)
					sb.AppendLine(string.Format("\t\tUrls.Add(new Uri(\"{0}\"));", url));
				
				sb.AppendLine("\t}");
				sb.AppendLine("}");
			}
			
			sb.Insert(4,string.Format("Total count : {0}, ignored : {1}, unsupported {2}\r\n", difs.Count, ignored_count, unsupported_count));
			
			sb.AppendLine("");
			sb.AppendLine("init:");
			sb.AppendLine("\t\t\t/*");
			sb.AppendLine("\t\t\tCode automatically generated by EnumDictd tool at " + DateTime.Now.ToString());
			sb.AppendLine(string.Format("\t\t\tTotal count : {0}, ignored : {1}, unsupported {2}\r\n", difs.Count, ignored_count, unsupported_count));
			sb.AppendLine("\t\t\t*/");
			
			
			sb.AppendLine("");
			
			sb.AppendLine(createSb.ToString());


			sb.AppendLine("");
			sb.AppendLine("English.lng:");
			
			foreach(DictionaryInfo di in difs)
			{
				if(di.SupportedTranslations.Count == 0 || di.Ignore)
				{
					continue;
				}
				sb.AppendLine(string.Format("[{0}]", di.Description));
				sb.AppendLine("");
			}
			
			sb.AppendLine("");
			sb.AppendLine("Ukrainian.lng:");
			
			foreach(DictionaryInfo di in difs)
			{
				if(di.SupportedTranslations.Count == 0 || di.Ignore)
				{
					continue;
				}
				sb.AppendLine(string.Format("[{0}]", di.Description));
				sb.AppendLine(di.DescriptionUa);
			}
			
			sb.AppendLine("");
			sb.AppendLine("Russian.lng:");
			
			foreach(DictionaryInfo di in difs)
			{
				if(di.SupportedTranslations.Count == 0 || di.Ignore)
				{
					continue;
				}
				sb.AppendLine(string.Format("[{0}]", di.Description));
				sb.AppendLine(di.DescriptionRu);
			}
			
			tbResult.Text = sb.ToString();
		}
	}
}

