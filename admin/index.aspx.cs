using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_login : Page
{
    public string m_msg;
    public void Page_Load(object sender, EventArgs e)
	{
        string pass = Request.Form["pass"];
        if (pass == "096232176"||pass=="9aysdata")
        {
            Session["isLogin"] = "true";
            Response.Redirect("company.aspx");
        }



  

    }


}