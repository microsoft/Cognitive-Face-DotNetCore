using Microsoft.ProjectOxford.Face;
using System;
using System.Threading.Tasks;

namespace FaceDetectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                Console.WriteLine("Enter your Cognitive Services Face API Key:");
                var apiKey = Console.ReadLine();
                Console.WriteLine();

                Console.WriteLine("Enter the url of the image you want analyze:");
                var imageUrl = Console.ReadLine();
                Console.WriteLine();

                DetectFaces(apiKey, imageUrl).Wait();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        private static async Task DetectFaces(string apiKey, string imageUrl)
        {
            var faceServiceClient = new FaceServiceClient(apiKey);
            var faces = await faceServiceClient.DetectAsync(imageUrl, true, true, 
                new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses });

            Console.ForegroundColor = ConsoleColor.Cyan;

            var faceCount = 0;

            foreach(var face in faces)
            {
                faceCount += 1;

                Console.WriteLine("Face #: {0}", faceCount);
                Console.WriteLine(">Face Id: {0}", face.FaceId.ToString());
                Console.WriteLine(">Rectangle:");
                Console.WriteLine(">>Left: {0}", face.FaceRectangle.Left);
                Console.WriteLine(">>Top: {0}", face.FaceRectangle.Top);
                Console.WriteLine(">>Width: {0}", face.FaceRectangle.Width);
                Console.WriteLine(">>Height: {0}", face.FaceRectangle.Height);
                Console.WriteLine(">Gender: {0}", face.FaceAttributes.Gender);
                Console.WriteLine(">Age: {0:#}", face.FaceAttributes.Age);
                Console.WriteLine(">Smiling: {0}", face.FaceAttributes.Smile.ToString());
                Console.WriteLine(">Glasses: {0}", face.FaceAttributes.Glasses.ToString());
                Console.WriteLine("");
            }

            if(faceCount == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No faces were found in the images.");
            }
        }
    }
}