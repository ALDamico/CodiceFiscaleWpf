using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using ALD.LibFiscalCode.Interfaces;
using ALD.LibFiscalCode.Persistence.Models;
using ALD.LibFiscalCode.StringManipulation;
using ALD.LibFiscalCode.ViewModels;

namespace ALD.LibFiscalCode.Exporters
{
    public class XmlExporter : IExportStrategy
    {
        public void Export(Person person, string path)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            var exportedObject = new PersonJson(person, person.FiscalCode);
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.IndentChars = "    ";
            using (XmlWriter writer = XmlWriter.Create(path, settings))
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

        public void Export(IEnumerable<Person> peopleList, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}