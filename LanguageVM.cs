using System;
using System.Collections.Generic;
using System.Xml.Linq;

using EasyBudget2.ViewModels.Shared;

namespace EasyBudget2.ViewModels
{
    public class LanguageVM : ObservableObject
    {
        #region Enumerations
        /// <summary>
        /// Normally, I would place enumerations in their own file...
        /// </summary>
        public enum LanguageTypes { Chapter, Paragraph, Sentence, Phrase, Word, Other }
        #endregion

        #region Properties
        /// <summary>
        /// Object's language type
        /// </summary>
        public LanguageTypes LanguageType
        {
            get { return languageType; }
            set
            {
                languageType = value;
                RaisePropertyChangedEvent("LanguageType");
            }
        }
        private LanguageTypes languageType;

        /// <summary>
        /// Name of object
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChangedEvent("Name");
            }
        }
        private string name;

        /// <summary>
        /// Length of object
        /// </summary>
        public int Length
        {
            get { return length; }
            set
            {
                length = value;
                RaisePropertyChangedEvent("Length");
            }
        }
        private int length;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public LanguageVM()
        {
            languageType = LanguageTypes.Other;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Formats object data into an XElement
        /// </summary>
        /// <returns>XElement representation of object's data</returns>
        public XElement ToXElement()
        {
            // Create XElement
            var elem = new XElement(languageType.ToString());

            // Attach Id attribute to elem
            elem.Add(new XAttribute("Id", Id.ToString()));

            // Add member data to elem
            elem.Add(new XElement("Name", name));
            elem.Add(new XElement("Length", length.ToString()));

            return elem;
        }
        #endregion
    }
}
