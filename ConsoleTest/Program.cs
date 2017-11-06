using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.Collections;
using System.Xml.Serialization;
using TreeCat.XrmToolBox.CodeNow;
using Microsoft.Xrm.Sdk.Messages;





namespace ConsoleTest
{

    class Program
    {
        public static IOrganizationService Service = null;

        static void Main(string[] args)
        {
            var conn = new Microsoft.Xrm.Tooling.Connector.CrmServiceClient(System.Configuration.ConfigurationManager.ConnectionStrings["CodeNow"].ConnectionString);
            Service = (IOrganizationService)conn.OrganizationWebProxyClient != null ? (IOrganizationService)conn.OrganizationWebProxyClient : (IOrganizationService)conn.OrganizationServiceProxy;
            SolutionStats();
        }

        public static void LogMessage(string msg)
        {
            Console.WriteLine(msg);
        }

        public static void SolutionStats()
        {
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            string fetchXml = @"<fetch version=""1.0"" mapping=""logical"" distinct=""false"">
                <entity name=""principalobjectaccess"">
                   <attribute name=""principalobjectaccessid""/>
                   <attribute name=""objecttypecode""/>
                </entity>
              </fetch>";
            var poaOverviewExists = DynamicsHelper.CheckEntityExists(Service, "ita_poaoverview");
            if(!poaOverviewExists)
            {
                LogMessage("Please install the Overview solution first..");
            }
            else
            {

                int fetchCount = 4500;
                int pageNumber = 1;
                string pagingCookie = null;

                //Delete current POA Overview records
                QueryExpression qs = new QueryExpression("ita_poaoverview");
                qs.ColumnSet = new ColumnSet("ita_poaoverviewid");
                var existing = Service.RetrieveMultiple(qs).Entities;
                foreach(var e in existing)
                {
                    Service.Delete(e.LogicalName, e.Id);
                }

                FetchExpression fe = new FetchExpression(fetchXml);


                while (true)
                {
                    // Build fetchXml string with the placeholders.
                    string xml = DynamicsHelper.CreateXml(fetchXml, pagingCookie, pageNumber, fetchCount);

                    // Excute the fetch query and get the xml result.
                    RetrieveMultipleRequest request = new RetrieveMultipleRequest
                    {
                        Query = new FetchExpression(xml)
                    };

                    EntityCollection returnCollection = ((RetrieveMultipleResponse)Service.Execute(request)).EntityCollection;

                    foreach (var c in returnCollection.Entities)
                    {
                        string objectTypeCode = (string)c["objecttypecode"];
                        if (!ht.ContainsKey(objectTypeCode))
                        {
                            ht[objectTypeCode] = 0;
                        }
                        ht[objectTypeCode] = ((int)ht[objectTypeCode]) + 1;
                    }

                    if (returnCollection.MoreRecords)
                    {
                        System.Threading.Thread.Sleep(10);
                        LogMessage("Loaded 4500 POA records..");
                        pageNumber++;
                        pagingCookie = returnCollection.PagingCookie;
                    }
                    else
                    {
                        break;
                    }
                }
                //Create POA Overview records
                foreach(var key in ht.Keys)
                {
                    Entity ent = new Entity("ita_poaoverview");
                    ent["ita_name"] = key;
                    ent["ita_recordcount"] = (int)ht[key];
                    Service.Create(ent);
                }

                LogMessage("Done!");
            }
        }
    }
}

