using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CSMobile.Pages
{
    /// <summary>
    /// Issue details.
    /// </summary>
    public partial class IssueDetails : ContentPage
    {
        /// <summary>
        /// The issue xml.
        /// </summary>
        string issueXML;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.Pages.IssueDetails"/> class.
        /// </summary>
        public IssueDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.Pages.IssueDetails"/> class.
        /// </summary>
        /// <param name="issueXML">Issue xml.</param>
		public IssueDetails(string issueXML)
		{
			InitializeComponent();

            this.issueXML = issueXML;

            editorIssueDetails.Text = this.issueXML;
		}
    }
}
