using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCL.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string DinnersEndpoint = $"{Prefix}/dinners";
        public static readonly string FestivalsEndpoint = $"{Prefix}/festivals";
        public static readonly string CafesEndpoint = $"{Prefix}/cafes";
        public static readonly string MoviesEndpoint = $"{Prefix}/movies";
        public static readonly string ApplicantsEndpoint = $"{Prefix}/applicants";
        public static readonly string MyActivitiesEndpoint = $"{Prefix}/myactivities";
    }
    
}
