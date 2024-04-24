using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.ExceptionHandler;

namespace QR_Publisher
{
    class DataDecoder
    {
        private String imagesPath;
        public DataDecoder(String imagesPath)
        {
            this.imagesPath = imagesPath;
        }
        private String getNewFile()
        {
            String result = "";
            String[] allFiles = Directory.GetFiles(imagesPath);
            if (allFiles.Length > 0)
            {
                DateTime dateTimeFile = DateTime.Parse("01.01.0001");
                foreach (String fileName in allFiles)
                {
                   DateTime curDt =  File.GetCreationTime(fileName);
                    if (dateTimeFile < curDt)
                    {
                        dateTimeFile = curDt;
                        result = fileName;
                    }
                }
            }
            return result;
        }

        public void clearFolder()
        {
            String[] allFiles = Directory.GetFiles(imagesPath);
            if (allFiles.Length > 0)
            {
                foreach (String fileName in allFiles)
                {
                    File.Delete(fileName);
                }
            }
        }
        
        private Bitmap getImageFromFile()
        {
            String file = getNewFile();
            if (!file.Equals("")) {
                if (file.EndsWith(".png") || file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".bmp"))
                {
                    Bitmap qrFile = new Bitmap(getNewFile());
                    return qrFile;
                }
                else clearFolder();
                
            } 
            return null;
        }

        public String getDecodedData()
        {
            QRCodeDecoder decoder = new QRCodeDecoder();
            Bitmap bImage = getImageFromFile();
            if (bImage != null)
            {
                try
                {
                    String result = decoder.Decode(new QRCodeBitmapImage(bImage));
                    bImage.Dispose();
                    return result;
                } catch(DecodingFailedException e)
                {
                    bImage.Dispose();
                    clearFolder();
                    return "";
                } catch (InvalidVersionException e)
                {
                    bImage.Dispose();
                    clearFolder();
                    return "";
                }

               

            }
            return "";            
        }
    }



    
}
