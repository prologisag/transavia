using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Windows.Forms;
using Buchung4U.API_SessionService;
using Buchung4U.API_BookingService;
using Buchung4U.API_OperationService;
using Buchung4U.API_ContentService;
using Buchung4U.API_VoucherService;
using Buchung4U.API_ScheduleService;
using Buchung4U.API_PersonService;
using Buchung4U.API_AccountService;

namespace Buchung4U
{
    public partial class Buchung3 : Form
    {
        #region Declaration
        private string _sessionSignature;
        private string nskAgentName = "";
        private string nskPassword = "";
        private string nskDomainCode = "";
        private string nskDepartureStation = "";
        private string nskArrivalStation = "";
        private int selectedOutboundFlightIndex = -1;
        private int selectedInboundFlightIndex = -1;
        private string currencyCode;
        private decimal balanceDueToPay;
        bool returnFlight = false;
        ISessionManager sessionManager;
        IBookingManager bookingManager;
        IOperationManager operationManager;
        IContentManager contentManager;
        IVoucherManager voucherManager;
        IScheduleManager scheduleManager;
        IPersonManager personManager;
        IAccountManager accountManager;
        GetAvailabilityResponse getAvRes;
        PriceItineraryResponse pricedItinerary;
        SellResponse soldFlight;
        AddPaymentToBookingResponse addedPayment;
        NewSkies3 newSkies3;
        private Journey[] tmpJourneys;
        #endregion

        #region Constructor
        public Buchung3()
        {
            InitializeComponent();
            nskAgentName = ConfigurationManager.AppSettings["agentName"];
            nskPassword = ConfigurationManager.AppSettings["password"];
            nskDomainCode = ConfigurationManager.AppSettings["domainCode"];
            nskDepartureStation = ConfigurationManager.AppSettings["defaultDepartureStation"];
            nskArrivalStation = ConfigurationManager.AppSettings["defaultArrivalStation"];
            sessionManager = new SessionManagerClient();
            bookingManager = new BookingManagerClient();
            operationManager = new OperationManagerClient();
            contentManager = new ContentManagerClient();
            voucherManager = new VoucherManagerClient();
            scheduleManager = new ScheduleManagerClient();
            personManager = new PersonManagerClient();
            accountManager = new AccountManagerClient();
            newSkies3 = new NewSkies3();
            txtAgentName.Text = nskAgentName;
            txtPassword.Text = nskPassword;
            txtDepartureStation.Text = nskDepartureStation;
            txtArrivalStation.Text = nskArrivalStation;
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
            dateOutbound.Value = DateTime.Now.AddDays(1);
            dateInbound.Value = DateTime.Now.AddDays(8);
        }
        #endregion

        #region Method: Write to Output
        public void writeToOutput(string output, bool error)
        {
            txtOutputWindow.AppendText(output);
            txtOutputWindow.AppendText(System.Environment.NewLine);
            txtOutputWindow.SelectionStart = txtOutputWindow.Text.Length;
            txtOutputWindow.ScrollToCaret();
            if (error)
            {
                tabAPIMethods.SelectTab(tabAusgabefenster);
            }
        }
        #endregion

        #region Logon / Logout
        private void btnLogon_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnLogon.Text == "Logout")
                {
                    LogoutRequest loreq = new LogoutRequest();
                    loreq.Signature = _sessionSignature;
                    LogoutResponse lores = sessionManager.Logout(loreq);

                    lblAgentName.Visible = true;
                    lblPassword.Visible = true;
                    txtAgentName.Visible = true;
                    txtPassword.Visible = true;
                    lblLoggedIn.Text = "";
                    lblLoggedIn.Visible = false;
                    btnLogon.Text = "Logon";
                    btnNavGetAvailability.Enabled = false;
                    btnNavFindBooking.Enabled = false;
                    btnNavAXSTest.Enabled = false;
                    btnNavClear.Enabled = false;
                    tabAPIMethods.TabPages.Clear();
                    tabAPIMethods.TabPages.Add(tabAusgabefenster);
                }
                else
                {
                    LogonResponse lres = newSkies3.nskLogon(this.txtAgentName.Text, this.txtPassword.Text, nskDomainCode);

                    

                    if ((lres != null) && (lres.Signature != null))
                    {
                        _sessionSignature = lres.Signature;
                        
                        lblAgentName.Visible = false;
                        lblPassword.Visible = false;
                        txtAgentName.Visible = false;
                        //txtPassword.Visible = false;
                        lblLoggedIn.Text = "You're logged in now";
                        lblLoggedIn.Visible = true;
                        btnLogon.Text = "Logout";
                        btnNavGetAvailability.Enabled = true;
                        btnNavFindBooking.Enabled = true;
                        btnNavAXSTest.Enabled = true;
                        btnNavClear.Enabled = true;
                        tabAPIMethods.TabPages.Clear();
                        tabAPIMethods.TabPages.Add(tabGetAvailability);
                        tabAPIMethods.TabPages.Add(tabAusgabefenster);
                        tabAPIMethods.TabPages.Add(tabAssignSeat);

                        #region TEST: GetAccountBalance
                        //GetAvailableCreditByReferenceResponse accountDetails = newSkies3.nskGetAvailableCreditByReference(_sessionSignature, "TRFS");

                        #endregion

                        #region TEST: UnAssignSeat
                        //GetBookingResponse gbres = newSkies3.nskGetBooking(_sessionSignature, "ZDT6TX");
                        //UnassignSeatsResponse unassignSeat = newSkies3.nskUnAssignSeats(_sessionSignature, gbres.Booking);
                        //GetBookingFromStateResponse updatedBooking = newSkies3.nskGetBookingFromState(_sessionSignature);
                        //writeToOutput("test", false);
                        #endregion

                        #region TEST: GetPerson
                        //GetPersonResponse personDetails = newSkies3.nskGetPerson(_sessionSignature, "6590000044");
                        #endregion

                        #region TEST: BuildAu
                        //BuildAUResponse updatedAU = newSkies3.nskBuildAU(_sessionSignature);
                        #endregion

                        #region CheckInPassenger
                        //string testPNR = "EFZCXB";
                        //GetBookingResponse gbres = newSkies3.nskGetBooking(_sessionSignature, testPNR);

                        //Buchung4U.API_OperationService.InventoryLegKey inventoryLegKey = new API_OperationService.InventoryLegKey();
                        //inventoryLegKey.ArrivalStation = gbres.Booking.Journeys[0].Segments[0].Legs[0].ArrivalStation;
                        //inventoryLegKey.CarrierCode = gbres.Booking.Journeys[0].Segments[0].Legs[0].FlightDesignator.CarrierCode;
                        //inventoryLegKey.DepartureDate = gbres.Booking.Journeys[0].Segments[0].Legs[0].STD;
                        //inventoryLegKey.DepartureStation = gbres.Booking.Journeys[0].Segments[0].Legs[0].DepartureStation;
                        //inventoryLegKey.FlightNumber = gbres.Booking.Journeys[0].Segments[0].Legs[0].FlightDesignator.FlightNumber;
                        //inventoryLegKey.OpSuffix = gbres.Booking.Journeys[0].Segments[0].Legs[0].FlightDesignator.OpSuffix;

                        //for (int i = 0; i < gbres.Booking.Passengers.Length; i++)
                        //{
                        //    CheckInPassengerResponse checkInPassengerResponse = newSkies3.nskCheckin(_sessionSignature,
                        //                           gbres.Booking.RecordLocator,
                        //                           inventoryLegKey,
                        //                           gbres.Booking.Passengers[i].Names[0].Title,
                        //                           gbres.Booking.Passengers[i].Names[0].FirstName,
                        //                           gbres.Booking.Passengers[i].Names[0].LastName,
                        //                           Buchung4U.API_OperationService.LiftStatus.CheckedIn);

                        //    writeToOutput("Checkin: " + checkInPassengerResponse.checkInResponse.ReturnFlightDesignator.CarrierCode +
                        //    checkInPassengerResponse.checkInResponse.ReturnFlightDesignator.FlightNumber +
                        //    " (" + gbres.Booking.Passengers[i].Names[0].Title + " " +
                        //    gbres.Booking.Passengers[i].Names[0].FirstName + " " +
                        //    gbres.Booking.Passengers[i].Names[0].LastName + ")", false);
                        //}
                        #endregion

                        #region TEST: GetPaymentFeePrice
                        //BuildAUResponse updatedAU = newSkies3.nskBuildAU(_sessionSignature);
                        //writeToOutput("BuildAU: " + updatedAU.BuildAUResponseData.SuccessfulUpdateCount.ToString(), false);
                        #endregion

                        #region TEST: Create Vouchers
                        //CreateVouchersResponse vouchers = newSkies3.nskCreateVouchers(_sessionSignature, "MHTST1");
                        #endregion

                        #region TEST: getBookingPayments
                        //string testPNR = "CB227K";
                        //GetBookingPaymentsResponse getBookingPayments = newSkies3.nskGetBookingPayments(_sessionSignature, testPNR);
                        //writeToOutput("PNR: " + testPNR, false);
                        //for (int i = 0; i < getBookingPayments.getBookingPaymentRespData.Payments.Length; i++)
                        //{
                        //    writeToOutput("Status " + i.ToString() + " " + getBookingPayments.getBookingPaymentRespData.Payments[i].Status.ToString(), false);
                        //    writeToOutput("AuthorizationStatus " + i.ToString() + " " + getBookingPayments.getBookingPaymentRespData.Payments[i].AuthorizationStatus.ToString(), false);
                        //}
                        #endregion

                        #region TEST: dividePassenger
                        //DivideResponse divideResponse = newSkies3.nskDivide(_sessionSignature, "P8ZRYZ", 1);
                        //writeToOutput("Divide: " + divideResponse.BookingUpdateResponseData.Success.RecordLocator, false);
                        #endregion

                        #region TEST: cancel
                        //CommitResponse commitResponse = newSkies3.nskCancel(_sessionSignature, "S9KTGT", CancelBy.Journey);
                        //writeToOutput("Cancel: " + commitResponse.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString("N2"), false);
                        #endregion

                        #region TEST: getSeatAvailability
                        //GetSeatAvailabilityResponse getSeatAvailability = newSkies3.nskGetSeatAvailability(_sessionSignature, "S9KTGT");
                        //writeToOutput("GetSeatAvailability: " + getSeatAvailability.SeatAvailabilityResponse.Legs.Length.ToString(), false);
                        #endregion
                    }
                    else
                    {
                        writeToOutput("Error Logon: Something went wrong with the logon", true);
                    }
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error Logon: " + ex.Message, true);
                tabAPIMethods.SelectTab(tabAusgabefenster);
            }
        }
        #endregion

        #region Navigation Links
        #region Nav: Get Availability
        private void btnGetAvailability_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabGetAvailability);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: Price Itinerary
        private void btnPriceItinerary_Click(object sender, EventArgs e)
        {
            // Check if flights have been selected on Availability Tab
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabPriceItinerary);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: Sell
        private void btnSell_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabSell);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: Add Payment
        private void btnAddPayment_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabAddPayment);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: Commit
        private void btnCommit_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabCommit);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: GetBookingFromState
        private void btnNavGetBookingFromState_Click(object sender, EventArgs e)
        {
            GetBookingFromStateResponse currentBookingState = newSkies3.nskGetBookingFromState(_sessionSignature);
        }
        #endregion

        #region Nav: Get Seat Availability
        private void btnGetSeatAvailability_Click(object sender, EventArgs e)
        {
            GetSeatAvailabilityResponse seatAvailability = newSkies3.nskGetSeatAvailability(_sessionSignature, "C7CKKW");

            if ((seatAvailability != null) && (seatAvailability.SeatAvailabilityResponse != null))
            {
                writeToOutput("Seat availability: " + seatAvailability.SeatAvailabilityResponse.Legs.Length.ToString() + " legs.", false);
            }
            else
            {
                writeToOutput("Error Get Seat Availability: Something went wrong", true);
            }
        }
        #endregion

        #region (Nav:) Clear
        private void btnClear_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: Find Booking
        private void btnNavFindBooking_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabFindBooking);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion

        #region Nav: AXS Test
        private void btnNavAXSTest_Click(object sender, EventArgs e)
        {
            tabAPIMethods.TabPages.Clear();
            tabAPIMethods.TabPages.Add(tabAXSTest);
            tabAPIMethods.TabPages.Add(tabAusgabefenster);
        }
        #endregion
        #endregion

        #region Switch between Oneway and Return flights
        private void radioSwitchFlightType_CheckedChanged(object sender, EventArgs e)
        {
            if (radioOneway.Checked)
            {
                returnFlight = false;
                lblInbound.Enabled = false;
                dateInbound.Enabled = false;
                lblSelectedInboundFlight.Enabled = false;
                dataGridViewAvailabilityInbound.Enabled = false;
                lblFareRuleInbound.Enabled = false;
                lblDetailSelectedFareRuleInbound.Enabled = false;
                richTxtFareRuleInbound.Enabled = false;
            }
            if (radioReturn.Checked)
            {
                returnFlight = true;
                lblInbound.Enabled = true;
                dateInbound.Enabled = true;
                lblSelectedInboundFlight.Enabled = true;
                dataGridViewAvailabilityInbound.Enabled = true;
                lblFareRuleInbound.Enabled = true;
                lblDetailSelectedFareRuleInbound.Enabled = true;
                richTxtFareRuleInbound.Enabled = true;
            }
        }
        #endregion

        #region API: Get Availability
        private void btnGetAvailabilityAPI_Click(object sender, EventArgs e)
        {
            try
            {
                bool returnFlight = true;
                if (radioOneway.Checked)
                    returnFlight = false;
                lblDetailSelectedOutboundFlight.Text = string.Empty;
                lblDetailSelectedInboundFlight.Text = string.Empty;
                lblDetailSelectedFareRuleOutbound.Text = string.Empty;
                lblDetailSelectedFareRuleInbound.Text = string.Empty;
                richTxtFareRuleOutbound.Rtf = string.Empty;
                richTxtFareRuleInbound.Rtf = string.Empty;
                selectedOutboundFlightIndex = -1;
                selectedInboundFlightIndex = -1;
                getAvRes = newSkies3.nskGetAvailability(_sessionSignature, returnFlight, txtDepartureStation.Text, txtArrivalStation.Text, dateOutbound.Value, dateInbound.Value, (int)numericAdults.Value, (int)numericChilds.Value, (int)numericInfants.Value, "EUR", txtOrganizationCode.Text, txtProductClass.Text, txtPromotionCode.Text);

                if ((getAvRes != null) && (getAvRes.GetTripAvailabilityResponse != null))
                {
                    currencyCode = getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[0].Segments[0].Fares[0].PaxFares[0].ServiceCharges[0].CurrencyCode;
                    dataGridViewAvailabilityOutbound.Rows.Clear();
                    dataGridViewAvailabilityOutbound.Columns.Clear();
                    dataGridViewAvailabilityInbound.Rows.Clear();
                    dataGridViewAvailabilityInbound.Columns.Clear();

                    dataGridViewAvailabilityOutbound.ColumnCount = 4;
                    dataGridViewAvailabilityOutbound.Columns[0].Name = "Flug";
                    dataGridViewAvailabilityOutbound.Columns[1].Name = "Zeiten";
                    dataGridViewAvailabilityOutbound.Columns[2].Name = "Datum";
                    dataGridViewAvailabilityOutbound.Columns[3].Name = "Route";

                    dataGridViewAvailabilityInbound.ColumnCount = 4;
                    dataGridViewAvailabilityInbound.Columns[0].Name = "Flug";
                    dataGridViewAvailabilityInbound.Columns[1].Name = "Zeiten";
                    dataGridViewAvailabilityInbound.Columns[2].Name = "Datum";
                    dataGridViewAvailabilityInbound.Columns[3].Name = "Route";

                    for (int j = 0; j < getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys.Length; j++)
                    {
                        dataGridViewAvailabilityOutbound.Rows.Add(
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[j].Segments[0].FlightDesignator.CarrierCode + 
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[j].Segments[0].FlightDesignator.FlightNumber.Trim(),
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[j].Segments[0].STD.ToShortTimeString() + "-" +
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[j].Segments[0].STA.ToShortTimeString(),
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].DepartureDate.ToShortDateString(),
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].DepartureStation + "-" +
                            getAvRes.GetTripAvailabilityResponse.Schedules[0][0].ArrivalStation);
                    }
                
                    if (getAvRes.GetTripAvailabilityResponse.Schedules.Length > 1)
                    {
                        for (int j = 0; j < getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys.Length; j++)
                        {
                            dataGridViewAvailabilityInbound.Rows.Add(
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[j].Segments[0].FlightDesignator.CarrierCode +
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[j].Segments[0].FlightDesignator.FlightNumber.Trim(),
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[j].Segments[0].STD.ToShortTimeString() + "-" +
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[j].Segments[0].STA.ToShortTimeString(),
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].DepartureDate.ToShortDateString(),
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].DepartureStation + "-" +
                                getAvRes.GetTripAvailabilityResponse.Schedules[1][0].ArrivalStation);
                        }
                    }
                }

                btnNavPriceItinerary.Enabled = true;
                btnNavSell.Enabled = true;
                btnPriceItinerary_Click(sender, e);
            }
            catch (Exception ex)
            {
                writeToOutput("Error GetAvailability: " + ex.Message, true);
            }
        }
        #endregion

        #region API: Price Itinerary
        private void btnAPIPriceItinerary_Click(object sender, EventArgs e)
        {
            try
            {
                BindingList<Journey> journeysToPrice = new BindingList<Journey>();
                if (selectedOutboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[selectedOutboundFlightIndex]);
                }
                if (selectedInboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[selectedInboundFlightIndex]);
                }
                tmpJourneys = new Journey[journeysToPrice.Count];
                journeysToPrice.CopyTo(tmpJourneys, 0);
                journeysToPrice.Clear();

                pricedItinerary = newSkies3.nskPriceItinerary(_sessionSignature, currencyCode, tmpJourneys, PriceItineraryBy.JourneyWithLegs, txtOrganizationCode.Text);

                if((pricedItinerary != null) && (pricedItinerary.Booking != null))
                {
                    writeToOutput("PriceItinerary: TotalCost " + pricedItinerary.Booking.BookingSum.TotalCost.ToString("N2"),false);
                    for (int i = 0; i < pricedItinerary.Booking.Journeys.Length; i++)
                    {
                        for (int j = 0; j < pricedItinerary.Booking.Journeys[i].Segments.Length; j++)
                        {
                            for (int k = 0; k < pricedItinerary.Booking.Journeys[i].Segments[j].Fares.Length; k++)
                            {
                                for (int l = 0; l < pricedItinerary.Booking.Journeys[i].Segments[j].Fares[k].PaxFares.Length; l++)
                                {
                                    for (int m = 0; m < pricedItinerary.Booking.Journeys[i].Segments[j].Fares[k].PaxFares[l].ServiceCharges.Length; m++)
                                    {
                                        writeToOutput("[" + i.ToString() + "][" + j.ToString() + "] " + 
                                            pricedItinerary.Booking.Journeys[i].Segments[j].Fares[k].PaxFares[l].ServiceCharges[m].ChargeType.ToString() + ": " +
                                            pricedItinerary.Booking.Journeys[i].Segments[j].Fares[k].PaxFares[l].ServiceCharges[m].Amount.ToString("N2") + " " +
                                            pricedItinerary.Booking.Journeys[i].Segments[j].Fares[k].PaxFares[l].ServiceCharges[m].CurrencyCode, false);
                                    }
                                }
                            }
                        }
                    }
                    btnNavSell.Enabled = true;
                }
                else
                {
                    writeToOutput("Error PriceItinerary: Something went wrong", true);
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error PriceItinerary: " + ex.Message, true);
            }
        }
        #endregion

        #region API: Sell Flight
        private void btnAPISellFlight_Click(object sender, EventArgs e)
        {
            try
            {
                BindingList<Journey> journeysToPrice = new BindingList<Journey>();
                if (selectedOutboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[selectedOutboundFlightIndex]);
                }
                if (selectedInboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[selectedInboundFlightIndex]);
                }
                tmpJourneys = new Journey[journeysToPrice.Count];
                journeysToPrice.CopyTo(tmpJourneys, 0);
                journeysToPrice.Clear();

                soldFlight = newSkies3.nskSellRequest(_sessionSignature, currencyCode , tmpJourneys, SellBy.Journey, txtOrganizationCode.Text);

                if ((soldFlight != null) && (soldFlight.BookingUpdateResponseData != null))
                {
                    if (soldFlight.BookingUpdateResponseData.Success != null)
                    {
                        balanceDueToPay = soldFlight.BookingUpdateResponseData.Success.PNRAmount.BalanceDue;
                        writeToOutput("SellRequest successfully", false);
                        writeToOutput("Total Amount: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.TotalCost.ToString("N2"), false);
                        writeToOutput("Balance Due: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString("N2"), false);
                        writeToOutput("Segment Count: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.SegmentCount.ToString(), false);
                    }
                    btnNavAddPayment.Enabled = true;
                    if (soldFlight.BookingUpdateResponseData.Success.PNRAmount.BalanceDue == Convert.ToDecimal("0"))
                    {
                        btnNavCommit.Enabled = true;
                    }
                }
                else
                {
                    writeToOutput("Error SellRequest: Something went wrong", true);
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error SellFlight: " + ex.Message, true);
            }


            //GetPaymentFeePriceResponse gpres = newSkies3.nskGetPaymentFeePrice(_sessionSignature, "GBP", "MC", Convert.ToDecimal("200.00"));
        }
        #endregion

        #region API: GetPaymentFee
        private void btnAPIGetPaymentFee_Clidk(object sender, EventArgs e)
        {

        }
        #endregion

        #region API: Add Payment
        private void btnAPIAddPayment_Click(object sender, EventArgs e)
        {
            try
            {
                AddPaymentToBookingResponse addedPayment = newSkies3.nskAddPaymentToBooking(_sessionSignature, "MC", currencyCode, balanceDueToPay);

                if ((addedPayment != null) && (addedPayment.BookingPaymentResponse != null))
                {
                    writeToOutput("Payment hinzugefügt: " + addedPayment.BookingPaymentResponse.ValidationPayment.Payment.AuthorizationStatus.ToString(), false);
                }
                else
                {
                    writeToOutput("Error Add Payment: Something went wrong", true);
                }
                btnNavCommit.Enabled = true;
            }
            catch (Exception ex)
            {
                writeToOutput("Error AddPayment: " + ex.Message, true);
            }
        }
        #endregion

        #region API: Sell SSRs
        private void btnAPISellSSR_Click(object sender, EventArgs e)
        {
            try
            {
                BindingList<Journey> journeysToPrice = new BindingList<Journey>();
                if (selectedOutboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[selectedOutboundFlightIndex]);
                }
                if (selectedInboundFlightIndex != -1)
                {
                    journeysToPrice.Add(getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[selectedInboundFlightIndex]);
                }
                tmpJourneys = new Journey[journeysToPrice.Count];
                journeysToPrice.CopyTo(tmpJourneys, 0);
                journeysToPrice.Clear();

                soldFlight = newSkies3.nskSellSSR(_sessionSignature, currencyCode, tmpJourneys, "BLND");

                if ((soldFlight != null) && (soldFlight.BookingUpdateResponseData != null))
                {
                    if (soldFlight.BookingUpdateResponseData.Success != null)
                    {
                        balanceDueToPay = soldFlight.BookingUpdateResponseData.Success.PNRAmount.BalanceDue;
                        writeToOutput("SellSSRRequest successfully", false);
                        writeToOutput("Total Amount: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.TotalCost.ToString("N2"), false);
                        writeToOutput("Balance Due: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString("N2"), false);
                        writeToOutput("Segment Count: " + soldFlight.BookingUpdateResponseData.Success.PNRAmount.SegmentCount.ToString(), false);
                    }
                    btnNavCommit.Enabled = true;
                }
                else
                {
                    writeToOutput("Error SellSSRRequest: Something went wrong", true);
                }

                #region Sell INF SSR if applicable
                if ((int)numericInfants.Value > 0)
                {
                    SellResponse soldInfantSSR = newSkies3.nskSellSSR(_sessionSignature, currencyCode, tmpJourneys, "INF");
                    if ((soldInfantSSR != null) && (soldInfantSSR.BookingUpdateResponseData != null))
                    {
                        if (soldInfantSSR.BookingUpdateResponseData.Success != null)
                        {
                            balanceDueToPay = soldInfantSSR.BookingUpdateResponseData.Success.PNRAmount.BalanceDue;
                            writeToOutput("SellSSRRequest successfully", false);
                            writeToOutput("Total Amount: " + soldInfantSSR.BookingUpdateResponseData.Success.PNRAmount.TotalCost.ToString("N2"), false);
                            writeToOutput("Balance Due: " + soldInfantSSR.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString("N2"), false);
                            writeToOutput("Segment Count: " + soldInfantSSR.BookingUpdateResponseData.Success.PNRAmount.SegmentCount.ToString(), false);
                        }
                        btnNavCommit.Enabled = true;
                    }
                    else
                    {
                        writeToOutput("Error SellSSRRequest: Something went wrong", true);
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                writeToOutput("Error SellFlight: " + ex.Message, true);
            }
        }
        #endregion

        #region API: Commit
        private void btnAPICommit_Click(object sender, EventArgs e)
        {
            BookingCommitResponse bookingCommitResponse = newSkies3.nskBookingCommit(_sessionSignature, currencyCode, txtOrganizationCode.Text, string.Empty);

            if ((bookingCommitResponse != null) && (bookingCommitResponse.BookingUpdateResponseData != null))
            {
                writeToOutput("Buchung erstellt: " + bookingCommitResponse.BookingUpdateResponseData.Success.RecordLocator, false);
            }
            else
            {
                writeToOutput("Error Commit: Something went wrong", true);
            }
        }
        #endregion

        #region API: Find Booking
        private void btnAPIFindBooking_Click(object sender, EventArgs e)
        {
            try
            {
                FindBookingResponse fbres = newSkies3.nskFindBooking(_sessionSignature, txtFindByDetail.Text);

                if ((fbres != null) && (fbres.FindBookingRespData != null))
                {
                    dataGridViewFindBooking.Rows.Clear();
                    dataGridViewFindBooking.Columns.Clear();
                    writeToOutput("FindBooking: " + fbres.FindBookingRespData.Records.ToString() + " Records found.", false);

                    dataGridViewFindBooking.ColumnCount = 4;

                    dataGridViewFindBooking.Columns[0].Name = "PNR";
                    dataGridViewFindBooking.Columns[1].Name = "Status";
                    dataGridViewFindBooking.Columns[2].Name = "Abflugdatum";
                    dataGridViewFindBooking.Columns[3].Name = "Passagiername";

                    for (int i = 0; i < fbres.FindBookingRespData.FindBookingDataList.Length; i++)
                    {
                        dataGridViewFindBooking.Rows.Add(fbres.FindBookingRespData.FindBookingDataList[i].RecordLocator,
                            fbres.FindBookingRespData.FindBookingDataList[i].BookingStatus,
                            fbres.FindBookingRespData.FindBookingDataList[i].FlightDate.ToShortDateString(),
                            fbres.FindBookingRespData.FindBookingDataList[i].Name.Title + " " + fbres.FindBookingRespData.FindBookingDataList[i].Name.FirstName + " " + fbres.FindBookingRespData.FindBookingDataList[i].Name.LastName);
                    }
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error FindBooking: " + ex.Message, true);
            }
        }
        #endregion

        #region GetBooking / Assign Seats / CheckIn
        private void dataGridViewFindBooking_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                GetBookingResponse gbres = newSkies3.nskGetBooking(_sessionSignature, dataGridViewFindBooking.Rows[e.RowIndex].Cells[0].Value.ToString());

                if ((gbres != null) && (gbres.Booking != null))
                {
                    writeToOutput("GetBooking: " + gbres.Booking.RecordLocator + " (ID: " + gbres.Booking.BookingID.ToString() + ")",false);
                    //AssignSeatsResponse asres = newSkies3.nskAssignSeats(_sessionSignature, gbres.Booking);
                    AssignSeatsResponse asres = null;
                    if ((asres != null) && (asres.BookingUpdateResponseData != null))
                    {
                        if (asres.BookingUpdateResponseData.Success != null)
                        {
                            writeToOutput("AssignSeats: Balance due of " + asres.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString(), false);

                            for (int i = 0; i < gbres.Booking.Passengers.Length; i++)
                            {
                                for (int j = 0; j < gbres.Booking.Journeys.Length; j++)
                                {
                                    for (int k = 0; k < gbres.Booking.Journeys[j].Segments.Length; k++)
                                    {
                                        for (int l = 0; l < gbres.Booking.Journeys[j].Segments[k].Legs.Length; l++)
                                        {
                                            Buchung4U.API_OperationService.InventoryLegKey inventoryLegKey = new Buchung4U.API_OperationService.InventoryLegKey();
                                            inventoryLegKey.ArrivalStation = gbres.Booking.Journeys[j].Segments[k].Legs[l].ArrivalStation;
                                            inventoryLegKey.DepartureStation = gbres.Booking.Journeys[j].Segments[k].Legs[l].DepartureStation;
                                            inventoryLegKey.CarrierCode = gbres.Booking.Journeys[j].Segments[k].Legs[l].FlightDesignator.CarrierCode;
                                            inventoryLegKey.FlightNumber = gbres.Booking.Journeys[j].Segments[k].Legs[l].FlightDesignator.FlightNumber;
                                            inventoryLegKey.OpSuffix = gbres.Booking.Journeys[j].Segments[k].Legs[l].FlightDesignator.OpSuffix;
                                            inventoryLegKey.DepartureDate = new DateTime(gbres.Booking.Journeys[j].Segments[k].Legs[l].STD.Year,
                                                gbres.Booking.Journeys[j].Segments[k].Legs[l].STD.Month,
                                                gbres.Booking.Journeys[j].Segments[k].Legs[l].STD.Day);

                                            CheckInPassengerResponse checkInPassengerResponse = newSkies3.nskCheckin(_sessionSignature,
                                               gbres.Booking.RecordLocator,
                                               inventoryLegKey,
                                               gbres.Booking.Passengers[i].Names[0].Title,
                                               gbres.Booking.Passengers[i].Names[0].FirstName,
                                               gbres.Booking.Passengers[i].Names[0].LastName,
                                               Buchung4U.API_OperationService.LiftStatus.CheckedIn);

                                            writeToOutput("Checkin: " + checkInPassengerResponse.checkInResponse.ReturnFlightDesignator.CarrierCode +
                                            checkInPassengerResponse.checkInResponse.ReturnFlightDesignator.FlightNumber + 
                                            " (" + gbres.Booking.Passengers[i].Names[0].Title + " " +
                                            gbres.Booking.Passengers[i].Names[0].FirstName + " " +
                                            gbres.Booking.Passengers[i].Names[0].LastName + ")", false);
                                        }
                                    }
                                }
                            }

                            CommitResponse commitResponse = newSkies3.nskCommit(_sessionSignature, gbres.Booking);

                            if ((commitResponse != null) && commitResponse.BookingUpdateResponseData != null)
                            {
                                if (commitResponse.BookingUpdateResponseData.Success != null)
                                {
                                    writeToOutput("Commit: Balance due of " + commitResponse.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString(), false);
                                }
                            }
                            else
                            {
                                writeToOutput("Error Commit: Something went wrong", true);
                            }
                        }
                    }
                    else
                    {
                        writeToOutput("Error AssignSeats: Something went wrong", true);
                    }
                }
                else
                {
                    writeToOutput("Error GetBooking: Something went wrong", true);
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error SeatAssign/Checkin: " + ex.Message, true);
            }
        }
        #endregion

        #region Select Flight and GetFareRuleInfo
        private void dataGridViewAvailabilityOutbound_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedOutboundFlightIndex = e.RowIndex;

            lblDetailSelectedOutboundFlight.Text = "Flug " +
                dataGridViewAvailabilityOutbound.Rows[e.RowIndex].Cells[3].Value.ToString() + " " +
                dataGridViewAvailabilityOutbound.Rows[e.RowIndex].Cells[0].Value.ToString() + " am " +
                dataGridViewAvailabilityOutbound.Rows[e.RowIndex].Cells[2].Value.ToString() + " um " +
                dataGridViewAvailabilityOutbound.Rows[e.RowIndex].Cells[1].Value.ToString();

            GetFareRuleInfoResponse getFareRule = newSkies3.nskGetFareRuleInfo(_sessionSignature, getAvRes.GetTripAvailabilityResponse.Schedules[0][0].Journeys[e.RowIndex].Segments[0]);

            Byte[] tmpByte;
            tmpByte = getFareRule.FareRuleInfo.Data;
            Encoding enc = Encoding.ASCII;

            lblDetailSelectedFareRuleOutbound.Text = getFareRule.FareRuleInfo.Name;
            richTxtFareRuleOutbound.Rtf = enc.GetString(tmpByte);
        }

        private void dataGridViewAvailabilityInbound_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedInboundFlightIndex = e.RowIndex;

            lblDetailSelectedInboundFlight.Text = "Flug " +
                dataGridViewAvailabilityInbound.Rows[e.RowIndex].Cells[3].Value.ToString() + " " +
                dataGridViewAvailabilityInbound.Rows[e.RowIndex].Cells[0].Value.ToString() + " am " +
                dataGridViewAvailabilityInbound.Rows[e.RowIndex].Cells[2].Value.ToString() + " um " +
                dataGridViewAvailabilityInbound.Rows[e.RowIndex].Cells[1].Value.ToString();

            GetFareRuleInfoResponse getFareRule = newSkies3.nskGetFareRuleInfo(_sessionSignature, getAvRes.GetTripAvailabilityResponse.Schedules[1][0].Journeys[e.RowIndex].Segments[0]);

            Byte[] tmpByte;
            tmpByte = getFareRule.FareRuleInfo.Data;
            Encoding enc = Encoding.ASCII;

            lblDetailSelectedFareRuleInbound.Text = getFareRule.FareRuleInfo.Name;
            richTxtFareRuleInbound.Rtf = enc.GetString(tmpByte);
        }
        #endregion        

        #region API: Test AXS process
        private void btnRetrievePNRforAXS_Click(object sender, EventArgs e)
        {
            #region Step 1: Retrieve PNR
            GetBookingResponse axsPNR = newSkies3.nskGetBooking(_sessionSignature, txtRetrievePNRforAXS.Text);
            #endregion

            if ((axsPNR != null) && (axsPNR.Booking != null))
            {
                writeToOutput("Total Cost: " + axsPNR.Booking.BookingSum.TotalCost.ToString(), false);
                writeToOutput("Balance Due: " + axsPNR.Booking.BookingSum.BalanceDue.ToString(), false);

                #region Step 2: Add Payment to Booking
                AddPaymentToBookingResponse addedPayment = newSkies3.nskAddPaymentToBooking(_sessionSignature, "XS", axsPNR.Booking.CurrencyCode, axsPNR.Booking.BookingSum.BalanceDue);

                if ((addedPayment != null) && (addedPayment.BookingPaymentResponse != null))
                {
                    if (addedPayment.BookingPaymentResponse.ValidationPayment.PaymentValidationErrors.Length != 0)
                    {
                        for (int i = 0; i < addedPayment.BookingPaymentResponse.ValidationPayment.PaymentValidationErrors.Length; i++)
                        {
                            writeToOutput("Error Add Payment: " + addedPayment.BookingPaymentResponse.ValidationPayment.PaymentValidationErrors[i].ErrorDescription, true);
                        }
                    }
                    else
                    {
                        writeToOutput("Payment hinzugefügt: " + addedPayment.BookingPaymentResponse.ValidationPayment.Payment.AuthorizationStatus.ToString(), false);

                        #region Step 3: Commit changes
                        CommitResponse commitedChanges = newSkies3.nskCommit(_sessionSignature, axsPNR.Booking);

                        if ((commitedChanges != null) && (commitedChanges.BookingUpdateResponseData != null))
                        {
                            writeToOutput("Changes have been saved: Balance due is now " + commitedChanges.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString(), false);
                        }
                        else
                        {
                            writeToOutput("Error Commit: Something went wrong", true);
                        }
                        #endregion
                    }
                }
                else
                {
                    writeToOutput("Error Add Payment: Something went wrong", true);
                }
                #endregion

                
            }
            else
            {
                writeToOutput("Error Retrieve Booking: " + txtRetrievePNRforAXS.Text + " could not be found.", true);
            }
        }
        #endregion

        #region API: GetBooking
        private void btnGetBooking_Click(object sender, EventArgs e)
        {
            GetBookingFromStateResponse tmpBookingStatus;

            #region Step 1: Retrieve PNR
            GetBookingResponse currentBooking = newSkies3.nskGetBooking(_sessionSignature, txtRetrievePNRforAXS.Text);
            //BookingCommitResponse add3rdparty = newSkies3.nskBookingCommit(_sessionSignature, currentBooking.Booking);
            #endregion

            #region -- extract passenger details
            // Pnr G52C8P
            foreach(Passenger pax in currentBooking.Booking.Passengers)
            {
                writeToOutput(pax.Names[0].FirstName + " " + pax.Names[0].LastName, false);
            }
            #endregion

            #region -- test to delete passenger travel document --
            //currentBooking.Booking.Passengers[0].PassengerTravelDocuments[0].State = API_BookingService.MessageState.Deleted;
            //currentBooking.Booking.Passengers[0].State = API_BookingService.MessageState.Modified;
            //UpdatePassengersResponse updatedPassenger = newSkies3.nskUpdatePassenger(_sessionSignature, currentBooking.Booking.Passengers[0]);
            //tmpBookingStatus = newSkies3.nskGetBookingFromState(_sessionSignature);
            #endregion

            #region Step 2: Cancel Journey (incl. SSRs)
            //CancelResponse cancelJourney = newSkies3.nskCancel(_sessionSignature, currentBooking.Booking.RecordLocator, CancelBy.Journey);
            //if (cancelJourney != null && cancelJourney.BookingUpdateResponseData.Success != null)
            //{
            //    writeToOutput("Cancel Response: " + cancelJourney.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString(), false);
            //}
            #endregion

            tmpBookingStatus = newSkies3.nskGetBookingFromState(_sessionSignature);
            
            #region Step 3: Sell new Segment
            //Journey oldJourney = currentBooking.Booking.Journeys[0];
            //GetAvailabilityResponse newAvailability = newSkies3.nskGetAvailability(_sessionSignature,
            //    false,
            //    oldJourney.Segments[0].DepartureStation,
            //    oldJourney.Segments[0].ArrivalStation,
            //    oldJourney.Segments[0].STD,
            //    oldJourney.Segments[0].STD,
            //    1,
            //    0,
            //    0,
            //    "EUR",
            //    "",
            //    "",
            //    "");

            //List<Journey> newJourneys = new List<Journey>();
            //newJourneys.Add(newAvailability.GetTripAvailabilityResponse.Schedules[0][0].Journeys[2]);

            //SellResponse newFlight = newSkies3.nskSellRequest(_sessionSignature,
            //    "EUR",
            //    newJourneys.ToArray(),
            //    SellBy.Journey,
            //    "");
            #endregion

            tmpBookingStatus = newSkies3.nskGetBookingFromState(_sessionSignature);

            #region Step 4: Re-sell SSRs
            //ResellSSRResponse newSSRs = newSkies3.nskReSellSSRs(_sessionSignature, 0);
            #endregion

            tmpBookingStatus = newSkies3.nskGetBookingFromState(_sessionSignature);

            #region Step 5: BookingCommit
            //BookingCommitResponse updatedPNR = newSkies3.nskBookingCommit(_sessionSignature, currentBooking.Booking);
            //GetBookingFromStateResponse currentBookingInState = newSkies3.nskGetBookingFromState(_sessionSignature);
            #endregion
        }
        #endregion

        #region API: Add CheckInBaggage
        private void btnAddBaggage_Click(object sender, EventArgs e)
        {
            try
            {
                CheckInBaggageResponse checkedInBaggage = newSkies3.AddCheckInBaggage(_sessionSignature, txtRetrievePNRforAXS.Text);

                if ((checkedInBaggage != null) && (checkedInBaggage.checkInBaggageRespData != null))
                {
                    for (int i = 0; i < checkedInBaggage.checkInBaggageRespData.BaggageInfoList.Length; i++)
                    {
                        if (checkedInBaggage.checkInBaggageRespData.BaggageInfoList[i].ErrorMessage != "")
                        {
                            writeToOutput("Error add Baggage: " + checkedInBaggage.checkInBaggageRespData.BaggageInfoList[i].ErrorMessage, true);
                        }
                        else
                        {
                            writeToOutput("Checked In Baggage: BaggageID = " + checkedInBaggage.checkInBaggageRespData.BaggageInfoList[i].BaggageID.ToString(), false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                writeToOutput("Error Add Baggage: " + ex.Message, true);
            }
        }
        #endregion


        private void btnAssignSeat_Click(object sender, EventArgs e)
        {
            GetBookingResponse currentBooking = newSkies3.nskGetBooking(_sessionSignature, txtPNRAssignSeat.Text);
            Decimal originalBalanceDue = currentBooking.Booking.BookingSum.BalanceDue;
            Decimal currentBalanceDue = 0;
            Boolean debug = true;
            Boolean overrideSeatFeeToZero = true;
            Decimal currentBalanceDueSeats = 0;

            GetBookingFromStateResponse currentBookingInState = new GetBookingFromStateResponse();

            AssignSeatsResponse seatsAssigned = new AssignSeatsResponse();
            

            try
            {
                //seatsAssigned = newSkies3.nskAssignSeats(_sessionSignature, currentBooking.Booking, txtUnitDesignator.Text);
                seatsAssigned = null;
                currentBalanceDue = seatsAssigned.BookingUpdateResponseData.Success.PNRAmount.BalanceDue;

                if (debug)
                {
                    writeToOutput(String.Format("{0,-35} {1,9:N} {2}", "Balance Due (Seat Sold):", currentBalanceDue, currentBooking.Booking.CurrencyCode), false);
                }

                
                if (currentBalanceDue != originalBalanceDue)
                {
                    if (overrideSeatFeeToZero)
                    {
                        currentBalanceDueSeats = currentBalanceDue - originalBalanceDue;
                        currentBalanceDue = originalBalanceDue;
                    }
                    else
                    {
                        currentBookingInState = newSkies3.nskGetBookingFromState(_sessionSignature);

                        for (int i = 0; i < currentBookingInState.BookingData.Passengers.Length; i++)
                        {
                            short currentPassengerNumber = currentBookingInState.BookingData.Passengers[i].PassengerNumber;
                            List<Buchung4U.API_BookingService.PassengerFee> originalPassengerFees = currentBooking.Booking.Passengers[i].PassengerFees.ToList();
                            List<Buchung4U.API_BookingService.PassengerFee> newPassengerFees = currentBookingInState.BookingData.Passengers[i].PassengerFees.ToList();
                            List<Buchung4U.API_BookingService.PassengerFee> newSSRFees = newPassengerFees.FindAll(item => item.FeeType == Buchung4U.API_BookingService.FeeType.SeatFee);

                            foreach (Buchung4U.API_BookingService.PassengerFee ssrFee in newSSRFees)
                            {
                                Buchung4U.API_BookingService.PassengerFee oldFee = getMatchingOldPassengerFee(originalPassengerFees, ssrFee);

                                Buchung4U.API_BookingService.BookingServiceCharge oldCharge = Array.Find(oldFee.ServiceCharges, item => item.ChargeType == Buchung4U.API_BookingService.ChargeType.ServiceCharge);
                                Buchung4U.API_BookingService.BookingServiceCharge newCharge = Array.Find(ssrFee.ServiceCharges, item => item.ChargeType == Buchung4U.API_BookingService.ChargeType.ServiceCharge);

                                if (oldCharge.Amount != newCharge.Amount)
                                {
                                    Decimal difference = newCharge.Amount - oldCharge.Amount;
                                    currentBalanceDueSeats += difference;
                                }
                            }
                        }
                        currentBalanceDue = currentBalanceDue - currentBalanceDueSeats;
                    }
                }
            }
            catch (Exception ex)
            {
                writeToOutput(ex.Message, true);
            }

            #region Override Seat Fee if applicable
            if (currentBalanceDueSeats != 0)
            {
                if (overrideSeatFeeToZero)
                {
                    try
                    {
                        currentBookingInState = newSkies3.nskGetBookingFromState(_sessionSignature);

                        for (int i = 0; i < currentBookingInState.BookingData.Passengers.Length; i++)
                        {
                            short currentPassengerNumber = currentBookingInState.BookingData.Passengers[i].PassengerNumber;
                            List<Buchung4U.API_BookingService.PassengerFee> newPassengerFees = currentBookingInState.BookingData.Passengers[i].PassengerFees.ToList();
                            List<Buchung4U.API_BookingService.PassengerFee> newSSRFees = newPassengerFees.FindAll(item => item.FeeType == Buchung4U.API_BookingService.FeeType.SeatFee);

                            if (newSSRFees.Count == 0)
                            {
                                writeToOutput("Error: Seat Fee Override, There is no seat fee to override", true);
                            }
                            else
                            {
                                foreach (Buchung4U.API_BookingService.PassengerFee ssrFee in newSSRFees)
                                {
                                    if (ssrFee.FlightReference.Contains("58"))
                                    {
                                        OverrideFeeResponse overrideFee = newSkies3.nskOverrideFee(_sessionSignature, "AUD",
                                                ssrFee.FeeNumber, currentPassengerNumber, 0);

                                        currentBalanceDue = overrideFee.Booking.BookingSum.BalanceDue;

                                        if (debug)
                                        {
                                            Console.WriteLine("{0,-35} {1,9:N} {2}", "Balance Due (Seat Fee Override):", currentBalanceDue, overrideFee.Booking.CurrencyCode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        writeToOutput("Error: Seat Fee Override " + ex.Message, true);
                    }
                }
                else
                {
                    try
                    {
                        for (int i = 0; i < currentBookingInState.BookingData.Passengers.Length; i++)
                        {
                            short currentPassengerNumber = currentBookingInState.BookingData.Passengers[i].PassengerNumber;
                            List<Buchung4U.API_BookingService.PassengerFee> originalPassengerFees = currentBooking.Booking.Passengers[i].PassengerFees.ToList();
                            List<Buchung4U.API_BookingService.PassengerFee> newPassengerFees = currentBookingInState.BookingData.Passengers[i].PassengerFees.ToList();
                            List<Buchung4U.API_BookingService.PassengerFee> newSSRFees = newPassengerFees.FindAll(item => item.FeeType == Buchung4U.API_BookingService.FeeType.SeatFee);

                            foreach (Buchung4U.API_BookingService.PassengerFee ssrFee in newSSRFees)
                            {
                                Buchung4U.API_BookingService.PassengerFee oldFee = getMatchingOldPassengerFee(originalPassengerFees, ssrFee);

                                Buchung4U.API_BookingService.BookingServiceCharge oldCharge = Array.Find(oldFee.ServiceCharges, item => item.ChargeType == Buchung4U.API_BookingService.ChargeType.ServiceCharge);
                                Buchung4U.API_BookingService.BookingServiceCharge newCharge = Array.Find(ssrFee.ServiceCharges, item => item.ChargeType == Buchung4U.API_BookingService.ChargeType.ServiceCharge);

                                if (oldCharge.Amount != newCharge.Amount)
                                {
                                    OverrideFeeResponse overrideFee = newSkies3.nskOverrideFee(_sessionSignature, newCharge.CurrencyCode,
                                            ssrFee.FeeNumber, currentPassengerNumber, oldCharge.Amount);

                                    currentBalanceDue = overrideFee.Booking.BookingSum.BalanceDue;

                                    if (debug)
                                    {
                                        Console.WriteLine("{0,-35} {1,9:N} {2}", "Balance Due (Seat Fee Override):", currentBalanceDue, overrideFee.Booking.CurrencyCode);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        writeToOutput("Error: Seat Fee Override " + ex.Message, true);
                    }
                }
            }
            #endregion



            //writeToOutput(seatsAssigned.BookingUpdateResponseData.Success.PNRAmount.BalanceDue.ToString(), false);

            currentBookingInState = newSkies3.nskGetBookingFromState(_sessionSignature);
        }

        private static Buchung4U.API_BookingService.PassengerFee getMatchingOldPassengerFee(List<Buchung4U.API_BookingService.PassengerFee> oldFees, Buchung4U.API_BookingService.PassengerFee newFee)
        {
            Buchung4U.API_BookingService.PassengerFee match = newFee;

            foreach (Buchung4U.API_BookingService.PassengerFee oldFee in oldFees)
            {
                if ((oldFee.FeeCode == newFee.FeeCode)
                    && (oldFee.FeeType == newFee.FeeType)
                    && (oldFee.FlightReference == newFee.FlightReference)
                    && (oldFee.SSRCode == newFee.SSRCode)
                    && (oldFee.SSRNumber == newFee.SSRNumber))
                {
                    match = oldFee;
                }
            }

            return match;
        }

        

        
    }
}
