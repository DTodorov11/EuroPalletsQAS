using EuroPallets.Common;
using EuroPallets.Data;
using EuroPallets.Models;
using EuroPallets.ViewModels.ProductsViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EuroPallets.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEvroPalletsData data;
        public AdminController(IEvroPalletsData data)
        {
            this.data = data;
        }

        // GET: Admin
        public ActionResult AdminPanel()
        {
            return View();
        }

        public ActionResult EmailEditor()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EmailEditor(string text, string type)
        {
            if (text != " " && type != " " && type != "Select Type")
            {
                try
                {
                    string templateName = type + ".txt";
                    using (System.IO.StreamWriter writer = new StreamWriter(GlobalConstants.EmailtemplateFolder + templateName, false))
                    {
                        writer.WriteLine(text);
                        writer.Close();
                    }
                    return this.View();
                }
                catch (Exception ex) { }
            }
            return this.RedirectToAction("Index", "Admin");
        }

        public ActionResult ShowEmailTemplates(string text)
        {
            string templateName = text + ".txt";

            if (System.IO.File.Exists(GlobalConstants.EmailtemplateFolder + templateName))
            {
                var fileContents = System.IO.File.ReadAllText(this.Server.MapPath("/Content/EmailEditor/" + templateName));
                return this.Content(fileContents);
            }

            return this.Json("<h1><span style=\"font-weight: bold; \">No Template</span></h1>");
        }

        public ActionResult UploadNewItem()
        {
            ViewData["GlobalCategoryDdl"] = this.data.GlobalCategories.All()
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Name })
                .ToList();

            ViewData["CategoryDdl"] = this.data.Category.All()
                .Select(x => new SelectListItem() { Text = x.Name, Value = x.Name })
                .ToList();

            return this.View(new EuroPalletFurniture());
        }

        [HttpPost]
        public ActionResult UploadNewItem(EuroPalletFurniture model)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        var currentFileBytes = ReadToEnd(Request.Files[i].InputStream);

                        //Check if browse image from Admin is blank
                        if (currentFileBytes != null && currentFileBytes.Length > 0)
                        {
                            model.EuroPalletImages.Add(new EuroPalletImage()
                            {
                                Image = currentFileBytes,
                                EuroPalletFurniture = model
                            });
                        }
                    }

                    if (model.EuroPalletImages.Count() > 0)
                    {
                        this.data.EuroPalletImages.AddAll(model.EuroPalletImages);
                    }
                }


                this.data.Specification.Add(model.Specification);
                this.data.EuroPalletFurnitures.Add(model);
               
                this.data.SaveChanges();

                TempData["Success"] = GlobalConstants.SuccessNewEuroPaletFurniture;

                //return JavaScript("<script>document.getElementById(\"uploadForm\").reset();</script>");
            }
            catch (Exception ex)
            {
                TempData["Error"] = GlobalConstants.FaliedToAddNewEuroPaletFurniture + ex.ToString();
            }


            return Redirect(Request.UrlReferrer.ToString());
        }
        public static byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = 0;

            if (stream.CanSeek)
            {
                originalPosition = stream.Position;
                stream.Position = 0;
            }

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                if (stream.CanSeek)
                {
                    stream.Position = originalPosition;
                }
            }
        }
    }
}