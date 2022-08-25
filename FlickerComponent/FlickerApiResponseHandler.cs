using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FlickerComponent.Interfaces;

namespace FlickerComponent
{
    /// <summary>
    ///     Handles Flicker API calls
    /// </summary>
    public class FlickerApiResponseHandler : IResponseHandler
    {
        public static string BaseUrl;
        private int _retryCount = 0;
        private WebException _webException;

        /// <summary>
        ///     Gets Flicker Api Response
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetResponse(string url)
        {
            if (_retryCount > 1)
            {
                throw _webException;
            }
            else
            {
                try
                {
                    using (WebClient client = new WebClient())
                    {
                        client.Encoding = Encoding.UTF8;
                        var searchUrl = BaseUrl + url;
                        var response = client.DownloadString(searchUrl);
                        _retryCount = 0;
                        _webException = null;
                        return response;
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.Timeout || ex.Status == WebExceptionStatus.ProtocolError)
                    {
                        if (ex.Response is HttpWebResponse response &&
                            (response.StatusCode == HttpStatusCode.BadGateway ||
                             response.StatusCode == HttpStatusCode.GatewayTimeout))
                        {
                            System.Threading.Thread.Sleep(1000);
                            _webException = ex;
                            _retryCount++;
                            GetResponse(url);
                        }
                    }

                    throw;
                }
            }
        }

        static FlickerApiResponseHandler()
        {
            var configuration = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
            var settings = configuration.AppSettings.Settings;
            var baseUrl = settings["BaseUrl"]?.Value + "?&api_key=" + settings["FlickerKey"].Value + "&";
            BaseUrl = baseUrl;
        }

    }
}
