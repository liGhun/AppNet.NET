using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppNetDotNet.Model
{
    public class Place
    {
        /// <summary>
        /// Primary identifier for a place. Uses factual.com's Place UUID.
        /// </summary>
        public string factual_id { get; set; }
        /// <summary>
        /// Human-friendly name.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Street address.
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// Apartment or Suite number in street address.
        /// </summary>
        public string address_extended { get; set; }
        /// <summary>
        /// City or town.
        /// </summary>
        public string locality { get; set; }
        /// <summary>
        /// State, region, province etc.
        /// </summary>
        public string region { get; set; }
        /// <summary>
        /// Additional sub-division (e.g. Wales).
        /// </summary>
        public string admin_region { get; set; }
        /// <summary>
        /// Town used in postal addressing.
        /// </summary>
        public string post_town { get; set; }
        /// <summary>
        /// PO Box.
        /// </summary>
        public string po_box { get; set; }
        /// <summary>
        /// Postcode / zipcode.
        /// </summary>
        public string postcode { get; set; }
        /// <summary>
        /// A two-letter country code in ISO 3166-1 alpha-2 format.
        /// </summary>
        public string country_code { get; set; }
        /// <summary>
        /// Latitude in decimal degrees.
        /// </summary>
        public decimal latitude { get; set; }
        /// <summary>
        /// Longitude in decimal degrees.
        /// </summary>
        public decimal longitude { get; set; }
        /// <summary>
        /// Whether the establishment is still "in business" and/or open to the public and does not refer to business hours or whether it may be serving customers at any particular moment in time.
        /// </summary>
        public bool is_open { get; set; }
        /// <summary>
        /// Telephone number in local formatting.
        /// </summary>
        public string telephone { get; set; }
        /// <summary>
        /// Fax number in local formatting.
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// Official URL of the establishment.
        /// </summary>
        public string website { get; set; }
        /// <summary>
        /// Places categories
        /// </summary>
        public List<place_category> categories { get; set; }

        public override string ToString()
        {
            if (factual_id != null && name != null)
            {
                if (!string.IsNullOrEmpty(locality))
                {
                    return name + " in " + locality + ": " + factual_id;
                }
                else
                {
                    return name + ": " + factual_id;
                }
            }
            return base.ToString();
        }
    }

    public class place_category
    {
        /// <summary>
        /// Human-friendly descriptive labels for this category. Ordered from general to specific.
        /// </summary>
        public List<string> labels { get; set; }
        /// <summary>
        /// Category ID. Corresponds to most specific category label.
        /// </summary>
        public string id { get; set; }

        public override string ToString()
        {
            if (labels != null && id != null)
            {
                return id + ": " + string.Join(",", labels);
            }
            return base.ToString();
        }
    }
}
