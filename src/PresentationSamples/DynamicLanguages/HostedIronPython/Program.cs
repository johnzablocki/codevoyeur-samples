using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostedIronPython.Container;
using HostedIronPython.Model;

namespace HostedIronPython {
    class Program {
        static void Main(string[] args) {

            try {

                PyCrust crust = new PyCrust("Objects.xml");
                AlbumLister lister = crust.GetFilling("AlbumLister") as AlbumLister;
                //AlbumLister lister = crust.GetObject("AlbumLister") as AlbumLister;
                //validateAlbums(lister.Finder.FindAll());                

                Console.Write("Search by Title [t], Artist [a] or Track [r]: ");
                string searchType = Console.ReadLine();
                
                Console.Write("Enter a search term: ");
                string searchTerm = Console.ReadLine();

                IEnumerable<Album> albums = null;
                switch (searchType.ToLower()) {
                    case "t":
                        albums = lister.FindByTitle(searchTerm);
                        break;
                    case "a":
                        albums = lister.FindByArtist(searchTerm);
                        break;
                    case "r":
                        albums = lister.FindByTrack(searchTerm);
                        break;
                    default:
                        Console.WriteLine("Invalid search option.");
                        return;
                }

                Console.WriteLine();
                Console.WriteLine();
                
                foreach (Album album in albums) {
                    Console.WriteLine("Found artist {0} with album {1}", album.Artist, album.Title);                    
                    foreach (string trackName in album.Tracks) {
                        Console.WriteLine("\tTrack: {0}", trackName);
                    }
                }                
            } catch (Exception ex) {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        private static void validateAlbums(IEnumerable<Album> albums) {

            foreach (Album album in albums) {
                if (!album.IsValid()) {
                    Console.WriteLine("Album {0} is not valid.", album.Title);

                    foreach (string key in album.PropertiesWithErrors.Keys) {
                        Console.WriteLine("\tProperty {0} failed for reason: {1}", key, album.PropertiesWithErrors[key]);
                    }
                }
            }

        }
    }
}
