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
		public async Task<Face> GetAsync() => await Request.GetAsync<Face>(Url);

		public async Task<string> EditAsync(Face face) => 
			await Request.EditAsync(face, Url);

	}
}
