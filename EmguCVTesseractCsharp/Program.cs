using EmguCVTesseractCsharp.Library;

namespace EmguCVTesseractCsharp
{
    /// <summary>
    /// Main Program class for console Application.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length==0)
            {
                Console.WriteLine("Enter the Path to Video:");

                string? PathToVideo = Console.ReadLine(); 

                if (PathToVideo != null) 
                {
                    string? LanguageToDetect = Console.ReadLine();

                    if (LanguageToDetect != null) 
                    {
                        Console.WriteLine("Press Enter to start processing:");
                        Console.ReadKey();

                        //Create VscProcessor Object.
                        VscProcessor vscProcessor = new VscProcessor(PathToVideo,LanguageToDetect);

                        //Call Process method.
                        vscProcessor.Process();

                        Console.WriteLine("Press Enter to Exit:");
                        Console.ReadKey();

                    }
                }
            }
        }
    }
}