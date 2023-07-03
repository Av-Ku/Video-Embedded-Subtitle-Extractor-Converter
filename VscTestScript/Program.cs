using VscTestScriptLibrary;

namespace VscTestScript
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting!");

            Console.ReadLine();

            VOCR vOCR = new VOCR("D:/GitHub_Projects_Source/source/repos/VideoSubtitlesConverter/VSC/VscTestScriptLibrary/Resources/GOY.mp4",
                "D:/GitHub_Projects_Source/source/repos/VideoSubtitlesConverter/VSC/VscTestScriptLibrary/tessdata/eng.traineddata");

            vOCR.VscVocr();

            Console.WriteLine("Out side VOCR!");

        }


    }
}