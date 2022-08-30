using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelpers
{
    public class FileHelper : IFileHelper
    {
        public void Delete(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public string Upload(IFormFile file, string root)
        {
            if (file != null)
            {
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                string extension = Path.GetExtension(file.FileName);
                string imageName = Guid.NewGuid().ToString() + extension;

                using (FileStream fileStream = File.Create(root + imageName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                    return imageName;
                }
            }
            return null;
        }

        public string Update(IFormFile file, string root, string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            return Upload(file, root);
        }
    }
}

