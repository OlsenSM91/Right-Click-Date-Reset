using System;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Metadata;
using SixLabors.ImageSharp.Metadata.Profiles.Exif;
using SixLabors.ImageSharp.Processing;

namespace ResetDate
{
    class ResetDateExtension
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
            {
                if (IsDirectory(arg))
                {
                    ResetDateRecursive(arg);
                }
                else if (IsImageFile(arg))
                {
                    UpdateExifDate(arg);
                }
            }
        }

        private static void ResetDateRecursive(string? directoryPath = null)
        {
            if (directoryPath == null)
            {
                Console.WriteLine("No directory path provided.");
                return;
            }

            foreach (var filePath in Directory.GetFiles(directoryPath).Where(path => IsImageFile(path)))
            {
                UpdateExifDate(filePath);
            }

            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                ResetDateRecursive(subDirectoryPath);
            }
        }

        private static void UpdateExifDate(string filePath)
        {
            try
            {
                using (var image = Image.Load(filePath))
                {
                    var exifProfile = image.Metadata.ExifProfile ?? new ExifProfile();

                    var dateTime = DateTime.Now;
                    var dateTimeString = dateTime.ToString("yyyy:MM:dd HH:mm:ss");

                    exifProfile.SetValue(ExifTag.DateTime, dateTimeString);
                    exifProfile.SetValue(ExifTag.DateTimeDigitized, dateTimeString);
                    exifProfile.SetValue(ExifTag.DateTimeOriginal, dateTimeString);

                    image.Metadata.ExifProfile = exifProfile;

                    // Determine the encoder based on the file extension
                    var encoder = GetImageEncoder(Path.GetExtension(filePath));

                    // Save the updated image using a FileStream
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.Save(fileStream, encoder);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle or log exceptions
                Console.WriteLine($"Error processing {filePath}: {ex.Message}");
            }
        }

        private static IImageEncoder GetImageEncoder(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                case ".bmp":
                    return new BmpEncoder();
                case ".gif":
                    return new GifEncoder();
                case ".tiff":
                    return new TiffEncoder();
                default:
                    throw new NotSupportedException($"File extension {extension} is not supported");
            }
        }

        private static bool IsImageFile(string path)
        {
            var extensions = new string[] { ".png", ".jpg", ".tiff", ".jpeg", ".bmp", ".gif" };
            return extensions.Contains(Path.GetExtension(path).ToLower());
        }

        private static bool IsDirectory(string path)
        {
            return Directory.Exists(path);
        }
    }
}
