using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_invoice : AdminBasePage
{
    public List<DirectoryInfo> companyDirs;
    public string[] allCategoryDirs;
    public string[] cats ;
    public List<string> exceptCompanyDirs = new List<string>(new string[] { "admin", ".git" });
    public List<string> undeleteCompanyList = new List<string>(new string[] { "asiasuperstd","actp" });
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["cmd"] == "addNewCompany")
        {
            CreateNewCompany();
        }
        if(!String.IsNullOrEmpty(Request.Form["delete"]))
        {
            DeleteCompany();
            
        }
        if (!String.IsNullOrEmpty(Request.Form["rename"]))
        {
            RenameCompany();

        }
        if (!String.IsNullOrEmpty(Request.Form["select"]))
        {
            SelectCategories();

        }
        GetCategoriesOfCompany();
        allCategoryDirs = Common.GetAllCategories();
		


	}
    public void SelectCategories()
    {
        string company = Request.Form["select"];
        string companyPath = Server.MapPath("../" + company);
        string configTxt = Server.MapPath("../" + company + "/config.txt");
        if (!string.IsNullOrEmpty(Request.Form["categories"]))
        {
            string[] cats = Request.Form["categories"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
           


            using (StreamWriter sw = new StreamWriter(configTxt))
            {
                foreach (string cat in cats)
                {
                    sw.WriteLine(cat);
                }

            }
        }
        else
        {
            using (StreamWriter sw = new StreamWriter(configTxt))
            {
                
                    sw.WriteLine("default");
             

            }
        }

    }
    public void RenameCompany()
    {
        string oldCompanyName = Request.Form["rename"];
        string newCompanyName = Request.Form["new_company_name"];
        string oldCompanyPath = Server.MapPath("../" + oldCompanyName);
        string newCompanyPath = Server.MapPath("../" + newCompanyName);
        if (!String.IsNullOrEmpty(newCompanyName) && !Directory.Exists(newCompanyPath))
        {
           
            Directory.Move(oldCompanyPath, newCompanyPath);
        }
   

    }
    public void DeleteCompany()
    {
        string company = Request.Form["delete"];
        string deletePath = Server.MapPath("../"+company); 
        if (Directory.Exists(deletePath)&&!String.IsNullOrEmpty(company))
        {

            Common.DeleteDir(deletePath);
        }

    }
    public void CreateNewCompany()
    {
        string company = Request.Form["company"];
        string sourcePath = Server.MapPath("./source");
        string targetPath = Server.MapPath("../" + company);
        if (!System.IO.Directory.Exists(targetPath))
        {
            System.IO.Directory.CreateDirectory(targetPath);
        }


        if (System.IO.Directory.Exists(sourcePath))
        {
            string[] files = System.IO.Directory.GetFiles(sourcePath);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
                System.IO.File.Copy(s, destFile, true);
            }
        }
    }
    public void GetCategoriesOfCompany()
    {
        string companyPath = Server.MapPath("../");
     
        DirectoryInfo DirInfo = new DirectoryInfo(companyPath);
    
        DirectoryInfo[] dirs = DirInfo.GetDirectories();
    
        companyDirs =new List<DirectoryInfo>();

        foreach (DirectoryInfo dir in dirs)
        {
            
            if (!exceptCompanyDirs.Contains(dir.ToString()))
            {
           
                companyDirs.Add(dir);
            }
        }
 



    }




}