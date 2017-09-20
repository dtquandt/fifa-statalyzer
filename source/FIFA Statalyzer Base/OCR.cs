using System;
using Tesseract;
using System.IO;
using System.Text.RegularExpressions;

public class OCR
{
    public static string ReadImage(string imagepath)
    {
        using (var ocrEngine = new TesseractEngine(@".\tessdata", @"eng", EngineMode.Default))
        {
            using (var image = Pix.LoadFromFile(imagepath))
            {
                using (var result = ocrEngine.Process(image, Tesseract.PageSegMode.SingleBlock))
                {
                    Console.WriteLine("Executing OCR for " + imagepath + "...");
                    Console.WriteLine("OCR Result:");
                    var text = result.GetText();
                    Console.WriteLine(text);
                    return text;
                }
            }
        }
    }

    public static string CleanUp(string ocr)
    {
        ocr = ocr.Replace("\n", " ");
        ocr = ocr.Trim();
        ocr = Regex.Replace(ocr, "[^0-9 ]", "");
        ocr = Regex.Replace(ocr, @"\s+", " ");
        ocr = ocr.Trim();
        return ocr;
    }
}
