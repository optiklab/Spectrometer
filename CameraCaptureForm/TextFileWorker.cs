using System;
using System.IO;
using System.Text;

namespace CameraCapture
{
    internal sealed class TextFileWorker
    {
        #region Public method

        /// <summary>
        /// Method read data from file.
        /// </summary>
        /// <param name="filePath">Path to file to read.</param>
        /// <returns>String of read data.</returns>
        public String ReadFromFile(String filePath)
        {
            String readText = String.Empty;

            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            try
            {
                using (var sr = new StreamReader(filePath, Encoding.Default))
                {
                    readText = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
            }

            return readText;
        }

        /// <summary>
        /// Method writes data to specified file.
        /// </summary>
        /// <param name="filePath">File to write.</param>
        /// <param name="textToWrite">Text to write.</param>
        /// <param name="format">Encoding format.</param>
        public void WriteToFile(String filePath, String textToWrite, Encoding format)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            try
            {
                using (var writetofile = new StreamWriter(filePath, false, format, textToWrite.Length))
                {
                    writetofile.Write(textToWrite);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Method writes data to specified file.
        /// </summary>
        /// <param name="filePath">File to write.</param>
        /// <param name="textToWrite">Text to write.</param>
        public void WriteToFile(String filePath, String textToWrite)
        {
            if (String.IsNullOrEmpty(filePath))
                throw new ArgumentNullException("filePath");

            try
            {
                using(var writetofile = new StreamWriter(filePath, false, Encoding.Default, textToWrite.Length))
                {
                    writetofile.Write(textToWrite);
                }
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }

}
