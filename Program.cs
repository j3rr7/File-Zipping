// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices;
using System.IO.Compression;

[DllImport("User32.dll", CharSet = CharSet.Unicode)]
static extern int MessageBox(IntPtr h, string m, string c, int type);


DirectoryInfo outputDir = new DirectoryInfo("output"); // output directory
DirectoryInfo inputDir = new DirectoryInfo(".\\public\\assets"); // input 1 PS: Change this to whatever you want
DirectoryInfo inputDir2 = new DirectoryInfo(".\\public\\uploads"); // input 2 PS: Change this to whatever you want

void CopyAll(DirectoryInfo source, DirectoryInfo target)
{
    if (source.FullName.ToLower() == target.FullName.ToLower())
    {
        return;
    }
    
    // Check if the target directory exists, if not, create it.
    if (Directory.Exists(target.FullName) == false)
    {
        Directory.CreateDirectory(target.FullName);
    }

    // Copy each file into it's new directory. PS: filtering file goes here so do whatever you want with this file
    foreach (FileInfo fi in source.GetFiles().Where(f => (f.FullName.EndsWith(".jpg") || f.FullName.EndsWith(".jpeg") || f.FullName.EndsWith(".png") || f.FullName.EndsWith(".svg"))))
    {
        Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
        fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
    }

    // Copy each subdirectory using recursion.
    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
    {
        DirectoryInfo nextTargetSubDir =
            target.CreateSubdirectory(diSourceSubDir.Name);
        CopyAll(diSourceSubDir, nextTargetSubDir);
    }
}
if (!inputDir.Exists || !inputDir2.Exists)// show error message if input is not correct
{
    MessageBox((IntPtr)0, "Please Make Sure This In The Correct Directory", "ERROR", 0);
    Environment.Exit(1);
}

// should be using some other function but this will do for now
CopyAll(inputDir, outputDir);
CopyAll(inputDir2, outputDir);

// zip the output file and remove the original
if (!File.Exists("output.zip"))
    ZipFile.CreateFromDirectory("output", "zip.zip" , CompressionLevel.Fastest, true);
if (Directory.Exists("output"))
    Directory.Delete("output", true);

Console.WriteLine("Done Migrating Data");// done 