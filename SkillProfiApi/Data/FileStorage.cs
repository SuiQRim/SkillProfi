namespace SkillProfiApi.Data
{
	public class FileStorage
	{
		private const string REPOSITORY = "DataFiles";

		private const string PICTURES = "Pictures";

		public static void CreateAllComponents() 
		{			
			if (!Directory.Exists(PICTURES))
				Directory.CreateDirectory(PICTURES);

			if (!Directory.Exists(REPOSITORY))
				Directory.CreateDirectory(REPOSITORY);
		}
	}
}
