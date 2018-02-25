using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopCoreApp.Areas.Admin.Controllers
{
    public class UploadController : BaseController
    {
        #region Variables
        private const string UPLOAD_FOLDER = "Uploaded";
        private readonly IHostingEnvironment _hostingEnvironment;
        #endregion

        #region Ctor
        public UploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region Action
        /// <summary>
        /// Upload Image
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult UploadImage()
        {
            DateTime now = DateTime.UtcNow;

            var files = Request.Form.Files;

            if (files.Count == 0)
            {
                return new BadRequestObjectResult(files);
            }

            var file = files[0];
            var fileName = Path.GetFileName(file.FileName.Trim());

            var imageUpload = $@"{UPLOAD_FOLDER}\images\{ now.ToString("yyyyMMdd")}";

            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, imageUpload);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string filePath = Path.Combine(uploadFolder, fileName);

            using (var fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            return new OkObjectResult(Path.Combine(imageUpload, fileName).Replace(@"\", @"/"));
        }

        [HttpPost]
        public async Task UploadImageForCKEditor(string CKEditorFuncNum, string CKEditor, string languCode)
        {
            DateTime now = DateTime.UtcNow;

            IList<IFormFile> files = Request.Form.Files.ToList();

            if (files.Count == 0)
            {
                await HttpContext.Response.WriteAsync("Yêu cầu nhập ảnh");
            }


            var file = files[0];
            var fileName = Path.GetFileName(file.FileName.Trim());

            var imageUpload = $@"{UPLOAD_FOLDER}\images\{ now.ToString("yyyyMMdd")}";

            var uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, imageUpload);

            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }
            string filePath = Path.Combine(uploadFolder, fileName);

            using (var fs = System.IO.File.Create(filePath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }

            await HttpContext.Response.WriteAsync("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", '" + Path.Combine(imageUpload, fileName).Replace(@"\", @"/") + "');</script>");
        }
        #endregion
    }
}