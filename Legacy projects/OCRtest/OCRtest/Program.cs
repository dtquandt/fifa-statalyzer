using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tessnet2;
using Tesseract;
using ImageProcessor;
using System.IO;

namespace OCRtest
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] photoBytes = File.ReadAllBytes(imagePath); // change imagePath with a valid image path
            int quality = 70;
            var format = ImageFormat.Png; // we gonna convert a jpeg image to a png one
            var size = new Size(200, 200);

            using (var inStream = new MemoryStream(photoBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    // Initialize the ImageFactory using the overload to preserve EXIF metadata.
                    using (var imageFactory = new ImageFactory(preserveExifData: true))
                    {
                        // Do your magic here
                        imageFactory.Load(inStream)
                            .Rotate(new RotateLayer(90))
                            .RoundedCorners(new RoundedCornerLayer(190, true, true, true, true))
                            .Watermark(new TextLayer()
                            {
                                DropShadow = true,
                                Font = "Arial",
                                Text = "Watermark",
                                Style = FontStyle.Bold,
                                TextColor = Color.DarkBlue
                            })
                            .Resize(size)
                            .Format(format)
                            .Quality(quality)
                            .Save(outStream);
                    }
                }
            }
        }
    }
}
