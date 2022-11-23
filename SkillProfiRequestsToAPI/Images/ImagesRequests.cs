using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Images
{
	public static class ImagesRequests
	{
		private const string _mainUrl = "https://localhost:7120/api/Picture";

		public static string GetURL(string Id) => _mainUrl + $"?id={Id}";

		public static byte[] GetImage(string Id) => Request.Get<byte[]>(_mainUrl + Id);
		
	}
}
