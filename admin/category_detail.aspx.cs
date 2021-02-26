using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_src_category_detail : AdminBasePage
{
    public string[] allCategoryDirs;
    public string title;
    public List<string> files;
    public List<string> supportedFileTypes = new List<string>(new string[]{ "jpg", "jpeg", "gif", "png", "bmp" });
    protected void Page_Load(object sender, EventArgs e)
    {
        title = Request.QueryString["cat"];

        CheckCatinvalidation();
        if (Request.Form["cmd"] == "save")
        {
            SaveImgs();
        }
        if (Request.Form["cmd"] == "upload")
        {

            UploadPic();
        }
         if (Request.Form["cmd"] == "copy")
        {
            CopyPic();
        }
        if (Request.Form["cmd"] == "move")
        {
            MovePic();
        }
        files = GetFiles();
        allCategoryDirs = Common.GetAllCategories();

    }
    public void MovePic(){
        string file=Request.Form["file"];
        string targetCategory=Request.Form["category"];
        string sourceCategoryPath=Server.MapPath("./category/" + title + "/" + file);
        string targetCategoryPath=Server.MapPath("./category/"+targetCategory+ "/" + file);
        if(!File.Exists(targetCategoryPath)){
            File.Move(sourceCategoryPath, targetCategoryPath);
        }

    }
    public void CopyPic(){
        
           
            string file=Request.Form["file"];
          string[] cats = Request.Form["categories"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
          string filePath=Server.MapPath("./category/" + title + "/" + file);
          if(!File.Exists(filePath)){
              return;
          }
          foreach( string cat in cats){
              string catPath=Server.MapPath("./category/"+cat);
                if (System.IO.Directory.Exists(catPath))
                {  
                    string destFile = System.IO.Path.Combine(catPath, file);
                    if(!File.Exists(destFile)){
                        System.IO.File.Copy(filePath, destFile, true);
                    }else{
                        destFile = System.IO.Path.Combine(catPath, "copy_"+file);
                         System.IO.File.Copy(filePath, destFile, true);
                    }          
                }
               
          }
    }
    private void SaveImgs()
    {
        List<string> fileList = GetFiles();
        foreach(string file in fileList)
        {
           
            string fileName = Common.GetLastDir(file);
           // Response.Write(fileName);
            string seq = Request.Form[fileName + "_seq"];
            string time= Request.Form[fileName + "_time"];
            string delete= Request.Form[fileName + "_delete"];
            string oldFilePath = Server.MapPath("./category/" + title + "/" + fileName);
            if (delete == "true")
            {
                if (File.Exists(oldFilePath))
                {
                   File.Delete(oldFilePath);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(time) || !Regex.IsMatch(time, @"^[+-]?\d*$"))
                {
                    time = "10";
                }
                string type = fileName.Substring(fileName.LastIndexOf(".") + 1);
                string newFileName = seq + "_" + Common.RandomName(6) + "_" + time + "." + type;

                string newFilePath = Server.MapPath("./category/" + title + "/" + newFileName);
             
                if (!File.Exists(newFilePath))
                {
                    Directory.Move(oldFilePath, newFilePath);
                }
            }
         
        }
    }
    private void CheckCatinvalidation()
    {
        string categoryPath =Server.MapPath("./category/" + title);
    
        if (string.IsNullOrEmpty(title)||!Directory.Exists(categoryPath))
        {
            Response.Redirect("./category.aspx");
        }

    }
    
    private List<string> GetFiles()
    {
        string tmpRootDir = Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath.ToString());//获取程序根目录
        string categoryPath = Server.MapPath("./category/" + title);
        string[] files = System.IO.Directory.GetFiles(categoryPath);
        List<string> result = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            string type = files[i].Substring(files[i].LastIndexOf(".") + 1);
            if (supportedFileTypes.Contains(type.ToLower()))
            {
               
                result.Add ("/" + files[i].Replace(tmpRootDir, ""));
            }
        }
        return result;
    }

    private void UploadPic()
    {
        Response.Clear();
        int count = Request.Files.Count;

        string information = "";
    
        try
        {
            if (System.IO.Directory.Exists(Server.MapPath("~/ecomsrc/")) == false)
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/ecomsrc/"));
            }
            string uploadFileName = Request.Files["file"].FileName;
            string type = uploadFileName.Substring(uploadFileName.LastIndexOf(".") + 1).ToLower();
            string fileName = "0000_" + Common.RandomName(6) + "_10." + type;
            string imgPath = Server.MapPath("./category/" + title + "/" + fileName);
      
            if (supportedFileTypes.Contains(type))
            {
                Request.Files["file"].SaveAs(imgPath);
                information = "Upload logo successfully.";
     
            }
            else
            {
                information = "File format is not supported.";
            }

        }
        catch (Exception e)
        {

            information = e.Message;

        }
        Response.Write(information);
        Response.End();
        //try
        //{

        //    if (System.IO.Directory.Exists(Server.MapPath("~/ecomsrc/")) == false)
        //    {
        //        System.IO.Directory.CreateDirectory(Server.MapPath("~/ecomsrc/"));
        //    }
        //    string filename = Path.GetFileName(Request.Files["logo"].FileName);
        //    string type = filename.Substring(filename.LastIndexOf(".") + 1).ToLower();
        //    string logo = "logo." + type;
        //    if (type == "bmp" || type == "jpg" || type == "gif" || type == "png")
        //    {
        //        Request.Files["logo"].SaveAs(Server.MapPath("~/ecomsrc/") + logo);
        //        information = "Upload logo successfully.";
        //        string logoPath = "/ecomsrc/" + logo;
        //        SaveLogo(logo, logoPath);

        //    }
        //    else
        //    {
        //        information = "File format is not supported.";
        //    }

        //}
        //catch (Exception e)
        //{

        //    information = e.Message;

        //}

    }
}