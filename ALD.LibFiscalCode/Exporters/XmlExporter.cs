using System;
using System.Text;
using System.Xml;
using ALD.LibFiscalCode.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;
using ALD.LibFiscalCode.ViewModels;

namespace ALD.LibFiscalCode.Exporters
{
    public class XmlExporter:IExportStrategy
    {
        public void Export(MainViewModel viewModel, string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var exportedObject = new PersonJson(viewModel.CurrentPerson, viewModel.FiscalCode);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "    ";
            using (XmlWriter writer = XmlTextWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("person");

                writer.WriteStartElement("name");
                writer.WriteString(exportedObject.Name);//name
                writer.WriteEndElement();//name

                writer.WriteStartElement("surname"); //surname
                writer.WriteString(exportedObject.Surname);
                writer.WriteEndElement(); //surname

                writer.WriteStartElement("dateOfBirth"); //dateOfBirth
                writer.WriteString(exportedObject.DateOfBirth.ToString(DateFormat.RoundTripSchema));
                writer.WriteEndElement(); //dateOfBirth

                writer.WriteStartElement("gender");//gender
                writer.WriteString(exportedObject.Gender);
                writer.WriteEndElement(); //gender

                writer.WriteStartElement("placeOfBirth");//placeOfBirth
                writer.WriteString(exportedObject.PlaceOfBirth);
                writer.WriteEndElement(); //placeOfBirth

                writer.WriteStartElement("fiscalCode");//fiscalCode
                writer.WriteString(exportedObject.FiscalCode);
                writer.WriteEndElement(); //fiscalCode

                writer.WriteEndElement(); //person
                writer.WriteEndDocument();
            }
        }
    }
}