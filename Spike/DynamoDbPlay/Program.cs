using System;
using System.Collections.Generic;
using System.Configuration;

using Amazon.Runtime;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;

namespace DynamoDbPlay
{
    class Program
    {
        private static AmazonDynamoDBClient client;

        static void Main(string[] args)
        {
            try
            {
                var config = new AmazonDynamoDBConfig();
                config.ServiceURL= ConfigurationManager.AppSettings["ServiceURL"];

                client = new AmazonDynamoDBClient(config);

                DynamoDBContext context = new DynamoDBContext(client);

                StateCityManager mgr = new StateCityManager(context);

                // mgr.SaveStateCity();
                // mgr.ShowCityList("s");

                mgr.ShowCityList("");

                // ShowTwoTablesAtATime();

                // TestCRUDOperations(context);
            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }

            Console.WriteLine("To continue, press Enter");
            Console.ReadLine();

        }

        public static void ShowTwoTablesAtATime()
        {
            Console.WriteLine("\n*** listing tables ***");
            string lastTableNameEvaluated = null;
            do
            {
                var request = new ListTablesRequest
                {
                    Limit = 2,
                    ExclusiveStartTableName = lastTableNameEvaluated
                };

                var response = client.ListTables(request);
                ListTablesResult result = response.ListTablesResult;
                foreach (string name in result.TableNames)
                {
                    Console.WriteLine("Table: " + name);
                    //ShowTableInformation(name);
                }

                lastTableNameEvaluated = result.LastEvaluatedTableName;

            } while (lastTableNameEvaluated != null);
        }

    }
}