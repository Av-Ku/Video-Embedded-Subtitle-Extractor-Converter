using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace EmguCVTesseractCsharp.Library
{
    /// <summary>
    /// This class is for processing the video and extracting the subtitles.
    /// It contains the methods and parameters for performing the Action.
    /// </summary>
    public class VscProcessor
    {
        public string PathToVideo { get; set; }

        private readonly Dictionary<string, string>? LanguageCode;

        public string LanguageToDetect { get; set; }

        private readonly string PathToTessData = "./tessdata";

        private readonly string TempImageOutputPath = "./Output.png";

        /// <summary>
        /// This is the constructor for initializing the required properties for the class to be able to process the data.
        /// </summary>
        /// <param name="pathToVideo"></param>
        /// <param name="languageToDetect"></param>
        public VscProcessor(string pathToVideo, string languageToDetect)
        {
            PathToVideo = pathToVideo;

            LanguageToDetect = languageToDetect;

        }

        /// <summary>
        /// This method Processes the video into frames and detects text from each frame.
        /// The detected text is provided as output to console.
        /// </summary>
        /// <exception cref="VscException"></exception>
        public void Process() 
        {
            VideoCapture capture = new VideoCapture(PathToVideo);

            using (TesseractEngine engine = new TesseractEngine(PathToTessData, LanguageToDetect, EngineMode.Default)) 
            {
                //Loop through the frames of the video/videostream which is provided by VideoCapture.
                while (capture.IsOpened)
                {
                    Mat frame = capture.QueryFrame();

                    if (frame == null)
                    {
                        break;
                    }

                    //Convert Frame Image to Grayscale Image.
                    //It reduces processing complexity.
                    Image<Gray,byte> grayImage = frame.ToImage<Gray,byte>();

                    //save image to a temporary file.
                    grayImage.Save(TempImageOutputPath);

                    //Perform text detection.
                    using (Page page = engine.Process(Pix.LoadFromFile(TempImageOutputPath))) 
                    {
                        string extractedText = page.GetText();

                        if (extractedText == null)
                        {
                            throw new VscException("Null Text detected Error!");
                        }
                        else if (extractedText.Equals(string.Empty))
                        {
                            Console.WriteLine("W/ :No text detected.");
                        }
                        else
                        {
                            Console.WriteLine(extractedText);
                        }

                    }

                    // Display the frame with text regions highlighted (optional)
                    // You can use Emgu.CV drawing functions to draw rectangles around the detected text regions
                    CvInvoke.Imshow("Text Detection", frame);
                    CvInvoke.WaitKey(1);

                }


            }

            capture.Dispose();
            CvInvoke.DestroyAllWindows();

            Console.WriteLine("I/ :Processing Completed.");

        }

        

    }
}
