using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Scania.Selenium.Support.DataImport;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net;

namespace SeleniumNunitTemplate
{

    public class CleanData
    {

        public void CleanDataRemoveAllPackages()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FPR"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText =
@"DECLARE @UserSeqNo int;

SELECT @UserSeqNo = UserSeqNo From FPRUser Where Upper(UserId) LIKE '%testfpruser%';

DELETE FROM FPRPackageGroupAdditionalCost
WHERE [PackageGroupPriceSeqNo] IN (SELECT [PackageGroupPriceSeqNo] 
									From FPRPackageGroupPrice
									Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo));
DELETE FROM [FPRPackagePartPrice]
WHERE [PackagePartSeqNo] IN (SELECT [PackagePartSeqNo]
							 From [FPRPackagePart]
							 Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo));
DELETE FROM [FPRPackagePart]
                             Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE FROM [FPRPackageStandardTimePrice]
WHERE [PackageStandardTimeSeqNo] IN (SELECT [PackageStandardTimeSeqNo]
									 From [FPRPackageStandardTime]
									 Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo));

DELETE From [FPRPackageStandardTime]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE FROM [FPRPackageSundryPrice]
WHERE [PackageSundrySeqNo] IN (SELECT [PackageSundrySeqNo]
									 From [FPRPackageSundry]
									 Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo));

DELETE From [FPRPackageSundry]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From FPRPackageGroupPrice
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRContentTranslation]
Where  [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRPackageVehicleType]
Where  [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRCampaign]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRGroupMember]
Where [GroupSeqNo] IN (Select [GroupSeqNo] From [FPRGroup] Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo)); 

DELETE From [FPRGroup]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRPackageAudit]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRCustomerPrice]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo); 

UPDATE FPRPackage
SET ParentSeqNo = NULL
Where ParentSeqNo IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE FROM FPRPackage
Where Creator = @UserSeqNo; 
";

                    com.ExecuteNonQuery();
                }
            }

        }

        public void RemovePackage(string packageId)
        {
            string packageSeqNo = GetPackageSeqNo(packageId);

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FPR"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = con.CreateCommand())
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText =

                    @"DECLARE @UserSeqNo int;
                    DECLARE @PackageId int = '0000850100';

                    SELECT @UserSeqNo = UserSeqNo From FPRUser Where Upper(UserId) LIKE '%MIVHDE%';

                    DELETE FROM FPRPackageGroupAdditionalCost
                    WHERE [PackageGroupPriceSeqNo] IN (SELECT [PackageGroupPriceSeqNo] 
                    From FPRPackageGroupPrice
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));

                    DELETE FROM [FPRPackagePartPrice]
                    WHERE [PackagePartSeqNo] IN (SELECT [PackagePartSeqNo]
                    From [FPRPackagePart]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));

                    DELETE FROM [FPRPackagePart]
                    Where [PackageSeqNo] IN (Select PackageSeqNo 
                    From FPRPackage Where PackageId = @PackageId);

                    DELETE FROM [FPRPackageStandardTimePrice]
                    WHERE [PackageStandardTimeSeqNo] IN (SELECT [PackageStandardTimeSeqNo]
                    From [FPRPackageStandardTime]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));

                    DELETE From [FPRPackageStandardTime]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE FROM [FPRPackageSundryPrice]
                    WHERE [PackageSundrySeqNo] IN (SELECT [PackageSundrySeqNo]
                    From [FPRPackageSundry]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));

                    DELETE From [FPRPackageSundry]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From FPRPackageGroupPrice
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRContentTranslation]
                    Where  [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRPackageVehicleType]
                    Where  [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRGroupMember]
                    Where [GroupSeqNo] IN (Select [GroupSeqNo] 
                    From [FPRGroup] 
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId)); 

                    DELETE From [FPRGroup]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRPackageAudit]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRCampaign]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE From [FPRCustomerPrice]
                    Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId); 

                    UPDATE FPRPackage
                    SET ParentSeqNo = NULL
                    Where ParentSeqNo IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

                    DELETE FROM FPRPackage
                    Where PackageId = @PackageId;";

                    com.ExecuteNonQuery();
                }
            }

            var httpRequest = HttpWebRequest.Create(new Uri("http://testsfpr.scania.com/Site/api/Packages/" + packageSeqNo));

            httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
            httpRequest.Method = "DELETE";
            var response = httpRequest.GetResponse();

            Assert.True(((System.Net.HttpWebResponse)response).StatusCode == HttpStatusCode.NoContent, "Server returned an error, CleanData: Can't remove the package");

        }
        
        public string GetPackageSeqNo(string packageId)
        {
            
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FPR"].ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();

                command.CommandText = "Select PackageSeqNo from FPRPackage where PackageId = '" + packageId + "'";
                command.CommandType = CommandType.Text;

                string test = command.ExecuteScalar().ToString();

                return test.ToString();

            }

        }
        

    }
}
