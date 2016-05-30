DECLARE @UserSeqNo int;
DECLARE @PackageId int;

SELECT @UserSeqNo = UserSeqNo From FPRUser Where Upper(UserId) LIKE '%MIVHDE%';
SELECT @PackageId = PackageId From FPRPackage Where PackageSeqNo LIKE '0000850100';

DELETE FROM FPRPackageGroupAdditionalCost
WHERE [PackageGroupPriceSeqNo] IN (SELECT [PackageGroupPriceSeqNo] 
									From FPRPackageGroupPrice
									Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));
DELETE FROM [FPRPackagePartPrice]
WHERE [PackagePartSeqNo] IN (SELECT [PackagePartSeqNo]
							 From [FPRPackagePart]
							 Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId));
DELETE FROM [FPRPackagePart]
                             Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId);

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
Where [GroupSeqNo] IN (Select [GroupSeqNo] From [FPRGroup] Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where PackageId = @PackageId)); 

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
Where PackageId = @PackageId;