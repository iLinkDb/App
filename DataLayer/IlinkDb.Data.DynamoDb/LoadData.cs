using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace IlinkDb.Data.DynamoDb
{
    internal class LoadData
    {
        private static AmazonDynamoDBClient _client;

        public LoadData(AmazonDynamoDBClient client)
        {
            // _client = new AmazonDynamoDBClient(AWSAccessKey, AWSSecretKey, RegionEndpoint.USWest2);
            _client = client;
        }

        public void Load()
        {
            ShowTwoTablesAtATime();
        }

        public void ShowTwoTablesAtATime()
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

                var response = _client.ListTables(request);
                ListTablesResult result = response.ListTablesResult;
                foreach (string name in result.TableNames)
                {
                    ShowTableInformation(name);
                }

                lastTableNameEvaluated = result.LastEvaluatedTableName;

            } while (lastTableNameEvaluated != null);
        }

        private static void ShowTableInformation(string tableName)
        {
            Console.WriteLine("\n*** Retrieving table information ***");
            var request = new DescribeTableRequest
            {
                TableName = tableName
            };

            var response = _client.DescribeTable(request);

            TableDescription description = response.DescribeTableResult.Table;
            Console.WriteLine("Name: {0}", description.TableName);
            Console.WriteLine("# of items: {0}", description.ItemCount);
            Console.WriteLine("Provision Throughput (reads/sec): {0}",
                             description.ProvisionedThroughput.ReadCapacityUnits);
            Console.WriteLine("Provision Throughput (writes/sec): {0}",
                             description.ProvisionedThroughput.WriteCapacityUnits);

        }

        public void CreateExampleTable(string tableName)
        {
            Console.WriteLine("\n*** Creating table ***");
            var request = new CreateTableRequest
            {
                AttributeDefinitions = new List<AttributeDefinition>()
        {
          new AttributeDefinition
          {
            AttributeName = "Id",
            AttributeType = "N"
          }
          //,
          //new AttributeDefinition
          //{
          //  AttributeName = "ReplyDateTime",
          //  AttributeType = "N"
          //}
        },
                KeySchema = new List<KeySchemaElement>
        {
          new KeySchemaElement
          {
            AttributeName = "Id",
            KeyType = "HASH"
          }
          //,
          //new KeySchemaElement
          //{
          //  AttributeName = "ReplyDateTime",
          //  KeyType = "RANGE"
          //}
        },
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 5,
                    WriteCapacityUnits = 6
                },
                TableName = tableName
            };

            var response = _client.CreateTable(request);

            var result = response.CreateTableResult;
            var tableDescription = result.TableDescription;
            Console.WriteLine("{1}: {0} \t ReadsPerSec: {2} \t WritesPerSec: {3}",
                            tableDescription.TableStatus,
                            tableDescription.TableName,
                            tableDescription.ProvisionedThroughput.ReadCapacityUnits,
                            tableDescription.ProvisionedThroughput.WriteCapacityUnits);

            string status = tableDescription.TableStatus;
            Console.WriteLine(tableName + " - " + status);

            WaitUntilTableReady(tableName);

        }

        private static void UpdateExampleTable(string tableName)
        {
            Console.WriteLine("\n*** Updating table ***");
            var request = new UpdateTableRequest()
            {
                TableName = tableName,
                ProvisionedThroughput = new ProvisionedThroughput()
                {
                    ReadCapacityUnits = 6,
                    WriteCapacityUnits = 7
                }
            };

            var response = _client.UpdateTable(request);

            WaitUntilTableReady(tableName);
        }

        private static void DeleteExampleTable(string tableName)
        {
            Console.WriteLine("\n*** Deleting table ***");
            var request = new DeleteTableRequest
            {
                TableName = tableName
            };

            var response = _client.DeleteTable(request);

            var result = response.DeleteTableResult;
            Console.WriteLine("Table is being deleted...");
        }

        private static void WaitUntilTableReady(string tableName)
        {
            string status = null;
            // Let us wait until table is created. Call DescribeTable.
            do
            {
                System.Threading.Thread.Sleep(5000); // Wait 5 seconds.
                try
                {
                    var res = _client.DescribeTable(new DescribeTableRequest
                    {
                        TableName = tableName
                    });

                    Console.WriteLine("Table name: {0}, status: {1}",
                                   res.DescribeTableResult.Table.TableName,
                                   res.DescribeTableResult.Table.TableStatus);
                    status = res.DescribeTableResult.Table.TableStatus;
                }
                catch (ResourceNotFoundException)
                {
                    // DescribeTable is eventually consistent. So you might
                    // get resource not found. So we handle the potential exception.
                }
            } while (status != "ACTIVE");
        }

        public void PutItem(string tableName)
        {
            var request = new PutItemRequest
                             {
                                 TableName = tableName,
                                 Item = new Dictionary<string, AttributeValue>()
                                       {
                                          {"Id", new AttributeValue {N = "201"}},
                                          {"Title", new AttributeValue {S = "Book 201 Title"}},
                                          {"ISBN", new AttributeValue {S = "11-11-11-11"}},
                                          {"Price", new AttributeValue {S = "20.00"}},
                                          {
                                             "Authors",
                                             new AttributeValue
                                                {SS = new List<string> {"Author1", "Author2"}}
                                             }
                                       }
                             };
            _client.PutItem(request);
        }

        public void GetItem(string tableName)
        {
            var request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>() { { "Id", new AttributeValue { N = "201" } } },
            };
            var response = _client.GetItem(request);

            // Check the response.
            var result = response.GetItemResult;
            var attributeMap = result.Item; // Attribute list in the response.
        }
    }
}