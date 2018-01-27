using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Net;
using System.Text.RegularExpressions;

namespace Biz.ThirdParty
{
    public static class Helper
    {
        public static string CurrencyConvert_(string value, string from, string to)
        {
            //Grab your values and build your Web Request to the API
            string apiURL = String.Format("https://finance.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", value, from, to, Guid.NewGuid().ToString());

            //Make your Web Request and grab the results
            var request = WebRequest.Create(apiURL);

            //Get the Response
            var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

            //Grab your converted value (ie 2.45 USD)
            var result = Regex.Matches(streamReader.ReadToEnd(), "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

            string[] parts = result.ToString().Split(' ');
            value = parts[0].Trim();
            value = Decimal.Round(Decimal.Parse(value), 2).ToString("0.##");
            return value;
        }

        public static Double ExchangeRate_(string from, string to)
        {
            //Grab your values and build your Web Request to the API
            string apiURL = String.Format("https://finance.google.com/finance/converter?a={0}&from={1}&to={2}&meta={3}", 1, from, to, Guid.NewGuid().ToString());

            //Make your Web Request and grab the results
            var request = WebRequest.Create(apiURL);

            //Get the Response
            var streamReader = new StreamReader(request.GetResponse().GetResponseStream(), System.Text.Encoding.ASCII);

            string resultStr = streamReader.ReadToEnd();

            //Grab your converted value (ie 2.45 USD)
            var result = Regex.Matches(resultStr, "<span class=\"?bld\"?>([^<]+)</span>")[0].Groups[1].Value;

            string[] parts = result.ToString().Split(' ');
            string value = parts[0].Trim();
            return Double.Parse(value);
        }


        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }


        public static byte[] GetBytesFromFile(string fullFilePath)
        {
            // this method is limited to 2^32 byte files (4.2 GB)

            FileStream fs = null;
            try
            {
                fs = File.OpenRead(fullFilePath);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                return bytes;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        public static string StripOffEndingSpacesAndCommas(string input)
        {
            //Validate Client address line to not to have spaces or commas
            string output = input;
            if (output != null)
            {
                output = output.Trim();
                if (output.EndsWith(","))
                {
                    output = output.Substring(0, output.LastIndexOf(","));
                }
            }
            return output;
        }

    }


    public static class EnumHelper<T>
    {
        public static IList<T> GetValues(Enum value)
        {
            var enumValues = new List<T>();

            foreach (FieldInfo fi in value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                enumValues.Add((T)Enum.Parse(value.GetType(), fi.Name, false));
            }
            return enumValues;
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static IList<string> GetNames(Enum value)
        {
            return value.GetType().GetFields(BindingFlags.Static | BindingFlags.Public).Select(fi => fi.Name).ToList();
        }

        public static IList<string> GetDisplayValues(Enum value)
        {
            return GetNames(value).Select(obj => GetDisplayValue(Parse(obj))).ToList();
        }

        public static string GetDisplayValue(T value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());

                var descriptionAttributes = fieldInfo.GetCustomAttributes(
                    typeof(DisplayAttribute), false) as DisplayAttribute[];

                if (descriptionAttributes == null) return string.Empty;
                return (descriptionAttributes.Length > 0) ? descriptionAttributes[0].Name : value.ToString();
            }
            catch (Exception e)
            {
                return value!=null?value.ToString():"";
            }
        }
    }
}