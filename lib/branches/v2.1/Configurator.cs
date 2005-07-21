using System;

namespace Wc3o {
	public class Configuration {

		#region " Configuration Settings "

		public const string Physical_Application_Path = "c:\\hosting\\webhost4life\\member\\sachsenhofer\\wc3o\\";
		//public const string Physical_Application_Path = "E:\\My Webs\\Wc3o";

		public const double Return_Factor_After_Attack = 1.7;
		public const double Return_Factor_After_Defend = 0.5;
		public const int Days_To_Keep_BattleLogs = 14;
		public const int Hours_To_Be_Protected = 48;
		public const int Minutes_To_See_Arriving_Units = 30;
		public const int Max_Food = 200;
		public const int Unit_Factor_For_Annect = 20;

		public const int Start_Night = 21;
		public const int End_Night = 6;

		public const int Hour_Correction = 9;
		public const string Default_Gfx_Path = "http://www.sachsenhofer.com/wc3o/gfx";

		public const int Seconds_For_Ressource_Tick = 1800;
		public const int Seconds_For_Ranking_Tick = 3600;
		public const int Seconds_For_Save_Tick = 3600;

		public const int Number_Of_News = 5;
		public const int Number_Of_Changelogs = 5;

		public const int Map_Size = 20;
		public const int Sectors_To_Show_On_Map = 10;
		public const string Color_Player = "#6699cc";
		public const string Color_Ally = "#66ff99";
		public const string Color_Enemy = "#ff6600";
		public const string Color_Neutral = "#ffffff";
		public const string Color_League = "#ffff66";

		public const int Gold_Per_Ressource_Tick = 8;
		public const int Lumber_Per_Ressource_Tick = 3;
		public const int Max_Gold_Worker_Per_Sector = 5;
		public const int Max_Sectors_Per_Player = 6;
		public const int Min_Sectors_Per_Player = 2;
		public const int Min_Gold_Income = 20;
		public const int Min_Lumber_Income = 5;
		public const int Upkeep_Level1 = 70;
		public const double Upkeep_Level1_Factor = 0.7;
		public const int Upkeep_Level2 = 150;
		public const double Upkeep_Level2_Factor = 0.3;

		public const int Max_Player = 100;
		public const int Player_Per_League = 20;
		public const int Days_To_Keep_Dead_Players = 7;

		public const string Email_From_Address = "wc3o@sachsenhofer.com";
		public const string Email_Server = "mail.sachsenhofer.com";
		public const string Email_Username = "wc3o@sachsenhofer.com";
		public const string Email_Password = "wc3o";

		public const int Healing_Orc_Humans = 3;
		public const int Healing_NightElves = 6;
		public const int Healing_Undead = 7;	

		#endregion

	}
}