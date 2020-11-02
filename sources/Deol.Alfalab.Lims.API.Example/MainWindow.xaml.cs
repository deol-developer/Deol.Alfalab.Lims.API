using Deol.Alfalab.Lims.API.Messages;
using Deol.Alfalab.Lims.API.Messages.Base;
using System;
using System.Linq;
using System.Windows;


namespace Deol.Alfalab.Lims.API.Example
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var adress = "http://xxx.xxx.xxx.xxx:xxxx";

            var authorizationData = new AuthorizationData("senderName", "receiverName", "password");

            var date = new DateTime(2020, 10, 15);

            var requestCreateReferral1 = new RequestCreateOrUpdateReferral(authorizationData, false, true)
            {
                Date = date,
                Pation =
                {
                    FirstName = "тест",
                    LastName = "тест",
                    MiddleName = "тест",
                    Gender = PationGender.Male,
                    BirthDate = new DateTime(1990, 1, 1)
                },
                Referral =
                {
                    Date = date,
                    SamplingDate = date,
                    DeliveryDate = date,
                    Nr = "1999999998",
                    Orders =
                    {
                        new RequestElementOrder { Code = "01.00.001" }
                    },
                    UserFields =
                    {
                        ["Adress"] = "Адрес",
                        ["AdressFact"] = "Адрес фактический",
                        ["Travel_ArrivalDate"] = MessageHelper.GetAttributeValue(DateTime.Now)
                    }
                }
            };

            var requestCreateReferral2 = new RequestCreateOrUpdateReferral(authorizationData, false, true)
            {
                Date = date,
                Pation =
                {
                    FirstName = "тест",
                    LastName = "тест",
                    MiddleName = "тест",
                    Gender = PationGender.Male,
                    BirthDate = new DateTime(1990, 1, 1)
                },
                Referral =
                {
                    Date = date,
                    SamplingDate = date,
                    DeliveryDate = date,
                    Nr = "1999999998",
                    UserFields =
                    {
                        ["Adress"] = "Адрес",
                        ["AdressFact"] = "Адрес фактический",
                        ["Travel_ArrivalDate"] = MessageHelper.GetAttributeValue(DateTime.Now)
                    }
                },
                Assays =
                {
                    new RequestElementAssay
                    {
                        Barcode = "1999999998",
                        BiomaterialCode = "М018",
                        Orders =
                        {
                            new RequestElementOrder
                            {
                                Code = "01.00.001"
                            }
                        }
                    }
                }
            };

            var requestCreateReferral3 = new RequestCreateOrUpdateReferral(authorizationData, false, true)
            {
                Date = date,
                Pation =
                {
                    FirstName = "тест",
                    LastName = "тест",
                    MiddleName = "тест",
                    Gender = PationGender.Male,
                    BirthDate = new DateTime(1990, 1, 1)
                },
                Referral =
                {
                    Date = date,
                    SamplingDate = date,
                    DeliveryDate = date,
                    Nr = "1999999998-1",
                    Orders =
                    {
                        new RequestElementOrder { Code = "01.00.002" }
                    },
                    UserFields =
                    {
                        ["Adress"] = "Адрес",
                        ["AdressFact"] = "Адрес фактический",
                        ["Travel_ArrivalDate"] = MessageHelper.GetAttributeValue(DateTime.Now)
                    }
                }
            };

            var requestCreateDoctorOrders = new RequestCreateDoctorOrders(authorizationData)
            {
                Date = date,
                Pation =
                {
                    FirstName = "тест",
                    LastName = "тест",
                    MiddleName = "тест",
                    Gender = PationGender.Male,
                    BirthDate = new DateTime(1990, 1, 1)
                },
                Referral =
                {
                    Date = date,
                    Nr = "1999999998",
                    Orders =
                    {
                        new RequestElementDoctorOrder { Code = "01.00.001" }
                    }
                }
            };

            var requestChangeEmail = new RequestChangeEmail(authorizationData)
            {
                Date = date,
                Query =
                {
                    LisId = 417622943,
                    EmailAddress = "user@server.org"
                }
            };

            var requestReferralResults = new RequestReferralResults(authorizationData)
            {
                Date = date
            };

            var requestNextReferralResults = new RequestNextReferralResults(authorizationData)
            {
                Date = date
            };

            var requestCountReferralResult = new RequestCountReferralResults(authorizationData)
            {
                Date = date
            };

            var requestNewReferralResults = new RequestNewReferralResults(authorizationData)
            {
                Date = date,
                Query =
                {
                    DateFrom = new DateTime(2020, 9, 15),
                    DateTill = new DateTime(2020, 9, 20)
                }
            };

            var requestReferralResultsImport = new RequestReferralResultsImport(authorizationData)
            {
                Date = date
            };

            var requestPatientReferralResults = new RequestPatientReferralResults(authorizationData)
            {
                Date = date,
                Query =
                {
                    PatientMisId = "1"
                }
            };

            var requestBlankFile = new RequestBlankFile(authorizationData)
            {
                Date = date,
                Query =
                {
                    BlankId = 1
                }
            };

            var requestRemoveReferral = new RequestRemoveReferral(authorizationData)
            {
                Date = date,
                Query =
                {
                    LisId = 417622943
                }
            };

            var requestDictionariesVersion = new RequestDictionariesVersion(authorizationData)
            {
                Date = date
            };

            var requestDictionaries = new RequestDictionaries(authorizationData)
            {
                Date = date
            };

            var requestPreprintBarcodes = new RequestPreprintBarcodes(authorizationData)
            {
                Date = date,
                Query =
                {
                    Count = 10
                }
            };

            using (var client = new Client(adress))
            {
                //CreateOrUpdateReferral1
                var responseCreateReferral1 = await client.CreateOrUpdateReferralAsync(requestCreateReferral1);

                //CreateOrUpdateReferral2
                var responseCreateReferral2 = await client.CreateOrUpdateReferralAsync(requestCreateReferral2);

                //CreateOrUpdateReferral3
                requestCreateReferral3.Referral.MasterLisId = responseCreateReferral1.Referral.LisId;
                var responseCreateReferral3 = await client.CreateOrUpdateReferralAsync(requestCreateReferral3);

                //CreateDoctorOrders
                var responseCreateDoctorOrders = await client.CreateDoctorOrdersAsync(requestCreateDoctorOrders);

                //ChangeEmail
                requestChangeEmail.Query.LisId = requestCreateReferral1.Referral.LisId;
                var responseChangeEmail = await client.ChangeEmailAsync(requestChangeEmail);

                //GetReferralResults
                requestReferralResults.Query.Nr = "1999999998";
                var responseReferralResults = await client.GetReferralResultsAsync(requestReferralResults);

                //GetNextReferralResults
                var responseNextReferralResult = await client.GetNextReferralResultsAsync(requestNextReferralResults);

                //GetCountReferralResults
                var responseCountReferralResult = await client.GetCountReferralResultsAsync(requestCountReferralResult);

                //GetNewReferralResults
                var responseNewReferralResults = await client.GetNewReferralResultsAsync(requestNewReferralResults);

                //SetReferralResultsImport
                requestReferralResultsImport.Version.Version = responseReferralResults.Version.Version;
                requestReferralResultsImport.Referral.LisId = responseReferralResults.Referral.LisId;
                var responseReferralResultsImport = await client.SetReferralResultsImportAsync(requestReferralResultsImport);

                //GetPatientReferralResults
                var responsePatientReferralResults = await client.GetPatientReferralResultsAsync(requestPatientReferralResults);

                //GetBlankFileBinary
                requestBlankFile.Query.BlankGUID = responseReferralResults.Blanks.FirstOrDefault()?.BlankGUID ?? "";
                var responseBlankFile = await client.GetBlankFileBinaryAsync(requestBlankFile);

                //RemoveReferral
                requestRemoveReferral.Query.LisId = responseCreateReferral1.Referral.LisId;
                var responseRemoveReferral = await client.RemoveReferralAsync(requestRemoveReferral);

                //GetDictionariesVersion
                var responseDictionariesVersion = await client.GetDictionariesVersionAsync(requestDictionariesVersion);

                //GetDictionaries
                var responseDictionaries = await client.GetDictionariesAsync(requestDictionaries);

                //GetPreprintBarcodes
                var responsePreprintBarcodes = await client.GetPreprintBarcodesAsync(requestPreprintBarcodes);
            }
        }
    }
}
