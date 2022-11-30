using SkillProfi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.VisualComponents
{
	public class FaceRequests : RequestController
	{
		public FaceRequests(Func<string> getBaseUrl) : base(getBaseUrl, "VisualComponents/Face") {}


		public Face Get() => Request.Get<Face>(Url);

		public async Task<Face> GetAsync() => await Request.GetAsync<Face>(Url);



		public string Edit(Face face, string accessToken) => Request.Edit(face, Url, accessToken: accessToken);

		public async Task<string> EditAsync(Face face, string accessToken) => 
			await Request.EditAsync(face, Url, accessToken: accessToken);

	}
}
