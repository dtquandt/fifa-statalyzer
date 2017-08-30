using System;
using Tesseract;
using System.IO;

public class OCR
{
	public static string ReadImage(string imagepath)
	{
        using (var ocrEngine = new TesseractEngine(@".\tessdata", @"eng", EngineMode.Default))
        {
            using (var image = Pix.LoadFromFile(imagepath))
            {
                using (var result = ocrEngine.Process(image,Tesseract.PageSegMode.SingleBlock))
                {
                    var text = result.GetText();
                    return text;
                }
            }
        }
    }
}
