using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{

		public static class Tutorial
		{
			public static string BuildBarn 		{ get{ return LocalizationManager.GetTranslation ("Tutorial/BuildBarn"); } }
			public static string BuildHouse 		{ get{ return LocalizationManager.GetTranslation ("Tutorial/BuildHouse"); } }
			public static string Congratulation 		{ get{ return LocalizationManager.GetTranslation ("Tutorial/Congratulation"); } }
			public static string GatheringWood 		{ get{ return LocalizationManager.GetTranslation ("Tutorial/GatheringWood"); } }
			public static string Welcome 		{ get{ return LocalizationManager.GetTranslation ("Tutorial/Welcome"); } }
		}
	}

    public static class ScriptTerms
	{

		public static class Tutorial
		{
		    public const string BuildBarn = "Tutorial/BuildBarn";
		    public const string BuildHouse = "Tutorial/BuildHouse";
		    public const string Congratulation = "Tutorial/Congratulation";
		    public const string GatheringWood = "Tutorial/GatheringWood";
		    public const string Welcome = "Tutorial/Welcome";
		}
	}
}