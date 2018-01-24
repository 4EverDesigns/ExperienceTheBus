using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExperienceTheBus
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void sendMessageButton_Click(object sender, EventArgs e)
        {
            //Response.Write("YOU CLICKED ME!!!");
            string sPreQual = ddPreQual.SelectedItem.ToString();
            string sAgentName = ddAgent.SelectedItem.ToString();
            string sName = txtName.Text;
            string sEmail = txtEmail.Text;
            string sPhone = txtPhone.Text;
            string sMessage = message.InnerText;
            string sIP = "";
            IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress addr in localIPs)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    sIP = addr.ToString();
                    break;
                }
            }

            //STORES INFORMATION INTO DATABASE
            /*
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["csTheTour"].ConnectionString;

            string sqlCmd = "INSERT INTO [dbo].[tblMainInfo] ([sName],[sEmail],[sPhone],[sIPAddress],[AgentName],[PreQual]) " +
                             "VALUES ('" + sName.Trim() + "','" + sEmail.Trim() + "','" + sPhone.Trim() + "','" + sIP.Trim() + "','" + sAgentName + "','" + sPreQual + "'); ";

            SqlCommand cmd = new SqlCommand(sqlCmd, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            //SEND EMAIL TO AGENT
            string sSenderEmail = "anthony@valleworld.com";
            string your_password = "P1an0K1ng";
            string sRecepient = getAgentEmail(ddAgent.SelectedValue.ToString());
            string sSubject = "EXPERIENCE THE TOUR - REGISTRATION";
            string sBody = sName + "<br>" + sEmail + "<br>" + sPhone + "<br>" + sMessage;
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "mail.valleworld.com",
                    Port = 587,
                    EnableSsl = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(sSenderEmail, your_password),
                    Timeout = 10000,
                };
                MailMessage mm = new MailMessage(sSenderEmail, sRecepient, sSubject, sBody);
                client.Send(mm);
                Console.WriteLine("Email Sent");
            }
            catch
            {
                Console.WriteLine("Could not end email\n\n");
            }
            
            Response.Redirect(Request.RawUrl, true);
            */
        }

        protected void agentSelect_Click(object sender, EventArgs e)
        {
            string sClientID = ((System.Web.UI.Control)sender).ClientID.ToString();
            var sAgentNumber = "0";

            switch (sClientID)
            {
                case "mhughes":
                    sAgentNumber = "1";
                    break;
                case "sdiggs":
                    sAgentNumber = "2";
                    break;
                case "mmendler":
                    sAgentNumber = "3";
                    break;
                case "etorres":
                    sAgentNumber = "4";
                    break;
            }
            ddAgent.SelectedValue = sAgentNumber;
            sendMessageButton.Focus();
        }

        public static String getAgentEmail(string agentID)
        {
            string agentEmail = "";
            SqlConnection con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTheTour"].ConnectionString;

            string sqlCmd = "SELECT [email] " +
                            "FROM [tblAgent] " +
                            "WHERE [ID] = '" + agentID + "'";
            SqlCommand cmd = new SqlCommand(sqlCmd, con);
            con.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    agentEmail = reader["Email"].ToString();
                }
            }
            con.Close();
            return agentEmail;
        }
    }
}