using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

///* Turn into string
/// (?<word>[a-z_]*),
/// "$1"
///



namespace InitialDatabase
{
    internal class Program
    {

        static List<string> seriesList = new() {
                                        
                                    };
        static List<string> characterList = new() {
                                        
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            // Scaffold database. Create models from database
            // dotnet ef dbcontext scaffold "Data Source=media_database.db;" Microsoft.EntityFrameworkCore.Sqlite


            var lib_root = @"";

            IList<string> list = Directory.EnumerateDirectories(lib_root).ToList();

            var foldersIntermediate = list.Where(s => s.Contains("__"))
                              .Select((s) => Path.GetFileNameWithoutExtension(s));
            var folders = foldersIntermediate
                              .Aggregate("\"" + foldersIntermediate.First().Trim('_') + "\"", (acc, s) => acc + ",\n" + "\"" + s.Trim('_') + "\"");


            Console.WriteLine(folders.ToString());
            Console.WriteLine("------------------------------------");

            var characterFoldersInter = list.Where(s => s.Contains("__"))
                                    .SelectMany(d => Directory.EnumerateDirectories(d))
                                    .Where(s => !s.Contains("name"));
            var characterFolders = characterFoldersInter
                                    .Aggregate("\"" + Path.GetFileName(characterFoldersInter.First()).Trim('_') + "\"", 
                                            (acc, s) => acc + ",\n" + "\"" + Path.GetFileName(s).Trim('_') + "\"");

            Console.WriteLine(characterFolders.ToString());


            var dbcontext = new MediaDatabaseContext();

            var tagTypes = dbcontext.TagTypes;
            var seriesType = tagTypes.Where((t) => t.TypeName == "series").First();
            
            var characterType = tagTypes.Where((t) => t.TypeName == "character").First();
            
            var tagRelations = new List<TagToImage>();

            var seriesFiles = new Dictionary<string,IList<MediaTable>>();
            foreach(var series in seriesList)
            {
                var currentFiles = Directory
                    .EnumerateFiles(list.Where(s => s.Contains(series)).First(), "*", SearchOption.AllDirectories);
                var temp = currentFiles.Distinct().Select((f) => new MediaTable { Location = f });
                seriesFiles[series] = temp.ToList();
                var seriesTag = new TagTable { Tag = series, TagType = seriesType };
                var t = temp.Select(f => new TagToImage { Media = f, Tag = seriesTag});

                var unique = t.GroupBy(tt => tt.Media.Location).Where(g => g.Count() == 1).Select( x => x.Key);
                var t2 = t.IntersectBy(unique, x => x.Media.Location);
                tagRelations.AddRange(t2);




            }

            //dbcontext.MediaTables.AddRange(tagRelations.Select(t => t.Media));
            //dbcontext.TagTables.AddRange(tagRelations.Select(t => t.Tag));
            //dbcontext.TagToImages.AddRange(tagRelations);
            dbcontext.ChangeTracker.DetectChanges();
            Console.WriteLine(dbcontext.ChangeTracker.DebugView.LongView);
            //dbcontext.SaveChanges();
            

            var tagRelations2 = new List<TagToImage>();
            var characterFiles = new Dictionary<string, IList<MediaTable>>();
            foreach (var character in characterList.Distinct())
            {
                var currentFiles = Directory
                    .EnumerateFiles(characterFoldersInter.Where(s => s.Contains(character)).First());
                var temp = currentFiles.Distinct().Select((f) => dbcontext.MediaTables.Where(t => t.Location == f).First());
                characterFiles[character] = temp.ToList();
                var characterTag = new TagTable { Tag = character, TagType = characterType };
                var t = temp.Select(f => new TagToImage { Media = f, Tag = characterTag });
                tagRelations2.AddRange(t);
            }


            

            List<TagTable> tagCharacters = characterList.Select(c => new TagTable { Tag = c, TagType = characterType}).ToList();
            var tagSeries = seriesList.Select(c => new TagTable { Tag = c, TagType = seriesType }).ToList();


            var nonUnique = tagRelations.GroupBy(t => t.Media.Location).Where(l => l.Count() > 0).Select(f => f.Key);
            //Console.WriteLine(string.Join(",\n", nonUnique));

            //dbcontext.AddRange(tagCharacters);
            //dbcontext.AddRange(tagSeries);
            //dbcontext.AddRange(seriesFiles.SelectMany(x => x.Value));
            //dbcontext.AddRange(characterFiles.SelectMany(x => x.Value));
            dbcontext.AddRange(tagRelations2);

            dbcontext.ChangeTracker.DetectChanges();
            Console.WriteLine(dbcontext.ChangeTracker.DebugView.LongView);

            dbcontext.SaveChanges();
            

        }
    }
}