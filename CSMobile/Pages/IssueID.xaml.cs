using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CSMobile.Pages
{
    /// <summary>
    /// Issue identifier.
    /// </summary>
    public partial class IssueID : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:CSMobile.Pages.IssueID"/> class.
        /// </summary>
        public IssueID()
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
            await Navigation.PushAsync(new IssueDetails());
        }
    }
}
