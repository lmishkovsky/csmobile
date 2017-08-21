using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace CSMobile
{
    /// <summary>
    /// CSM obile page.
    /// </summary>
    public partial class CSMobilePage : ContentPage
    {
        /// <summary>
        /// The SOAP envelope.
        /// </summary>
        string soapEnvelope = "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\n  <soap:Body>\n    <Login xmlns=\"http://schemas.microsoft.com/sharepoint/soap/\">\n      <username>{0}</username>\n      <password>{1}</password>\n    </Login>\n  </soap:Body>\n</soap:Envelope>";

        /// <summary>
        /// The remote site URL.
        /// </summary>
        string remoteSiteUrl = "https://grcinternal.corestream.co.uk";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.CSMobilePage"/> class.
        /// </summary>
		public CSMobilePage()
        {
            InitializeComponent();

            Authenticate(remoteSiteUrl, "username", "password");
        }

        /// <summary>
        /// Authenticate the specified siteUri, userName and password.
        /// </summary>
        /// <returns>The authenticate.</returns>
        /// <param name="siteUri">Site URI.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public async void Authenticate(string siteUri, string userName, string password) 
        {
            string authServiceUrl = string.Format("{0}/_vti_bin/authentication.asmx", remoteSiteUrl);

            // get the FedAuth cookie and save it in a CookieContainer
            var request = WebRequest.CreateHttp(authServiceUrl);
			request.CookieContainer = new CookieContainer();
			request.ContentType = "text/xml; charset=utf-8";
			request.Method = "POST";
			var requestStream = await request.GetRequestStreamAsync();
            var loginBody = string.Format(soapEnvelope, userName, password);
			using (var w = new StreamWriter(requestStream)) await w.WriteAsync(loginBody);
			var response = await request.GetResponseAsync();
			if (response.Headers["Set-Cookie"] == null) throw new Exception("BAD!");
			var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri(siteUri), response.Headers["Set-Cookie"]);

            // pass the FedAuth cookie to an HttpCientHandler
			HttpClientHandler handler = new HttpClientHandler
			{
				UseCookies = true,
                CookieContainer = cookieContainer
			};

            // create the http client passing the handler and its cookies
			HttpClient client = new HttpClient(handler);

            // request SharePoint list data
			HttpResponseMessage responseMessage = await client.GetAsync("https://grcinternal.corestream.co.uk/ProPo/_vti_bin/listdata.svc/IssuesAndUserStories(3020)");
            HttpStatusCode statusCode = responseMessage.StatusCode;
            var content = await responseMessage.Content.ReadAsStringAsync();

            string data = content;
        }
    }
}
