using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GACViewer
{
    public class AssemblyObject : INotifyPropertyChanged
    {
        private String m_AssemblyName;
        private String m_Version;
        private String m_Culture;
        private String m_PublicKeyToken;
        private String m_processorArchitecture;

        public String AssemblyName
        {
            get { return m_AssemblyName; }
            set { if (m_AssemblyName == value)return; m_AssemblyName = value; Notify("AssemblyName"); }
        }

        public String Version
        {
            get { return m_Version; }
            set { if (m_Version == value)return; m_Version = value; Notify("Version"); }
        }

        public String Culture
        {
            get { return m_Culture; }
            set { if (m_Culture == value)return; m_Culture = value; Notify("Culture"); }
        }

        public String PublicKeyToken
        {
            get { return m_PublicKeyToken; }
            set { if (m_PublicKeyToken == value)return; m_PublicKeyToken = value; Notify("PublicKeyToken"); }
        }

        public String ProcessorArchitecture
        {
            get { return m_processorArchitecture; }
            set { if (m_processorArchitecture == value)return; m_processorArchitecture = value; Notify("processorArchitecture"); }
        }

        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void Notify(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
