using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSBHelper.Common
{
    public class Utils
    {
        
        public static string CurrentDir
        {
            get
            {                
                return Directory.GetCurrentDirectory(); 
            }            
        }        
        public static string SSBHelperDir
        {
            get
            {
                return Path.Combine(CurrentDir, Constants.SSBHelperDirName);
            }
        }
        public static string PPDTDir
        {
            get
            {
                return Path.Combine(SSBHelperDir, Constants.PPDTDirName);
            }
        }
        public static string TATDir
        {
            get
            {
                return Path.Combine(SSBHelperDir, Constants.TATDirName);
            }
        }
        public static List<string> PPDT_Pictures { get; set; }        
        public static List<string> TAT_Pictures { get; set; }                        
        public static int MAX_PPDT_PIC_COUNT
        {
            get
            {
                if (null != PPDT_Pictures)
                    return PPDT_Pictures.Count;
                return 0;
            }
        }

        public static string GetPictureNo(string filePath)
        {
            return System.IO.Path.GetFileNameWithoutExtension(filePath);
        }

        public static int MAX_TAT_PIC_COINT
        {
            get
            {
                if (null != TAT_Pictures)
                    return TAT_Pictures.Count;
                return 0;
            }
        }
        public static List<string> WAT_SET_LIST { get; set; }
        public static List<string> SRT_SET_LIST { get; set; }
        public static void ReloadTATPictues()
        {
            TAT_Pictures = new List<string>();
            if (Directory.Exists(TATDir))
            {
                TAT_Pictures = Directory.GetFiles(TATDir).ToList();
            }
        }
        public static void ReloadPPDTPictues()
        {
            PPDT_Pictures = new List<string>();
            if (Directory.Exists(PPDTDir))
            {
                PPDT_Pictures = Directory.GetFiles(PPDTDir).ToList();
            }
        }
        public static void InitializeApp()
        {
            // Getting PPDT pictures list
            PPDT_Pictures = new List<string>();
            if (Directory.Exists(PPDTDir))
            {                
                PPDT_Pictures = Directory.GetFiles(PPDTDir).ToList();                
            }

            // Getting TAT pictures  list
            TAT_Pictures = new List<string>();
            if (Directory.Exists(TATDir))
            {                
                TAT_Pictures = Directory.GetFiles(TATDir).ToList();                
            }

            //Getting list of sets for WAT
            if (Directory.Exists(WATDir))
            {
                WAT_SET_LIST = Directory.GetFiles(WATDir).Select(i => Path.GetFileNameWithoutExtension(i)).ToList();
            }

            //Getting list of sets for SRT
            if (Directory.Exists(SRTDir))
            {
                SRT_SET_LIST = Directory.GetFiles(SRTDir).Select(i => Path.GetFileNameWithoutExtension(i)).ToList();
            }
        }
        public static string WATDir
        {
            get
            {
                return Path.Combine(SSBHelperDir, Constants.WATDirName);
            }
        }
        public static string SRTDir
        {
            get
            {
                return Path.Combine(SSBHelperDir, Constants.SRTDirName);
            }
        }

        public static List<string> VISITED_PPDT_PICTURES = new List<string>();

        public static async Task PutTaskDelay(int waitTime)
        {
            await Task.Delay(waitTime);
        }
        public static void CreateDirStructure()
        {
            if (!Directory.Exists(SSBHelperDir))
                Directory.CreateDirectory(SSBHelperDir);
            if (!Directory.Exists(PPDTDir))
                Directory.CreateDirectory(PPDTDir);
            if (!Directory.Exists(TATDir))
                Directory.CreateDirectory(TATDir);
            if (!Directory.Exists(WATDir))
                Directory.CreateDirectory(WATDir);
            if (!Directory.Exists(SRTDir))
                Directory.CreateDirectory(SRTDir);
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        public static bool IsOnlyNumbersInIt(string text)
        {
            return _regex.IsMatch(text);
        }
    }
}
