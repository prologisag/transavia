using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Buchung4U.API_SessionService;
using Buchung4U.API_BookingService;
using Buchung4U.API_OperationService;
using Buchung4U.API_ContentService;
using Buchung4U.API_VoucherService;
using Buchung4U.API_ScheduleService;
using Buchung4U.API_PersonService;
using Buchung4U.API_AccountService;
using Buchung4U.API_QueueService;

namespace Buchung4U
{
    /// <summary>
    /// New Skies 3.3 Methods to provide New Skies functionality
    /// UI (Buchung.cs) can be used to connect to different New Skies versions, just make sure
    /// the methods are called the same when changing the New Skies class
    /// </summary>
    public partial class NewSkies3
    {
        #region Declaration
        private ISessionManager sessionManager = new SessionManagerClient();
        private IBookingManager bookingManager = new BookingManagerClient();
        private IOperationManager operationManager = new OperationManagerClient();
        private IContentManager contentManager = new ContentManagerClient();
        private IVoucherManager voucherManager = new VoucherManagerClient();
        private IScheduleManager scheduleManager = new ScheduleManagerClient();
        private IPersonManager personManager = new PersonManagerClient();
        private IAccountManager accountManager = new AccountManagerClient();
        private IQueueManager queueManager = new QueueManagerClient();
        private int _contractVersion = Convert.ToInt16(ConfigurationManager.AppSettings["contractVersion"]);
        private string _messageContractVersion = ConfigurationManager.AppSettings["messageContractVersion"].ToString();
        private string _carrierCode = ConfigurationManager.AppSettings["defaultCarrierCode"].ToString();
        private string _currencyCode = ConfigurationManager.AppSettings["defaultCurrencyCode"].ToString();
        private string _fareType = ConfigurationManager.AppSettings["defaultFareType"].ToString();
        private int paxCount;
        private int paxAdults;
        private int paxChilds;
        private int paxInfants;
        private string[] tmpFareTypes;
        private PaxPriceType[] tmpPaxPriceTypes;
        private PassengerTypeInfo[] tmpPaxTypeInfo;
        private AvailabilityRequest[] tmpAvailRequests;
        private JourneySortKey[] tmpJourneySortKeys;
        private Buchung4U.API_BookingService.SegmentSeatRequest[] tmpSegmentSeatRequests;
        //private Buchung4U.API_BookingService.PassengerSeatPreference[] tmpPassengerSeatPreferences;
        private SellKeyList[] tmpSellKeyList;
        private SellJourney[] tmpSellJourney;
        private SellSegment[] tmpSellSegment;
        private SSRRequest[] tmpSSRRequest;
        private SegmentSSRRequest[] tmpSegmentSSRRequest;
        private Buchung4U.API_BookingService.PaxSSR[] tmpPaxSSR;
        private BookingContact[] tmpBookingContacts;
        private Passenger[] tmpPassengers;
        private Payment[] tmpPayments;
        private Buchung4U.API_BookingService.BookingName[] tmpBookingNames;
        private PaymentField[] tmpPaymentFields;
        private short[] tmpPassengerNumbers;
        private Journey[] tmpJourneys;
        #endregion

        #region nskLogon
        /// <summary>
        /// Creates a session on the New Skies system which has to be used for any 
        /// further request.
        /// </summary>
        /// <param name="agentName"></param>
        /// <param name="password"></param>
        /// <param name="domainCode"></param>
        /// <returns>LogonResponse</returns>
        public LogonResponse nskLogon(string agentName, string password, string domainCode)
        {
            LogonRequest logonRequest = new LogonRequest();
            logonRequest.ContractVersion = _contractVersion;
            logonRequest.logonRequestData = new LogonRequestData();
            logonRequest.logonRequestData.AgentName = agentName;
            logonRequest.logonRequestData.DomainCode = domainCode;
            logonRequest.logonRequestData.Password = password;
            //logonRequest.logonRequestData.LocationCode = "EXT";

            return sessionManager.Logon(logonRequest);
        }
        #endregion

        #region nskGetAvailability
        /// <summary>
        /// Searches for availability for the given route on a certain flight date.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="returnFlight">TRUE (return) | FALSE (one-way)</param>
        /// <param name="departureStation"></param>
        /// <param name="arrivalStation"></param>
        /// <param name="outboundFlightDate"></param>
        /// <param name="inboundFlightDate"></param>
        /// <param name="nrAdults"></param>
        /// <param name="nrChilds"></param>
        /// <returns>GetAvailabilityResponse</returns>
        public GetAvailabilityResponse nskGetAvailability(string sessionSignature, bool returnFlight, string departureStation, string arrivalStation, DateTime outboundFlightDate, DateTime inboundFlightDate, int nrAdults, int nrChilds, int nrInfants, string currencyCode, string organizationCode, string productClass, string promotionCode)
        {
            GetAvailabilityRequest getAvailabilityRequest = new GetAvailabilityRequest();
            getAvailabilityRequest.ContractVersion = _contractVersion;
            getAvailabilityRequest.MessageContractVersion = _messageContractVersion;
            getAvailabilityRequest.EnableExceptionStackTrace = true;
            getAvailabilityRequest.Signature = sessionSignature;
            getAvailabilityRequest.TripAvailabilityRequest = new TripAvailabilityRequest();
            BindingList<AvailabilityRequest> availabilityRequests = new BindingList<AvailabilityRequest>();

            availabilityRequests.Add(buildAvailabilityRequest(departureStation, arrivalStation, outboundFlightDate, nrAdults, nrChilds, nrInfants, currencyCode, organizationCode, productClass, promotionCode));
            if (returnFlight)
                availabilityRequests.Add(buildAvailabilityRequest(arrivalStation, departureStation, inboundFlightDate, nrAdults, nrChilds, nrInfants, currencyCode, organizationCode, productClass, promotionCode));

            tmpAvailRequests = new AvailabilityRequest[availabilityRequests.Count];
            availabilityRequests.CopyTo(tmpAvailRequests, 0);
            getAvailabilityRequest.TripAvailabilityRequest.AvailabilityRequests = tmpAvailRequests;

            GetAvailabilityResponse getAvRes = bookingManager.GetAvailability(getAvailabilityRequest);
            return getAvRes;
        }
        #endregion

        #region buildAvailabilityRequest
        /// <summary>
        /// Creates AvailabilityRequest objects which can be used to fill the
        /// GetAvailability request.
        /// </summary>
        /// <param name="Dep"></param>
        /// <param name="Arr"></param>
        /// <param name="DepDate"></param>
        /// <param name="nrAdults"></param>
        /// <param name="nrChilds"></param>
        /// <returns>AvailabilityRequest</returns>
        protected AvailabilityRequest buildAvailabilityRequest(String Dep, String Arr, DateTime DepDate, Int32 nrAdults, Int32 nrChilds, Int32 nrInfants, string currencyCode, string organizationCode, string productClass, string promotionCode)
        {
            AvailabilityRequest availabilityRequest = new AvailabilityRequest();
            BindingList<PaxPriceType> paxPriceTypes = new BindingList<PaxPriceType>();
            paxAdults = nrAdults;
            paxChilds = nrChilds;
            paxCount = nrAdults + nrChilds;
            paxInfants = nrInfants;
            if (nrAdults != 0)
            {
                PaxPriceType paxPriceType = new PaxPriceType();
                paxPriceType.PaxType = "ADT";
                //paxPriceType.PaxCount = (short)nrAdults;
                paxPriceType.PaxDiscountCode = string.Empty;
                paxPriceTypes.Add(paxPriceType);
            }
            if (nrChilds != 0)
            {
                PaxPriceType paxPriceType = new PaxPriceType();
                paxPriceType.PaxType = "CHD";
                paxPriceType.PaxDiscountCode = "";
                paxPriceTypes.Add(paxPriceType);
            }
            tmpPaxPriceTypes = new PaxPriceType[paxPriceTypes.Count];
            paxPriceTypes.CopyTo(tmpPaxPriceTypes, 0);
            paxPriceTypes.Clear();
            availabilityRequest.PaxPriceTypes = tmpPaxPriceTypes;
            availabilityRequest.AvailabilityFilter = AvailabilityFilter.Default;
            availabilityRequest.AvailabilityType = AvailabilityType.Default;
            availabilityRequest.FareClassControl = FareClassControl.Default;
            availabilityRequest.PaxCount = (short)paxCount;
            availabilityRequest.DepartureStation = Dep;
            availabilityRequest.ArrivalStation = Arr;
            availabilityRequest.CarrierCode = _carrierCode;
            availabilityRequest.BeginDate = new DateTime(DepDate.Year, DepDate.Month, DepDate.Day);
            availabilityRequest.EndDate = new DateTime(DepDate.Year, DepDate.Month, DepDate.Day);
            availabilityRequest.FlightType = Buchung4U.API_BookingService.FlightType.All;
            availabilityRequest.CurrencyCode = _currencyCode;
            availabilityRequest.DisplayCurrencyCode = _currencyCode;
            availabilityRequest.Dow = Buchung4U.API_BookingService.DOW.Daily;
            availabilityRequest.MaximumConnectingFlights = 0;
            availabilityRequest.InboundOutbound = Buchung4U.API_BookingService.InboundOutbound.Both;
            availabilityRequest.SSRCollectionsMode = Buchung4U.API_BookingService.SSRCollectionsMode.None;
            if(promotionCode != string.Empty)
            {
                availabilityRequest.PromotionCode = promotionCode;
            }
            availabilityRequest.IncludeTaxesAndFees = true;
            availabilityRequest.IncludeAllotments = true;
            if (productClass != string.Empty)
            {
                availabilityRequest.ProductClassCode = productClass;
            }
            if (organizationCode != string.Empty)
            {
                availabilityRequest.SourceOrganization = organizationCode;
            }

            BindingList<string> fareTypes = new BindingList<string>();
            fareTypes.Add(_fareType);
            tmpFareTypes = new string[fareTypes.Count];
            fareTypes.CopyTo(tmpFareTypes, 0);
            fareTypes.Clear();
            availabilityRequest.FareTypes = tmpFareTypes;

            BindingList<JourneySortKey> journeySortKeys = new BindingList<JourneySortKey>();
            journeySortKeys.Add(JourneySortKey.EarliestDeparture);
            tmpJourneySortKeys = new JourneySortKey[journeySortKeys.Count];
            journeySortKeys.CopyTo(tmpJourneySortKeys, 0);
            journeySortKeys.Clear();
            availabilityRequest.JourneySortKeys = tmpJourneySortKeys;

            /* new elements for New Skies 4.1 */
            //availabilityRequest.ServiceBundleControl = ServiceBundleControl.Disabled;

            return availabilityRequest;
        }
        #endregion

        #region nskGetFareRuleInfo
        /// <summary>
        /// Retrieves the Fare Rule information for a certain segment.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="selectedSegment"></param>
        /// <returns>GetFareRuleInfoResponse</returns>
        public GetFareRuleInfoResponse nskGetFareRuleInfo(string sessionSignature, Buchung4U.API_BookingService.Segment selectedSegment)
        {
            GetFareRuleInfoRequest getFareRuleRequest = new GetFareRuleInfoRequest();
            getFareRuleRequest.ContractVersion = _contractVersion;
            getFareRuleRequest.MessageContractVersion = _messageContractVersion;
            getFareRuleRequest.EnableExceptionStackTrace = false;
            getFareRuleRequest.Signature = sessionSignature;
            getFareRuleRequest.fareRuleReqData = new FareRuleRequestData();
            getFareRuleRequest.fareRuleReqData.CarrierCode = selectedSegment.Fares[0].CarrierCode;
            getFareRuleRequest.fareRuleReqData.ClassOfService = selectedSegment.Fares[0].ClassOfService;
            getFareRuleRequest.fareRuleReqData.FareBasisCode = selectedSegment.Fares[0].FareBasisCode;
            getFareRuleRequest.fareRuleReqData.RuleNumber = selectedSegment.Fares[0].RuleNumber;
            getFareRuleRequest.fareRuleReqData.RuleTariff = selectedSegment.Fares[0].RuleTariff;
            return contentManager.GetFareRuleInfo(getFareRuleRequest);
        }
        #endregion

        #region nskPriceItinerary
        /// <summary>
        /// Retrieves the price details for a given number of Journeys.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="journeysToPrice">Array of Journey objects</param>
        /// <param name="type">JourneyBySellKey | Journey</param>
        /// <returns>PriceItineraryResponse</returns>
        public PriceItineraryResponse nskPriceItinerary(string sessionSignature, string currencyCode, Journey[] journeysToPrice, PriceItineraryBy type, string organizationCode)
        {
            PriceItineraryRequest priceItineraryRequest = new PriceItineraryRequest();
            priceItineraryRequest.ContractVersion = _contractVersion;
            priceItineraryRequest.MessageContractVersion = _messageContractVersion;
            priceItineraryRequest.EnableExceptionStackTrace = false;
            priceItineraryRequest.Signature = sessionSignature;
            priceItineraryRequest.ItineraryPriceRequest = new ItineraryPriceRequest();
            priceItineraryRequest.ItineraryPriceRequest.PriceItineraryBy = type;

            #region PriceItineraryBy.JourneyBySellKey
            if (type == PriceItineraryBy.JourneyBySellKey)
            {
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest = new SellJourneyByKeyRequestData();
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.ActionStatusCode = "NN";
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.CurrencyCode = currencyCode;
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.PaxCount = (short)paxCount;

                BindingList<SellKeyList> sellKeyLists = new BindingList<SellKeyList>();
                for (int i = 0; i < journeysToPrice.Length; i++)
                {
                    SellKeyList sellKeyList = new SellKeyList();
                    sellKeyList.JourneySellKey = journeysToPrice[i].JourneySellKey;
                    sellKeyList.FareSellKey = journeysToPrice[i].Segments[0].Fares[0].FareSellKey;
                    sellKeyLists.Add(sellKeyList);
                }
                tmpSellKeyList = new SellKeyList[sellKeyLists.Count];
                sellKeyLists.CopyTo(tmpSellKeyList, 0);
                sellKeyLists.Clear();
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.JourneySellKeys = tmpSellKeyList;

                BindingList<PaxPriceType> paxPriceTypes = new BindingList<PaxPriceType>();
                for (int i = 0; i < journeysToPrice[0].Segments[0].Fares[0].PaxFares.Length; i++)
                {
                    PaxPriceType paxPriceType = new PaxPriceType();
                    paxPriceType.PaxType = journeysToPrice[0].Segments[0].Fares[0].PaxFares[i].PaxType;
                    paxPriceType.PaxDiscountCode = journeysToPrice[0].Segments[0].Fares[0].PaxFares[i].PaxDiscountCode;
                    paxPriceTypes.Add(paxPriceType);
                }
                tmpPaxPriceTypes = new PaxPriceType[paxPriceTypes.Count];
                paxPriceTypes.CopyTo(tmpPaxPriceTypes, 0);
                paxPriceTypes.Clear();
                priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.PaxPriceType = tmpPaxPriceTypes;

                if (organizationCode != string.Empty)
                {
                    priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.SourcePOS = new API_BookingService.PointOfSale();
                    priceItineraryRequest.ItineraryPriceRequest.SellByKeyRequest.SourcePOS.OrganizationCode = organizationCode;
                }
            }
            #endregion

            #region PriceItineraryBy.JourneyWithLegs
            if (type == PriceItineraryBy.JourneyWithLegs)
            {
                priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest = new PriceJourneyRequestData();
                priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.CurrencyCode = currencyCode;
                priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.PaxCount = (short)paxCount;

                BindingList<PriceJourney> priceJourneys = new BindingList<PriceJourney>();
                for (int i = 0; i < journeysToPrice.Length; i++)
                {
                    PriceJourney priceJourney = new PriceJourney();
                    priceJourney.State = API_BookingService.MessageState.New;
                    BindingList<PriceSegment> priceSegments = new BindingList<PriceSegment>();
                    for (int j = 0; j < journeysToPrice[i].Segments.Length; j++)
                    {
                        PriceSegment priceSegment = new PriceSegment();
                        priceSegment.ActionStatusCode = "NN";
                        priceSegment.State = Buchung4U.API_BookingService.MessageState.New;
                        priceSegment.ArrivalStation = journeysToPrice[i].Segments[j].ArrivalStation;
                        priceSegment.DepartureStation = journeysToPrice[i].Segments[j].DepartureStation;
                        priceSegment.FlightDesignator = new Buchung4U.API_BookingService.FlightDesignator();
                        priceSegment.FlightDesignator.CarrierCode = journeysToPrice[i].Segments[j].FlightDesignator.CarrierCode;
                        priceSegment.FlightDesignator.FlightNumber = journeysToPrice[i].Segments[j].FlightDesignator.FlightNumber;
                        priceSegment.FlightDesignator.OpSuffix = journeysToPrice[i].Segments[j].FlightDesignator.OpSuffix;
                        priceSegment.STD = journeysToPrice[i].Segments[j].STD;
                        priceSegment.STA = journeysToPrice[i].Segments[j].STA;
                        priceSegment.Fare = new SellFare();
                        priceSegment.Fare.CarrierCode = journeysToPrice[i].Segments[j].Fares[0].CarrierCode;
                        priceSegment.Fare.ClassOfService = journeysToPrice[i].Segments[j].Fares[0].ClassOfService;
                        priceSegment.Fare.FareApplicationType = journeysToPrice[i].Segments[j].Fares[0].FareApplicationType;
                        priceSegment.Fare.FareBasisCode = journeysToPrice[i].Segments[j].Fares[0].FareBasisCode;
                        priceSegment.Fare.FareClassOfService = journeysToPrice[i].Segments[j].Fares[0].FareClassOfService;
                        priceSegment.Fare.ProductClass = journeysToPrice[i].Segments[j].Fares[0].ProductClass;
                        priceSegment.Fare.RuleNumber = journeysToPrice[i].Segments[j].Fares[0].RuleNumber;
                        priceSegment.Fare.RuleTariff = journeysToPrice[i].Segments[j].Fares[0].RuleTariff;
                        priceSegment.Fare.FareSequence = journeysToPrice[i].Segments[j].Fares[0].FareSequence;
                        priceSegment.Fare.IsAllotmentMarketFare = journeysToPrice[i].Segments[j].Fares[0].IsAllotmentMarketFare;
                        priceSegment.Fare.ClassType = journeysToPrice[i].Segments[j].Fares[0].ClassType;
                        priceSegments.Add(priceSegment);
                    }
                    priceJourney.Segments = priceSegments.ToArray();
                    priceJourneys.Add(priceJourney);
                }

                priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.PriceJourneys = priceJourneys.ToArray();

                BindingList<Passenger> passengers = new BindingList<Passenger>();
                for (int i = 0; i < journeysToPrice[0].Segments[0].Fares[0].PaxFares.Length; i++)
                {
                    Passenger passenger = new Passenger();
                    BindingList<PassengerTypeInfo> paxTypeInfos = new BindingList<PassengerTypeInfo>();
                    PassengerTypeInfo paxTypeInfo = new PassengerTypeInfo();
                    paxTypeInfo.PaxType = journeysToPrice[0].Segments[0].Fares[0].PaxFares[i].PaxType;
                    paxTypeInfos.Add(paxTypeInfo);
                    tmpPaxTypeInfo = new PassengerTypeInfo[paxTypeInfos.Count];
                    paxTypeInfos.CopyTo(tmpPaxTypeInfo, 0);
                    paxTypeInfos.Clear();
                    passenger.PassengerTypeInfos = tmpPaxTypeInfo;
                    passenger.PaxDiscountCode = journeysToPrice[0].Segments[0].Fares[0].PaxFares[i].PaxDiscountCode;
                    passengers.Add(passenger);
                }
                tmpPassengers = new Passenger[passengers.Count];
                passengers.CopyTo(tmpPassengers, 0);
                passengers.Clear();
                priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.Passengers = tmpPassengers;

                if (organizationCode != string.Empty)
                {
                    priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.SourcePOS = new API_BookingService.PointOfSale();
                    priceItineraryRequest.ItineraryPriceRequest.PriceJourneyWithLegsRequest.SourcePOS.OrganizationCode = organizationCode;
                }
            }
            #endregion

            return bookingManager.GetItineraryPrice(priceItineraryRequest);
        }
        #endregion

        #region nskSellRequest (JourneySellKey / Journey)
        /// <summary>
        /// Sells the given number of Journeys.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="journeyToSells"></param>
        /// <param name="type">JourneyBySellKey | Journey</param>
        /// <returns>SellResponse</returns>
        public SellResponse nskSellRequest(string sessionSignature, string currencyCode, Journey[] journeyToSells, SellBy type, string organizationCode)
        {
            SellRequest sellRequest = new SellRequest();
            sellRequest.ContractVersion = _contractVersion;
            sellRequest.MessageContractVersion = _messageContractVersion;
            sellRequest.EnableExceptionStackTrace = false;
            sellRequest.Signature = sessionSignature;
            sellRequest.SellRequestData = new SellRequestData();
            sellRequest.SellRequestData.SellBy = type;

            #region SellBy.JourneyBySellKey
            if (type == SellBy.JourneyBySellKey)
            {
                sellRequest.SellRequestData.SellJourneyByKeyRequest = new SellJourneyByKeyRequest();
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData = new SellJourneyByKeyRequestData();
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.ActionStatusCode = "NN";
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.CurrencyCode = currencyCode;
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.PaxCount = (short)paxCount;

                BindingList<SellKeyList> sellKeyLists = new BindingList<SellKeyList>();
                for (int i = 0; i < journeyToSells.Length; i++)
                {
                    SellKeyList sellKeyList = new SellKeyList();
                    sellKeyList.JourneySellKey = journeyToSells[i].JourneySellKey;
                    sellKeyList.FareSellKey = journeyToSells[i].Segments[0].Fares[0].FareSellKey;
                    sellKeyLists.Add(sellKeyList);
                }
                tmpSellKeyList = new SellKeyList[sellKeyLists.Count];
                sellKeyLists.CopyTo(tmpSellKeyList, 0);
                sellKeyLists.Clear();
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.JourneySellKeys = tmpSellKeyList;

                BindingList<PaxPriceType> paxPriceTypes = new BindingList<PaxPriceType>();
                for (int i = 0; i < journeyToSells[0].Segments[0].Fares[0].PaxFares.Length; i++)
                {
                    PaxPriceType paxPriceType = new PaxPriceType();
                    paxPriceType.PaxType = journeyToSells[0].Segments[0].Fares[0].PaxFares[i].PaxType;
                    paxPriceType.PaxDiscountCode = journeyToSells[0].Segments[0].Fares[0].PaxFares[i].PaxDiscountCode;
                    paxPriceTypes.Add(paxPriceType);
                }
                tmpPaxPriceTypes = new PaxPriceType[paxPriceTypes.Count];
                paxPriceTypes.CopyTo(tmpPaxPriceTypes, 0);
                paxPriceTypes.Clear();
                sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.PaxPriceType = tmpPaxPriceTypes;

                if (organizationCode != string.Empty)
                {
                    sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.SourcePOS = new API_BookingService.PointOfSale();
                    sellRequest.SellRequestData.SellJourneyByKeyRequest.SellJourneyByKeyRequestData.SourcePOS.OrganizationCode = organizationCode;
                }
            }
            #endregion

            #region SellBy.Journey
            if (type == SellBy.Journey)
            {
                sellRequest.SellRequestData.SellJourneyRequest = new SellJourneyRequest();
                sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData = new SellJourneyRequestData();
                sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.CurrencyCode = currencyCode;
                sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.PaxCount = (short)paxCount;

                BindingList<SellJourney> sellJourneys = new BindingList<SellJourney>();
                for (int i = 0; i < journeyToSells.Length; i++)
                {
                    SellJourney sellJourney = new SellJourney();
                    sellJourney.State = Buchung4U.API_BookingService.MessageState.New;
                    BindingList<SellSegment> sellSegments = new BindingList<SellSegment>();
                    for (int j = 0; j < journeyToSells[i].Segments.Length; j++)
                    {
                        SellSegment sellSegment = new SellSegment();
                        sellSegment.ActionStatusCode = "NN";
                        sellSegment.State = Buchung4U.API_BookingService.MessageState.New;
                        sellSegment.ArrivalStation = journeyToSells[i].Segments[j].ArrivalStation;
                        sellSegment.DepartureStation = journeyToSells[i].Segments[j].DepartureStation;
                        sellSegment.FlightDesignator = new Buchung4U.API_BookingService.FlightDesignator();
                        sellSegment.FlightDesignator.CarrierCode = journeyToSells[i].Segments[j].FlightDesignator.CarrierCode;
                        sellSegment.FlightDesignator.FlightNumber = journeyToSells[i].Segments[j].FlightDesignator.FlightNumber;
                        sellSegment.FlightDesignator.OpSuffix = journeyToSells[i].Segments[j].FlightDesignator.OpSuffix;
                        sellSegment.STD = journeyToSells[i].Segments[j].STD;
                        sellSegment.STA = journeyToSells[i].Segments[j].STA;
                        sellSegment.Fare = new SellFare();
                        sellSegment.Fare.CarrierCode = journeyToSells[i].Segments[j].Fares[0].CarrierCode;
                        sellSegment.Fare.ClassOfService = journeyToSells[i].Segments[j].Fares[0].ClassOfService;
                        sellSegment.Fare.FareApplicationType = journeyToSells[i].Segments[j].Fares[0].FareApplicationType;
                        sellSegment.Fare.FareBasisCode = journeyToSells[i].Segments[j].Fares[0].FareBasisCode;
                        sellSegment.Fare.FareClassOfService = journeyToSells[i].Segments[j].Fares[0].FareClassOfService;
                        sellSegment.Fare.ProductClass = journeyToSells[i].Segments[j].Fares[0].ProductClass;
                        sellSegment.Fare.RuleNumber = journeyToSells[i].Segments[j].Fares[0].RuleNumber;
                        sellSegment.Fare.RuleTariff = journeyToSells[i].Segments[j].Fares[0].RuleTariff;
                        sellSegment.Fare.FareSequence = journeyToSells[i].Segments[j].Fares[0].FareSequence;
                        sellSegment.Fare.IsAllotmentMarketFare = journeyToSells[i].Segments[j].Fares[0].IsAllotmentMarketFare;
                        sellSegment.Fare.ClassType = journeyToSells[i].Segments[j].Fares[0].ClassType;
                        sellSegments.Add(sellSegment);
                    }
                    tmpSellSegment = new SellSegment[sellSegments.Count];
                    sellSegments.CopyTo(tmpSellSegment, 0);
                    sellSegments.Clear();
                    sellJourney.Segments = tmpSellSegment;
                    sellJourneys.Add(sellJourney);
                }
                tmpSellJourney = new SellJourney[sellJourneys.Count];
                sellJourneys.CopyTo(tmpSellJourney, 0);
                sellJourneys.Clear();
                sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.Journeys = tmpSellJourney;

                BindingList<Passenger> passengers = new BindingList<Passenger>();
                for (int i = 0; i < journeyToSells[0].Segments[0].Fares[0].PaxFares.Length; i++)
                {
                    Passenger passenger = new Passenger();
                    BindingList<PassengerTypeInfo> paxTypeInfos = new BindingList<PassengerTypeInfo>();
                    PassengerTypeInfo paxTypeInfo = new PassengerTypeInfo();
                    paxTypeInfo.PaxType = journeyToSells[0].Segments[0].Fares[0].PaxFares[i].PaxType;
                    paxTypeInfos.Add(paxTypeInfo);
                    tmpPaxTypeInfo = new PassengerTypeInfo[paxTypeInfos.Count];
                    paxTypeInfos.CopyTo(tmpPaxTypeInfo, 0);
                    paxTypeInfos.Clear();
                    passenger.PassengerTypeInfos = tmpPaxTypeInfo;
                    passenger.PaxDiscountCode = journeyToSells[0].Segments[0].Fares[0].PaxFares[i].PaxDiscountCode;
                    passengers.Add(passenger);
                }
                tmpPassengers = new Passenger[passengers.Count];
                passengers.CopyTo(tmpPassengers, 0);
                passengers.Clear();
                sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.Passengers = passengers.ToArray();

                if (organizationCode != string.Empty)
                {
                    sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.SourcePOS = new API_BookingService.PointOfSale();
                    sellRequest.SellRequestData.SellJourneyRequest.SellJourneyRequestData.SourcePOS.OrganizationCode = organizationCode;
                }
            }
            #endregion

            return bookingManager.Sell(sellRequest);
        }
        #endregion

        #region nskSellSSR
        /// <summary>
        /// Sells the SSR code on the given Journeys.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="journeysToSellSSRsOn"></param>
        /// <param name="SSRCode"></param>
        /// <returns>SellResponse</returns>
        public SellResponse nskSellSSR(string sessionSignature, string currencyCode, Journey[] journeysToSellSSRsOn, string SSRCode)
        {
            SellRequest sellRequest = new SellRequest();
            sellRequest.ContractVersion = _contractVersion;
            sellRequest.MessageContractVersion = _messageContractVersion;
            sellRequest.EnableExceptionStackTrace = false;
            sellRequest.Signature = sessionSignature;
            sellRequest.SellRequestData = new SellRequestData();
            sellRequest.SellRequestData.SellBy = SellBy.SSR;
                
            sellRequest.SellRequestData.SellSSR = new SellSSR();
            BindingList<SSRRequest> ssrRequests = new BindingList<SSRRequest>();

            SSRRequest ssrRequest = new SSRRequest();
            ssrRequest.CurrencyCode = currencyCode;

            BindingList<SegmentSSRRequest> segmentSSRRequests = new BindingList<SegmentSSRRequest>();
            for (int i = 0; i < journeysToSellSSRsOn.Length; i++)
            {
                for (int j = 0; j < journeysToSellSSRsOn[i].Segments.Length; j++)
                {
                    SegmentSSRRequest segmentSSRRequest = new SegmentSSRRequest();
                    segmentSSRRequest.ArrivalStation = journeysToSellSSRsOn[i].Segments[j].ArrivalStation;
                    segmentSSRRequest.DepartureStation = journeysToSellSSRsOn[i].Segments[j].DepartureStation;
                    segmentSSRRequest.FlightDesignator = new Buchung4U.API_BookingService.FlightDesignator();
                    segmentSSRRequest.FlightDesignator.CarrierCode = journeysToSellSSRsOn[i].Segments[j].FlightDesignator.CarrierCode;
                    segmentSSRRequest.FlightDesignator.FlightNumber = journeysToSellSSRsOn[i].Segments[j].FlightDesignator.FlightNumber;
                    segmentSSRRequest.FlightDesignator.OpSuffix = journeysToSellSSRsOn[i].Segments[j].FlightDesignator.OpSuffix;
                    segmentSSRRequest.STD = journeysToSellSSRsOn[i].Segments[j].STD;

                    BindingList<Buchung4U.API_BookingService.PaxSSR> paxSSRs = new BindingList<Buchung4U.API_BookingService.PaxSSR>();
                    Buchung4U.API_BookingService.PaxSSR paxSSR = new Buchung4U.API_BookingService.PaxSSR();
                    paxSSR.ActionStatusCode = "SS";
                    paxSSR.SSRCode = SSRCode;
                    paxSSR.SSRNumber = 0;
                    paxSSR.State = Buchung4U.API_BookingService.MessageState.New;
                    paxSSR.DepartureStation = journeysToSellSSRsOn[i].Segments[j].DepartureStation;
                    paxSSR.ArrivalStation = journeysToSellSSRsOn[i].Segments[j].ArrivalStation;
                    paxSSR.PassengerNumber = 0;

                    paxSSRs.Add(paxSSR);
                    tmpPaxSSR = new Buchung4U.API_BookingService.PaxSSR[paxSSRs.Count];
                    paxSSRs.CopyTo(tmpPaxSSR, 0);
                    paxSSRs.Clear();
                    segmentSSRRequest.PaxSSRs = tmpPaxSSR;

                    segmentSSRRequests.Add(segmentSSRRequest);
                }
            }

            ssrRequest.SegmentSSRRequests = segmentSSRRequests.ToArray();
            sellRequest.SellRequestData.SellSSR.SSRRequest = ssrRequest;

            return bookingManager.Sell(sellRequest);
        }
        #endregion

        #region nskReSellSSRs
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="journeyNumber"></param>
        /// <returns></returns>
        public ResellSSRResponse nskReSellSSRs(string sessionSignature, int journeyNumber)
        {
            ResellSSRRequest resellSSRs = new ResellSSRRequest();
            resellSSRs.ContractVersion = _contractVersion;
            resellSSRs.MessageContractVersion = _messageContractVersion;
            resellSSRs.EnableExceptionStackTrace = false;
            resellSSRs.Signature = sessionSignature;
            resellSSRs.ResellSSR = new ResellSSR();
            resellSSRs.ResellSSR.JourneyNumber = journeyNumber;
            resellSSRs.ResellSSR.ResellSeatSSRs = true;
            resellSSRs.ResellSSR.ResellSSRs = true;
            resellSSRs.ResellSSR.WaiveSeatFee = false;

            return bookingManager.ResellSSR(resellSSRs);
        }
        #endregion

        #region nskAddPaymentToBooking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="paymentMethodCode"></param>
        /// <param name="currencyCode"></param>
        /// <param name="paymentAmount"></param>
        /// <returns>AddPaymentToBookingResponse</returns>
        public AddPaymentToBookingResponse nskAddPaymentToBooking(string sessionSignature, string paymentMethodCode, string currencyCode, decimal paymentAmount)
        {
            AddPaymentToBookingRequest addPaymentToBookingRequest = new AddPaymentToBookingRequest();
            addPaymentToBookingRequest.ContractVersion = _contractVersion;
            addPaymentToBookingRequest.MessageContractVersion = _messageContractVersion;
            addPaymentToBookingRequest.EnableExceptionStackTrace = false;
            addPaymentToBookingRequest.Signature = sessionSignature;
            addPaymentToBookingRequest.addPaymentToBookingReqData = new AddPaymentToBookingRequestData();

            addPaymentToBookingRequest.addPaymentToBookingReqData.ReferenceType = PaymentReferenceType.Session;
            addPaymentToBookingRequest.addPaymentToBookingReqData.PaymentMethodCode = paymentMethodCode;
            BindingList<PaymentField> paymentFields = new BindingList<PaymentField>();
            PaymentField accountHolderName;
            PaymentField verificationCode;
            PaymentField receiptNumber;
            //PaymentField ipAddress;
            //PaymentField rmkNumber;
            switch (paymentMethodCode)
            {
                case "MC":
                    addPaymentToBookingRequest.addPaymentToBookingReqData.PaymentMethodType = RequestPaymentMethodType.ExternalAccount;
                    addPaymentToBookingRequest.addPaymentToBookingReqData.Installments = 1;
                    addPaymentToBookingRequest.addPaymentToBookingReqData.AccountNumber = "5555444433331111";
                    accountHolderName = new PaymentField();
                    accountHolderName.FieldName = "CC::AccountHolderName";
                    accountHolderName.FieldValue = "Matthias Hansen";
                    paymentFields.Add(accountHolderName);
                    verificationCode = new PaymentField();
                    verificationCode.FieldName = "CC::VerificationCode";
                    verificationCode.FieldValue = "737";
                    paymentFields.Add(verificationCode);
                    //ipAddress = new PaymentField();
                    //ipAddress.FieldName = "BillTo::IPAddress";
                    //ipAddress.FieldValue = "77.241.232.246";
                    //paymentFields.Add(ipAddress);
                    break;
                case "VI":
                    addPaymentToBookingRequest.addPaymentToBookingReqData.PaymentMethodType = RequestPaymentMethodType.ExternalAccount;
                    addPaymentToBookingRequest.addPaymentToBookingReqData.Installments = 1;
                    break;
                case "CA":
                    addPaymentToBookingRequest.addPaymentToBookingReqData.PaymentMethodType = RequestPaymentMethodType.PrePaid;
                    addPaymentToBookingRequest.addPaymentToBookingReqData.Installments = 1;
                    receiptNumber = new PaymentField();
                    //receiptNumber.FieldName = "ReceiptNumber";
                    //receiptNumber.FieldValue = "123456";
                    //paymentFields.Add(receiptNumber);
                    break;
            }
            addPaymentToBookingRequest.addPaymentToBookingReqData.QuotedAmount = paymentAmount;
            addPaymentToBookingRequest.addPaymentToBookingReqData.QuotedCurrencyCode = currencyCode;
            addPaymentToBookingRequest.addPaymentToBookingReqData.Expiration = new DateTime(2016, 06, 30);
            tmpPaymentFields = new PaymentField[paymentFields.Count];
            paymentFields.CopyTo(tmpPaymentFields, 0);
            paymentFields.Clear();
            addPaymentToBookingRequest.addPaymentToBookingReqData.PaymentFields = tmpPaymentFields;
            addPaymentToBookingRequest.addPaymentToBookingReqData.Status = BookingPaymentStatus.New;
            addPaymentToBookingRequest.addPaymentToBookingReqData.MessageState = Buchung4U.API_BookingService.MessageState.New;

            AddPaymentToBookingResponse addPaymentToBookingResponse = bookingManager.AddPaymentToBooking(addPaymentToBookingRequest);

            return addPaymentToBookingResponse;
        }
        #endregion

        #region BookingCommit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="organizationCode"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public BookingCommitResponse nskBookingCommit(string sessionSignature, string currencyCode, string organizationCode, string groupName)
        {
            GetBookingFromStateRequest getBookingFromStateRequest = new GetBookingFromStateRequest();
            getBookingFromStateRequest.ContractVersion = _contractVersion;
            getBookingFromStateRequest.MessageContractVersion = _messageContractVersion;
            getBookingFromStateRequest.EnableExceptionStackTrace = false;
            getBookingFromStateRequest.Signature = sessionSignature;
            GetBookingFromStateResponse getBookingFromStateResponse = bookingManager.GetBookingFromState(getBookingFromStateRequest);

            BookingCommitRequest bookingCommitRequest = new BookingCommitRequest();
            bookingCommitRequest.ContractVersion = _contractVersion;
            bookingCommitRequest.Signature = sessionSignature;
            bookingCommitRequest.BookingCommitRequestData = new BookingCommitRequestData();
            bookingCommitRequest.BookingCommitRequestData.State = API_BookingService.MessageState.New;
            bookingCommitRequest.BookingCommitRequestData.DistributeToContacts = false;
            bookingCommitRequest.BookingCommitRequestData.CurrencyCode = currencyCode;
            if (groupName != string.Empty)
            {
                bookingCommitRequest.BookingCommitRequestData.GroupName = groupName;
            }
            bookingCommitRequest.BookingCommitRequestData.ReceivedBy = new ReceivedByInfo();
            bookingCommitRequest.BookingCommitRequestData.ReceivedBy.State = API_BookingService.MessageState.New;
            bookingCommitRequest.BookingCommitRequestData.ReceivedBy.ReceivedBy = "Matthias Hansen";

            #region BookingContact
            List<BookingContact> bookingContacts = new List<BookingContact>();
            BookingContact bookingContact = new BookingContact();
            bookingContact.TypeCode = "P";
            List<API_BookingService.BookingName> bookingNames = new List<API_BookingService.BookingName>();
            API_BookingService.BookingName bookingName = new API_BookingService.BookingName();
            bookingName.FirstName = "Matthias";
            bookingName.LastName = "Hansen";
            bookingName.Title = "MR";
            bookingNames.Add(bookingName);
            bookingContact.Names = bookingNames.ToArray();
            bookingContact.AddressLine1 = "Klugkiststraße 2H";
            bookingContact.City = "Bremen";
            bookingContact.PostalCode = "28209";
            bookingContact.CountryCode = "DE";
            bookingContact.HomePhone = "+49 160 8490222";
            bookingContact.SourceOrganization = "1122334455";
            bookingContact.CompanyName = "1234/test company name asdfasdf";
            bookingContact.CultureCode = "de-DE";
            bookingContact.EmailAddress = "hansen@prologis.aero";
            bookingContact.DistributionOption = DistributionOption.Email;
            bookingContact.NotificationPreference = Buchung4U.API_BookingService.NotificationPreference.None;
            bookingContact.State = API_BookingService.MessageState.New;
            bookingContacts.Add(bookingContact);
            bookingCommitRequest.BookingCommitRequestData.BookingContacts = bookingContacts.ToArray();
            #endregion

            #region Passengers
            List<Passenger> passengers = new List<Passenger>();
            for (int i = 0; i < paxAdults; i++)
            {
                Passenger passenger = new Passenger();
                List<API_BookingService.BookingName> passengerNames = new List<API_BookingService.BookingName>();
                API_BookingService.BookingName passengerName = new API_BookingService.BookingName();
                if (i == 0)
                {
                    passengerName.FirstName = "Matthias";
                    passengerName.LastName = "Hansen";
                    passengerName.Title = "MR";
                    if (paxInfants > 0)
                    {
                        passenger.Infant = new PassengerInfant();
                        passenger.Infant.State = API_BookingService.MessageState.New;
                        BindingList<Buchung4U.API_BookingService.BookingName> names = new BindingList<API_BookingService.BookingName>();
                        Buchung4U.API_BookingService.BookingName name = new API_BookingService.BookingName();
                        name.FirstName = "Mini";
                        name.LastName = "Hansen";
                        name.Title = "MR";
                        names.Add(name);
                        passenger.Infant.Names = names.ToArray();
                        passenger.Infant.Gender = API_BookingService.Gender.Male;
                        passenger.Infant.DOB = new DateTime(2012, 01, 01);
                    }
                }
                else
                {
                    passengerName.FirstName = "Matze" + i.ToString();
                    passengerName.LastName = "Hansen";
                    passengerName.Title = "MR";
                }
                passengerNames.Add(passengerName);
                passenger.Names = passengerNames.ToArray();
                List<PassengerTypeInfo> paxTypeInfos = new List<PassengerTypeInfo>();
                PassengerTypeInfo paxTypeInfo = new PassengerTypeInfo();
                paxTypeInfo.PaxType = "ADT";
                paxTypeInfos.Add(paxTypeInfo);
                passenger.PassengerTypeInfos = paxTypeInfos.ToArray();
                passenger.PassengerInfo = new PassengerInfo();
                passenger.PassengerInfo.Gender = API_BookingService.Gender.Male;
                passenger.PassengerInfo.WeightCategory = API_BookingService.WeightCategory.Male;
                passenger.PaxDiscountCode = string.Empty;
                passenger.State = API_BookingService.MessageState.Modified;
                
                passengers.Add(passenger);
            }
            for (int i = 0; i < paxChilds; i++)
            {
                Passenger passenger = new Passenger();
                List<API_BookingService.BookingName> passengerNames = new List<API_BookingService.BookingName>();
                API_BookingService.BookingName passengerName = new API_BookingService.BookingName();
                passengerName.FirstName = "Small" + i.ToString();
                passengerName.LastName = "Hansen";
                passengerName.Title = "CHD";
                passengerNames.Add(passengerName);
                passenger.Names = passengerNames.ToArray();
                List<PassengerTypeInfo> paxTypeInfos = new List<PassengerTypeInfo>();
                PassengerTypeInfo paxTypeInfo = new PassengerTypeInfo();
                paxTypeInfo.PaxType = "CHD";
                paxTypeInfos.Add(paxTypeInfo);
                passenger.PassengerTypeInfos = paxTypeInfos.ToArray();
                passenger.PassengerInfo = new PassengerInfo();
                passenger.PassengerInfo.Gender = API_BookingService.Gender.Male;
                passenger.PassengerInfo.WeightCategory = API_BookingService.WeightCategory.Child;
                passenger.PaxDiscountCode = "50";
                passenger.State = API_BookingService.MessageState.Modified;
                passengers.Add(passenger);
            }
            bookingCommitRequest.BookingCommitRequestData.Passengers = passengers.ToArray();
            bookingCommitRequest.BookingCommitRequestData.PaxCount = (short)passengers.Count;
            #endregion

            //bookingCommitRequest.BookingCommitRequestData.RecordLocators = new RecordLocator[1];
            //bookingCommitRequest.BookingCommitRequestData.RecordLocators[0] = new RecordLocator();
            //bookingCommitRequest.BookingCommitRequestData.RecordLocators[0].State = new API_BookingService.MessageState();
            //bookingCommitRequest.BookingCommitRequestData.RecordLocators[0].SystemCode = "BTG";
            //bookingCommitRequest.BookingCommitRequestData.RecordLocators[0].RecordCode = "123456";

            #region Third Party RecordLocator
            //if (organizationCode != string.Empty)
            //{
            //    string systemCode = organizationCode.Substring(0, 3);
            //    BindingList<RecordLocator> recordLocators = new BindingList<RecordLocator>();
            //    RecordLocator recLoc = new RecordLocator();
            //    recLoc.State = API_BookingService.MessageState.New;
            //    recLoc.SystemCode = "BTG"; //systemCode;
            //    recLoc.RecordCode = "123456";
            //    recordLocators.Add(recLoc);
            //    bookingCommitRequest.BookingCommitRequestData.RecordLocators = recordLocators.ToArray();
            //}
            #endregion

            API_BookingService.PointOfSale pos = new API_BookingService.PointOfSale();
            pos.State = API_BookingService.MessageState.New;
            pos.AgentCode = "12345678";
            pos.DomainCode = "12345"; // max. 5 characters
            pos.LocationCode = "locat"; // max. 5 characters
            pos.OrganizationCode = "Philips";
            bookingCommitRequest.BookingCommitRequestData.SourcePOS = pos;

            return bookingManager.BookingCommit(bookingCommitRequest);
        }


        public BookingCommitResponse nskBookingCommit(string sessionSignature, Booking currentBooking)
        {
            BookingCommitRequest commitRequest = new BookingCommitRequest();
            commitRequest.ContractVersion = _contractVersion;
            commitRequest.Signature = sessionSignature;
            commitRequest.BookingCommitRequestData = new BookingCommitRequestData();
            commitRequest.BookingCommitRequestData.State = API_BookingService.MessageState.Modified;
            commitRequest.BookingCommitRequestData.RecordLocator = currentBooking.RecordLocator;
            commitRequest.BookingCommitRequestData.CurrencyCode = currentBooking.CurrencyCode;
            commitRequest.BookingCommitRequestData.ReceivedBy = new ReceivedByInfo();
            commitRequest.BookingCommitRequestData.ReceivedBy.ReceivedBy = "API Test Tool";

            List<BookingComment> bookingComments = new List<BookingComment>();
            BookingComment bc = new BookingComment();
            bc.CommentType = API_BookingService.CommentType.Itinerary;
            bc.CommentText = "Travel document deleted via the API: " + DateTime.Now.ToString();
            bookingComments.Add(bc);
            commitRequest.BookingCommitRequestData.BookingComments = bookingComments.ToArray();

            return bookingManager.BookingCommit(commitRequest);
        }
        #endregion

        #region nskClear
        /// <summary>
        /// Clear the session
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <returns></returns>
        public ClearResponse nskClear(string sessionSignature)
        {
            ClearRequest clearRequest = new ClearRequest();
            clearRequest.ContractVersion = _contractVersion;
            clearRequest.MessageContractVersion = _messageContractVersion;
            clearRequest.EnableExceptionStackTrace = false;
            clearRequest.Signature = sessionSignature;

            return bookingManager.Clear(clearRequest);
        }
        #endregion

        #region nskGetPaymentFeePrice
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="feeCode"></param>
        /// <param name="paymentAmount"></param>
        /// <returns>GetPaymentFeePriceResponse</returns>
        public GetPaymentFeePriceResponse nskGetPaymentFeePrice(string sessionSignature, string currencyCode, string feeCode, decimal paymentAmount)
        {
            GetPaymentFeePriceRequest getPaymentFeePriceRequest = new GetPaymentFeePriceRequest();
            getPaymentFeePriceRequest.ContractVersion = _contractVersion;
            getPaymentFeePriceRequest.MessageContractVersion = _messageContractVersion;
            getPaymentFeePriceRequest.EnableExceptionStackTrace = false;
            getPaymentFeePriceRequest.Signature = sessionSignature;
            getPaymentFeePriceRequest.paymentFeePriceReqData = new PaymentFeePriceRequest();
            getPaymentFeePriceRequest.paymentFeePriceReqData.CurrencyCode = currencyCode;
            getPaymentFeePriceRequest.paymentFeePriceReqData.FeeCode = feeCode;
            getPaymentFeePriceRequest.paymentFeePriceReqData.PaymentAmount = paymentAmount;

            return bookingManager.GetPaymentFeePrice(getPaymentFeePriceRequest);
        }
        #endregion

        #region nskGetBookingPayments
        /// <summary>
        /// Liefert die Payments für eine bestimmte Buchung, die mit dem Parameter
        /// 'recordLocator' übermittelt wird.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <returns>GetBookingPaymentsResponse</returns>
        public GetBookingPaymentsResponse nskGetBookingPayments(string sessionSignature, string recordLocator)
        {
            GetBookingPaymentsRequest getBookingPaymentsRequest = new GetBookingPaymentsRequest();
            getBookingPaymentsRequest.ContractVersion = _contractVersion;
            getBookingPaymentsRequest.MessageContractVersion = _messageContractVersion;
            getBookingPaymentsRequest.EnableExceptionStackTrace = false;
            getBookingPaymentsRequest.Signature = sessionSignature;
            getBookingPaymentsRequest.GetBookingPaymentsReqData = new GetBookingPaymentsRequestData();
            getBookingPaymentsRequest.GetBookingPaymentsReqData.GetByRecordLocator = new GetByRecordLocator();
            getBookingPaymentsRequest.GetBookingPaymentsReqData.GetByRecordLocator.RecordLocator = recordLocator;

            return bookingManager.GetBookingPayments(getBookingPaymentsRequest);
        }

        /// <summary>
        /// Liefert die Payments für die im State befindliche Buchung.
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <returns>GetBookingPaymentsResponse</returns>
        public GetBookingPaymentsResponse nskGetBookingPayments(string sessionSignature)
        {
            GetBookingPaymentsRequest getBookingPaymentsRequest = new GetBookingPaymentsRequest();
            getBookingPaymentsRequest.ContractVersion = _contractVersion;
            getBookingPaymentsRequest.Signature = sessionSignature;
            getBookingPaymentsRequest.GetBookingPaymentsReqData = new GetBookingPaymentsRequestData();
            getBookingPaymentsRequest.GetBookingPaymentsReqData.GetCurrentState = true;

            return bookingManager.GetBookingPayments(getBookingPaymentsRequest);
        }
        #endregion

        #region nskFindBooking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="emailAddress"></param>
        /// <returns>FindBookingResponse</returns>
        public FindBookingResponse nskFindBooking(string sessionSignature, string emailAddress)
        {
            FindBookingRequest fbreq = new FindBookingRequest();
            fbreq.ContractVersion = _contractVersion;
            fbreq.MessageContractVersion = _messageContractVersion;
            fbreq.EnableExceptionStackTrace = false;
            fbreq.Signature = sessionSignature;
            fbreq.FindBookingRequestData = new FindBookingRequestData();
            fbreq.FindBookingRequestData.FindBookingBy = FindBookingBy.EmailAddress;
            fbreq.FindBookingRequestData.FindByEmailAddress = new FindByEmailAddress();
            fbreq.FindBookingRequestData.FindByEmailAddress.EmailAddress = emailAddress;

            return bookingManager.FindBooking(fbreq);
        }
        #endregion

        #region nskGetBooking
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="PNR"></param>
        /// <returns>GetBookingResponse</returns>
        public GetBookingResponse nskGetBooking(string sessionSignature, string PNR)
        {
            GetBookingRequest gbreq = new GetBookingRequest();
            gbreq.ContractVersion = _contractVersion;
            gbreq.MessageContractVersion = _messageContractVersion;
            gbreq.EnableExceptionStackTrace = false;
            gbreq.Signature = sessionSignature;
            gbreq.GetBookingReqData = new GetBookingRequestData();
            gbreq.GetBookingReqData.GetBookingBy = GetBookingBy.RecordLocator;
            gbreq.GetBookingReqData.GetByRecordLocator = new GetByRecordLocator();
            gbreq.GetBookingReqData.GetByRecordLocator.RecordLocator = PNR;

            return bookingManager.GetBooking(gbreq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="thirdPartyRecordLocator"></param>
        /// <param name="systemCode"></param>
        /// <returns>GetBookingResponse</returns>
        public GetBookingResponse nskGetBooking(string sessionSignature, string thirdPartyRecordLocator, string systemCode)
        {
            GetBookingRequest gbreq = new GetBookingRequest();
            gbreq.ContractVersion = _contractVersion;
            gbreq.Signature = sessionSignature;
            gbreq.GetBookingReqData = new GetBookingRequestData();
            gbreq.GetBookingReqData.GetBookingBy = GetBookingBy.ThirdPartyRecordLocator;
            gbreq.GetBookingReqData.GetByThirdPartyRecordLocator = new GetByThirdPartyRecordLocator();
            gbreq.GetBookingReqData.GetByThirdPartyRecordLocator.RecordLocator = thirdPartyRecordLocator;
            gbreq.GetBookingReqData.GetByThirdPartyRecordLocator.SystemCode = systemCode;

            return bookingManager.GetBooking(gbreq);
        }
        #endregion

        #region nskGetBookingFromState
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <returns>GetBookingFromStateResponse</returns>
        public GetBookingFromStateResponse nskGetBookingFromState(string sessionSignature)
        {
            GetBookingFromStateRequest gbsreq = new GetBookingFromStateRequest();
            gbsreq.ContractVersion = _contractVersion;
            gbsreq.MessageContractVersion = _messageContractVersion;
            gbsreq.EnableExceptionStackTrace = false;
            gbsreq.Signature = sessionSignature;

            return bookingManager.GetBookingFromState(gbsreq);
        }
        #endregion

        #region nskAddCheckInBaggage
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="PNR"></param>
        /// <returns></returns>
        public CheckInBaggageResponse AddCheckInBaggage(string sessionSignature, string PNR)
        {
            GetBookingResponse currentBooking = this.nskGetBooking(sessionSignature, PNR);

            API_BookingService.Segment segment = currentBooking.Booking.Journeys[0].Segments[0];

            CheckInBaggageRequest checkInBaggageRequest = new CheckInBaggageRequest();
            checkInBaggageRequest.ContractVersion = _contractVersion;
            checkInBaggageRequest.MessageContractVersion = _messageContractVersion;
            checkInBaggageRequest.EnableExceptionStackTrace = false;
            checkInBaggageRequest.Signature = sessionSignature;
            checkInBaggageRequest.CheckInBaggageRequestData = new CheckInBaggageRequestData();
            checkInBaggageRequest.CheckInBaggageRequestData.Count = 1;

            checkInBaggageRequest.CheckInBaggageRequestData.ProcessBaggageActionType = ProcessBaggageActionType.Add;
            checkInBaggageRequest.CheckInBaggageRequestData.RecordLocator = PNR;
            checkInBaggageRequest.CheckInBaggageRequestData.WeightType = API_OperationService.WeightType.Kilograms;
            checkInBaggageRequest.CheckInBaggageRequestData.Weight = 15;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey = new Buchung4U.API_OperationService.InventoryLegKey();
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.DepartureStation = segment.DepartureStation;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.ArrivalStation = segment.ArrivalStation;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.DepartureDate = segment.STD;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.CarrierCode = segment.FlightDesignator.CarrierCode;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.FlightNumber = segment.FlightDesignator.FlightNumber;
            checkInBaggageRequest.CheckInBaggageRequestData.InventoryLegKey.OpSuffix = segment.FlightDesignator.OpSuffix;

            checkInBaggageRequest.CheckInBaggageRequestData.Name = new Buchung4U.API_OperationService.Name();
            checkInBaggageRequest.CheckInBaggageRequestData.Name.Title = currentBooking.Booking.Passengers[0].Names[0].Title;
            checkInBaggageRequest.CheckInBaggageRequestData.Name.FirstName = currentBooking.Booking.Passengers[0].Names[0].FirstName;
            checkInBaggageRequest.CheckInBaggageRequestData.Name.LastName = currentBooking.Booking.Passengers[0].Names[0].LastName;

            List<BaggageInfo> baggageInfos = new List<BaggageInfo>();
            BaggageInfo baggageInfo = new BaggageInfo();
            baggageInfo.ManualBagTag = true;
            baggageInfo.OSTag = segment.ArrivalStation + "223456";
            baggageInfo.TaggedToFlightNumber = segment.FlightDesignator.FlightNumber;
            baggageInfo.TaggedToStation = segment.ArrivalStation;
            baggageInfo.BaggageStatus = API_OperationService.BaggageStatus.Added;
            baggageInfo.Weight = 15;
            baggageInfos.Add(baggageInfo);
            checkInBaggageRequest.CheckInBaggageRequestData.BaggageInfoList = baggageInfos.ToArray();

            return operationManager.CheckInBaggage(checkInBaggageRequest);
        }
        #endregion

        #region nskGetSeatAvailability
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <returns>GetSeatAvailabilityResponse</returns>
        public GetSeatAvailabilityResponse nskGetSeatAvailability(string sessionSignature, string recordLocator)
        {
            GetBookingResponse getBookingInState = nskGetBooking(sessionSignature, recordLocator);

            GetSeatAvailabilityRequest getSeatAvailabilityRequest = new GetSeatAvailabilityRequest();
            getSeatAvailabilityRequest.ContractVersion = _contractVersion;
            getSeatAvailabilityRequest.MessageContractVersion = _messageContractVersion;
            getSeatAvailabilityRequest.EnableExceptionStackTrace = false;
            getSeatAvailabilityRequest.Signature = sessionSignature;
            getSeatAvailabilityRequest.SeatAvailabilityRequest = new Buchung4U.API_BookingService.SeatAvailabilityRequest();
            getSeatAvailabilityRequest.SeatAvailabilityRequest.DepartureStation = getBookingInState.Booking.Journeys[0].Segments[0].DepartureStation;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.ArrivalStation = getBookingInState.Booking.Journeys[0].Segments[0].ArrivalStation;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.CarrierCode = getBookingInState.Booking.Journeys[0].Segments[0].FlightDesignator.CarrierCode;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.FlightNumber = getBookingInState.Booking.Journeys[0].Segments[0].FlightDesignator.FlightNumber;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.STD = getBookingInState.Booking.Journeys[0].Segments[0].STD;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.OpSuffix = getBookingInState.Booking.Journeys[0].Segments[0].FlightDesignator.OpSuffix;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.IncludeSeatFees = true;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.CompressProperties = true;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.IncludePropertyLookup = true;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.SeatGroup = 0;
            getSeatAvailabilityRequest.SeatAvailabilityRequest.SeatAssignmentMode = Buchung4U.API_BookingService.SeatAssignmentMode.PreSeatAssignment;

            return bookingManager.GetSeatAvailability(getSeatAvailabilityRequest);
        }
        #endregion

        #region nskAssignSeats
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currentBooking"></param>
        /// <returns>AssignSeatsResponse</returns>
        public AssignSeatsResponse nskAssignSeats(string sessionSignature, Booking currentBooking)
        {
            AssignSeatsRequest assignSeatsRequest = new AssignSeatsRequest();
            assignSeatsRequest.ContractVersion = _contractVersion;
            assignSeatsRequest.MessageContractVersion = _messageContractVersion;
            assignSeatsRequest.EnableExceptionStackTrace = false;
            assignSeatsRequest.Signature = sessionSignature;
            assignSeatsRequest.SellSeatRequest = new SeatSellRequest();
            assignSeatsRequest.SellSeatRequest.SeatAssignmentMode = Buchung4U.API_BookingService.SeatAssignmentMode.AutoDetermine;
            assignSeatsRequest.SellSeatRequest.AssignNoSeatIfAlreadyTaken = false;

            BindingList<Buchung4U.API_BookingService.SegmentSeatRequest> segmentSeatRequests = new BindingList<Buchung4U.API_BookingService.SegmentSeatRequest>();
            for (int i = 0; i < currentBooking.Journeys.Length; i++)
            {
                for (int j = 0; j < currentBooking.Journeys[0].Segments.Length; j++)
                {
                    Buchung4U.API_BookingService.SegmentSeatRequest segmentSeatRequest = new Buchung4U.API_BookingService.SegmentSeatRequest();
                    segmentSeatRequest.ArrivalStation = currentBooking.Journeys[i].Segments[j].ArrivalStation;
                    segmentSeatRequest.DepartureStation = currentBooking.Journeys[i].Segments[j].DepartureStation;
                    segmentSeatRequest.FlightDesignator = currentBooking.Journeys[i].Segments[j].FlightDesignator;
                    segmentSeatRequest.STD = currentBooking.Journeys[i].Segments[j].STD;
                    segmentSeatRequest.PassengerNumbers = new short[currentBooking.Passengers.Length];
                    for (int k = 0; k < currentBooking.Passengers.Length; k++)
                    {
                        segmentSeatRequest.PassengerNumbers[k] = new short();
                        segmentSeatRequest.PassengerNumbers[k] = currentBooking.Passengers[k].PassengerNumber;
                    }
                    
                    segmentSeatRequest.UnitDesignator = "12A";
                    segmentSeatRequests.Add(segmentSeatRequest);
                }
            }
            assignSeatsRequest.SellSeatRequest.SegmentSeatRequests = segmentSeatRequests.ToArray();

            return bookingManager.AssignSeats(assignSeatsRequest);
        }
        #endregion

        #region nskUnAssignSeats
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currentBooking"></param>
        /// <returns></returns>
        public UnassignSeatsResponse nskUnAssignSeats(string sessionSignature, Booking currentBooking)
        {
            UnassignSeatsRequest unAssignSeatsRequest = new UnassignSeatsRequest();
            unAssignSeatsRequest.ContractVersion = _contractVersion;
            unAssignSeatsRequest.MessageContractVersion = _messageContractVersion;
            unAssignSeatsRequest.EnableExceptionStackTrace = false;
            unAssignSeatsRequest.Signature = sessionSignature;
            unAssignSeatsRequest.SellSeatRequest = new SeatSellRequest();
            unAssignSeatsRequest.SellSeatRequest.BlockType = API_BookingService.UnitHoldType.Session;

            BindingList<Buchung4U.API_BookingService.SegmentSeatRequest> segmentSeatRequests = new BindingList<Buchung4U.API_BookingService.SegmentSeatRequest>();
            for (int i = 0; i < currentBooking.Journeys.Length; i++)
            {
                for (int j = 0; j < currentBooking.Journeys[0].Segments.Length; j++)
                {
                    for (int k = 0; k < currentBooking.Journeys[0].Segments[0].PaxSeats.Length; k++)
                    {
                        Buchung4U.API_BookingService.SegmentSeatRequest segmentSeatRequest = new Buchung4U.API_BookingService.SegmentSeatRequest();
                        segmentSeatRequest.ArrivalStation = currentBooking.Journeys[i].Segments[j].ArrivalStation;
                        segmentSeatRequest.DepartureStation = currentBooking.Journeys[i].Segments[j].DepartureStation;
                        segmentSeatRequest.FlightDesignator = currentBooking.Journeys[i].Segments[j].FlightDesignator;
                        segmentSeatRequest.STD = currentBooking.Journeys[i].Segments[j].STD;
                        segmentSeatRequest.PassengerNumbers = new short[1];
                        segmentSeatRequest.PassengerNumbers[0] = new short();
                        segmentSeatRequest.PassengerNumbers[0] = currentBooking.Journeys[0].Segments[0].PaxSeats[k].PassengerNumber;
                        segmentSeatRequest.UnitDesignator = currentBooking.Journeys[0].Segments[0].PaxSeats[k].UnitDesignator;
                        segmentSeatRequests.Add(segmentSeatRequest);
                    }
                }
            }
            unAssignSeatsRequest.SellSeatRequest.SegmentSeatRequests = segmentSeatRequests.ToArray();

            return bookingManager.UnassignSeats(unAssignSeatsRequest);
        }
        #endregion

        #region nskOverrideFee
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currencyCode"></param>
        /// <param name="feeNumber"></param>
        /// <param name="passengerNumber"></param>
        /// <param name="netAmount"></param>
        /// <returns>OverrideFeeResponse</returns>
        public OverrideFeeResponse nskOverrideFee(string sessionSignature, string currencyCode, short feeNumber, short passengerNumber, decimal netAmount)
        {
            OverrideFeeRequest overrideFee = new OverrideFeeRequest();
            overrideFee.ContractVersion = _contractVersion;
            overrideFee.MessageContractVersion = _messageContractVersion;
            overrideFee.EnableExceptionStackTrace = false;
            overrideFee.Signature = sessionSignature;
            overrideFee.FeeRequest = new FeeRequest();
            overrideFee.FeeRequest.CurrencyCode = currencyCode;
            overrideFee.FeeRequest.FeeNumber = feeNumber;
            overrideFee.FeeRequest.NetAmount = netAmount;
            overrideFee.FeeRequest.PassengerNumber = passengerNumber;

            return bookingManager.OverrideFee(overrideFee);
        }
        #endregion

        #region nskCheckIn
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <param name="inventoryLegKey"></param>
        /// <param name="title"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="type"></param>
        /// <returns>CheckInPassengerResponse</returns>
        public CheckInPassengerResponse nskCheckin(string sessionSignature, string recordLocator, Buchung4U.API_OperationService.InventoryLegKey inventoryLegKey, string title, string firstname, string lastname, Buchung4U.API_OperationService.LiftStatus type)
        {
            CheckInPassengerRequest checkInPassengerRequest = new CheckInPassengerRequest();
            checkInPassengerRequest.ContractVersion = _contractVersion;
            checkInPassengerRequest.MessageContractVersion = _messageContractVersion;
            checkInPassengerRequest.EnableExceptionStackTrace = false;
            checkInPassengerRequest.Signature = sessionSignature;
            checkInPassengerRequest.checkInRequest = new CheckInRequestData();
            checkInPassengerRequest.checkInRequest.RecordLocator = recordLocator;
            checkInPassengerRequest.checkInRequest.Name = new Buchung4U.API_OperationService.Name();
            checkInPassengerRequest.checkInRequest.Name.Title = title;
            checkInPassengerRequest.checkInRequest.Name.FirstName = firstname;
            checkInPassengerRequest.checkInRequest.Name.LastName = lastname;
            checkInPassengerRequest.checkInRequest.LiftStatus = type;
            checkInPassengerRequest.checkInRequest.InventoryLegKey = inventoryLegKey;

            return operationManager.CheckInPassenger(checkInPassengerRequest);
        }
        #endregion

        #region nskCommit (just Commit to save any changes, no more details)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="currentBooking"></param>
        /// <returns>CommitResponse</returns>
        public CommitResponse nskCommit(string sessionSignature, Booking currentBooking)
        {
            CommitRequest commitRequest = new CommitRequest();
            commitRequest.ContractVersion = _contractVersion;
            commitRequest.Signature = sessionSignature;
            commitRequest.BookingRequest = new CommitRequestData();
            commitRequest.BookingRequest.Booking = new Booking();
            commitRequest.BookingRequest.Booking.RecordLocator = currentBooking.RecordLocator;
            commitRequest.BookingRequest.Booking.CurrencyCode = currentBooking.CurrencyCode;
            commitRequest.BookingRequest.Booking.ReceivedBy = new ReceivedByInfo();
            commitRequest.BookingRequest.Booking.ReceivedBy.ReceivedBy = "API Test Tool";

            BindingList<RecordLocator> recordLocators = new BindingList<RecordLocator>();
            RecordLocator rl = new RecordLocator();
            rl.SystemCode = "CJI";
            rl.RecordCode = "071278";
            recordLocators.Add(rl);
            commitRequest.BookingRequest.Booking.RecordLocators = recordLocators.ToArray();

            //List<BookingComment> bookingComments = new List<BookingComment>();
            //BookingComment bc = new BookingComment();
            //bc.CommentType = API_BookingService.CommentType.Itinerary;
            //bc.CommentText = "AXS Reference Number: 9181716151413121";
            //bookingComments.Add(bc);
            //commitRequest.BookingRequest.Booking.BookingComments = bookingComments.ToArray();
            
            return bookingManager.Commit(commitRequest);
        }
        #endregion

        #region nskDivide
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <param name="cancelPassengerNumber"></param>
        /// <returns>DivideResponse</returns>
        public DivideResponse nskDivide(string sessionSignature, string recordLocator, short cancelPassengerNumber)
        {
            GetBookingResponse getBookingInState = nskGetBooking(sessionSignature, recordLocator);

            DivideRequest divideRequest = new DivideRequest();
            divideRequest.ContractVersion = _contractVersion;
            divideRequest.MessageContractVersion = _messageContractVersion;
            divideRequest.EnableExceptionStackTrace = false;
            divideRequest.Signature = sessionSignature;
            divideRequest.DivideReqData = new DivideRequestData();
            divideRequest.DivideReqData.DivideAction = DivideAction.Divide;
            divideRequest.DivideReqData.SourceRecordLocator = recordLocator;
            divideRequest.DivideReqData.AddComments = true;
            divideRequest.DivideReqData.AutoDividePayments = true;
            divideRequest.DivideReqData.OverrideRestrictions = true;
            divideRequest.DivideReqData.ReceivedBy = "API Test Tool";
            BindingList<short> passengerNumbers = new BindingList<short>();
            passengerNumbers.Add(cancelPassengerNumber);
            divideRequest.DivideReqData.PassengerNumbers = passengerNumbers.ToArray();

            return bookingManager.Divide(divideRequest);
        }
        #endregion

        #region nskCancel
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <param name="cancelType">All | Journey | Fee | SSR</param>
        /// <returns>CancelResponse</returns>
        public CancelResponse nskCancel(string sessionSignature, string recordLocator, CancelBy cancelType)
        {
            GetBookingResponse getBookingInState = nskGetBooking(sessionSignature, recordLocator);

            CancelRequest cancelRequest = new CancelRequest();
            cancelRequest.ContractVersion = _contractVersion;
            cancelRequest.MessageContractVersion = _messageContractVersion;
            cancelRequest.EnableExceptionStackTrace = false;
            cancelRequest.Signature = sessionSignature;
            cancelRequest.CancelRequestData = new CancelRequestData();
            cancelRequest.CancelRequestData.CancelBy = cancelType;

            #region CancelBy.All
            if (cancelType == CancelBy.All)
            {
                // that's it, nothing else required for CancelBy.All
            }
            #endregion

            #region CancelBy.Journey
            if (cancelType == CancelBy.Journey)
            {
                cancelRequest.CancelRequestData.CancelJourney = new CancelJourney();
                cancelRequest.CancelRequestData.CancelJourney.CancelJourneyRequest = new CancelJourneyRequest();
                BindingList<Journey> cancelJourneys = new BindingList<Journey>();
                cancelJourneys.Add(getBookingInState.Booking.Journeys[0]);
                cancelRequest.CancelRequestData.CancelJourney.CancelJourneyRequest.Journeys = cancelJourneys.ToArray();
                cancelRequest.CancelRequestData.CancelJourney.CancelJourneyRequest.WaivePenaltyFee = false;
                cancelRequest.CancelRequestData.CancelJourney.CancelJourneyRequest.WaiveSpoilageFee = false; 
                cancelRequest.CancelRequestData.CancelJourney.CancelJourneyRequest.PreventReprice = true;
            }
            #endregion

            #region CancelBy.Fee
            if (cancelType == CancelBy.Fee)
            {
                // to be completed
                cancelRequest.CancelRequestData.CancelFee = new CancelFee();
                cancelRequest.CancelRequestData.CancelFee.FeeRequest = new FeeRequest();
            }
            #endregion

            #region CancelBy.SSR
            if (cancelType == CancelBy.SSR)
            {
                // Find 'BLND' SSR to be cancelled
                BindingList<Buchung4U.API_BookingService.PaxSSR> cancelPaxSSRs = new BindingList<API_BookingService.PaxSSR>();
                cancelPaxSSRs.Add(Array.Find(getBookingInState.Booking.Journeys[0].Segments[0].PaxSSRs, item => item.SSRCode == "BLND"));
                
                cancelRequest.CancelRequestData.CancelSSR = new CancelSSR();
                cancelRequest.CancelRequestData.CancelSSR.SSRRequest = new SSRRequest();
                cancelRequest.CancelRequestData.CancelSSR.SSRRequest.CurrencyCode = getBookingInState.Booking.CurrencyCode;
                BindingList<SegmentSSRRequest> ssrs = new BindingList<SegmentSSRRequest>();
                SegmentSSRRequest ssr = new SegmentSSRRequest();
                ssr.ArrivalStation = getBookingInState.Booking.Journeys[0].Segments[0].ArrivalStation;
                ssr.DepartureStation = getBookingInState.Booking.Journeys[0].Segments[0].DepartureStation;
                ssr.FlightDesignator = getBookingInState.Booking.Journeys[0].Segments[0].FlightDesignator;
                ssr.PaxSSRs = cancelPaxSSRs.ToArray();
                ssr.STD = getBookingInState.Booking.Journeys[0].Segments[0].STD;
                ssrs.Add(ssr);
                cancelRequest.CancelRequestData.CancelSSR.SSRRequest.SegmentSSRRequests = ssrs.ToArray();
            }
            #endregion

            return bookingManager.Cancel(cancelRequest);
        }
        #endregion

        #region nskGetFareRule
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="carrierCode"></param>
        /// <param name="classOfService"></param>
        /// <param name="fareBasisCode"></param>
        /// <param name="ruleNumber"></param>
        /// <param name="ruleTariff"></param>
        /// <returns></returns>
        public GetFareRuleInfoResponse nskGetFareRule(string sessionSignature, string carrierCode, string classOfService, string fareBasisCode, string ruleNumber, string ruleTariff)
        {
            GetFareRuleInfoRequest getFareRequest = new GetFareRuleInfoRequest();
            getFareRequest.ContractVersion = _contractVersion;
            getFareRequest.MessageContractVersion = _messageContractVersion;
            getFareRequest.EnableExceptionStackTrace = false;
            getFareRequest.Signature = sessionSignature;
            getFareRequest.fareRuleReqData = new FareRuleRequestData();
            getFareRequest.fareRuleReqData.CarrierCode = carrierCode;
            getFareRequest.fareRuleReqData.ClassOfService = classOfService;
            getFareRequest.fareRuleReqData.FareBasisCode = fareBasisCode;
            getFareRequest.fareRuleReqData.RuleNumber = ruleNumber;
            getFareRequest.fareRuleReqData.RuleTariff = ruleTariff;

            return contentManager.GetFareRuleInfo(getFareRequest);
        }
        #endregion

        #region nskUpdatePassenger (not fully implemented)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="updatePax"></param>
        /// <returns></returns>
        public UpdatePassengersResponse nskUpdatePassenger(string sessionSignature, Passenger updatePax)
        {
            UpdatePassengersRequest up = new UpdatePassengersRequest();
            up.ContractVersion = _contractVersion;
            up.MessageContractVersion = _messageContractVersion;
            up.EnableExceptionStackTrace = false;
            up.Signature = sessionSignature;
            up.updatePassengersRequestData = new UpdatePassengersRequestData();
            up.updatePassengersRequestData.WaiveNameChangeFee = true;
            List<Passenger> passengers = new List<Passenger>();
            Passenger pax = new Passenger();

            // start changing pax data
            pax = updatePax;
            pax.State = API_BookingService.MessageState.Modified;
            pax.PassengerTravelDocuments = null;
            //pax.CustomerNumber = updatePax.CustomerNumber;
            //pax.FamilyNumber = updatePax.FamilyNumber;
            //pax.Infant = updatePax.Infant;
            //pax.Names = updatePax.Names;
            //updatePax.Names.CopyTo(pax.Names, 0);
            //pax.PassengerID = updatePax.PassengerID;
            //pax.PassengerNumber = updatePax.PassengerNumber;
            //updatePax.PassengerTypeInfos.CopyTo(pax.PassengerTypeInfos, 0);
            
            // end changing pax data
            
            passengers.Add(pax);
            up.updatePassengersRequestData.Passengers = passengers.ToArray();

            return bookingManager.UpdatePassengers(up);
        }
        #endregion

        #region nskGetManifest
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        public void nskGetManifestMethods(string sessionSignature)
        {
            //GetFlightInformationRequest gfireq = new GetFlightInformationRequest();
            //gfireq.ContractVersion = _contractVersion;
            //gfireq.Signature = sessionSignature;
            //gfireq.flightInformationRequest = new FlightInformationRequestData();
            //gfireq.flightInformationRequest.ArrivalStation = "BCN";
            //gfireq.flightInformationRequest.CarrierCode = "HV";
            //gfireq.flightInformationRequest.DepartureDate = new DateTime(2012, 05, 09);
            //gfireq.flightInformationRequest.DepartureStation = "AMS";
            //gfireq.flightInformationRequest.FlightNumber = "5135";
            //gfireq.flightInformationRequest.OpSuffix = " ";
            //GetFlightInformationResponse gfires = operationManager.GetFlightInformation(gfireq);


            /* UNFILTERED */
            GetManifestFilteredRequest gmfreq = new GetManifestFilteredRequest();
            gmfreq.ContractVersion = _contractVersion;
            gmfreq.MessageContractVersion = _messageContractVersion;
            gmfreq.EnableExceptionStackTrace = false;
            gmfreq.Signature = sessionSignature;
            gmfreq.manifestFilteredRequest = new ManifestFilteredRequestData();

            Buchung4U.API_OperationService.InventoryLegKey inventoryLegKey = new Buchung4U.API_OperationService.InventoryLegKey();
            inventoryLegKey.ArrivalStation = "BCN";
            inventoryLegKey.CarrierCode = "HV";
            inventoryLegKey.DepartureDate = new DateTime(2012, 05, 09);
            inventoryLegKey.DepartureStation = "AMS";
            inventoryLegKey.FlightNumber = "5135";
            inventoryLegKey.OpSuffix = " ";

            ManifestFilter filter = new ManifestFilter();

            gmfreq.manifestFilteredRequest.FlightType = API_OperationService.FlightType.All;
            gmfreq.manifestFilteredRequest.InventoryLegKey = inventoryLegKey;
            gmfreq.manifestFilteredRequest.ManifestFilter = filter;
            
            GetManifestFilteredResponse mgfres = operationManager.GetManifestFiltered(gmfreq);

            ///* FILTERED */
            //gmfreq.manifestFilteredRequest.ManifestFilter = new ManifestFilter();
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludeBaggage = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludeBookingComments = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludeBookingQueues = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerAddresses = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerInfants = true;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerJourneySegmentProperties = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerJourneySSRs = true;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerSecurityInfo = false;
            //gmfreq.manifestFilteredRequest.ManifestFilter.IncludePassengerTravelDocs = false;

            //mgfres = null;
            //mgfres = operationManager.GetManifestFiltered(gmfreq);
        }

        #endregion

        #region nskCreateVouchers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="voucherBasisCode"></param>
        /// <returns></returns>
        public CreateVouchersResponse nskCreateVouchers(string sessionSignature, string voucherBasisCode)
        {
            CreateVouchersRequest cvr = new CreateVouchersRequest();
            cvr.ContractVersion = _contractVersion;
            cvr.MessageContractVersion = _messageContractVersion;
            cvr.EnableExceptionStackTrace = false;
            cvr.Signature = sessionSignature;
            cvr.voucherIssuanceBatchReq = new API_VoucherService.VoucherIssuanceBatchRequest();
            cvr.voucherIssuanceBatchReq.VoucherBasisCode = voucherBasisCode;
            cvr.voucherIssuanceBatchReq.LastName = "Hansen";
            cvr.voucherIssuanceBatchReq.FirstName = "Matthias";
            cvr.voucherIssuanceBatchReq.IssuanceReasonCode = "TEST";
            cvr.voucherIssuanceBatchReq.CurrencyCode = "EUR";
            cvr.voucherIssuanceBatchReq.Note = "Test voucher created via the API";
            cvr.voucherIssuanceBatchReq.State = API_VoucherService.MessageState.New;

            return voucherManager.CreateVouchers(cvr);
        }
        #endregion

        #region nskBuildAU
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <returns></returns>
        public BuildAUResponse nskBuildAU(string sessionSignature)
        {
            BuildAURequest buildAU = new BuildAURequest();
            buildAU.Signature = sessionSignature;
            buildAU.ContractVersion = _contractVersion;
            buildAU.buildAUReqData = new BuildAURequestData();
            buildAU.buildAUReqData.BuildAUType = "L"; // L(eg) or R(oute)
            buildAU.buildAUReqData.StartDate = new DateTime(2013, 06, 06);
            buildAU.buildAUReqData.EndDate = new DateTime(2013, 06, 06);
            buildAU.buildAUReqData.DOW = API_ScheduleService.DOW.Daily;
            buildAU.buildAUReqData.SendAVSMessages = false;
            
            List<BuildAUFlightSpecification> flightSpecs = new List<BuildAUFlightSpecification>();
            BuildAUFlightSpecification flightSpec = new BuildAUFlightSpecification();
            flightSpec.DepartureStation = "AMS";
            flightSpec.ArrivalStation = "BCN";
            flightSpec.FlightDesignator = new API_ScheduleService.FlightDesignator();
            flightSpec.FlightDesignator.CarrierCode = "HV";
            flightSpec.FlightDesignator.FlightNumber = "5131";
            flightSpec.FlightDesignator.OpSuffix = " ";
            flightSpecs.Add(flightSpec);
            buildAU.buildAUReqData.BuildAUFlightSpecifications = flightSpecs.ToArray();

            List<BuildAULegSpecification> legSpecs = new List<BuildAULegSpecification>();
            BuildAULegSpecification legSpec = new BuildAULegSpecification();
            legSpec.BuildAULegStatus = BuildAULegStatus.Default;
            legSpec.LegAdjustedCapacity = 149;
            legSpec.LegLid = 149;
            legSpecs.Add(legSpec);
            buildAU.buildAUReqData.BuildAULegSpecifications = legSpecs.ToArray();

            List<BuildAUNestSpecification> nestSpecs = new List<BuildAUNestSpecification>();
            BuildAUNestSpecification nestSpec = new BuildAUNestSpecification();
            nestSpec.ClassNest = 1;
            nestSpec.NestAdjustedCapacity = 149;
            nestSpec.NestLid = 149;
            nestSpecs.Add(nestSpec);
            buildAU.buildAUReqData.BuildAUNestSpecifications = nestSpecs.ToArray();

            return scheduleManager.BuildAU(buildAU);
        }
        #endregion

        #region nskUpdateFeeStatus
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <param name="feeNumber"></param>
        /// <param name="passengerNumber"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        public UpdateFeeStatusResponse nskUpdateFeeStatus(string sessionSignature, string recordLocator, short feeNumber, short passengerNumber, string notes)
        {
            UpdateFeeStatusRequest1 updateFeeStatusRequest = new UpdateFeeStatusRequest1();
            updateFeeStatusRequest.ContractVersion = _contractVersion;
            updateFeeStatusRequest.MessageContractVersion = _messageContractVersion;
            updateFeeStatusRequest.EnableExceptionStackTrace = false;
            updateFeeStatusRequest.Signature = sessionSignature;
            updateFeeStatusRequest.UpdateFeeStatusRequest = new UpdateFeeStatusRequest();
            updateFeeStatusRequest.UpdateFeeStatusRequest.UpdateFeeStatusReqData = new UpdateFeeStatusRequestData();
            updateFeeStatusRequest.UpdateFeeStatusRequest.UpdateFeeStatusReqData.RecordLocator = recordLocator;

            List<FeeStatusRequest> feeStatusRequests = new List<FeeStatusRequest>();
            FeeStatusRequest feeStatusRequest = new FeeStatusRequest();
            feeStatusRequest.FeeNumber = feeNumber;
            feeStatusRequest.PassengerNumber = passengerNumber;
            feeStatusRequest.State = API_BookingService.MessageState.Modified;
            feeStatusRequest.Notes = notes;
            feeStatusRequests.Add(feeStatusRequest);

            updateFeeStatusRequest.UpdateFeeStatusRequest.UpdateFeeStatusReqData.FeeStatusRequests = feeStatusRequests.ToArray();


            return bookingManager.UpdateFeeStatus(updateFeeStatusRequest);
        }
        #endregion

        #region nskGetPerson
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="customerNumber"></param>
        /// <returns></returns>
        public GetPersonResponse nskGetPerson(string sessionSignature, string customerNumber)
        {
            GetPersonRequest getPersonRequest = new GetPersonRequest();
            getPersonRequest.ContractVersion = _contractVersion;
            getPersonRequest.MessageContractVersion = _messageContractVersion;
            getPersonRequest.EnableExceptionStackTrace = false;
            getPersonRequest.Signature = sessionSignature;
            getPersonRequest.GetPersonRequestData = new GetPersonRequestData();
            getPersonRequest.GetPersonRequestData.GetDetails = false;
            getPersonRequest.GetPersonRequestData.GetPersonBy = GetPersonBy.GetPersonByCustomerNumber;
            getPersonRequest.GetPersonRequestData.GetPersonByCustomerNumber = new GetPersonByCustomerNumber();
            getPersonRequest.GetPersonRequestData.GetPersonByCustomerNumber.CustomerNumber = customerNumber;
            getPersonRequest.GetPersonRequestData.GetPersonByCustomerNumber.WithInfoData = true;

            return personManager.GetPerson(getPersonRequest);
        }
        #endregion

        #region --- ACCOUNTMANAGER METHODS ---

        #region nskGetAvailableCreditByReference
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="accountReference"></param>
        /// <returns></returns>
        public GetAvailableCreditByReferenceResponse nskGetAvailableCreditByReference(string sessionSignature, string accountReference)
        {
            GetAvailableCreditByReferenceRequest getAvailCredit = new GetAvailableCreditByReferenceRequest();
            getAvailCredit.ContractVersion = _contractVersion;
            getAvailCredit.MessageContractVersion = _messageContractVersion;
            getAvailCredit.EnableExceptionStackTrace = false;
            getAvailCredit.Signature = sessionSignature;
            getAvailCredit.GetAvailableCreditByReferenceReqData = new GetAvailableCreditByReferenceRequestData();
            getAvailCredit.GetAvailableCreditByReferenceReqData.AccountReference = accountReference;

            return accountManager.GetAvailableCreditByReference(getAvailCredit);
        }
        #endregion

        #endregion --- ACCOUNTMANAGER METHODS ---

        #region --- QUEUEMANAGER METHODS ---

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="recordLocator"></param>
        /// <param name="queueCode"></param>
        /// <returns></returns>
        public CommitBookingQueueResponse nskCommitBookingQueue(string sessionSignature, string recordLocator, string queueCode)
        {
            CommitBookingQueueRequest commitBookingQueue = new CommitBookingQueueRequest();
            commitBookingQueue.Signature = sessionSignature;
            commitBookingQueue.MessageContractVersion = _messageContractVersion;
            commitBookingQueue.EnableExceptionStackTrace = false;
            commitBookingQueue.ContractVersion = _contractVersion;
            commitBookingQueue.BookingQueue = new BookingQueue();
            commitBookingQueue.BookingQueue.RecordLocator = recordLocator;
            commitBookingQueue.BookingQueue.QueueCode = queueCode;
            commitBookingQueue.BookingQueue.PriorityDate = DateTime.Now;

            return queueManager.CommitBookingQueue(commitBookingQueue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionSignature"></param>
        /// <param name="bookingID"></param>
        /// <param name="queueCode"></param>
        /// <returns></returns>
        public DeleteBookingFromQueueResponse nskDeleteBookingFromQueue(string sessionSignature, long bookingID, string queueCode)
        {
            DeleteBookingFromQueueRequest deleteBookingFromQueue = new DeleteBookingFromQueueRequest();
            deleteBookingFromQueue.Signature = sessionSignature;
            deleteBookingFromQueue.MessageContractVersion = _messageContractVersion;
            deleteBookingFromQueue.EnableExceptionStackTrace = false;
            deleteBookingFromQueue.ContractVersion = _contractVersion;
            deleteBookingFromQueue.BookingQueueRequestData = new BookingQueueRequest();
            deleteBookingFromQueue.BookingQueueRequestData.BookingID = bookingID;
            deleteBookingFromQueue.BookingQueueRequestData.QueueCode = queueCode;

            return queueManager.DeleteBookingFromQueue(deleteBookingFromQueue);
        }

        #endregion --- QUEUEMANAGER METHODS ---
    }
}
