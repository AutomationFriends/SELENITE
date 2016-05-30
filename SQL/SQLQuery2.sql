DECLARE @UserSeqNo int; 

SELECT @UserSeqNo = UserSeqNo
From FPRUser
Where Upper(UserId) LIKE '%MIVHDE%';

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

DELETE From [FPRGroupMember]
Where [GroupSeqNo] IN (Select [GroupSeqNo] From [FPRGroup] Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo)); 

DELETE From [FPRGroup]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRPackageAudit]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRCampaign]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE From [FPRCustomerPrice]
Where [PackageSeqNo] IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo); 

UPDATE FPRPackage
SET ParentSeqNo = NULL
Where ParentSeqNo IN (Select PackageSeqNo From FPRPackage Where Creator = @UserSeqNo);

DELETE FROM FPRPackage
Where Creator = @UserSeqNo; 
