using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace QR_Publisher
{   
    
    class Settings
    {
        private bool isLoaded;
        private string filesPath;
        private String[] settings;
        private string autoUpdate;
        private string UpdateTime;
        private string PortName;

        public Settings()
        {
            isLoaded = false;
            loadSettings();
        }
        private void loadSettings()
        {
            try
            {
                this.settings = File.ReadAllLines("settings.ini");
                foreach (string item in settings)
                {
                    if (item.ToUpper().StartsWith("path=".ToUpper()))
                    {
                        this.filesPath = item.Substring(5);
                    }
                    if (item.ToUpper().StartsWith("PortName=".ToUpper()))
                    {
                        this.PortName = item.Substring(9);
                    }
                    if (item.ToUpper().StartsWith("AutoUpdate=".ToUpper()))
                    {
                        this.autoUpdate = item.Substring(11);
                    }
                    if (item.ToUpper().StartsWith("UpdateTime=".ToUpper()))
                    {
                        this.UpdateTime = item.Substring(11);
                    }
                }
                this.isLoaded = true;
            } catch (Exception e)
            {
                isLoaded = false;
            }
        }
        public String getPortName()
        {
            return this.PortName;
        }
        public String getAutoUpdate()
        {
            return this.autoUpdate;
        }
        public String getUpdateTime()
        {
            return this.UpdateTime;
        }
        public String getPath()
        {
            return this.filesPath;
        }
        public Boolean getLoaded()
        {
            return this.isLoaded;
        }
    }
}
