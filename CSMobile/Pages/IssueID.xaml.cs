using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;

namespace CSMobile.Pages
{
    /// <summary>
    /// Issue identifier.
    /// </summary>
    public partial class IssueID : ContentPage
    {
        CookieContainer cookieContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.Pages.IssueID"/> class.
        /// </summary>
        public IssueID()
        {
            InitializeComponent();
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:CSMobile.Pages.IssueID"/> class.
		/// </summary>
		public IssueID(CookieContainer cookieContainer)
		{
			InitializeComponent();

            this.cookieContainer = cookieContainer;
		}

        /// <summary>
        /// Handles the clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbxIssueID.Text.Trim()))
            {
				// pass the FedAuth cookie to an HttpCientHandler
				HttpClientHandler handler = new HttpClientHandler
				{
					UseCookies = true,
					CookieContainer = cookieContainer
				};

				// create the http client passing the handler and its cookies
				HttpClient client = new HttpClient(handler);

                string remoteIssueUrl = string.Format("{0}/ProPo/_vti_bin/listdata.svc/IssuesAndUserStories({1})", Constants.REMOTESITEURL, tbxIssueID.Text);

				// request SharePoint list data
                HttpResponseMessage responseMessage = await client.GetAsync(remoteIssueUrl);
				HttpStatusCode statusCode = responseMessage.StatusCode;
				var content = await responseMessage.Content.ReadAsStringAsync();

                await Navigation.PushAsync(new IssueDetails(content));
            }
            else {
                await DisplayAlert("Input missing!", "Please enter the issue or user story ID.", "Close");
            }
        }
    }
}
