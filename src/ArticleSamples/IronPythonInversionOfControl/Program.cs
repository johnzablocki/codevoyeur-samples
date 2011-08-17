using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPythonInversionOfControl.Model;
using IronPythonInversionOfControl.Core;

namespace IronPythonInversionOfControl
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                XmlContainer container = new XmlContainer("Objects.xml");
                AlbumLister lister = container.GetObject("AlbumLister") as AlbumLister;
                
                string response = "";
                while (response != "x")
                {
                    Console.Write("Enter an artist: ");
                    IEnumerable<Album> albums = lister.FindByArtist(Console.ReadLine());

                    foreach (Album album in albums)
                    {
                        Console.WriteLine(album.Title);
                    }


                    Console.Write("\r\nPress any key to continue or type x to quit: ");
                    response = Console.ReadLine();
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }
    }
}
