using InitialDatabase;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;


namespace ImageAppCLI
{
    internal class Program
    {
        static string browseDir = @"D:\Anime pictures\__idolmaster__";
        static int exitApp = 1;
        static MediaDatabaseContext dbcontext = new MediaDatabaseContext();
        

        static MediaTable GetMediaCreateNewIfNotExists(string path)
        {
            return dbcontext.MediaTables
                .Where(m => m.Location == path).Include(m => m.TagToImages).ThenInclude(t=>t.Tag).AsEnumerable()
                .FirstOrDefault(new MediaTable { Location = path });
        }
        static IList<TagTable> GetTagsFromMedia(MediaTable table)
        {
            return dbcontext.MediaTables
                .Include(s => s.TagToImages).ThenInclude(t => t.Tag).ThenInclude(t=> t.TagType)
                .Single(s => s.Id == table.Id).TagToImages.Select(t => t.Tag).ToList();
        }

        static TagTable GetTagCreateNewIfNotExists(string tag, int tagType)
        {
            return dbcontext.TagTables.Where(t => t.Tag == tag).SingleOrDefault(new TagTable { Tag = tag, TagTypeId = tagType });
        }

        static bool TagExists(string tag)
        {
            return dbcontext.TagTables.Any(t => t.Tag == tag);
        }

        static int EnumarateAllMode() {


            var fileList = Directory.EnumerateFiles(browseDir, "*", SearchOption.AllDirectories);

            foreach (var file in fileList)
            {
                var m = GetMediaCreateNewIfNotExists($"{file}");
                Console.WriteLine("Current file: " + file);
                Console.WriteLine("""
                    Choose action:
                    1 - Show tags from current image
                    2 - Add tag to current image
                    3 - Delete tag from current image
                    4 - Go to next image
                    """);
                int userChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (userChoice)
                {
                    case 0:
                        return 0;
                    case 1:
                        var showTagsFunc = () => {
                        var l = GetTagsFromMedia(m);
                            Console.WriteLine(string.Join("\n", l.Select(t => $"{t.Id} | {t.Tag} | {t.TagType.TypeName}").ToList()));
                            Console.WriteLine();
                        };
                        showTagsFunc();
                        break;
                    case 2:
                        var addTagFunc = () =>
                        {
                            Console.WriteLine("Enter tag:");
                            var inputTag = Console.ReadLine();
                            // if tag does not already exist ask if user is sure
                            int tagType = 0;
                            if(!TagExists(inputTag!))
                            {
                                Console.WriteLine("Tag does not already exist are you sure?:");
                                Console.WriteLine("Enter tag type\n1 - Series\n2-Character"); // TODO add generic type would be nr 5
                            }

                            var dbTag = GetTagCreateNewIfNotExists(inputTag!, tagType);
                            dbcontext.TagToImages.Add( new TagToImage {Media = m, Tag = dbTag });
                        };
                        addTagFunc();
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    default: exitApp = 0; return 0;
                }
            }

            return 0; 
        }
        static void Main(string[] args)
        {
            
            while(exitApp != 0)
            {
                Console.WriteLine("""
                Choose whether you want to:
                1 - use search queries
                2 - go through each image one by one
                3 - Show all tags
                Enter 0 at any time to go back to the beginning
                """);
                int userChoice = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Not implemented yet. Try again");
                        break;
                    case 2:
                        EnumarateAllMode();
                        break;
                    case 3:
                        Console.WriteLine(string.Join("\n", 
                            dbcontext.TagTables.Include(t=> t.TagType).Select(t => $"{t.Id} | {t.Tag} | {t.TagType.TypeName}").ToList()
                            )
                            );
                        Console.WriteLine();
                        break;
                    case 0:
                        exitApp = 0;
                        break;
                    default:
                        exitApp = 0;
                        break;
                }

            }

        }
    }
}