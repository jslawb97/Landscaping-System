using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public static class Constants
    {
        public static readonly int MINPASSWORDLENGTH = 5;  // business rule
        public static readonly int MINUSERNAMELENGTH = 8;  // forced by naming rules
        public static readonly int MAXUSERNAMELENGTH = 100;// forced by db field length  
        public static readonly int IDSTARTVALUE = 1000000;
        public static readonly int MAXNAMELENGTH = 100;
        public static readonly int MAXDESCRIPTIONLENGTH = 1000;

        public static readonly int MAXADDRESSLENGTH = 250;
        public static readonly int MAXPHONENUMBERLENGTH = 15;
        public static readonly int MAXEMAILLENGTH = 100;

        public const int MAX_JOB_LOCATION_STREET_LENGTH = 100;
        public const int MAX_JOB_LOCATION_CITY_LENGTH = 100;
        public const int MAX_JOB_LOCATION_ZIP_CODE_LENGTH = 15;
        public const int MAX_CUSTOMER_EMAIL_LENGTH = 100;
        public const int MAX_CUSTOMER_FIRST_NAME_LENGTH = 100;
        public const int MAX_CUSTOMER_LAST_NAME_LENGTH = 100;


        public const int MAX_SPECIAL_ITEM_NAME_LENGTH = 100;

        public static readonly string[] STATES = { "AK",
                      "AL",
                      "AR",
                      "AS",
                      "AZ",
                      "CA",
                      "CO",
                      "CT",
                      "DC",
                      "DE",
                      "FL",
                      "GA",
                      "GU",
                      "HI",
                      "IA",
                      "ID",
                      "IL",
                      "IN",
                      "KS",
                      "KY",
                      "LA",
                      "MA",
                      "MD",
                      "ME",
                      "MI",
                      "MN",
                      "MO",
                      "MS",
                      "MT",
                      "NC",
                      "ND",
                      "NE",
                      "NH",
                      "NJ",
                      "NM",
                      "NV",
                      "NY",
                      "OH",
                      "OK",
                      "OR",
                      "PA",
                      "PR",
                      "RI",
                      "SC",
                      "SD",
                      "TN",
                      "TX",
                      "UT",
                      "VA",
                      "VI",
                      "VT",
                      "WA",
                      "WI",
                      "WV",
                      "WY"};

    }
}
