using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillProfiRequestsToAPI.Images
{
	public static class ImagesRequests
	{
		private const string _mainUrl = "https://localhost:7120/api/Picture";

		public static string GetURL(string Id) => _mainUrl + $"?id={Id}";

		public static byte[]? GetImage(string Id)
		{
            HttpWebRequest lxRequest = (HttpWebRequest)WebRequest.Create(GetURL(Id));
            byte[]? lnByte;

            try
            {
                using HttpWebResponse lxResponse = (HttpWebResponse)lxRequest.GetResponse();
                using BinaryReader reader = new(lxResponse.GetResponseStream());
                lnByte = reader.ReadBytes(1 * 1024 * 1024 * 10);
            }
            catch
            {
                lnByte = null;
            }

            return lnByte;

        }
		
	}
}
