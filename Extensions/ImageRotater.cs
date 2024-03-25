using OptimizedPhotoViewer.DataStructures;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace OptimizedPhotoViewer.Extensions
{
    public static class ImageRotater
    {

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        public static void RotateOnDisk()
        {
            // Load the image from disk
            using (Bitmap originalImage = new Bitmap(TempSettings.CurrentImage))
            {
                // Rotate the image by 90 degrees
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Save the rotated image by replacing the old one
                using (var encoderParameters = new EncoderParameters(1))
                {
                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L); // Set the desired image quality (100% in this case)
                    originalImage.Save(TempSettings.CurrentImage, GetImageCodecInfo(ImageFormat.Bmp), encoderParameters);
                }
            }
        }
    }
}
