using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_category : AdminBasePage
{
    public string[] categoryList;
    public List<string> undeleteCategoryList = new List<string>( new string[] { "default" } ) ;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["cmd"] == "new")
        {
            CreateCategory();
        }
        if (Request.Form["cmd"] == "copy")
        {
            CopyCategory();
        }
        if (!String.IsNullOrEmpty(Request.Form["delete"]))
        {
            DeleteCategory();

        }
        categoryList = Common.GetAllCategories();
    }
    public void CopyCategory()
    {
        string targerCategory = Request.Form["category"];
        string sourceCategory = Request.Form["copy_category"];
        string targetPath = Server.MapPath("./category/" + targerCategory);
        string sourcePath = Server.MapPath("./category/" + sourceCategory);
        if (System.IO.Directory.Exists(sourcePath))
        {
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }
            string[] files = System.IO.Directory.GetFiles(sourcePath);
            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
                string fileName = System.IO.Path.GetFileName(s);
                string destFile = System.IO.Path.Combine(targetPath, fileName);
               File.Copy(s, destFile, true);
            }
        }
    }
    public void CreateCategory()
    {
        string newCategory = Request.Form["category"];
        string newCategoryPath = Server.MapPath("./category/" + newCategory);
        if (!string.IsNullOrEmpty(newCategory)&&!Directory.Exists(newCategoryPath))
        {
            Directory.CreateDirectory(newCategoryPath);
          
        }
    }
    public void DeleteCategory()
    {
        string category = Request.Form["delete"];
        string deletePath = Server.MapPath("./category/" + category);
        if (Directory.Exists(deletePath) && !String.IsNullOrEmpty(category))
        {

            Common.DeleteDir(deletePath);
        }

    }
}
