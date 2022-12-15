using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace LtiLaunchDemo
{
    public partial class Launch : System.Web.UI.Page
    {
        protected LtiTicket Ticket { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            LtiValidate.Click += LtiValidate_Click;
        }

        private void LtiValidate_Click(object sender, EventArgs e)
        {
            CreateLtiTicket();

            var sortedParameters = new SortedDictionary<string, string>();
            foreach (var key in Ticket.Parameters.AllKeys)
                sortedParameters.Add(key, Ticket.Parameters[key]);
            var parameterItems = sortedParameters.Select(x => new { x.Key, x.Value });

            ParameterRepeater2.DataSource = parameterItems;
            ParameterRepeater2.DataBind();

            ParameterRepeater3.DataSource = parameterItems;
            ParameterRepeater3.DataBind();

            LtiProcess.ActiveViewIndex = 1;
        }

        private void CreateLtiTicket()
        {
            var identifier = LtiOrganizationIdentifier.Text;
            var group = LtiGroupName.Text;
            var secret = LtiOrganizationSecret.Text;
            var code = LtiLearnerCode.Text;
            var email = LtiLearnerEmail.Text;
            var firstName = LtiLearnerNameFirst.Text;
            var lastName = LtiLearnerNameLast.Text;
            var role = "Learner";

            var url = LtiLaunchUrl.Text;

            var postParameters = new LtiParameters("POST");

            postParameters.Add("oauth_consumer_key", secret);
            postParameters.Add("user_id", code);
            postParameters.Add("lis_person_name_given", firstName);
            postParameters.Add("lis_person_name_family", lastName);
            postParameters.Add("lis_person_contact_email_primary", email);
            postParameters.Add("roles", role);

            postParameters.Add("shift_group_name", group);
            postParameters.Add("shift_organization_identifier", identifier);

            Ticket = LtiTicketHelper.GetTicket(secret, url, postParameters);

            var parameters = new NameValueCollection();
            foreach (string key in Ticket.Parameters)
                parameters.Add(key, Ticket.Parameters[key]);

            parameters.Add("oauth_signature", Ticket.Signature);
        }
    }
}