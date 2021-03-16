using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;

namespace OpenMacroInputComAPI
{

    public class TargetMemoryFileWithMutex
    {

        public string m_fileName = "";
        public const int m_maxMemorySize = 1000000;
        public MemoryMappedFile m_memoryFile;
        public Mutex m_memoryFileMutex;



        public TargetMemoryFileWithMutex(string fileName)
        {
            m_fileName = fileName;
            m_memoryFile = MemoryMappedFile.CreateOrOpen(fileName, m_maxMemorySize);

            string mutexId = string.Format("Global\\{{{0}}}", fileName + "mutex");
            bool createdNew;
            var allowEveryoneRule =
                new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid
                                                           , null)
                                   , MutexRights.FullControl
                                   , AccessControlType.Allow
                                   );
            var securitySettings = new MutexSecurity();
            securitySettings.AddAccessRule(allowEveryoneRule);
            m_memoryFileMutex = new Mutex(false, mutexId, out createdNew, securitySettings);
        }


        public delegate void DoWhenFileNotUsed();
        public void WaitUntilMutexAllowIt(DoWhenFileNotUsed todo)
        {

            var hasHandle = false;
            try
            {
                try
                {

                    // mutex.WaitOne(Timeout.Infinite, false);
                    hasHandle = m_memoryFileMutex.WaitOne(5000, false);
                    if (hasHandle == false)
                        throw new TimeoutException("Timeout waiting for exclusive access");
                }
                catch (AbandonedMutexException)
                {
                    hasHandle = true;
                }
                todo();
            }
            finally
            {
                if (hasHandle)
                    m_memoryFileMutex.ReleaseMutex();
            }

        }

        public void ResetMemory()
        {

            WaitUntilMutexAllowIt(MutexResetMemory);

        }

        private void MutexResetMemory()
        {


            using (MemoryMappedViewStream stream = m_memoryFile.CreateViewStream())
            {
                BinaryWriter writer = new BinaryWriter(stream);
                writer.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                writer.BaseStream.Write(new byte[m_maxMemorySize], 0, m_maxMemorySize);
                writer.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);

                //                    Thread.Sleep(1000);
            }
        }



        public void AppendText(string textToAdd)
        {
            WaitUntilMutexAllowIt(() =>
            {
                MutexAppendText(textToAdd);
            });

        }
        private void MutexAppendText(string textToAdd)
        {

            string readText;
            using (MemoryMappedViewStream stream = m_memoryFile.CreateViewStream())
            {

                MutexTextRecovering(out readText, false);

                BinaryWriter writer = new BinaryWriter(stream);
                string nexText = readText + textToAdd;
                if (nexText.Length > m_maxMemorySize)
                    nexText = nexText.Substring(0, m_maxMemorySize);

                writer.Write(nexText);

            }



        }



        public void SetText(string text)
        {
            WaitUntilMutexAllowIt(() =>
            {
                MutexSetText(text);
            });

        }
        private void MutexSetText(string text)
        {

            using (MemoryMappedViewStream stream = m_memoryFile.CreateViewStream())
            {

                MutexResetMemory();

                BinaryWriter writer = new BinaryWriter(stream);
                string nexText =  text.Trim();
                if (nexText.Length > m_maxMemorySize)
                    nexText = nexText.Substring(0, m_maxMemorySize);

                writer.Write(nexText);

            }



        }

        public void TextRecovering(out string readText, bool removeContentAfter = true)
        {

            string textFound = "";
            WaitUntilMutexAllowIt(() => {
                MutexTextRecovering(out textFound, removeContentAfter);
            });
            readText = textFound;

        }

        private void MutexTextRecovering(out string readText, bool directremove = true)
        {
            readText = "";

            using (MemoryMappedViewStream stream = m_memoryFile.CreateViewStream())
            {
                BinaryReader reader = new BinaryReader(stream);
                StringBuilder strb = new StringBuilder();
                string str;
                do
                {
                    str = reader.ReadString();
                    if ((!String.IsNullOrEmpty(str) && str[0] != 0))
                        strb.AppendLine(str);
                } while (!String.IsNullOrEmpty(str));

                readText = strb.ToString();

                if (directremove)
                {
                    MutexResetMemory();
                }

            }
        }


    }

}
