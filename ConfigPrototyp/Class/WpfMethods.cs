using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigPrototyp.Class
{
    public class WpfMethods
    {
        /// <summary>
        /// Refreshes the Combibox Items
        /// </summary>
        /// <param name="sList"></param>
        public void CombiBoxRefresh(out string[] sList)
        {
            var configStr = ConfigurationManager.AppSettings;
            string[] values = new string[configStr.Count];
            int i = 0;
            List<string> configList = new List<string>();

            // reading the key and values Bin the config file
            foreach (var key in configStr.AllKeys)
            {
                values[i] = configStr[key];
                i++;
            }
            configList.Add(values[0]);
            //if (configList[0].Contains(","))
            //{

            //}
            sList = configList[0].Split(',');
            i = 0;
        }
        /// <summary>
        /// Updating the Configfile.
        /// </summary>
        /// <param name="filetype"></param>
        /// <param name="datapath"></param>
        public void Update(string filetype, string datapath)
        {

            var configStr = ConfigurationManager.AppSettings;
            string[] keys = new string[configStr.Count];
            string[] values = new string[configStr.Count];
            int i = 0;
           

            // reading the key and values in the config file
            foreach (var key in configStr.AllKeys)
            {
                keys[i] = key;
                values[i] = configStr[key];
                i++;
            }

            i = 0;

            // Variable to open the config file
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // overwrite the settings
            if (filetype != "")
            {
                configFile.AppSettings.Settings["fileTyp"].Value += "," + filetype;

            }
            //configFile.AppSettings.Settings["fileTyp"].Value = fileTyp.Text;

            configFile.AppSettings.Settings["filePath"].Value = datapath;

            // Save the configfile 
            ConfigurationManager.RefreshSection("appSettings");
            configFile.Save(ConfigurationSaveMode.Full);
            configFile.AppSettings.SectionInformation.ForceSave = true;
        }
        /// <summary>
        /// Updating the data path in the Configfile
        /// </summary>
        /// <param name="datapath"></param>
        public void ChangeDatapath(string datapath)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configFile.AppSettings.Settings["filePath"].Value = datapath;
            // Save the configfile 
            ConfigurationManager.RefreshSection("appSettings");
            configFile.Save(ConfigurationSaveMode.Full);
            configFile.AppSettings.SectionInformation.ForceSave = true;
        }
      

    }
    }
