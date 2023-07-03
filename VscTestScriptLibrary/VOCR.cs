using Emgu.CV;
using Emgu.CV.Structure;
using Tesseract;
using System;
using System.Drawing;

namespace VscTestScriptLibrary
{
    public class VOCR
    {
        public string VideoFilePath { get; set; }

        public string TessDataFilePath { get; set; }

        public VOCR(string videoFilePath, string tessDataFilePath)
        {
            VideoFilePath = videoFilePath;

            TessDataFilePath = tessDataFilePath;
        }

        public void VscVocr() 
        {
            VideoCapture capture = new VideoCapture(VideoFilePath);

            List<string> list = new List<string>();

            using (TesseractEngine tesseractEngine = new TesseractEngine("./tessdata", "chi_sim", EngineMode.Default))
            {

                // Loop through the video frames
                while (capture.IsOpened)
                {
                    // Read the next frame
                    Mat frame = capture.QueryFrame();

                    if (frame == null)
                        break;

                    // Convert the frame to a grayscale image
                    Image<Gray, byte> grayImage = frame.ToImage<Gray, byte>();

                     grayImage.Save("./Output.png");
                    // Perform text detection using Tesseract.NET
                    using (Page page = tesseractEngine.Process(Pix.LoadFromFile("./Output.png")))
                    {
                        string extractedText = page.GetText();

                        if (extractedText == null)
                        {
                            Console.WriteLine("Null String!");
                            Console.ReadLine();
                        }
                        else if (extractedText == string.Empty)
                        {
                            Console.WriteLine("Empty String!");
                            Console.ReadLine();

                        }

                        // Do something with the extracted text
                        Console.WriteLine(extractedText);

                        list.Add(extractedText);
                    }

                    // Display the frame with text regions highlighted (optional)
                    // You can use Emgu.CV drawing functions to draw rectangles around the detected text regions
                    CvInvoke.Imshow("Text Detection", frame);
                    CvInvoke.WaitKey(1);
                }

            }

            capture.Dispose();
            CvInvoke.DestroyAllWindows();

            FileStream fileStream = new FileStream("./Output.txt", FileMode.OpenOrCreate);

            StreamWriter streamWriter = new StreamWriter(fileStream);

            foreach (string i in list)
            {
                streamWriter.WriteLine(i);

                streamWriter.Flush();
            }

            streamWriter.Close();

            fileStream.Close();

            Console.WriteLine("Completed!");
            Console.ReadLine();
        }
    }
}
