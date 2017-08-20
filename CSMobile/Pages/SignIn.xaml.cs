using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Xamarin.Forms;

namespace CSMobile.Pages
{
    /// <summary>
    /// Sign in.
    /// </summary>
    public partial class SignIn : ContentPage
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
        /// The cookie container.
        /// </summary>
        CookieContainer cookieContainer = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.Pages.SignIn"/> class.
        /// </summary>
        public SignIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            string username = tbxUsername.Text.Trim();
            string password = tbxPassword.Text.Trim();

            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                Authenticate(remoteSiteUrl, username, password);

                if (cookieContainer != null && cookieContainer.Count > 0)
                {
                    // authenticated
                    await Navigation.PushAsync(new IssueID());
                }
                else
                {
                    await DisplayAlert("Failed authentication!", "Invalid username or password", "Close");
                }
            }
            else
            {
                await DisplayAlert("Missing data!", "Please enter your username and password", "Close");
            }
        }

        /// <summary>
        /// Authenticate the specified siteUri, userName and passWord.
        /// </summary>
        /// <returns>The authenticate.</returns>
        /// <param name="siteUri">Site URI.</param>
        /// <param name="userName">User name.</param>
        /// <param name="passWord">Pass word.</param>
        async void Authenticate(string siteUri, string userName, string passWord)
        {
            string authServiceUrl = string.Format("{0}/_vti_bin/authentication.asmx", remoteSiteUrl);

            // get the FedAuth cookie and save it in a CookieContainer
            var request = WebRequest.CreateHttp(authServiceUrl);
            request.CookieContainer = new CookieContainer();
            request.ContentType = "text/xml; charset=utf-8";
            request.Method = "POST";
            var requestStream = await request.GetRequestStreamAsync();
            var loginBody = string.Format(soapEnvelope, userName, passWord);
            using (var w = new StreamWriter(requestStream)) await w.WriteAsync(loginBody);
            var response = await request.GetResponseAsync();
            if (response.Headers["Set-Cookie"] != null)
            {
                cookieContainer = new CookieContainer();
                cookieContainer.SetCookies(new Uri(siteUri), response.Headers["Set-Cookie"]);
            }
        }        
    }
}
