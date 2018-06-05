using MVCImageResizer.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCImageResizer.Controllers
{
    public class HomeController : Controller
    {
        public string StorageDirectory = "ImagesRepo";

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Index(string resizeFileName , int resizeValueText, MVCImageResizeTypes resizeTypeSelect)
        {
            string resultStr = string.Empty;
            string thumbnailFileName = string.Empty;

            // Specify the route of the file in storage directory
            var path = Path.Combine(Server.MapPath("~/" + StorageDirectory), resizeFileName);

            // Create a bitmap of the content of the stored image in memory
            //Bitmap originalBMP = new Bitmap(fileUpload.FileContent);

            Bitmap originalBMP = (Bitmap)Image.FromFile(path , true);

            // Calculate the new image dimensions
            int origWidth = originalBMP.Width;
            int origHeight = originalBMP.Height;
            
            

            //based on resize type we calculate the new dimentions
            int newWidth = 0;
            int newHeight = 0;
            if (resizeTypeSelect.Equals(MVCImageResizeTypes.ByWidth))
            {
                double sngRatioWbyH = (double)origWidth / (double)origHeight;
                newWidth = resizeValueText;
                newHeight = (int)(newWidth / sngRatioWbyH);
            }
            else if (resizeTypeSelect.Equals(MVCImageResizeTypes.ByHeight))
            {
                double sngRatioHbyW = (double)origHeight / (double)origWidth;
                newHeight = resizeValueText;
                newWidth = (int)(newHeight / sngRatioHbyW);
            }
            else if (resizeTypeSelect.Equals(MVCImageResizeTypes.ByRatio))
            {
                double newRatio = resizeValueText;
                newWidth = (int)(origWidth / newRatio);
                newHeight = (int)(origHeight / newRatio);
            }

            
            // Create a new bitmap which will hold the previous resized bitmap
            Bitmap newBMP = new Bitmap(originalBMP, newWidth, newHeight);
            // Create a graphic based on the new bitmap
            Graphics oGraphics = Graphics.FromImage(newBMP);

            // Set the properties for the new graphic file
            oGraphics.SmoothingMode = SmoothingMode.AntiAlias; oGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw the new graphic based on the resized bitmap
            oGraphics.DrawImage(originalBMP, 0, 0, newWidth, newHeight);

            try
            {
                thumbnailFileName = Path.GetFileNameWithoutExtension(resizeFileName) + "_thumbnail_" + Guid.NewGuid() + Path.GetExtension(resizeFileName);
                path = Path.Combine(Server.MapPath("~/" + StorageDirectory), thumbnailFileName);

                // Save the new graphic file to the server
                newBMP.Save(path);

                //return result str
                resultStr = "Successfully 1 file has been resized!";
            }
            catch (Exception ex)
            {
                resultStr = ex.Message;
            }
            

            // Once finished with the bitmap objects, we deallocate them.
            originalBMP.Dispose();
            newBMP.Dispose();
            oGraphics.Dispose();

            return Json(new { Data = resultStr, src = "../" + StorageDirectory + "/" + thumbnailFileName }, JsonRequestBehavior.AllowGet);
            //return Content(origWidth.ToString() + "X" + origHeight.ToString());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Farzad(Fred) Seifi";

            return View();
        }
    }
}