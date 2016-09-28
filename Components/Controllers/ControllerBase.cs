using System;
using System.Text;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace DNNGamification.Components.Controllers
{
    public class ControllerBase : IPortable
    {
        private const string RootSettings = "Settings";

        protected virtual string RootElement { get { return string.Empty; } }

        private static string ExtractString(object obj)
        {
            return obj == null ? string.Empty : obj.ToString();
        }

        public string ExportModule(int moduleID)
        {
            var strXML = new StringBuilder();
            var controller = new ModuleController();
            var settingsTable = controller.GetModule(moduleID, Null.NullInteger, true).ModuleSettings;

            var xmlSsettings = new XmlWriterSettings { Indent = true, OmitXmlDeclaration = true };
            var writer = XmlWriter.Create(strXML, xmlSsettings);

            writer.WriteStartElement(RootElement);
            writer.WriteStartElement(RootSettings);

            var settings = settingsTable.GetEnumerator();

            while (settings.MoveNext())
            {
                writer.WriteElementString(settings.Key.ToString(), ExtractString(settings.Value));
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
            return strXML.ToString();

        }

        public void ImportModule(int moduleID, string content, string version, int userID)
        {
            var objModuleController = new ModuleController();
            var contentType =  String.Format("{0}/{1}", RootElement, RootSettings);  
            var xmlSettings = DotNetNuke.Common.Globals.GetContent(content, contentType);
            foreach (XmlNode xmlSetting in xmlSettings)
            {
                var settingName = xmlSetting.Name;
                var node = xmlSetting.ChildNodes.Item(0);
                if (node == null) continue;
                var settingValue = node.Value;
                objModuleController.UpdateModuleSetting(moduleID, settingName, settingValue);
            }
            ModuleController.SynchronizeModule(moduleID);
        }
    }
}
