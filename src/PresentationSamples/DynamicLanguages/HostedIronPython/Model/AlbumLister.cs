using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;

namespace HostedIronPython.Model {
    public class AlbumLister {
        
        public AlbumFinder Finder { get; set; }
        
        public IEnumerable<Album> FindByArtist(string artistName) {
        
            IEnumerable<Album> albums = Finder.FindAll();

            //return albums.Where(a => a.Artist.StartsWith(artistName, StringComparison.CurrentCultureIgnoreCase));
            return albums.Where(getQuery("FindByArtist", artistName));
        }

        public IEnumerable<Album> FindByTitle(string title) {

            IEnumerable<Album> albums = Finder.FindAll();
            
            return albums.Where(getQuery("FindByTitle", title));
        }

        public IEnumerable<Album> FindByTrack(string trackName) {

            IEnumerable<Album> albums = Finder.FindAll();

            return albums.Where(getQuery("FindByTrack", trackName));
        }

        private Func<Album, bool> getQuery(string queryName, string searchParam) {

            XDocument doc = XDocument.Load("AlbumQueries.xml");

            var query = (from q in doc.Descendants("query")
                           where q.Attribute("name").Value == queryName
                           select new {
                              Query = q.Value
                          }).FirstOrDefault();

            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            ScriptRuntime runtime = new ScriptRuntime(setup);
            
            ScriptScope scope = runtime.CreateScope("IronPython");
            scope.SetVariable("search_param", searchParam);            
            
            scope.Engine.CreateScriptSourceFromString("import clr", SourceCodeKind.SingleStatement).Execute(scope);
            scope.Engine.CreateScriptSourceFromString("clr.AddReference('System')", SourceCodeKind.SingleStatement).Execute(scope);
            scope.Engine.CreateScriptSourceFromString("from System import *", SourceCodeKind.SingleStatement).Execute(scope);
            ScriptSource source = scope.Engine.CreateScriptSourceFromString(query.Query, SourceCodeKind.Statements);
            source.Execute(scope);                       

            return scope.GetVariable<Func<Album, bool>>("query");

        }
    }    
}
