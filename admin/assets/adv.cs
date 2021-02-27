using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class v2_invoice : Page
{
    string category = "../admin/category";
    string configFile = "config.txt";
    string defaultPath = "/default";
	
	protected void Page_Load(object sender, EventArgs e)
    {
        SaveLog();

    }
	void CreateLog()
	{
		string logPath = Server.MapPath("./log.txt");
		if (!System.IO.File.Exists(logPath))
		{
			System.IO.FileStream fs = System.IO.File.Create(logPath);
			fs.Close();
			using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
			{
				sw.WriteLine("TIME,LOCAL_IP");
			}
		}
	}
	void SaveLog()
	{
		CreateLog();
		string localIP = Request.UserHostName.ToString();
		string hostName = Request.ServerVariables.Get("Remote_Host").ToString();

		string result = DateTime.Now.ToString() + "," + localIP ;
		string logPath = Server.MapPath("./log.txt");
		// 	   foreach(String o in Request.ServerVariables){
		//  Response.Write(o+"="+Request.ServerVariables[o]+"<br>");
		// }
		using (System.IO.StreamWriter sw = System.IO.File.AppendText(logPath))
		{
			sw.WriteLine(result);


		}


	}



	public string PrintImg()
	{

		StringBuilder result = new StringBuilder();
		string fullpath = Server.MapPath(configFile);
		string content ;
		if (!System.IO.File.Exists(fullpath))
		{
			content = "default";
		}
		else
		{
			content = System.IO.File.ReadAllText(fullpath);
		}
		string[] cat = content.Split('\n');


		string categoryPath = Server.MapPath(category);
		System.IO.DirectoryInfo DirInfo = new System.IO.DirectoryInfo(categoryPath);
		System.IO.DirectoryInfo[] dirs = DirInfo.GetDirectories();
		for (int i = 0; i < cat.Length; i++)
		{
			for (int j = 0; j < dirs.Length; j++)
			{

				if (dirs[j].ToString().Trim() == cat[i].ToString().Trim())
				{
					System.IO.DirectoryInfo targetDir = new System.IO.DirectoryInfo(categoryPath + "\\" + dirs[j]
						.ToString().Trim());
					System.IO.FileInfo[] imgs = targetDir.GetFiles();
					for (int k = 0; k < imgs.Length; k++)
					{
						PicName pn = new PicName(imgs[k].Name);
                        
							if (pn.IsShow()&&Common.supportedFileTypes.Contains(GetFileType(imgs[k].ToString())))
							//if (imgs[k].Name != "Thumbs.db")
							{
								result.Append(GetImgWrapper(dirs[j].ToString().Trim(), imgs[k].Name, imgs[k].Extension));
							}
						
						

					}

				}
			}
		}

		if (result.ToString() == "" || content == "default")
		{

			System.IO.DirectoryInfo targetDir = new System.IO.DirectoryInfo(categoryPath + defaultPath);
			System.IO.FileInfo[] imgs = targetDir.GetFiles();
			for (int k = 0; k < imgs.Length; k++)
			{
				PicName pn = new PicName(imgs[k].Name);
					//if (imgs[k].Name != "Thumbs.db")
				if (pn.IsShow()&&Common.supportedFileTypes.Contains(GetFileType(imgs[k].ToString())))
				{
					result.Append(GetImgWrapper("default".Trim(), imgs[k].Name, imgs[k].Extension));
				}
			}
		}


		return result.ToString();
	}
	string GetFileType(string fileName)
    {
		string type=fileName.Substring(fileName.LastIndexOf(".") + 1);
		return type;
    }
	string GetImgWrapper(string cat, string name, string extension)
	{
		string filename = name.Substring(0, name.LastIndexOf("."));

		string time = "10";
		// string result="";
		if (filename.LastIndexOf("_") != -1)
		{
			string suffix = filename.Substring(filename.LastIndexOf("_") + 1);

			if (Regex.IsMatch(suffix, @"^[+-]?\d*$"))
			{
				time = suffix;
				//following code means maxmium second is 60
				//if (Convert.ToInt32(suffix) < 60)
				//{
				//	time = suffix;
				//}
				//else
				//{
				//	time = "60";
				//}
			}
		}
		Random random = new Random();
		string src = category + "/" + cat + "/" + name + "?q=" + random.Next(1000).ToString();
		// string template ="<div class='wrapper' data-time="+time+" style='display:none;background:url("+src+") no-repeat top center;'></div>";   
		string template = @"<div class='wrapper' data-time=" + time + @" style='display:none'><img src = " + src + @" ></div>";
		return template;
	}


}