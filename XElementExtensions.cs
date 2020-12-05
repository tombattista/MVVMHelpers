using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace EasyBudget2.ViewModels.Shared
{
    /// <summary>
    /// Extension methods to facilitate populating objects from XElements
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Parses basic data type from XElement value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="hasError"></param>
        /// <returns></returns>
        public static T GetXElementChildValueFromParent<T>(this XElement parentElem, string childElementName, T defaultValue, ObservableObject parentObject, ref bool hasError) where T : struct
        {
            // Return value
            if (parentElem.Element(childElementName) == null || parentElem.Element(childElementName).TryParseValue(out T returnVal) == false)
            {
                hasError = true;
                returnVal = defaultValue;
                parentObject.SetErrorState(childElementName, true);
            }

            return returnVal;
        }

        /// <summary>
        /// Parses basic data type from XElement value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="hasError"></param>
        /// <returns></returns>
        public static T? GetXElementChildValueFromParent<T>(this XElement parentElem, string childElementName, T? defaultValue, ObservableObject parentObject, ref bool hasError) where T : struct
        {
            // Return value
            if (parentElem.Element(childElementName) == null || parentElem.Element(childElementName).TryParseValue(out T? returnVal) == false)
            {
                hasError = true;
                returnVal = defaultValue;
                parentObject.SetErrorState(childElementName, true);
            }

            return returnVal;
        }
        
        /// <summary>
        /// Parses basic data type from XElement value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="hasError"></param>
        /// <returns></returns>
        public static T GetXElementChildValueFromParent<T>(this XElement parentElem, string childElementName, ObservableObject parentObject, ref bool hasError) where T : struct
        {
            // Return value
            if (parentElem.Element(childElementName) == null || parentElem.Element(childElementName).TryParseValue(out T returnVal) == false)
            {
                hasError = true;
                returnVal = default;
                parentObject.SetErrorState(childElementName, true);
            }

            return returnVal;
        }
        
        /// <summary>
        /// Parses enum data type from XElement value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parentObject"></param>
        /// <param name="hasError"></param>
        /// <returns></returns>
        public static T GetXElementChildValueFromParent<T>(this XElement parentElem, string childElementName, ObservableObject parentObject, T defaultValue, ref bool hasError) where T : struct, IConvertible
        {
            // Return value
            T returnVal;
            if (parentElem.Element(childElementName) == null || Enum.TryParse<T>(parentElem.Element(childElementName).Value, out returnVal) == false)
            {
                hasError = true;
                returnVal = defaultValue;
                parentObject.SetErrorState(childElementName, true);
            }

            return returnVal;
        }

        /// <summary>
        /// Parses string from XElement value
        /// </summary>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parentObject"></param>
        /// <param name="hasError"></param>
        /// <param name="isRequired"></param>
        /// <returns></returns>
        public static string GetXElementChildValueFromParent(this XElement parentElem, string childElementName, string defaultValue, ObservableObject parentObject, ref bool hasError, bool isRequired = false)
        {
            // Return value
            string returnVal = defaultValue;
            if ((parentElem.Element(childElementName) == null || string.IsNullOrEmpty(parentElem.Element(childElementName).Value)) && isRequired == true)
            {
                hasError = true;
                parentObject.SetErrorState(childElementName, true);
            }
            else if (parentElem.Element(childElementName) != null && string.IsNullOrEmpty(parentElem.Element(childElementName).Value) == false)
            {
                returnVal = parentElem.Element(childElementName).Value;
            }

            return returnVal;
        }

        /// <summary>
        /// Parses string from XElement value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parentElem"></param>
        /// <param name="childElementName"></param>
        /// <param name="defaultValue"></param>
        /// <param name="hasError"></param>
        /// <returns></returns>
        public static string GetXElementChildValueFromParent(this XElement parentElem, string childElementName, ObservableObject parentObject, ref bool hasError, bool isRequired = false)
        {
            // Return value
            string returnVal = "";
            if ((parentElem.Element(childElementName) == null || string.IsNullOrEmpty(parentElem.Element(childElementName).Value)) && isRequired == true)
            {
                hasError = true;
                parentObject.SetErrorState(childElementName, true);
            }
            else if (parentElem.Element(childElementName) != null && string.IsNullOrEmpty(parentElem.Element(childElementName).Value) == false)
            {
                returnVal = parentElem.Element(childElementName).Value;
            }

            return returnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseValue<T>(this XElement elem, out T val) where T : struct
        {
            bool isSuccessful = true;

            if (TryParseHelper<T>.TryParse(elem.Value, out val) == false)
            {
                val = default;
                isSuccessful = false;
            }

            return isSuccessful;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool TryParseValue<T>(this XElement elem, out T? val) where T : struct
        {
            bool isSuccessful = true;

            if (elem == null || TryParseHelper<T>.TryParse(elem.Value, out T returnValue) == false)
            {
                val = null;
                isSuccessful = false;
            }
            else
            {
                val = returnValue;
            }

            return isSuccessful;
        }
    }
}
