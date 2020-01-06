using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SaveClipboardImages
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("All your images will be stored under C:/Snapshots/");
            Console.WriteLine("Enter the Title: ");
            string title = Console.ReadLine();

            Thread t = new Thread(() =>
            {
                SaveImagesFromClipboard(title);

            });

            t.SetApartmentState(ApartmentState.STA);
            t.Start();

            if(t.ThreadState != ThreadState.Running)
            {
                Console.WriteLine($"All your images will be stored under C:/Snapshots/{title}");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("Snapshot saver is still running!!");
            }


        }

        private static void SaveImagesFromClipboard(string title)
        {
            while (true)
            {
                if (Clipboard.ContainsImage())
                {
                    var datetime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    BitmapSource image = Clipboard.GetImage();
                    using (var fileStream = new FileStream($"C:/Snapshots/{title}_{datetime}.png", FileMode.Create))
                    {
                        BitmapEncoder encoder = new PngBitmapEncoder();
                        encoder.Frames.Add(BitmapFrame.Create(image));
                        encoder.Save(fileStream);
                    }
                    Clipboard.Clear();
                }
            }
        }


    }
}
