using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public string Upload(HttpPostedFileBase file)
        {
            string theFileName = Path.GetFileName(file.FileName);
            byte[] thePictureAsBytes = new byte[file.ContentLength];

            string base64String = "";

            var path = Path.Combine(Server.MapPath("~/Images"), theFileName);

            file.SaveAs(path);

            using (Image image = Image.FromFile(path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    base64String = Convert.ToBase64String(imageBytes);
                }
            }

            using (BinaryReader theReader = new BinaryReader(file.InputStream))
            {
                thePictureAsBytes = theReader.ReadBytes(file.ContentLength);
            }
            string thePictureDataAsString = Convert.ToBase64String(thePictureAsBytes);

            string postData = "";

            Dictionary<string, string> postParameters = new Dictionary<string, string>();

            postParameters.Add("key", "9c9dfe77cd3bdbaa7220c6bbaf7452e7");
            postParameters.Add("source", $"{base64String}");
            postParameters.Add("format", "txt");

            foreach (string key in postParameters.Keys)
            {
                postData += HttpUtility.UrlEncode(key) + "="
                      + HttpUtility.UrlEncode(postParameters[key]) + "&";
            }

            var url = "http://ap.imagensbrasil.org/api/1/upload";

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            myHttpWebRequest.Method = "POST";

            byte[] data = Encoding.ASCII.GetBytes(postData);

            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = data.Length;

            Stream requestStream = myHttpWebRequest.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();

            try
            {
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                Stream responseStream = myHttpWebResponse.GetResponseStream();

                StreamReader myStreamReader = new StreamReader(responseStream, Encoding.Default);

                string pageContent = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                responseStream.Close();

                myHttpWebResponse.Close();
            }
            catch (Exception ex)
            {

            }


            return "<script>top.$('.mce-btn.mce-open').parent().find('.mce-textbox').val('"+ path + "').closest('.mce-window').find('.mce-primary').click();</script>";
        }
    }
}