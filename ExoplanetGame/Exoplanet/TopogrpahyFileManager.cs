using System;
using System.IO;

namespace ExoplanetGame.Exoplanet
{
    public class TopographyFileManager
    {
        public void SaveTopographyToFile(string[] topography, string filePath)
        {
            try
            {
                File.WriteAllLines(filePath, topography);
                Console.WriteLine("Topography saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving topography: {ex.Message}");
            }
        }

        public string[] LoadTopographyFromFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string[] topography = File.ReadAllLines(filePath);
                    Console.WriteLine("Topography loaded successfully.");
                    return topography;
                }
                else
                {
                    Console.WriteLine("File does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading topography: {ex.Message}");
            }

            return null;
        }
    }
}