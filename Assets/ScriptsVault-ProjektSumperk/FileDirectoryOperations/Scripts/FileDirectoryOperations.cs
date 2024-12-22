using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

namespace ProjektSumperk
{
    public class FileDirectoryOperations : MonoBehaviour
    {
        public TMP_Text logText; // Assign your TMP_Text component in the Inspector.

        private string rootDirectoryPath;

        private void Start()
        {
            // Set the root directory path based on the platform
            rootDirectoryPath = GetPlatformDirectoryPath();
        }

        private void LogToText(string message)
        {
            if (logText != null)
            {
                logText.text += message + "\n";
            }
            else
            {
                Debug.LogError("TMP_Text component not assigned to logText field.");
            }
        }

        private string GetPlatformDirectoryPath()
        {
            string platformPath = Application.persistentDataPath;
            return platformPath;
        }

        public void CreateDirectory()
        {
            string directoryPath = Path.Combine(rootDirectoryPath, "MyFolder");
            bool created = CreateDirectory(directoryPath);
            if (created)
            {
                LogToText($"Directory created: {directoryPath}");
            }
        }

        public void CreateFile()
        {
            string filePath = Path.Combine(rootDirectoryPath, "MyFile.txt");
            bool created = CreateFile(filePath, "Hello, Unity!");
            if (created)
            {
                LogToText($"File created: {filePath}");
            }
        }

        public void CheckDirectoryExists()
        {
            bool directoryExists = DirectoryExists(rootDirectoryPath);
            LogToText($"Directory '{rootDirectoryPath}' exists: {directoryExists}");
        }

        public void CheckFileExists()
        {
            string filePath = Path.Combine(rootDirectoryPath, "MyFile.txt");
            bool fileExists = FileExists(filePath);
            LogToText($"File '{filePath}' exists: {fileExists}");
        }

        public void ListFilesInDirectory()
        {
            ListFilesInDirectory(rootDirectoryPath);
        }

        public void ListSubdirectoriesInDirectory()
        {
            ListSubdirectoriesInDirectory(rootDirectoryPath);
        }

        public void DeleteFile()
        {
            string filePath = Path.Combine(rootDirectoryPath, "MyFile.txt");
            bool deleted = DeleteFile(filePath);
            if (deleted)
            {
                LogToText($"File deleted: {filePath}");
            }
        }

        public void DeleteDirectory()
        {
            string directoryPath = Path.Combine(rootDirectoryPath, "MyFolder");
            bool deleted = DeleteDirectory(directoryPath);
            if (deleted)
            {
                LogToText($"Directory deleted: {directoryPath}");
            }
        }

        public void CopyFile()
        {
            string sourceFilePath = "SourceFolder/SourceFile.txt";
            string destinationFilePath = "DestinationFolder/DestinationFile.txt";
            bool copied = CopyFile(sourceFilePath, destinationFilePath);
            if (copied)
            {
                LogToText($"File copied from '{sourceFilePath}' to '{destinationFilePath}'");
            }
        }

        public void MoveFile()
        {
            string moveSourceFilePath = "MoveSourceFolder/MoveSourceFile.txt";
            string moveDestinationFilePath = "MoveDestinationFolder/MoveDestinationFile.txt";
            bool moved = MoveFile(moveSourceFilePath, moveDestinationFilePath);
            if (moved)
            {
                LogToText($"File moved from '{moveSourceFilePath}' to '{moveDestinationFilePath}'");
            }
        }

        public void ReadFileContent()
        {
            string readFilePath = "ReadFile.txt";
            string content = ReadFileContent(readFilePath);
            if (!string.IsNullOrEmpty(content))
            {
                LogToText($"File content: {content}");
            }
        }

        public void WriteToFile()
        {
            string writeFilePath = "WriteFile.txt";
            string dataToWrite = "Writing data to file!";
            bool written = WriteToFile(writeFilePath, dataToWrite);
            if (written)
            {
                LogToText($"Data written to file '{writeFilePath}': {dataToWrite}");
            }
        }

        private bool CreateDirectory(string directoryPath)
        {
            try
            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private bool CreateFile(string filePath, string content)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, content);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private bool DirectoryExists(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        private bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        private void ListFilesInDirectory(string directoryPath)
        {
            try
            {
                string[] files = Directory.GetFiles(directoryPath);
                foreach (string file in files)
                {
                    LogToText(file);
                }
            }
            catch (System.Exception)
            {
            }
        }

        private void ListSubdirectoriesInDirectory(string directoryPath)
        {
            try
            {
                string[] subdirectories = Directory.GetDirectories(directoryPath);
                foreach (string subdirectory in subdirectories)
                {
                    LogToText(subdirectory);
                }
            }
            catch (System.Exception)
            {
            }
        }

        private bool DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private bool DeleteDirectory(string directoryPath)
        {
            try
            {
                if (Directory.Exists(directoryPath))
                {
                    Directory.Delete(directoryPath, true); // Delete recursively
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private bool CopyFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                File.Copy(sourceFilePath, destinationFilePath);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private bool MoveFile(string sourceFilePath, string destinationFilePath)
        {
            try
            {
                File.Move(sourceFilePath, destinationFilePath);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        private string ReadFileContent(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }

        private bool WriteToFile(string filePath, string content)
        {
            try
            {
                File.WriteAllText(filePath, content);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
