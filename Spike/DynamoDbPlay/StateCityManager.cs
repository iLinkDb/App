using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDbPlay
{
    [DynamoDBTable("StateCity")]
    public class StateCity
    {
        [DynamoDBHashKey]
        public string Key_State
        {
            get { return State.ToLower().Replace(" ", ""); }
            set { }
        }

        [DynamoDBRangeKey]
        public string Key_City
        {
            get { return City.ToLower().Replace(" ", ""); }
            set { }
        }

        [DynamoDBRangeKey]
        public string Key_County
        {
            get { return County.ToLower().Replace(" ", ""); }
            set { }
        }

        [DynamoDBRangeKey]
        public string Key_Zip
        {
            get { return Zip.ToLower().Replace(" ", ""); }
            set { }
        }

        [DynamoDBRangeKey]
        public string Key_AreaCode
        {
            get { return AreaCode.ToLower().Replace(" ", ""); }
            set { }
        }

        public string State { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Zip { get; set; }
        public string AreaCode { get; set; }
    }

    internal class StateCityManager
    {
        private DynamoDBContext _context;

        internal StateCityManager(DynamoDBContext context)
        {
            _context = context;
        }

        internal void SaveStateCity()
        {
            AddCity("MI", "Ada");
            AddCity("MI", "Addison");
            AddCity("MI", "Adrian");
            AddCity("MI", "Alanson");
            AddCity("MI", "Albertville");
            AddCity("MI", "Albion");
            AddCity("MI", "Algonac");
            AddCity("MI", "Allegan");
            AddCity("MI", "Allen Park");
            AddCity("MI", "Allendale");
            AddCity("MI", "Alma");
            AddCity("MI", "Almont");
            AddCity("MI", "Alpena");
            AddCity("MI", "Ann Arbor");
            AddCity("MI", "Armada");
            AddCity("MI", "Au Gres");
            AddCity("MI", "Auburn");
            AddCity("MI", "Auburn Hills");
            AddCity("MI", "Bad Axe");
            AddCity("MI", "Baldwin");
            AddCity("MI", "Bangor");
            AddCity("MI", "Baraga");
            AddCity("MI", "Barryton");
            AddCity("MI", "Battle Creek");
            AddCity("MI", "Bay City");
            AddCity("MI", "Beaverton");
            AddCity("MI", "Belding");
            AddCity("MI", "Bellaire");
            AddCity("MI", "Belleville");
            AddCity("MI", "Bellevue");
            AddCity("MI", "Benton Harbor");
            AddCity("MI", "Berkley");
            AddCity("MI", "Berrien Springs");
            AddCity("MI", "Bessemer");
            AddCity("MI", "Beverly Hills");
            AddCity("MI", "Big Rapids");
            AddCity("MI", "Birch Run");
            AddCity("MI", "Birmingham");
            AddCity("MI", "Blissfield");
            AddCity("MI", "Bloomfield Hills");
            AddCity("MI", "Bloomingdale");
            AddCity("MI", "Breckenridge");
            AddCity("MI", "Bridgman");
            AddCity("MI", "Brighton");
            AddCity("MI", "Brimley");
            AddCity("MI", "Britton");
            AddCity("MI", "Bronson");
            AddCity("MI", "Brooklyn");
            AddCity("MI", "Brown City");
            AddCity("MI", "Brownstown");
            AddCity("MI", "Brownstown Twp");
            AddCity("MI", "Bruce Twp");
            AddCity("MI", "Buchanan");
            AddCity("MI", "Burton");
            AddCity("MI", "Byron");
            AddCity("MI", "Byron Center");
            AddCity("MI", "Cadillac");
            AddCity("MI", "Caledonia");
            AddCity("MI", "Calumet");
            AddCity("MI", "Camden");
            AddCity("MI", "Canton");
            AddCity("MI", "Capac");
            AddCity("MI", "Carleton");
            AddCity("MI", "Caro");
            AddCity("MI", "Carson City");
            AddCity("MI", "Caseville");
            AddCity("MI", "Cass City");
            AddCity("MI", "Cassopolis");
            AddCity("MI", "Cedar Springs");
            AddCity("MI", "Cedarville");
            AddCity("MI", "Center Line");
            AddCity("MI", "Centreville");
            AddCity("MI", "Champion");
            AddCity("MI", "Charlotte");
            AddCity("MI", "Chassell");
            AddCity("MI", "Chatham");
            AddCity("MI", "Cheboygan");
            AddCity("MI", "Chelsea");
            AddCity("MI", "Chesaning");
            AddCity("MI", "Chesterfield");
            AddCity("MI", "Clare");
            AddCity("MI", "Clarkston");
            AddCity("MI", "Clarksville");
            AddCity("MI", "Clawson");
            AddCity("MI", "Clay");
            AddCity("MI", "Clinton");
            AddCity("MI", "Clinton Twp");
            AddCity("MI", "Clio");
            AddCity("MI", "Coldwater");
            AddCity("MI", "Coleman");
            AddCity("MI", "Coloma");
            AddCity("MI", "Colon");
            AddCity("MI", "Columbiaville");
            AddCity("MI", "Commerce Twp");
            AddCity("MI", "Comstock Park");
            AddCity("MI", "Concord");
            AddCity("MI", "Constantine");
            AddCity("MI", "Coopersville");
            AddCity("MI", "Corunna");
            AddCity("MI", "Croswell");
            AddCity("MI", "Crystal");
            AddCity("MI", "Davison");
            AddCity("MI", "Dearborn");
            AddCity("MI", "Dearborn Heights");
            AddCity("MI", "Decatur");
            AddCity("MI", "Deerfield");
            AddCity("MI", "Delton");
            AddCity("MI", "Detroit");
            AddCity("MI", "Dewitt");
            AddCity("MI", "Dexter");
            AddCity("MI", "Dimondale");
            AddCity("MI", "Dowagiac");
            AddCity("MI", "Drummond Island");
            AddCity("MI", "Dryden");
            AddCity("MI", "Dundee");
            AddCity("MI", "Durand");
            AddCity("MI", "East China");
            AddCity("MI", "East Lansing");
            AddCity("MI", "East Tawas");
            AddCity("MI", "Eastpointe");
            AddCity("MI", "Eaton Rapids");
            AddCity("MI", "Ecorse");
            AddCity("MI", "Edmore");
            AddCity("MI", "Edwardsburg");
            AddCity("MI", "Elk Rapids");
            AddCity("MI", "Elkton");
            AddCity("MI", "Elsie");
            AddCity("MI", "Escanaba");
            AddCity("MI", "Essexville");
            AddCity("MI", "Evart");
            AddCity("MI", "Ewen");
            AddCity("MI", "Farmington");
            AddCity("MI", "Farmington Hills");
            AddCity("MI", "Farwell");
            AddCity("MI", "Fennville");
            AddCity("MI", "Fenton");
            AddCity("MI", "Ferndale");
            AddCity("MI", "Flat Rock");
            AddCity("MI", "Flint");
            AddCity("MI", "Flushing");
            AddCity("MI", "Fort Gratiot");
            AddCity("MI", "Fowlerville");
            AddCity("MI", "Frankenmuth");
            AddCity("MI", "Frankfort");
            AddCity("MI", "Fraser");
            AddCity("MI", "Freeland");
            AddCity("MI", "Fremont");
            AddCity("MI", "Gagetown");
            AddCity("MI", "Galesburg");
            AddCity("MI", "Garden City");
            AddCity("MI", "Gaylord");
            AddCity("MI", "Gladstone");
            AddCity("MI", "Gladwin");
            AddCity("MI", "Gobles");
            AddCity("MI", "Grand Blanc");
            AddCity("MI", "Grand Haven");
            AddCity("MI", "Grand Ledge");
            AddCity("MI", "Grand Rapids");
            AddCity("MI", "Grandville");
            AddCity("MI", "Grant");
            AddCity("MI", "Grayling");
            AddCity("MI", "Greenville");
            AddCity("MI", "Grosse Ile");
            AddCity("MI", "Grosse Pte Farms");
            AddCity("MI", "Gwinn");
            AddCity("MI", "Hale");
            AddCity("MI", "Hamtramck");
            AddCity("MI", "Hancock");
            AddCity("MI", "Hanover");
            AddCity("MI", "Harbor Beach");
            AddCity("MI", "Harbor Springs");
            AddCity("MI", "Harper Woods");
            AddCity("MI", "Harrison");
            AddCity("MI", "Harrison Twp");
            AddCity("MI", "Hart");
            AddCity("MI", "Hartford");
            AddCity("MI", "Hartland");
            AddCity("MI", "Harvey");
            AddCity("MI", "Haslett");
            AddCity("MI", "Hastings");
            AddCity("MI", "Hazel Park");
            AddCity("MI", "Hemlock");
            AddCity("MI", "Hermansville");
            AddCity("MI", "Hesperia");
            AddCity("MI", "Highland");
            AddCity("MI", "Highland Park");
            AddCity("MI", "Hillman");
            AddCity("MI", "Hillsdale");
            AddCity("MI", "Holland");
            AddCity("MI", "Holly");
            AddCity("MI", "Holt");
            AddCity("MI", "Homer");
            AddCity("MI", "Houghton");
            AddCity("MI", "Houghton Lake");
            AddCity("MI", "Howard City");
            AddCity("MI", "Howell");
            AddCity("MI", "Hudson");
            AddCity("MI", "Hudsonville");
            AddCity("MI", "Idlewild");
            AddCity("MI", "Imlay City");
            AddCity("MI", "Indian River");
            AddCity("MI", "Inkster");
            AddCity("MI", "Ionia");
            AddCity("MI", "Iron Mountain");
            AddCity("MI", "Iron River");
            AddCity("MI", "Ironwood");
            AddCity("MI", "Ishpeming");
            AddCity("MI", "Ithaca");
            AddCity("MI", "Jackson");
            AddCity("MI", "Jenison");
            AddCity("MI", "Jonesville");
            AddCity("MI", "Kalamazoo");
            AddCity("MI", "Kalkaska");
            AddCity("MI", "Keego Harbor");
            AddCity("MI", "Kentwood");
            AddCity("MI", "Kimball");
            AddCity("MI", "Kingsford");
            AddCity("MI", "Kingsley");
            AddCity("MI", "Kingston");
            AddCity("MI", "Laingsburg");
            AddCity("MI", "Lake Linden");
            AddCity("MI", "Lake Odessa");
            AddCity("MI", "Lake Orion");
            AddCity("MI", "Lakeview");
            AddCity("MI", "Lanse");
            AddCity("MI", "Lansing");
            AddCity("MI", "Lapeer");
            AddCity("MI", "Lawrence");
            AddCity("MI", "Lawton");
            AddCity("MI", "Leslie");
            AddCity("MI", "Lexington");
            AddCity("MI", "Lincoln Park");
            AddCity("MI", "Linden");
            AddCity("MI", "Linwood");
            AddCity("MI", "Litchfield");
            AddCity("MI", "Livonia");
            AddCity("MI", "Lowell");
            AddCity("MI", "Ludington");
            AddCity("MI", "Mackinac Island");
            AddCity("MI", "Macomb");
            AddCity("MI", "Madison Heights");
            AddCity("MI", "Mancelona");
            AddCity("MI", "Manchester");
            AddCity("MI", "Manistee");
            AddCity("MI", "Manistique");
            AddCity("MI", "Manitou Beach");
            AddCity("MI", "Manton");
            AddCity("MI", "Maple Rapids");
            AddCity("MI", "Marcellus");
            AddCity("MI", "Marine City");
            AddCity("MI", "Marion");
            AddCity("MI", "Marlette");
            AddCity("MI", "Marquette");
            AddCity("MI", "Marshall");
            AddCity("MI", "Martin");
            AddCity("MI", "Marysville");
            AddCity("MI", "Mason");
            AddCity("MI", "Mattawan");
            AddCity("MI", "Mayville");
            AddCity("MI", "Mc Bain");
            AddCity("MI", "Melvindale");
            AddCity("MI", "Memphis");
            AddCity("MI", "Menominee");
            AddCity("MI", "Metamora");
            AddCity("MI", "Middleville");
            AddCity("MI", "Midland");
            AddCity("MI", "Milan");
            AddCity("MI", "Milford");
            AddCity("MI", "Mio");
            AddCity("MI", "Mohawk");
            AddCity("MI", "Monroe");
            AddCity("MI", "Montague");
            AddCity("MI", "Montrose");
            AddCity("MI", "Morenci");
            AddCity("MI", "Mt Clemens");
            AddCity("MI", "Mt Morris");
            AddCity("MI", "Mt Pleasant");
            AddCity("MI", "Muir");
            AddCity("MI", "Munising");
            AddCity("MI", "Muskegon");
            AddCity("MI", "Mussey");
            AddCity("MI", "Nashville");
            AddCity("MI", "Nazareth");
            AddCity("MI", "Negaunee");
            AddCity("MI", "New Baltimore");
            AddCity("MI", "New Buffalo");
            AddCity("MI", "New Haven");
            AddCity("MI", "New Hudson");
            AddCity("MI", "Newaygo");
            AddCity("MI", "Newberry");
            AddCity("MI", "Newport");
            AddCity("MI", "Niles");
            AddCity("MI", "North Branch");
            AddCity("MI", "North Muskegon");
            AddCity("MI", "Northville");
            AddCity("MI", "Norway");
            AddCity("MI", "Novi");
            AddCity("MI", "Nunica");
            AddCity("MI", "Oak Park");
            AddCity("MI", "Okemos");
            AddCity("MI", "Olivet");
            AddCity("MI", "Onaway");
            AddCity("MI", "Onsted");
            AddCity("MI", "Orion");
            AddCity("MI", "Oscoda");
            AddCity("MI", "Otisville");
            AddCity("MI", "Otsego");
            AddCity("MI", "Ottawa Lake");
            AddCity("MI", "Ovid");
            AddCity("MI", "Owosso");
            AddCity("MI", "Oxford");
            AddCity("MI", "Parchment");
            AddCity("MI", "Paw Paw");
            AddCity("MI", "Pentwater");
            AddCity("MI", "Perry");
            AddCity("MI", "Petersburg");
            AddCity("MI", "Petoskey");
            AddCity("MI", "Pewamo");
            AddCity("MI", "Pigeon");
            AddCity("MI", "Pinconning");
            AddCity("MI", "Plainwell");
            AddCity("MI", "Plymouth");
            AddCity("MI", "Pontiac");
            AddCity("MI", "Port Austin");
            AddCity("MI", "Port Huron");
            AddCity("MI", "Port Sanilac");
            AddCity("MI", "Portage");
            AddCity("MI", "Portland");
            AddCity("MI", "Potterville");
            AddCity("MI", "Powers");
            AddCity("MI", "Prudenville");
            AddCity("MI", "Pullman");
            AddCity("MI", "Quincy");
            AddCity("MI", "Rapid River");
            AddCity("MI", "Ravenna");
            AddCity("MI", "Reading");
            AddCity("MI", "Redford");
            AddCity("MI", "Reed City");
            AddCity("MI", "Remus");
            AddCity("MI", "Republic");
            AddCity("MI", "Richland");
            AddCity("MI", "Richmond");
            AddCity("MI", "River Rouge");
            AddCity("MI", "Riverview");
            AddCity("MI", "Rochester");
            AddCity("MI", "Rochester Hills");
            AddCity("MI", "Rockford");
            AddCity("MI", "Rockwood");
            AddCity("MI", "Rogers City");
            AddCity("MI", "Romeo");
            AddCity("MI", "Romulus");
            AddCity("MI", "Roscommon");
            AddCity("MI", "Rose City");
            AddCity("MI", "Rosebush");
            AddCity("MI", "Roseville");
            AddCity("MI", "Royal Oak");
            AddCity("MI", "Saginaw");
            AddCity("MI", "Saline");
            AddCity("MI", "Sand Lake");
            AddCity("MI", "Sandusky");
            AddCity("MI", "Sanford");
            AddCity("MI", "Saranac");
            AddCity("MI", "Saugatuck");
            AddCity("MI", "Sault Ste Marie");
            AddCity("MI", "Schoolcraft");
            AddCity("MI", "Scottville");
            AddCity("MI", "Sebewaing");
            AddCity("MI", "Shelby");
            AddCity("MI", "Shelby Twp");
            AddCity("MI", "Shepherd");
            AddCity("MI", "Sheridan");
            AddCity("MI", "South Haven");
            AddCity("MI", "South Lyon");
            AddCity("MI", "South Range");
            AddCity("MI", "South Rockwood");
            AddCity("MI", "Southfield");
            AddCity("MI", "Southgate");
            AddCity("MI", "Sparta");
            AddCity("MI", "Spring Arbor");
            AddCity("MI", "Spring Lake");
            AddCity("MI", "Springfield");
            AddCity("MI", "Springport");
            AddCity("MI", "St Charles");
            AddCity("MI", "St Clair");
            AddCity("MI", "St Clair Shores");
            AddCity("MI", "St Ignace");
            AddCity("MI", "St Johns");
            AddCity("MI", "St Joseph");
            AddCity("MI", "St Louis");
            AddCity("MI", "Standish");
            AddCity("MI", "Stanton");
            AddCity("MI", "Sterling Heights");
            AddCity("MI", "Stevensville");
            AddCity("MI", "Stockbridge");
            AddCity("MI", "Sturgis");
            AddCity("MI", "Sunfield");
            AddCity("MI", "Swartz Creek");
            AddCity("MI", "Taylor");
            AddCity("MI", "Tecumseh");
            AddCity("MI", "Tekonsha");
            AddCity("MI", "Temperance");
            AddCity("MI", "Three Oaks");
            AddCity("MI", "Three Rivers");
            AddCity("MI", "Traverse City");
            AddCity("MI", "Trenton");
            AddCity("MI", "Troy");
            AddCity("MI", "Twin Lake");
            AddCity("MI", "Union City");
            AddCity("MI", "Utica");
            AddCity("MI", "Van Buren Twp");
            AddCity("MI", "Vanderbilt");
            AddCity("MI", "Vassar");
            AddCity("MI", "Vicksburg");
            AddCity("MI", "Wakefield");
            AddCity("MI", "Waldron");
            AddCity("MI", "Walled Lake");
            AddCity("MI", "Warren");
            AddCity("MI", "Washington");
            AddCity("MI", "Waterford");
            AddCity("MI", "Watervliet");
            AddCity("MI", "Wayland");
            AddCity("MI", "Wayne");
            AddCity("MI", "Webberville");
            AddCity("MI", "West Bloomfield");
            AddCity("MI", "West Branch");
            AddCity("MI", "West Olive");
            AddCity("MI", "Westland");
            AddCity("MI", "White Cloud");
            AddCity("MI", "White Lake");
            AddCity("MI", "White Pigeon");
            AddCity("MI", "Whitehall");
            AddCity("MI", "Whitmore Lake");
            AddCity("MI", "Whittemore");
            AddCity("MI", "Williamston");
            AddCity("MI", "Wixom");
            AddCity("MI", "Woodland");
            AddCity("MI", "Wyandotte");
            AddCity("MI", "Wyoming");
            AddCity("MI", "Yale");
            AddCity("MI", "Ypsilanti");
            AddCity("MI", "Zeeland");

        }

        internal void ShowCityList(string rangeStartsWith)
        {
            string hashKey = "mi"; // Hash value.

            IEnumerable<StateCity> cityList;
            if (string.IsNullOrEmpty(rangeStartsWith))
            {
                cityList = _context.Query<StateCity>(hashKey);
            }
            else
            {
                cityList = _context.Query<StateCity>(hashKey, QueryOperator.BeginsWith, rangeStartsWith);
            }

            foreach (StateCity item in cityList)
            {
                Console.WriteLine("City: " + item.City + " (key=" + item.Key_City + ")");
            }
        }

        private void AddCity(string state, string city)
        {
            StateCity one = new StateCity
            {
                State = state,
                City = city,
            };

            _context.Save(one);
            Console.WriteLine("City: " + city);
            System.Threading.Thread.Sleep(100);
        }

    }

}
