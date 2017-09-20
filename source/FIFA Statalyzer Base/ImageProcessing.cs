using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using ImageProcessor.Imaging;
using System.Drawing;
using ImageProcessor.Processors;
using ImageProcessor.Imaging.Filters.Photo;

public class ImageProcessing
{
    public static string ProcessScreenshot(string filePath)
    {
        Console.WriteLine("Processing " + Path.GetFileName(filePath) + "...");
        var imgBytes = File.ReadAllBytes(filePath);
        CropLayer fifaCrop = new CropLayer(11.5f, 34.8f, 32.5f, 17.5f, CropMode.Percentage);
        CropLayer tempCrop = new CropLayer(0, 0, 0, 92f, CropMode.Percentage);
        Image originalImage = Image.FromFile(filePath);
        string savePath = (Path.GetDirectoryName(filePath) + @"\Processed\" + Path.GetFileName(filePath));
        using (var tempStream1 = new MemoryStream())
        using (var tempStream2 = new MemoryStream())
        using (var imageFactory = new ImageFactory(false))
        {
            imageFactory.Load(originalImage)
                .Saturation(-100)
                .Crop(fifaCrop)
                .Contrast(100)
                .Save(tempStream1)
                .Crop(tempCrop)
                .Filter(MatrixFilters.Invert)
                .Save(tempStream2);

            Image goalsImage = Image.FromStream(tempStream2);
            ImageLayer goals = new ImageLayer()
            {
                Image = goalsImage,
                Size = goalsImage.Size,
                Opacity = 100,
                Position = new Point(originalImage.Size.Height)
            };

            imageFactory.Load(tempStream1)
                .Overlay(goals)
                .Save(savePath);

            tempStream1.Dispose();
            tempStream2.Dispose();
            imageFactory.Dispose();
        }
        Console.WriteLine("Image processed and saved to " + savePath);
        return savePath;
    }
}