using SkillProfi;
using SkillProfiRequestsToAPI;
using SkillProfiWPF.ViewModels.Prefab;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SkillProfiWPF.ViewModels
{
	internal class FaceViewModel : EditorViewModel
	{
		public string BackGroundImage { get; } = @"Assets\Studying.jpg";

		public FaceViewModel()
		{
			Face = Task.Run(async () => await UserContext.SPClient.Face.GetAsync()).Result;
		}

		protected override bool CanSave(object p)
		{
			if (string.IsNullOrEmpty(Slogan) || string.IsNullOrEmpty(Opportunity))
			{
				return false;
			}
			return true;

		}
		protected override void OnSave(object p)
		{
			Task.Run(async () => await UserContext.SPClient.Face
				.EditAsync(new Face() { Opportunity = Opportunity, Slogan = Slogan })).Wait();

            Face = Task.Run(async () =>
				await UserContext.SPClient.Face.GetAsync()).Result;
			IsObjectEdit = false;
		}

		protected override void OnReturn(object p)
		{
			base.OnReturn(p);
            Face = Task.Run(async () =>
                await UserContext.SPClient.Face.GetAsync()).Result;
        }



		private string _slogan = "";
		public string Slogan
		{
			get => _slogan;
			set => Set(ref _slogan, value);

		}

		private string _opportunity = "";
		public string Opportunity
		{
			get => _opportunity;
			set => Set(ref _opportunity, value);

		}


		private Face _face;
		public Face Face
		{
			get => _face;
			set
			{
				if (value != null)
				{			
					Slogan = value.Slogan;
					Opportunity = value.Opportunity;
					IsObjectSelect = true;
				}
				else
				{
					IsObjectSelect = false;
					IsObjectEdit = false;
				}


				Set(ref _face, value);
			}

		}

		protected override void OnDelete(object p)
		{
			throw new System.NotImplementedException();
		}
	}
}
