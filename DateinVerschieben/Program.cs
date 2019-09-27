using System;
using System.IO;
using System.IO.Compression;


namespace DateinVerschieben
{
    class Program
    {
        public static void Main() 
        {
            //selbsteingabe
            Console.WriteLine("wrote the path of the file you want to copy/move:");
            string file1=(Console.ReadLine());

            string fielname = Path.GetFileName(file1);

            Console.WriteLine("do you have a 2nd file to copy/move?");
            Console.WriteLine("\ty -yes");
            Console.WriteLine("\tn -no");
            string key1 = Console.ReadLine();

            switch (key1)
            {
            case "y":
                try
                {
                Console.WriteLine("wrote the path of the 2nd file you want to copy/move:");
                string file2 = (Console.ReadLine());
                string filename3 = Path.GetFileName(file2);


                // Erstellen der Datei, falls net vorhanden
                if (!File.Exists(file2))
                { using (FileStream fs = File.Create(file2)) { } }


                // Try to create the directory.
                Console.WriteLine("What should be the name of the directory?");
                string diname = Console.ReadLine();


                Console.WriteLine("Write the second path. (where you want to have the file)");
                string path2 = Console.ReadLine() + @"\" + diname;


                if (Directory.Exists(path2))
                { Console.WriteLine("That Directory exists already.");
                    return; }
                   

                DirectoryInfo di = Directory.CreateDirectory(path2);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path2));


                string CopyInDirectory = path2 + @"\";


                // copy files in the directory
                File.Copy(file1, CopyInDirectory + fielname);
                File.Copy(file2, CopyInDirectory + filename3);


                string zipPath = path2 + @".zip";
                ZipFile.CreateFromDirectory(path2, zipPath);
                Console.WriteLine("The directory is now a .Zip file");

                Console.WriteLine("Now you have two files (one .zip, ond not), you want to delate the non .zip file?");
                Console.WriteLine("\tn - no, I won't");
                Console.WriteLine("\ty - yes, I want");

                string key2 = Console.ReadLine();

                switch (key2)
                {   case "n":
                        break;
                    case "y":
                        Directory.Delete(path2, true);
                        break;   } 

                }

                catch (Exception e)
                {
                    Console.WriteLine("The process failed:{0}", e.ToString());
                    return;
                }
            break;

            case "n":

                try
                {
                Console.WriteLine("Write the second path. (where you want to have the file)");
                string path2 = Console.ReadLine() + @"\" + fielname;

                Console.WriteLine("GetFileName '{0}' returns '{1}'", file1, fielname);

                Console.WriteLine("What do you want?");
                Console.WriteLine("\tc - copy");
                Console.WriteLine("\tm - move");
                Console.WriteLine("Your choose?");
                string key3 = Console.ReadLine();

                // Erstellen der Datei, falls net vorhanden
                if (!File.Exists(file1))
                { using (FileStream fs = File.Create(file1)) { } }

                //Sicherstellung, dass das Ziel nicht vorhanden ist.
                if (File.Exists(path2))
                { File.Delete(path2); }

                switch (key3)
                {
                case "c":

                    // Kopieren der Datei.  
                    File.Copy(file1, path2);
                    Console.WriteLine("file1 was copyed to path2.", file1, path2);


                    // gucken, ob das Original jetzt vorhanden ist.
                    if (File.Exists(file1))
                    { Console.WriteLine("The original file still exists, which is expected."); }


                    else
                    { Console.WriteLine("The original file no longer exists, which is unexpected."); }
                    break;

                case "m":

                    // Verschieben der Datei
                    File.Move(file1, path2);
                    Console.WriteLine("file1 was moved to path2.", file1, path2);


                    // gucken, ob das Original jetzt vorhanden ist.
                    if (File.Exists(file1))
                    { Console.WriteLine("The original file still exists, which is unexpected."); }


                    else
                    { Console.WriteLine("The original file no longer exists, which is expected."); }
                    break;
                } 
            break;
                }

                catch (Exception e)
                {  Console.WriteLine("The process failed:{0}", e.ToString());
                    return;  }
            }
        }
    }
} 