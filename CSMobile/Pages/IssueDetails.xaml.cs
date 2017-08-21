using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;
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

            lblTitleValue.Text = GetXmlNodeValue("<d:Title>", "</d:Title>");
            lblStatusValue.Text = GetXmlNodeValue("<d:StatusValue>", "</d:StatusValue>");
            lblTypeValue.Text = GetXmlNodeValue("<d:ItemTypeValue>", "</d:ItemTypeValue>");
            lblPriorityValue.Text = GetXmlNodeValue("<d:PriorityValue>", "</d:PriorityValue>");
            lblSummaryValue.Text = GetXmlNodeValue("<d:Summary>", "</d:Summary>");
            lblCommentsValue.Text = GetXmlNodeValue("<d:CommentsAndUpdates>", "</d:CommentsAndUpdates>");
		}

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns>The value.</returns>
        /// <param name="startTag">Start tag.</param>
        /// <param name="endTag">End tag.</param>
        private string GetXmlNodeValue(string startTag, string endTag)
        {
            string retValue = string.Empty;

            int startIndex = this.issueXML.IndexOf(startTag);
            int endIndex = this.issueXML.IndexOf(endTag);

            if (startIndex != -1 && endIndex != -1)
            {
                retValue = this.issueXML.Substring(startIndex + startTag.Length, endIndex - startIndex - endTag.Length + 1);
            }

            return retValue;
        }
    }
}
