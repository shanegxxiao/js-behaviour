using System;
using System.Diagnostics;
using System.IO;

namespace Base.Editor
{
    public static class CmdExecutor
    {
        public static string RunExe(string file, string args = "", string workingDir = null)
        {
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = false,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = file,
                    Arguments = args,
                    WorkingDirectory = workingDir ?? GetWorkingDirByFilePath(file) ?? ""
                }
            };

            var output = "";
            try
            {
                process.Start();
                while (!process.StandardError.EndOfStream)
                {
                    output += process.StandardError.ReadLine() + "\n";
                }

                while (!process.StandardOutput.EndOfStream)
                {
                    output += process.StandardOutput.ReadLine() + "\n";
                }

                process.WaitForExit();
                return output;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(file + "\nRunCmdLine error: " + e);
                return output;
            }
            finally
            {
                process.Dispose();
            }
        }

        public static void RunShell(string cmd, string args = "")
        {
            var process = new Process
            {
                StartInfo =
                {
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = cmd,
                    Arguments = args,
                }
            };
            try
            {
                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError(cmd + "\nRunShell error: " + e);
            }
            finally
            {
                process.Dispose();
            }
        }

        public static string GetWorkingDirByFilePath(string filePath)
        {
            if (!Path.IsPathRooted(filePath))
            {
                return Path.GetFullPath("./");
            }

            var fileName = Path.GetFileName(filePath);
            if (string.IsNullOrEmpty(fileName))
            {
                return filePath;
            }

            return filePath.Substring(0, filePath.Length - fileName.Length);
        }
    }
}