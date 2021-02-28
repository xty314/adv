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

    protected void Page_Load(object sender, EventArgs e)
    {
        title = Request.QueryString["cat"];

        CheckCatinvalidation();
        if (Request.Form["cmd"] == "save")
        {
            SaveImgs();
            RefreshPage();
        }
        if (Request.Form["cmd"] == "upload")
        {
            //async method
            UploadPic();
        
        }
         if (Request.Form["cmd"] == "copy")
        {
            CopyPic();
            RefreshPage();
        }
        if (Request.Form["cmd"] == "move")
        {
            MovePic();
            RefreshPage();
        }
        files = GetFiles();
        allCategoryDirs = Common.GetAllCategories();

    }
    private void RefreshPage()
    {
        Response.Write("<meta http-equiv=\"refresh\" content=\"0\">");
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
                if (Directory.Exists(catPath))
                {  
                    string destFile = Path.Combine(catPath, file);
                    if(!File.Exists(destFile)){
                        File.Copy(filePath, destFile, true);
                    }else{
                        destFile = Path.Combine(catPath, "copy_"+file);
                        File.Copy(filePath, destFile, true);
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
            PicName pn = new PicName(fileName);       
            string seq = Request.Form[fileName + "_seq"];
            string startDate = Request.Form[fileName + "_start"]; 
            string endDate = Request.Form[fileName + "_end"];
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
                string newFileName = seq + "_" +startDate+"_" + pn.Id + "_"+endDate+ "_" + time + "." + type;
                //string newFileName = seq + "_"  + + "_" + time + "." + type;
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
            if (Common.supportedFileTypes.Contains(type.ToLower()))
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

        string information ;
    
        try
        {
           
            string uploadFileName = Request.Files["file"].FileName;
            string type = uploadFileName.Substring(uploadFileName.LastIndexOf(".") + 1).ToLower();
            string fileName = "0000_01-01-2020_" + Common.RandomName(6) + "_31-12-2099_10." + type;
            string imgPath = Server.MapPath("./category/" + title + "/" + fileName);
            if (File.Exists(imgPath))
            {
                fileName = "0001_01-01-2020_" + Common.RandomName(6) + "_31-12-2099_10." + type;
                imgPath = Server.MapPath("./category/" + title + "/" + fileName);
            }
            if (Common.supportedFileTypes.Contains(type))
            {
                Request.Files["file"].SaveAs(imgPath);
                information = "Upload file successfully.";
     
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

   public string BackgroundColor(PicName pn)
    {
        if (!pn.IsStart())
        {
            return "bg-success";
        }
        if (pn.IsEnd())
        {
            return "bg-danger";
        }
        return "";
    }
}