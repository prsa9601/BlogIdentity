using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Common.Application
{
    public static class ImageConvertor
    {
        /// <summary>
        /// برای کوچک کردن عکس از این متد استفاده کنید
        /// </summary>
        /// <param name="inputImagePath">آدرس عکس را وارد کنید</param>
        /// <param name="outputPath">مسیری که قراره فایل بیت مپ ذخیره شود </param>
        /// <param name="newWidth">عرض عکس</param>
        /// <param name="new_height">ارتفاع عکس</param>
        public static void CreateBitMap(string inputImagePath, string outputPath, int newWidth, int new_height)
        {

            var inputDirectory = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{inputImagePath.Replace("/", "\\")}");
            #region OutPut
            var pathSplit = inputImagePath.Split('/');
            var imageName = pathSplit[^1];

            var folderName = Path.Combine(Directory.GetCurrentDirectory(), outputPath.Replace("/", "\\"));
            if (!Directory.Exists(folderName))
            {
                //Create Folder
                Directory.CreateDirectory(folderName);
            }
            var outputDirectory = Path.Combine(folderName, imageName);

            #endregion
            Image_resize(inputDirectory, outputDirectory, newWidth, new_height);
        }
        private static void Image_resize(string inputImagePath, string outputImagePath, int new_Width, int new_Height)
        {
            const long quality = 50L;
            Bitmap sourceBitmap = new Bitmap(inputImagePath);
            double dblWidthOriginal = sourceBitmap.Width;
            double dblHeightOriginal = sourceBitmap.Height;
            double relationHeightWidth = dblHeightOriginal / dblWidthOriginal;
            //int new_Height = (int)(new_Width * relation_height_width);
            var newDrawArea = new Bitmap(new_Width, new_Height);
            using (var graphicOfDrawArea = Graphics.FromImage(newDrawArea))
            {
                 graphicOfDrawArea.CompositingQuality = CompositingQuality.HighSpeed;

                graphicOfDrawArea.InterpolationMode = InterpolationMode.HighQualityBicubic;

                graphicOfDrawArea.CompositingMode = CompositingMode.SourceCopy;

                graphicOfDrawArea.DrawImage(sourceBitmap, 0, 0, new_Width, new_Height);

                using (var output = System.IO.File.Open(outputImagePath, FileMode.Create))
                {

                    var qualityParamId = System.Drawing.Imaging.Encoder.Quality;

                    var encoderParameters = new EncoderParameters(1);

                    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);

                    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

                    if (codec == null)
                    {
                        throw new Exception();
                    }
                    newDrawArea.Save(output, codec, encoderParameters);

                    output.Close();

                }
                graphicOfDrawArea.Dispose();
            }
            sourceBitmap.Dispose();
        }


        #region CompresImage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath">آدرس عکس</param>
        /// <param name="destPath">آدرس ذخیره عکس</param>
        /// <param name="quality">عددی بین 0 تا 100</param>
        public static void CompressImage(string imagePath, string destPath, long quality)
        {
            using (Bitmap bmp1 = new Bitmap(imagePath))
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    
                Encoder qualityEncoder = Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(qualityEncoder, quality);

                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(destPath, jpgEncoder, myEncoderParameters);
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null!;
        }
        #endregion
    }
}
